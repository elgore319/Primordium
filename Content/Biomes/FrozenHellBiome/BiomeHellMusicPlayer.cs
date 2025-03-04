using Terraria;
using Terraria.ModLoader;

namespace Primordium.Content.Biomes.FrozenHellBiome;

//is not set up correctly yet
//this will be used to change the hell music to the custom track once it freezes, but for now it just overrides the original music constantly

public class BiomeHellMusicPlayer : ModSceneEffect
{

    public override bool IsSceneEffectActive(Player player)
    {
        // Check if the player is in the Underworld
        return player.ZoneUnderworldHeight;

    }

    public override int Music
    {
        get
        {
            // Replace with the custom Hell music
            return MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Hell_Froze_Over");
        }
    }

    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
}