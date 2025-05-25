using UnityEngine;
using WelwiseCharacterModule.Runtime.Scripts.HeroLogic;
using WelwiseSharedModule.Runtime.Scripts.Client.Tools;

namespace WelwiseCharacterModule.Runtime.Scripts.ClientInput
{
    public class InputService : MonoBehaviour
    {
        public bool IsEnabled { get; private set; }

        private MobileHud _mobileHud;
        private IInputHandler _inputHandler;
        private ICursorHandler _cursorHandler;
        private MoveComponent _moveComponent;
        private CameraComponent _cameraComponent;
        private bool _isInitialized;

        public void Construct(MobileHud mobileMobileHud, MoveComponent moveComponent, CameraComponent cameraComponent)
        {
            _mobileHud = mobileMobileHud;

#if UNITY_STANDALONE
            _inputHandler = new InputHandler();
            _cursorHandler = (ICursorHandler)_inputHandler;
            
            if (CursorSwitcherTools.IsCursorEnabled)
                CursorSwitcherTools.TryDisablingCursor();

#elif UNITY_WEBGL
            if (DeviceDetectorTools.IsMobile())
            {
                _mobileHud.Enable();
                _inputHandler = new MobileInputHandler(_mobileHud);
            }
            else
            {
                _inputHandler = new InputHandler();
                _cursorHandler = (ICursorHandler)_inputHandler;
                
                if (CursorSwitcherTools.IsCursorEnabled)
                    CursorSwitcherTools.TryDisablingCursor();
            }
#else
            _inputHandler = new MobileInputHandler(_mobileHud);
#endif

            _moveComponent = moveComponent;
            _cameraComponent = cameraComponent;
            _isInitialized = true;
            IsEnabled = true;
        }

        private void Update()
        {
            if (!_isInitialized || !IsEnabled) return;
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