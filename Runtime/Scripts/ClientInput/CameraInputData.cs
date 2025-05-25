namespace WelwiseCharacterModule.Runtime.Scripts.ClientInput
{
    public struct CameraInputData
    {
        public readonly float InputAxisX, InputAxisY;
        public readonly bool IsPressed;

        public CameraInputData(float inputAxisX, float inputAxisY, bool isPressed)
        {
            InputAxisX = inputAxisX;
            InputAxisY = inputAxisY;
            IsPressed = isPressed;
        }
    }
}