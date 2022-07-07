using StaminaExtension;

namespace CorgiWorks.Scripts
{
    public class CharacterStaminaX : Stamina
    {
        public bool ConsumeStamina(float amount)
        {
            if (CurrentStamina < amount) return false;
            CurrentStamina -= amount;
            return true;
        }
    }
}