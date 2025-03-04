using Terraria;
using Terraria.ModLoader;


namespace Primordium.Content.Items.Material
{
    public class BypassBoard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 256; // The item texture's width
            Item.height = 256; // The item texture's height
            Item.scale = 0.44f; // Changes in-game scale of item (uses f for floating)

            Item.maxStack = Item.maxStack; // The item's max stack value
            Item.value = Item.buyPrice(platinum: 12); // The value of the item in copper coins. Item.buyPrice & Item.sellPrice are helper methods that returns costs in copper coins based on platinum/gold/silver/copper arguments provided to it.
        }
    }
}