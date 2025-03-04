using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.ID;

namespace Primordium.Content.Tiles
{
    internal class DivineBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileShine[Type] = 1100;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(255, 215, 0), Language.GetText("MapObject.MetalBar"));
        }

        // The code below is not yet finished
        // It will be used to animate the tile to better match its item counterpart
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {

            // Spend 9 ticks on each of 6 frames, looping
            frameCounter++;
            if (frameCounter >= 9)
            {
                frameCounter = 0;
                if (++frame >= 6)
                {
                    frame = 0;
                }
            }
        }
    }
}
