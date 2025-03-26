using Microsoft.Xna.Framework;
using Primordium.Content.Projectiles.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Primordium.Content.Items.Weapons.Magic.Hardmode

{
    internal class Unknown_Entity : ModItem
    {
        private bool projectileActive = false;

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
            Item.UseSound = SoundID.DD2_DarkMageAttack;
            Item.autoReuse = true;
            Item.uniqueStack = true;
            Item.holdStyle = 6;
            Item.newAndShiny = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<TheOrb>();
            Item.shootSpeed = 10f;

            //Use below to animate item with multiple frames. Left number is ticks, right is frame count.

            /*
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
            */
        }

        public override bool CanUseItem(Player player)
        {
            if (projectileActive)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            projectileActive = true;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            return false; 
        }

        public override void UpdateInventory(Player player)
        {
            projectileActive = false;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == player.whoAmI && proj.type == Item.shoot)
                {
                    projectileActive = true;
                    break;
                }
            }
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
