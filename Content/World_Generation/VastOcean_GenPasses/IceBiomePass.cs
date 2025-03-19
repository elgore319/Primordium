using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;

//Work in progress
public class IceBiomePass : GenPass
{
    private bool _isIceOnLeft; // Determines if the ice biome is on the left side of the subworld

    public IceBiomePass(bool isIceOnLeft) : base("Ice Biome", 1)
    {
        _isIceOnLeft = isIceOnLeft; // Set the side of the ice biome
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Generating Ice Biome";

        // Define the boundaries of the ice biome
        int iceBiomeStartX = _isIceOnLeft ? 0 : Main.maxTilesX / 2;
        int iceBiomeEndX = _isIceOnLeft ? Main.maxTilesX / 2 : Main.maxTilesX;
        int iceLevel = 501;

        for (int i = 0; i < Main.maxTilesX; i++)
        {
            for (int j = iceLevel; j < Main.maxTilesY; j++)
            {
                // Update progress
                progress.Set((float)(i * Main.maxTilesY + j) / (Main.maxTilesX * Main.maxTilesY));

                // Safely get or initialize the tile
                Tile tile = Framing.GetTileSafely(i, j);

                tile.HasTile = true;
                tile.TileType = TileID.IceBlock;

            }
        }
    }

    private bool IsInIceBiome(int x, int y)
    {
        // Define ice biome boundaries (e.g., top half of the world)
        return y < Main.maxTilesY / 2;
    }
}
