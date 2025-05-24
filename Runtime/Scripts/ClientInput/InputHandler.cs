using UnityEngine;

namespace MainHub.Modules.WelwiseCharacter.Runtime.Scripts.ClientInput
{
    public class InputHandler : IDesktopInputHandler
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public float GetHorizontalAxis() => UnityEngine.Input.GetAxisRaw(HorizontalAxis);

        public float GetVerticalAxis() => UnityEngine.Input.GetAxisRaw(VerticalAxis);

        public bool IsJump() => UnityEngine.Input.GetKeyDown(KeyCode.Space);

        public CameraInputData GetCameraInputData() =>
            new(UnityEngine.Input.GetAxis("Mouse X"),
                -UnityEngine.Input.GetAxis("Mouse Y"), Input.GetMouseButton(1));

        public bool SwitchCursor() => UnityEngine.Input.GetKeyDown(KeyCode.Tab);

        public bool SwitchCameraMode() => UnityEngine.Input.GetKeyDown(KeyCode.V);

        public bool IsMobile() => false;
    }
}