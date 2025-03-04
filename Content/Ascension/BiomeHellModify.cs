using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using WarriorsPath.Content.Tiles.Blocks;


namespace  WarriorsPath.Content.Ascended_Mode;


public class BiomeHellModify : ModSystem
{
    private bool ashReplaced = false; // Tracks if the replacement has occurred
    public static bool IsHellTransformed = false; // Static flag to track the Hell transformation state
    public override void PostUpdateWorld()
    {
        // Check if Moon Lord is defeated and replacement hasn't occurred yet
        if (NPC.downedMoonlord && !ashReplaced)
        {
            ReplaceAshBlocksInHell();
            ashReplaced = true; // Prevents the code from running multiple times
            IsHellTransformed = true; // Set the biome as transformed
            Main.NewText("The fires of Hell transform as the Moon Lord falls...", Color.OrangeRed);
        }
        
        
    }
    
    private void ReplaceAshBlocksInHell()
    {
        int hellLayer = Main.maxTilesY - 200; // Approximate start of Hell layer

        // Loop through the Hell layer tiles
        for (int x = 0; x < Main.maxTilesX; x++)
        {
            for (int y = hellLayer; y < Main.maxTilesY; y++)
            {
                Tile tile = Framing.GetTileSafely(x, y);

                // Replace Ash Blocks with the custom block
                if (tile.TileType == TileID.Ash)
                {
                    tile.TileType = (ushort)ModContent.TileType<FrozenAsh>();
                    WorldGen.SquareTileFrame(x, y, true); // Refresh the tile frame
                }
                // Replace Lava with Custom Liquid
                if (tile.LiquidType == LiquidID.Lava)
                {
                    tile.LiquidType = 0; // No liquid
                    tile.LiquidAmount = 0; // Set liquid amount to 0
                    WorldGen.SquareTileFrame(x, y, true); // Refresh the tile frame
                }
                // Replace HellStone with the custom block
                if (tile.TileType == TileID.Hellstone)
                {
                    tile.TileType = (ushort)ModContent.TileType<Tiles.FrozenHellStone>();
                    WorldGen.SquareTileFrame(x, y, true); // Refresh the tile frame
                }
            }
        }

        // Updates the world visuals
        NetMessage.SendData(MessageID.WorldData); // Syncs changes with all players
    }
    


}