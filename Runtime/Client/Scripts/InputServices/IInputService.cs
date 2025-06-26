using UnityEngine;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.InputServices
{
    public interface IInputService
    {
        Vector2 GetInputAxis();
        bool ShouldJump();
        CameraInputData GetCameraInputData();
        bool ShouldSwitchCameraMode();
        
    }
}