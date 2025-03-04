using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace WarriorsPath.Content.NPCs.Bosses.GiantLeech
{
    public abstract class GiantLeech : ModNPC
    {
        public abstract int BodyType { get; }
        public abstract int TailType { get; }

        public override void SetDefaults()
        {
            NPC.lifeMax = 5000; // Total health
            NPC.damage = 50;    // Damage dealt
            NPC.defense = 10;   // Defense stat
            NPC.knockBackResist = 0f; // No knockback
            NPC.width = 32;
            NPC.height = 32;
            NPC.aiStyle = -1; // Custom AI
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.netAlways = true;
        }

        public override void AI()
        {
            WormAI();
        }

        public void WormAI()
        {
            // Ensure the NPC is initialized properly
            if (NPC.realLife == -1)
            {
                NPC.realLife = NPC.whoAmI; // Link the segment to the head
            }

            // If this is the head
            if (NPC.ai[0] == 0)
            {
                NPC.TargetClosest(true);  // Target the closest player
                Player player = Main.player[NPC.target];

                if (!player.active || player.dead)
                {
                    NPC.TargetClosest(false);
                    if (!player.active || player.dead)
                    {
                        NPC.active = false;  // Despawn if no active player
                        return;
                    }
                }

               
            }
            else
            {
                // Body and Tail Segments follow the previous segment
                int previousSegment = (int)NPC.ai[1]; // This links the segment to the previous segment

                if (previousSegment >= 0 && Main.npc[previousSegment].active)
                {
                    NPC realNPC = Main.npc[previousSegment];

                    // Adjust segment distances for head, body, and tail
                    float segmentDistance;
                    if (NPC.type == ModContent.NPCType<GiantLeech_Head>())
                    {
                        // Head to body distance (increase the gap)
                        segmentDistance = NPC.width; // Increased gap between head and body
                    }
                    else if (NPC.type == ModContent.NPCType<GiantLeech_Body>())
                    {
                        // Body to body distance (reduce the gap)
                        segmentDistance = NPC.width; // Reduced gap for body segments
                    }
                    else if (NPC.type == ModContent.NPCType<GiantLeech_Tail>())
                    {
                        // Tail distance (smaller gap)
                        segmentDistance = NPC.width; // Slightly smaller distance for the tail
                    }
                    else
                    {
                        // Default fallback
                        segmentDistance = NPC.width;
                    }

                    Vector2 segmentCenter = realNPC.Center;
                    if (Vector2.Distance(NPC.Center, segmentCenter) > segmentDistance)
                    {
                        // Follow the previous segment (head or body)
                        Vector2 direction = segmentCenter - NPC.Center;
                        direction.Normalize();
                        NPC.velocity = direction * 10f;  // Adjust speed if necessary
                    }

                    // Apply rotation to the body based on the movement direction
                    if (NPC.velocity.Length() > 0f)
                    {
                        float targetRotation = NPC.velocity.ToRotation() + MathHelper.PiOver2; // Rotate the body towards the movement direction
                        NPC.rotation = MathHelper.Lerp(NPC.rotation, targetRotation, 0.2f); // Smooth the rotation
                    }
                }
                else
                {
                    // If this segment doesn't have a leader, despawn
                    NPC.active = false;
                }
            }
        }
    }
}