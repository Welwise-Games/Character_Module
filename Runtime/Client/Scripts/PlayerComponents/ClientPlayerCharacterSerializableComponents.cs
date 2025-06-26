using TMPro;
using UnityEngine;
using WelwiseClothesSharedModule.Runtime.Client.Scripts;
using WelwiseSharedModule.Runtime.Client.Scripts;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.PlayerComponents
{
    public class ClientPlayerCharacterSerializableComponents : MonoBehaviour
    {
        [field: SerializeField] public Transform AnimatorChildrenParent { get; private set; }
        [field: SerializeField] public ToCameraLooker NicknameLooker { get; private set; }
        [field: SerializeField] public TextMeshProUGUI NicknameText { get; private set; }
        [field: SerializeField] public SkinColorChangerSerializableComponents SkinColorChangerSerializableComponents { get; private set; }
        [field: SerializeField] public ColorableClothesViewSerializableComponents ColorableClothesViewSerializableComponents { get; private set; }
    }
}