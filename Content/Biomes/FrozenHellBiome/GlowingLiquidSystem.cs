/*

using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Primordium.Content.Biomes.FrozenHellBiome
{
    public class GlowingLiquidSystem : ModSystem
    {
        public override void PostUpdateWorld()
        {
            int hellLayerStart = Main.maxTilesY - 200;

            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = hellLayerStart; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    if (tile.LiquidAmount > 0 && tile.LiquidType == LiquidID.Water) // Or LiquidID.Lava
                    {
                        // Add a purple glow to the liquid
                        Lighting.AddLight(new Vector2(x, y) * 16, 0.6f, 0.1f, 1.2f);
                    }
                }
            }
        }
    }
}

*/
//This code works, but its supposed to go with "HellWaterSystem" so it's kinda pointless without it
//Will use as a source for other glowing liquids for now