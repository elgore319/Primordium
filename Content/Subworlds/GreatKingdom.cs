using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria;
using SubworldLibrary;
using Microsoft.Xna.Framework;

public class GreatKingdom : Subworld
{
    public override int Width => 4000;
    public override int Height => 1000;
    public override bool ShouldSave => true;
    public override bool NoPlayerSaving => false;
    private static readonly Vector2 CustomSpawn = new Vector2(45, 500); // X,Y coordinates

    public override List<GenPass> Tasks => new List<GenPass>()
    {
        new FirstKingdomPass()
	// other passes
    };

    //Always night and storming
    public override void OnLoad()
    {
        Main.time = 0;
        Main.spawnTileX = (int)CustomSpawn.X;
        Main.spawnTileY = (int)CustomSpawn.Y;
    }
    public override void OnEnter()
    {
        SubworldSystem.noReturn = false;
        SubworldSystem.hideUnderworld = true;
        Main.LocalPlayer.position = CustomSpawn;
    }
}
