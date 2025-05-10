namespace ClientInput
{
    public interface IInputHandler
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
        bool IsJump();
        CameraInputData GetCameraInputData();
        bool SwitchCameraMode();
        bool IsMobile();
    }
}