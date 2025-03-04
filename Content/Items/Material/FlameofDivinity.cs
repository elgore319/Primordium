using Terraria;
using Terraria.ModLoader;


namespace WarriorsPath.Content.Items.Material
{
    public class FlameofDivinity : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 128; // The item texture's width
            Item.height = 128; // The item texture's height
            Item.scale = 1; // Changes in-game scale of item (uses f for floating)

            Item.maxStack = 1; // The item's max stack value
            Item.value = Item.buyPrice(platinum: 12); 
        }
    }
}