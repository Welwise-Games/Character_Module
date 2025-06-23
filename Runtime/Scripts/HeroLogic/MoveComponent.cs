using System;
using UnityEngine;
using WelwiseCharacterModule.Runtime.Scripts.HeroLogic.Animators;

namespace WelwiseCharacterModule.Runtime.Scripts.HeroLogic
{
    public class MoveComponent : MonoBehaviour
    {
        public event Action Jumped, MovedOnGround, NotMovedOnGround;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityForce;


        private Vector3 _lastDirection;

        private float _verticalVelocity;
        private bool _isInitialized;
        private HeroAnimatorController _heroAnimatorComp;
        private ArmsAnimatorController _armsAnimatorComp;
        private Transform _playerTransform;
        private CameraComponent _cameraComponent;

        public void SetDirection(Vector3 direction)
        {
            _lastDirection = direction;
        }


        public void Construct(HeroAnimatorController animatorComponent, ArmsAnimatorController armsAnimatorComponent,
            Transform playerTransform, CameraComponent cameraComponent = null)
        {
            _cameraComponent = cameraComponent;
            _heroAnimatorComp = animatorComponent;
            _armsAnimatorComp = armsAnimatorComponent;
            _playerTransform = playerTransform;
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized) return;
            HandleGravity();
            Move(_lastDirection);

            _heroAnimatorComp.Fall(!_characterController.isGrounded && _verticalVelocity < 0);
        }

        private void HandleGravity() => _verticalVelocity += _gravityForce * Time.deltaTime;

        private void Move(Vector3 direction)
        {
            _heroAnimatorComp.Run(direction.magnitude != 0);
            _armsAnimatorComp.Run(direction.magnitude != 0);
            var forwardDirection = new Vector3(_playerTransform.forward.x, 0f, _playerTransform.forward.z)
                .normalized;
            var rightDirection = new Vector3(_playerTransform.right.x, 0f, _playerTransform.right.z)
                .normalized;
            var movementDirection = rightDirection * direction.x + forwardDirection * direction.z;
            var movementDelta = movementDirection * _moveSpeed * Time.deltaTime;
            Rotate(movementDelta);
            var finalMovement = new Vector3(movementDelta.x, _verticalVelocity * Time.deltaTime, movementDelta.z);
            _characterController.Move(finalMovement);

            if (direction.magnitude != 0 && _characterController.isGrounded)
                MovedOnGround?.Invoke();
            else
                NotMovedOnGround?.Invoke();
        }


        public void Rotate(Vector3 direction)
        {
            if (direction.magnitude > 0)
            {
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }


        public void Jump()
        {
            if (!CanJump()) return;

            if (!_cameraComponent || !_cameraComponent.IsFirstCamera)
            {
                _heroAnimatorComp.Jump();
            }
            else
            {
                _armsAnimatorComp.Jump();
            }

            _verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);

            Jumped?.Invoke();
        }

        private bool CanJump() => _characterController.isGrounded;
    }
}