using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Primordium.Content.Tiles.Blocks
{
    public class FrozenAsh : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true; // Makes the tile solid
            Main.tileMergeDirt[Type] = true; // Allows it to merge with dirt
            Main.tileBlockLight[Type] = true; // Blocks light
            Main.tileShine[Type] = 400; // Makes it shiny like gold tiles

            AddMapEntry(new Color(200, 50, 50));


            DustType = DustID.Torch; // Dust effect when mined
            HitSound = SoundID.Tink; // Sound when hit
        }

        // Optional: Custom behavior when tile is destroyed
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            // Custom destruction effects or logic
        }
    }
}
