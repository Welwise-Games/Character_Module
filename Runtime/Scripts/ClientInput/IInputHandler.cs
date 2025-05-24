namespace MainHub.Modules.WelwiseCharacter.Runtime.Scripts.ClientInput
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