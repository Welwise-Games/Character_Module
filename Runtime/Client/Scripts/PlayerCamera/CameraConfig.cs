using System;
using UnityEngine;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.PlayerCamera
{
    [Serializable]
    public class CameraConfig
    {
        [field: Header("Camera Settings")] 
        [field: SerializeField] public Vector3 Offset { get; private set; } = new(0, 0, -6);
        [field: SerializeField] public Transform FpsCameraPosition { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; } = 5f;
        [field: SerializeField] public float MinimumVerticalAngle { get; private set; } = -30f;
        [field: SerializeField] public float MaximumVerticalAngle { get; private set; } = 40f;

        [field: Header("Zoom Settings")]
        [field: SerializeField] public float ZoomSpeed { get; private set; } = 7f;

        [field: SerializeField] public float MinimumZoomDistance { get; private set; } = 2f;
        [field: SerializeField] public float MaximumLookUpAngle { get; private set; } = 45;
    }
}