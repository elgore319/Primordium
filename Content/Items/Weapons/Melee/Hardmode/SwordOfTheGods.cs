using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Primordium.Content.Items.Bars_Ore;
using Primordium.Content.Items.Material;

namespace Primordium.Content.Items.Weapons.Melee.Hardmode
{


    public class SwordOfTheGods : ModItem
    {
        public int attackType = 0; // keeps track of which attack it is
        public int comboExpireTimer = 0; // we want the attack pattern to reset if the weapon is not used for certain period of time

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 250;
            Item.height = 250;
            Item.value = Item.sellPrice(platinum: 40, gold: 25, silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;

            // Weapon Properties
            Item.knockBack = 7;  
            Item.autoReuse = true;
            Item.damage = 1000; 
            Item.DamageType = DamageClass.Melee; 
            Item.noMelee = false;  
            Item.noUseGraphic = true;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 9));

            // Projectile Properties
            Item.shoot = ModContent.ProjectileType<GodSwordSwingProjectile>(); // The sword as a projectile
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Using the shoot function, we override the swing projectile to set ai[0] (which attack it is)
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
            attackType = (attackType + 1) % 2; // Increment attackType to make sure next swing is different
            comboExpireTimer = 0; // Every time the weapon is used, we reset this so the combo does not expire
            return false; // return false to prevent original projectile from being shot
        }

        public override void UpdateInventory(Player player)
        {
            if (comboExpireTimer++ >= 120) // after 120 ticks (== 2 seconds) in inventory, reset the attack pattern
                attackType = 0;
        }

        public override bool MeleePrefix()
        {
            return true; // return true to allow weapon to have melee prefixes (e.g. Legendary)
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DivineBar>(12)
                .AddIngredient<FlameofDivinity>(1)
                .AddIngredient<SwordOfScripture>(1)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        
        }
    }
}