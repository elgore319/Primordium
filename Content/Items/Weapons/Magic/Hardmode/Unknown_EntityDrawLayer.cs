using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Primordium.Content.Items.Weapons.Magic.Hardmode
{
    public class Unknown_EntityDrawLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;

            if (player.HeldItem.type == ModContent.ItemType<Unknown_Entity>())
            {

                Texture2D texture = ModContent.Request<Texture2D>("Primordium/Content/Items/Weapons/Magic/Hardmode/UnknownEntity_Mouse").Value;

                Vector2 mousePosition = Main.MouseWorld;

                //Offset
                Vector2 offset = new Vector2(7f, 7f);
                Vector2 drawPosition = mousePosition + offset;

                // Rotation
                float rotation = (float)Main.timeForVisualEffects * 0.05f;

                // Draw 
                Main.spriteBatch.Draw(
                    texture, 
                    drawPosition - Main.screenPosition, 
                    null,
                    Color.White, 
                    rotation, 
                    texture.Size() * 0.5f, 
                    1f, 
                    SpriteEffects.None,
                    0f 
                );
            }
        }
    }
}