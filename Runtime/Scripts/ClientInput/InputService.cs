using UnityEngine;
using WelwiseCharacterModule.Runtime.Scripts.HeroLogic;
using WelwiseSharedModule.Runtime.Client.Scripts.Tools;

namespace WelwiseCharacterModule.Runtime.Scripts.ClientInput
{
    public class InputService : MonoBehaviour
    {
        public bool IsEnabled { get; private set; }

        private IInputHandler _inputHandler;
        private ICursorHandler _cursorHandler;
        private MoveComponent _moveComponent;
        private CameraComponent _cameraComponent;
        private bool _isConstructed;

        public void MobileConstruct(MobileHud mobileHud, MoveComponent moveComponent, CameraComponent cameraComponent)
        {
            mobileHud.Enable();
            _inputHandler = new MobileInputHandler(mobileHud);
            
            SharedConstruct(moveComponent, cameraComponent);
        }

        public void StandaloneConstruct(MoveComponent moveComponent, CameraComponent cameraComponent)
        {
            _inputHandler = new InputHandler();
            _cursorHandler = (ICursorHandler)_inputHandler;
                
            if (CursorSwitcherTools.IsCursorEnabled)
                CursorSwitcherTools.TryDisablingCursor();
            
            SharedConstruct(moveComponent, cameraComponent);
        }

        private void SharedConstruct(MoveComponent moveComponent, CameraComponent cameraComponent)
        {
            _moveComponent = moveComponent;
            _cameraComponent = cameraComponent;
            _isConstructed = true;
            IsEnabled = true;
        }

        private void Update()
        {
            if (!_isConstructed || !IsEnabled) return;
            if (_inputHandler.IsJump())
            {
                _moveComponent.Jump();
            }

            HandleMovementInput();
            HandleCameraInput();
            if (!_inputHandler.IsMobile())
            {
                HandleCursorSwitch();
            }
        }

        private void HandleCameraInput()
        {
            var isCameraRotate = _inputHandler.GetCameraInputData();
            if (_inputHandler.SwitchCameraMode()) _cameraComponent.SwitchCameraMode();

            if (isCameraRotate.IsPressed && CursorSwitcherTools.IsCursorEnabled || !CursorSwitcherTools.IsCursorEnabled)
            {
                _cameraComponent.Rotate(isCameraRotate.InputAxisX, isCameraRotate.InputAxisY);
            }
        }

        private void HandleCursorSwitch()
        {
            if (_cursorHandler.SwitchCursor() && !_cameraComponent.IsFirstCamera)
                CursorSwitcherTools.TrySwitchingCursor();
        }


        private void HandleMovementInput()
        {
            var horizontalAxis = _inputHandler.GetHorizontalAxis();
            var verticalAxis = _inputHandler.GetVerticalAxis();
            if (horizontalAxis != 0 || verticalAxis != 0)
            {
                Move(horizontalAxis, verticalAxis);
            }
            else
            {
                _moveComponent.SetDirection(Vector3.zero);
            }
        }

        public void DisableInput()
        {
            _moveComponent.SetDirection(Vector3.zero);
            IsEnabled = false;
        }

        private void Move(float horizontalAxis, float verticalAxis)
        {
            var direction = new Vector3(horizontalAxis, 0, verticalAxis);
            if (direction.magnitude > 0) direction.Normalize();
            _moveComponent.SetDirection(direction);
        }

        public void EnableInput()
        {
            IsEnabled = true;
        }
    }
}