using System;
using UnityEngine;
using WelwiseCharacter.Runtime.Scripts.ClientInput;
using WelwiseCharacter.Runtime.Scripts.HeroLogic.Animators;

namespace WelwiseCharacter.Runtime.Scripts.HeroLogic
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroAnimatorController _heroAnimatorController;

        [SerializeField] private ArmsAnimatorController _armsAnimatorController;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private InputService _inputService;
        private CameraComponent _cameraComponent;
        private CameraObserver _cameraObserver;
        public InputService InputService => _inputService;
        public bool IsInitialized { get; private set; }

        public void Start() => TryInitializing(FindObjectOfType<MobileHud>());

        public void TryInitializing(MobileHud mobileHud)
        {
            if (IsInitialized)
                return;
            
            _inputService = GetComponent<InputService>();
            _cameraComponent = GetComponent<CameraComponent>();
            _moveComponent.Initialize(_cameraComponent, _heroAnimatorController, _armsAnimatorController, Camera.main);
            _inputService.Construct(mobileHud, _moveComponent, _cameraComponent);
            _cameraComponent.Construct(transform, Camera.main);
            _cameraObserver = new CameraObserver(_cameraComponent,
                GetComponentInChildren<SkinColorChanger>());
            
            IsInitialized = true;
        }

    }
}