using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Primordium.Developer_Items.Accessories
{
    public class Coordinate_Detector : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // This makes the item function while in inventory (not just accessory slot)
            player.GetModPlayer<CustomInfoPlayer>().hasDevice = true;
        }
    }

    public class CustomInfoPlayer : ModPlayer
    {
        public bool hasDevice = false;

        public override void PostUpdate()
        {
            if (hasDevice)
            {
                // Display your custom information
                string displayText = GetCustomInfo();
                Main.instance.MouseText(displayText);
            }
        }

        private string GetCustomInfo()
        {
            Player player = Main.LocalPlayer;
            int depth = (int)player.position.Y;
            int width = (int)player.position.X;

            return $"X: {width} " + $"Y: {depth} ";
        }
    }
}