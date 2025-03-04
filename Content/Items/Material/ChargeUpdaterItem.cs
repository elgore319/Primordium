using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static WarriorsPath.WarriorsPath;
using WarriorsPath.Content.UI;

namespace WarriorsPath.Content.Items.Material
{
    public class ChargeUpdaterItem : ModItem
    {
        

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp; // Hold up and consume
            Item.value = Item.buyPrice(0, 1, 0, 0); // 1 gold
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item4; // Use a magic sound
            Item.autoReuse = false; // Don't auto-reuse
            Item.consumable = true; // Consume on use
        }

        public override bool CanUseItem(Player player)
        {
            // Only allow use if charges are below max
            return TemporalChargeUI.currentCharges < TemporalChargeUI.maxCharges;
        }

        public override bool? UseItem(Player player)
        {
            // Increase charges by 1
            TemporalChargeUI.currentCharges++;

            // Sync charges in multiplayer
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MessageType.UpdateCharges); // Write the message type
                packet.Write(TemporalChargeUI.currentCharges); // Write the updated charges
                packet.Send();
            }

            return true;
        }
    }
}