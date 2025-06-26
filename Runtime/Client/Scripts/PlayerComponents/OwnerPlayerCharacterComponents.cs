using WelwiseCharacterModule.Runtime.Client.Scripts.OwnerPlayerMovement;

namespace WelwiseCharacterModule.Runtime.Client.Scripts.PlayerComponents
{
    public class OwnerPlayerCharacterComponents
    {
        public readonly OwnerPlayerCharacterSerializableComponents CharacterSerializableComponents;
        public readonly ClientPlayerCharacterComponents ClientCharacterComponents;
        public readonly OwnerPlayerMovementController MovementController;

        public OwnerPlayerCharacterComponents(OwnerPlayerCharacterSerializableComponents characterSerializableComponents, 
            OwnerPlayerMovementController movementController, ClientPlayerCharacterComponents clientCharacterComponents)
        {
            CharacterSerializableComponents = characterSerializableComponents;
            MovementController = movementController;
            ClientCharacterComponents = clientCharacterComponents;
        }
    }
}