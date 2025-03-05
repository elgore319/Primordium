using Terraria.ModLoader;
using Terraria;

namespace Primordium.Content.Ascension
{
    public class Ascension : ModSystem
    {
        public override void PostUpdateWorld()
        {
            if (NPC.downedMoonlord && !MyModVariables.DialogueShown)
            {
                ShowCustomDialogue();
                MyModVariables.DialogueShown = true; // Prevent repeating the dialogue
            }
        }

        private void ShowCustomDialogue()
        {
            string message = "Higher deities have noticed your presence...";
            Main.NewText(message, 175, 75, 255); 
        }
    }

    public static class MyModVariables
    {
        public static bool DialogueShown = false; // Tracks if the dialogue has been shown
    }
}