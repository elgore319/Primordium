using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Primordium.Content.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Primordium.Content.Systems
{
    public class UISystem : ModSystem
    {
        private UserInterface temporalChargeUI;
        private TemporalChargeUI temporalChargeUIState;

        public override void Load()
        {
            if (!Main.dedServ) // Ensure this only runs on the client
            {
                temporalChargeUIState = new TemporalChargeUI();
                temporalChargeUI = new UserInterface();
                temporalChargeUI.SetState(temporalChargeUIState);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            temporalChargeUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "WarriorsPath: TemporalChargeUI",
                    delegate {
                        temporalChargeUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}