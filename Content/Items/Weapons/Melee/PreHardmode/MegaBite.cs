using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarriorsPath.Content.Items.Material;
using WarriorsPath.Content.Tiles.CraftingStations;

namespace WarriorsPath.Content.Items.Weapons.Melee.PreHardmode
{
    public class MegaBite : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Melee;
            // size (height and width) goes here.
            Item.value = Item.buyPrice(0, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient<Nanites>()
               .AddTile<NanoCompiler> ()
               .Register();
        }
    }
}
