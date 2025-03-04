using Terraria.ModLoader;

namespace Primordium.Content.Assets.Classes
{
    public class Temporaldamage : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
            {
                return StatInheritanceData.Full; // Inherits all stats from the generic damage class
            }
            return base.GetModifierInheritance(damageClass);
        }
    }
}