using System;
using UnityEngine;
using WelwiseCharacterModule.Runtime.Client.Scripts.InputServices;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.OwnerPlayerMovement
{
    public class OwnerPlayerMovementController
    {
        public float VerticalVelocity { get; private set; }

        public bool IsEnabled { get; set; }

        private CharacterController CharacterController =>
            _serializableComponents.CharacterController ?? _characterController;

        public event Action Jumped, MovedOnGround, NotMovedOnGround;
        public event Action<Vector3> Moved;

        private readonly Transform _playerTransform;
        private readonly OwnerPlayerMovementSerializableComponents _serializableComponents;
        private readonly CharacterController _characterController;
        private readonly IInputService _inputService;

        public OwnerPlayerMovementController(Transform playerTransform,
            OwnerPlayerMovementSerializableComponents serializableComponents, IInputService inputService,
            CharacterController characterController = null)
        {
            _playerTransform = playerTransform;
            _serializableComponents = serializableComponents;
            _inputService = inputService;
            _characterController = characterController;

            serializableComponents.MonoBehaviourObserver.Updated += OnUpdate;
        }

        public void TryJumping()
        {
            if (!_characterController.isGrounded) return;

            VerticalVelocity = _serializableComponents.MovementConfig.JumpForce *
                               _serializableComponents.MovementConfig.GravityForce;

            Jumped?.Invoke();
        }

        private void OnUpdate()
        {
            if (_inputService.ShouldJump())
                TryJumping();

            HandleGravity();

            Move(!IsEnabled
                ? Vector2.zero
                : _inputService.GetInputAxis(), out var movementDelta);

            TryRotating(movementDelta);
        }

        private void HandleGravity() =>
            VerticalVelocity += _serializableComponents.MovementConfig.GravityForce * Time.deltaTime;

        private void Move(Vector2 direction, out Vector3 movementDelta)
        {
            var forwardDirection = new Vector3(_playerTransform.forward.x, 0f, _playerTransform.forward.z).normalized;
            var rightDirection = new Vector3(_playerTransform.right.x, 0f, _playerTransform.right.z).normalized;
            var movementDirection = rightDirection * direction.x + forwardDirection * direction.y;

            movementDelta = movementDirection * _serializableComponents.MovementConfig.MoveSpeed * Time.deltaTime;

            var finalMovement = new Vector3(movementDelta.x, VerticalVelocity * Time.deltaTime, movementDelta.z);

            CharacterController.Move(finalMovement);

            Moved?.Invoke(direction);

            if (direction.magnitude != 0 && CharacterController.isGrounded)
                MovedOnGround?.Invoke();
            else
                NotMovedOnGround?.Invoke();
        }

        private void TryRotating(Vector3 direction)
        {
            if (!(direction.magnitude > 0)) return;

            var targetRotation = Quaternion.LookRotation(direction);
            CharacterController.transform.rotation =
                Quaternion.Lerp(CharacterController.transform.rotation, targetRotation,
                    _serializableComponents.MovementConfig.RotationSpeed * Time.deltaTime);
        }
    }
}