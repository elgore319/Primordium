using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace WarriorsPath.Content.UI
{
    public class TemporalChargeUI : UIState
    {
        public static int maxCharges = 10; // Set max Temporal Charges

        public static int currentCharges = 0; // This should be updated from your class system

        private Texture2D barBackgroundTexture;
        private Texture2D barFillTexture;

        public override void OnInitialize()
        {
            // Load textures once during initialization
            barBackgroundTexture = ModContent.Request<Texture2D>("WarriorsPath/Content/UI/Textures/barBackgroundTexture").Value;
            barFillTexture = ModContent.Request<Texture2D>("WarriorsPath/Content/UI/Textures/barFillTexture").Value;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            Main.NewText("TemporalChargeUI Draw method called!"); // Debug message

            // Don't draw if the player is dead or in the main menu
            if (Main.LocalPlayer.dead || Main.gameMenu)
                return;

            // Position the bar near the player's health or mana bar
            Vector2 position = new Vector2(Main.screenWidth, Main.screenHeight);

            // Draw the bar background
            spriteBatch.Draw(barBackgroundTexture, position, Color.White);

            // Calculate the width of the filled portion
            float fillWidth = (float)currentCharges / maxCharges * barBackgroundTexture.Width;

            // Draw the filled portion of the bar
            spriteBatch.Draw(barFillTexture, new Rectangle((int)position.X, (int)position.Y, (int)fillWidth, barFillTexture.Height), Color.White);
        }
    }
}