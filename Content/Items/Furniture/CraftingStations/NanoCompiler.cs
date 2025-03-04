using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Primordium.Content.Items.Bars_Ore;

namespace Primordium.Content.Items.Furniture.CraftingStations
{
	public class NanoCompiler : ModItem
	{
		public override void SetDefaults() {
			
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CraftingStations.NanoCompiler>());
			Item.width = 28; // The item texture's width
			Item.height = 14; // The item texture's height
			Item.value = 150;
		}

		public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) {
			itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
		}

		
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.MythrilAnvil)
				.AddIngredient<EnigmaticOre>(10)
				.Register();
		}
	}
}
