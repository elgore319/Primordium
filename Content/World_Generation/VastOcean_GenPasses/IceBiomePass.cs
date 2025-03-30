using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;

//Basic Ice Biome
public class IceBiomePass : GenPass
{
    public IceBiomePass() : base("Terrain", 0) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Freezing Ocean"; 

        int iceLevel = 501;
        int iceWidth = Main.maxTilesX / 8;

        for (int i = 0; i < iceWidth; i++)
        {
            for (int j = iceLevel; j < Main.maxTilesY; j++)
            {
                // Update progress
                progress.Set((float)(i * Main.maxTilesY + j) / (Main.maxTilesX * Main.maxTilesY));

                Tile tile = Framing.GetTileSafely(i, j);
                tile.HasTile = true;

                tile.TileType = TileID.IceBlock;
            }
        }
    }
}