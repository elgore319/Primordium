using Terraria.ModLoader.IO;
using Terraria.ModLoader;

public class MyModWorld : ModSystem
{
    public static bool downedPrimordial;

    public override void OnWorldLoad()
    {
        downedPrimordial = false;
    }

    public override void OnWorldUnload()
    {
        downedPrimordial = false;
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["downedPrimordial"] = downedPrimordial;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        downedPrimordial = tag.GetBool("downedPrimordial");
    }

    public static void StartPrimordialRemnantEvent()
    {
        // Your custom post-boss event implementation
    }
}
