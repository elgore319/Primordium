using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;

public class SeedGenPass : GenPass
{
    //TODO: remove this once tML changes generation passes
    public SeedGenPass() : base("Set Seed", 0.01f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Setting subworld seed"; // Sets the text displayed for this pass
        Main.ActiveWorldFileData.SetSeedToRandom(); // Randomizes the subworld seed
                                                    //Main.ActiveWorldFileData.SetSeed(100.ToString()); // Sets the subworld seed to 100

        // You can do other things in this pass as long as they don't make RNG calls! Having specific passes is cleaner though.
    }
}
// This pass is currently not in use
// It is an example pulled from the SubworldLibrary wiki