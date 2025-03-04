using Primordium.Content.NPCs.Bosses.GiantLeech;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Primordium.Content.Items.Boss_Items.Summons
{
    public class WormSummonItem : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 20; // Item sprite width
            Item.height = 20; // Item sprite height
            Item.useStyle = ItemUseStyleID.HoldUp; // Style of using the item
            Item.useTime = 30; // How long it takes to use the item
            Item.useAnimation = 30; // Animation length
            Item.consumable = true; // Item is consumed on use
            Item.rare = ItemRarityID.Green; // Rarity of the item
            Item.value = Item.buyPrice(gold: 10); // Item cost
            Item.maxStack = 20; // Maximum stack size
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {

                SoundEngine.PlaySound(SoundID.Mech, player.position);

                int type = ModContent.NPCType<GiantLeech_Head>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {

                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 10); // Example ingredient
            recipe.AddIngredient(ItemID.ShadowScale, 5); // Another example ingredient
            recipe.AddTile(TileID.DemonAltar); // Crafting station
            recipe.Register();
        }
    }
}