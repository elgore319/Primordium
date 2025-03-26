using Terraria;
using Terraria.ModLoader;

namespace Primordium.Content.Items.Boss_Items.Drops.ThePrimordial
{
    public class PrimordialShard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 44; // The item texture's width
            Item.height = 76; // The item texture's height
            Item.scale = 1; // Changes in-game scale of item

            Item.maxStack = 9999; 
            Item.value = Item.buyPrice(platinum: 12); 
        }
    }
}
