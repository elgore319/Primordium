using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WarriorsPath.Content.Items.Weapons.Melee.PreHardmode;

namespace WarriorsPath.Content.Items.Weapons.Melee.Hardmode
{
    public class SwordOfScripture : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.WarriorsPath.hjson' file.
        public override void SetDefaults()
        {
            Item.damage = 500;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(platinum: 12);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            //the left number is the time separation between frames
            //the right number is the ammount of frames your item/image has.
            
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));      
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BrokenSwordOfScripture>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
