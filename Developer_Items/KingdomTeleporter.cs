using Terraria.ModLoader;
using Terraria;
using SubworldLibrary;
using Terraria.ID;

namespace Primordium.Developer_Items
{
    public class KingdomTeleporter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20; 
            Item.height = 38; 
            Item.value = 150;
            Item.useStyle = 1;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (SubworldSystem.IsActive<GreatKingdom>())
                {
                    SubworldSystem.Exit();
                }
                else
                {
                    SubworldSystem.Enter<GreatKingdom>();
                }
            }
            return true;
        }
    }
}
