using System.Linq;
using UnityEngine;

namespace WelwiseCharacter.Runtime.Scripts.ClientInput
{
    public class MobileInputHandler : IMobileInputHandler
    {
        private readonly float _lookAreaScreenMultiplier = 0.5f;
        private readonly MobileHud _mobileHud;
        public MobileInputHandler(MobileHud mobileHud) => _mobileHud = mobileHud;

        public float GetHorizontalAxis() => _mobileHud.Joystick.GetInputVector().x;

        public float GetVerticalAxis() => _mobileHud.Joystick.GetInputVector().y;

        public bool IsJump() => _mobileHud.JumpButton.IsPressed();

        public CameraInputData GetCameraInputData()
        {
            if (UnityEngine.Input.touchCount <= 0) return new CameraInputData();

            var touches = Enumerable.Range(0, UnityEngine.Input.touchCount).Select(UnityEngine.Input.GetTouch).ToList();
            
            var movingTouchIndex = touches.FindIndex(touch => touch.phase == TouchPhase.Moved && IsTouchInLookArea(touch.position));
            
            if (movingTouchIndex == -1) return new CameraInputData();

            var movingTouch = touches[movingTouchIndex];
            
            var sensitivity = 0.1f;

            return new CameraInputData(-movingTouch.deltaPosition.x * sensitivity,
                movingTouch.deltaPosition.y * sensitivity, true);
        }
        
        private bool IsTouchInLookArea(Vector2 touchPosition) => touchPosition.x >= Screen.width * Mathf.Clamp01(_lookAreaScreenMultiplier);


        public bool SwitchCameraMode() => _mobileHud.CameraSwitchButton.IsPressed();

        public bool IsMobile() => true;
    }
}