using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria;
using SubworldLibrary;

public class GreatKingdom : Subworld
{
    public override int Width => 4000;
    public override int Height => 1000;

    public override bool ShouldSave => true;
    public override bool NoPlayerSaving => false;

    public override List<GenPass> Tasks => new List<GenPass>()
    {
        new FirstKingdomPass()
	// other passes
    };

    //Always night and storming
    public override void OnLoad()
    {
        Main.dayTime = false;
    }
    public override void OnEnter()
    {
        SubworldSystem.noReturn = false;
        SubworldSystem.hideUnderworld = true;
    }
}
