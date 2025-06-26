using UnityEngine;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.HeroAnimators
{
    public class ArmsAnimatorSerializableComponents : MonoBehaviour
    {
        [field: SerializeField] public float IdleAnimationDelay { get; private set; } = 8f;
    }
}