using System;
using UnityEngine.EventSystems;
using WelwiseSharedModule.Runtime.Client.Scripts.UI;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.MobileHud
{
    public class HoldableButtonController
    {
        private bool _isButtonDown;
        private bool _wasPressed;

        private event Action _invokedIsHold;

        public HoldableButtonController(PointerUpDownObserver pointerUpDownObserver)
        {
            pointerUpDownObserver.PointerDowned += OnPointerDown;
            pointerUpDownObserver.PointerUpped += OnPointerUp;

            _invokedIsHold += MakeWasPressedTrue;
        }

        public bool IsHold()
        {
            var isHold = _isButtonDown && !_wasPressed;
            
            if (!isHold)
                _invokedIsHold?.Invoke();

            return isHold;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isButtonDown = true;
            _wasPressed = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isButtonDown = false;
        }

        private void MakeWasPressedTrue() => _wasPressed = true;
    }
}