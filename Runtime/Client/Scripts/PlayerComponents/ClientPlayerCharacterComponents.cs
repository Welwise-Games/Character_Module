using WelwiseClothesSharedModule.Runtime.Client.Scripts;
using WelwiseEmotionsModule.Runtime.Client.Scripts.Animations;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.PlayerComponents
{
    public class ClientPlayerCharacterComponents
    {
        public readonly ClientPlayerCharacterSerializableComponents SerializableComponents;

        public ClientPlayerCharacterComponents(
            ClientPlayerCharacterSerializableComponents serializableComponents)
        {
            SerializableComponents = serializableComponents;
        }
    }
}