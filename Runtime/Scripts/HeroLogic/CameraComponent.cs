using System;
using MainHub.Modules.WelwiseSharedModule.Runtime.Scripts.Client.Tools;
using MainHub.Modules.WelwiseSharedModule.Runtime.Scripts.Tools;
using UnityEngine;

namespace MainHub.Modules.WelwiseCharacter.Runtime.Scripts.HeroLogic
{
    public class CameraComponent : MonoBehaviour
    {
        [Header("Camera Settings")] [SerializeField]
        private Vector3 _offset = new(0, 0, -6);

        [SerializeField] private Transform _fpsCameraPosition;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _minVerticalAngle = -30f;
        [SerializeField] private float _maxVerticalAngle = 40f;

        [Header("Zoom Settings")] 
        [SerializeField] private float _zoomSpeed = 2f;

        [SerializeField] private float _minZoomDistance = 2f;
        [SerializeField] private float _maxLookUpAngle = 30f;

        public event Action<bool> OnCameraModeChanged;
        public Camera Camera { get; private set; }

        private Transform _target;
        private bool _isInitialized;
        private float _currentHorizontalAngle = 0f;
        private float _currentVerticalAngle = 20f;
        private float _currentZoom = 1f;
        private float _additionalLookUpAngle = 0f;
        private float _targetZoom = 1f;
        public float CameraDistance { get; private set; }
        public bool IsFirstCamera { get; private set; }

        public void Construct(Transform target, Camera mainCamera)
        {
            _target = target;
            _isInitialized = true;
            Camera = mainCamera;
        }
        

        public void SwitchCameraMode()
        {
            IsFirstCamera = !IsFirstCamera;
            OnCameraModeChanged?.Invoke(IsFirstCamera);
            TrySwitchingBodyMode();
        }


        private void LateUpdate()
        {
            if (!_isInitialized) return;
            if (IsFirstCamera)
            {
                UpdateFirstPersonCamera();
            }
            else
            {
                UpdateThirdPersonCamera();
            }
        }


        private void TrySwitchingBodyMode()
        {
            if (IsFirstCamera)
                CursorSwitcherTools.TryDisablingCursor();
        }


        private void UpdateFirstPersonCamera()
        {
            Camera.transform.position = _fpsCameraPosition.position;
    
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -90f, 90f);
            Quaternion rotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0);
    
            Camera.transform.rotation = rotation;
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }

        private void UpdateThirdPersonCamera()
        {
            _currentZoom = _targetZoom;

            Quaternion rotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0);
            Vector3 desiredPosition = _target.position + rotation * (_offset * _currentZoom);

            float zoomFactor = Mathf.InverseLerp(_minZoomDistance / _offset.magnitude, 1f, _currentZoom);
            _additionalLookUpAngle = Mathf.Lerp(0f, _maxLookUpAngle, 1f - zoomFactor);
            Camera.transform.position = desiredPosition;
            CameraDistance = Vector3.Distance(Camera.transform.position, _target.position);
            Camera.transform.LookAt(_target.position + Vector3.up * (_additionalLookUpAngle * 0.1f));
        }

        public void Rotate(float inputX, float inputY)
        {
            if (!_isInitialized || (Mathf.Approximately(inputX, 0f) && Mathf.Approximately(inputY, 0f))) 
                return;

            _currentHorizontalAngle += inputX * _rotationSpeed;
            _currentVerticalAngle += inputY * _rotationSpeed;
    
            if (IsFirstCamera)
            {
                _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -75, 45);
            }
            else
            {
                _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
            }

            if (_currentVerticalAngle <= _minVerticalAngle)
            {
                if (inputY < 0)
                {
                    _targetZoom -= Mathf.Abs(inputY) * _zoomSpeed * Time.deltaTime;
                }
                else if (inputY > 0)
                {
                    _targetZoom += inputY * _zoomSpeed * Time.deltaTime;
                }

                _targetZoom = Mathf.Clamp(_targetZoom, _minZoomDistance / _offset.magnitude, 1f);
            }
            else if (_currentVerticalAngle > _minVerticalAngle && inputY > 0 && _currentZoom < 1f)
            {
                _targetZoom += inputY * _zoomSpeed * Time.deltaTime;
                _targetZoom = Mathf.Min(_targetZoom, 1f);
            }
        }
    }
}