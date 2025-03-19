using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria;
using SubworldLibrary;

public class VastOcean : Subworld
{
    public override int Width => 4000;
    public override int Height => 1000;

    public override bool ShouldSave => false;
    public override bool NoPlayerSaving => false;
    
    bool isIceOnLeft = Main.dungeonX < Main.maxTilesX / 2;

    public override List<GenPass> Tasks => new List<GenPass>()
    {
        new FirstOceanPass(),
        new IceBiomePass(isIceOnLeft)
	// other passes
    };

    // Sets the time to the middle of the day whenever the subworld loads
    // Eventually fix this to be the same time as the main world
    public override void OnLoad()
    {
        Main.dayTime = true;
        Main.time = 27000;
    }
    public override void OnEnter()
    {
        SubworldSystem.noReturn = false;
        SubworldSystem.hideUnderworld = true;
    }
}