using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
public class FirstOceanPass : GenPass
{
    public FirstOceanPass() : base("Terrain", 0) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Filling Ocean"; 

        
        Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
        Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds

        int waterLevel = 501;

        for (int i = 0; i < Main.maxTilesX; i++)
        {
            for (int j = waterLevel; j < Main.maxTilesY; j++)
            {
                progress.Set((float)(i * Main.maxTilesY + j) / (Main.maxTilesX * Main.maxTilesY));

                Tile tile = Framing.GetTileSafely(i, j);

                tile.HasTile = false;
                tile.LiquidAmount = 255;
                tile.LiquidType = LiquidID.Water;
            }
        }
    }
}