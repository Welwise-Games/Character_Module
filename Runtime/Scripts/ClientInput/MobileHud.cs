using UnityEngine;

namespace MainHub.Modules.WelwiseCharacter.Runtime.Scripts.ClientInput
{
    public class MobileHud : MonoBehaviour
    {
        public bool IsEnabled { get; private set; }
        
        public Joystick Joystick => _joystick;
        public CustomUiButton JumpButton => _jumpButton;
        public CustomUiButton CameraSwitchButton => _cameraSwitchButton;

        [SerializeField] private Joystick _joystick;
        [SerializeField] private CustomUiButton _jumpButton;
        [SerializeField] private CustomUiButton _cameraSwitchButton;

        public void Enable()
        {
            IsEnabled = true;
            gameObject.SetActive(true);
        }
    }
}