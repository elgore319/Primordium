using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;


namespace Primordium.Content.Items.Bars_Ore
{
    internal class EnigmaticBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            ItemID.Sets.SortingPriorityMaterials[Type] = 59;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 1, copper: 75);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<Tiles.EnigmaticBar>();
            Item.placeStyle = 0;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 14));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<EnigmaticOre>(12)
                .Register();
        }
    }
}