﻿using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;

public class FirstKingdomPass : GenPass
{
    public FirstKingdomPass() : base("Terrain", 0) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Building Foundation"; // Sets the text displayed for this pass

        // Adjust world layers
        Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
        Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds

        // Define the water level (y = 501 in your case)
        int waterLevel = 501;

        for (int i = 0; i < Main.maxTilesX; i++)
        {
            for (int j = waterLevel; j < Main.maxTilesY; j++)
            {
                // Update progress
                progress.Set((float)(i * Main.maxTilesY + j) / (Main.maxTilesX * Main.maxTilesY));

                // Safely get or initialize the tile
                Tile tile = Framing.GetTileSafely(i, j);

                // Clear any existing tiles
                tile.HasTile = true;

                tile.TileType = TileID.Dirt;
            }
        }
    }
}