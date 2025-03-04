using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;

public class FirstGenPass : GenPass
{
    public FirstGenPass() : base("Terrain", 0) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Generating terrain"; // Sets the text displayed for this pass

        // Adjust world layers
        Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
        Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds



        // Loop through all tiles in the world
        for (int i = 0; i < Main.maxTilesX; i++)
        {
            for (int j = 0; j < Main.maxTilesY + 999; j++)
            {
                // Update progress
                progress.Set((float)(i * Main.maxTilesY + j) / (Main.maxTilesX * Main.maxTilesY));

                // Safely get or initialize the tile
                Tile tile = Framing.GetTileSafely(i, j);

                // If the tile is below the spawn point, make it solid
                if (j <= 501)
                {
                    tile.HasTile = false; // Empty tile above the spawn point
                }
                else
                {
                    tile.LiquidAmount = 255;
                    tile.LiquidType = LiquidID.Water; // Set the tile type to dirt (or any other type)
                }
            }
        }
    }
}