/*
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Audio;
namespace WarriorsPath.Content.NPCs.Bosses.WormBoss 
{     */
    //I don't know whats wrong with this code, keeps telling me there is something wrong with the Image paths
    //Until I get it figured out I'll just leave it here, don't want to waste it
    
    /*
    public class TheWorm : ModNPC
    {
        private List<Vector2> segments = new List<Vector2>(); // Store positions of the segments
        private Vector2 tailPosition; // Position of the tail
        private const int NumSegments = 6; // Number of segments in the worm

        // Textures for the head, segments, and tail
        private Texture2D headTexture;
        private Texture2D segmentTexture;
        private Texture2D tailTexture;

        public override void SetDefaults()
        {
            // Set the "head" of the worm (main body)
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 60;
            NPC.defense = 25;
            NPC.lifeMax = 10000;
            NPC.life = NPC.lifeMax;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/CustomWormBossTheme");

            // Load textures
            headTexture = ModContent.Request<Texture2D>("WarriorsPath/Content/NPCs/Bosses/WormBoss/Sprites/WormBoss_Head").Value;
            segmentTexture = ModContent.Request<Texture2D>("WarriorsPath/Content/NPCs/Bosses/WormBoss/Sprites/WormBoss_Segment").Value;
            tailTexture = ModContent.Request<Texture2D>("WarriorsPath/Content/NPCs/Bosses/WormBoss/Sprites/WormBoss_Tail").Value;
        }

        public override void AI()
        {
            // Update the segments and tail
            UpdateSegments();

            // Basic movement logic for the worm's head
            Player player = Main.player[NPC.target];
            if (player.active && !player.dead)
            {
                Vector2 direction = player.Center - NPC.Center;
                direction.Normalize();
                NPC.velocity = direction * 4f;
            }
            else
            {
                NPC.velocity = Vector2.Zero;
            }

            // Update segment positions to follow the head
            for (int i = 1; i < segments.Count; i++)
            {
                Vector2 previousSegment = i == 1 ? NPC.Center : segments[i - 1];
                segments[i] = Vector2.Lerp(segments[i], previousSegment, 0.1f);
            }

            // Update the tail position to follow the last segment
            Vector2 lastSegment = segments[segments.Count - 1];
            tailPosition = Vector2.Lerp(tailPosition, lastSegment, 0.1f);
        }

        private void UpdateSegments()
        {
            if (segments.Count == 0)
            {
                segments.Add(NPC.Center);
                for (int i = 1; i < NumSegments; i++)
                {
                    segments.Add(NPC.Center + new Vector2(0, 50 * i));
                }

                tailPosition = segments[segments.Count - 1] + new Vector2(0, 50);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            // You can control which texture is drawn for each part here
            if (segments.Count > 0)
            {
                // Drawing the head (first part of the worm)
                Main.spriteBatch.Draw(headTexture, segments[0] - Main.screenPosition, Color.White);

                // Drawing the segments
                for (int i = 1; i < segments.Count; i++)
                {
                    Main.spriteBatch.Draw(segmentTexture, segments[i] - Main.screenPosition, Color.White);
                }

                // Drawing the tail (last part of the worm)
                Main.spriteBatch.Draw(tailTexture, tailPosition - Main.screenPosition, Color.White);
            }
        }

        public override void OnKill()
        {
            SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
            Main.NewText("The Worm Boss has been defeated!", Color.OrangeRed);
        }
    }
}
    
     
     */