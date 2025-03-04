/*

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarriorsPath.Content
{
    public class HellWaterSystem : ModSystem
    {
        public override void PostUpdateWorld()
        {
            // Loop through all tiles in Hell
            int hellLayer = Main.maxTilesY - 200; // Approximate start of Hell layer

            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = hellLayer; y < Main.maxTilesY; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);

                    // If the liquid is evaporating, restore it
                    if (tile.LiquidType == LiquidID.Water && tile.LiquidAmount == 0)
                    {
                        tile.LiquidAmount = 255; // Refill water
                        tile.LiquidType = LiquidID.Water; // Ensure it's water
                        WorldGen.SquareTileFrame(x, y); // Update tile visuals
                        NetMessage.SendTileSquare(-1, x, y, 1); // Sync changes with all players
                    }
                }
            }
        }
    }
}

*/

//preserving this code just in case
//As of right now it's too laggy to be used in the actual game