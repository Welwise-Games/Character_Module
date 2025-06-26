using UnityEngine;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.InputServices
{
    public class DekstopInputService : IDesktopInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public Vector2 GetInputAxis() =>
            new Vector2(UnityEngine.Input.GetAxisRaw(HorizontalAxis),
                UnityEngine.Input.GetAxisRaw(VerticalAxis));

        public bool ShouldJump() => UnityEngine.Input.GetKeyDown(KeyCode.Space);

        public CameraInputData GetCameraInputData() =>
            new CameraInputData(Input.GetMouseButton(1), new Vector2(UnityEngine.Input.GetAxis("Mouse X"),
                -UnityEngine.Input.GetAxis("Mouse Y")));

        public bool ShouldSwitchCursor() => UnityEngine.Input.GetKeyDown(KeyCode.Tab);

        public bool ShouldSwitchCameraMode() => UnityEngine.Input.GetKeyDown(KeyCode.V);
    }
}