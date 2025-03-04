using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Primordium.Content.Items.Boss_Items.BossVanity
{
	[AutoloadEquip(EquipType.Head)]
	public class DefenseMechMask : ModItem
	{
		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 28;

			// Common values for every boss mask
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 75);
			Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
