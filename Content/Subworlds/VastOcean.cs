using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria;
using SubworldLibrary;
using Primordium.Content.World_Generation.VastOcean_GenPasses;

public class VastOcean : Subworld
{
    public override int Width => 4000;
    public override int Height => 1000;

    public override bool ShouldSave => false;
    public override bool NoPlayerSaving => false;
    public override List<GenPass> Tasks => new List<GenPass>()
    {
        new FirstOceanPass(),
        new IceBiomePass()
	// other passes
    };

    public override void OnLoad()
    {

    }
    public override void OnEnter()
    {
        SubworldSystem.noReturn = false;
        SubworldSystem.hideUnderworld = true;
    }
}