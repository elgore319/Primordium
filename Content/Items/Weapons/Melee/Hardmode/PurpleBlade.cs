using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Primordium.Content.Items.Weapons.Melee.Hardmode
{
    public class PurpleBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 94;
            Item.width = 94;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 40000;
            Item.crit = 40;
            Item.knockBack = 6;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(platinum: 12);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            //Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(0, 0));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SwordOfTheGods>()
                .AddIngredient<SwordOfScripture>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
