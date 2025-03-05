using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;

public class FirstOceanPass : GenPass
{
    public FirstOceanPass() : base("Terrain", 0) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Filling Ocean"; // Sets the text displayed for this pass

        // Adjust world layers
        Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
        Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds

        for (int i = 0; i < Main.maxTilesX; i++)
        {
            for (int j = 0; j < Main.maxTilesY + 999; j++)
            {
                // Update progress
                progress.Set((float)(i * Main.maxTilesY + j) / (Main.maxTilesX * Main.maxTilesY));

                // Safely get or initialize the tile
                Tile tile = Framing.GetTileSafely(i, j);

                if (j <= 501)
                {
                    tile.HasTile = false; 
                }
                else
                {
                    tile.LiquidAmount = 255;
                    tile.LiquidType = LiquidID.Water; 
                }
            }
        }
    }
}