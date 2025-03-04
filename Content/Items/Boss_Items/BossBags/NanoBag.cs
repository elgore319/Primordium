using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Primordium.Content.Items.Boss_Items.BossVanity;
using Primordium.Content.Items.Currency;
using Primordium.Content.Items.Material;
using Primordium.Content.NPCs.Bosses.DefenseMech;

namespace Primordium.Content.Items.Boss_Items.BossBags
{
    public class NanoBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; 

            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true; // This makes sure that "Expert" displays in the tooltip and the item name color changes
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DefenseMechMask>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Crypto>(), 1, 12, 16));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Nanites>(), 3));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<DefenseMechBody>()));
        }
    }
}
