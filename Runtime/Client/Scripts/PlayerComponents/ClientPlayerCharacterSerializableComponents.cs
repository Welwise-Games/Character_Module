using TMPro;
using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Client.Scripts;
using WelwiseSharedModule.Runtime.Client.Scripts;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.PlayerComponents
{
    public class ClientPlayerCharacterSerializableComponents : MonoBehaviour
    {
        [field: SerializeField] public Transform AnimatorChildrenParent { get; private set; }
    }
}