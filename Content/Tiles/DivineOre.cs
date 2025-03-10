using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Primordium.Content.Tiles
{
    internal class DivineOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 350;

            AddMapEntry(new Color(146, 161, 185), CreateMapEntryName());

            DustType = DustID.IchorTorch;

            HitSound = SoundID.LiquidsHoneyLava;

            MineResist = 1.5f;
            MinPick = 60;
        }
    }
}