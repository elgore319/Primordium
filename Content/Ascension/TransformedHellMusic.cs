
/*
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace WarriorsPath.Content.Ascended_Mode
{
    public class TransformedHellMusic : ModSceneEffect
    {
        // Set the priority of this scene effect
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        // Define the conditions for this scene effect to be active
        public override bool IsSceneEffectActive(Player player)
        {
            bool isActive = BiomeHellModify.IsHellTransformed && player.ZoneUnderworldHeight;
            if (isActive)
            {
                Main.NewText("Transformed Hell music is active!", Color.LimeGreen);
            }
            if (BiomeHellModify.IsHellTransformed && Main.LocalPlayer.ZoneUnderworldHeight)
            {
                // Force the music to your custom track
                Main.musicFade[Main.curMusic] = 0; // Fade out the current music
                Main.curMusic = MusicLoader.GetMusicSlot(Mod, "Assets/Music/The_Beginning_of_the_End"); // Set the custom music
                Main.musicFade[Main.curMusic] = 1; // Fade in the new music
            }
            return isActive;
        }
    }
} */