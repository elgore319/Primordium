using Primordium.Content.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarriorsPath.Content.Items.Weapons.Magic.Hardmode

// This is going to be a lamp-shaped magic weapon
{
    internal class Unknown_Entity : ModItem
    {
        public override void SetDefaults() 
        {
            Item.damage = 500;
            Item.DamageType = DamageClass.Magic;
            Item.width = 26;
            Item.height = 40;
            Item.scale = 0.65f;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.UseSound = SoundID.DD2_BetsyFlameBreath;
            Item.autoReuse = true;
            Item.uniqueStack = true;
            Item.holdStyle = 6;
            Item.newAndShiny = true;
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<ExampleHomingProjectile>();

            //Use below to animate objects with multiple frames. Left number is ticks, right is frame count.

            /*
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
            */
        }
        public override void AddRecipes()
        {
            /*
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<"Item slot">();   //put item in "item slot" to add it to the recipe.
            recipe.AddTile(TileID."Station slot"); //add any crafting station to "Station slot" if desired.
            recipe.Register();
            */
        }
    }
}
