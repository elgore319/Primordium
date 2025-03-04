using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WarriorsPath.Content.Assets.Classes;

namespace WarriorsPath.Content.Items.Weapons.Temporal
{
    public class ChronopsisBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40; // Weapon damage
            Item.DamageType = ModContent.GetInstance<Temporaldamage>(); // Custom damage type
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (var line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "Damage")
                {
                    line.Text = line.Text.Replace("Temporaldamage", "temporal damage");
                }
            }
        }
    }
}