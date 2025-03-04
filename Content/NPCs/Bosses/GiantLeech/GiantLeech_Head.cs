using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Primordium.Content.NPCs.Bosses.GiantLeech
{
    public class GiantLeech_Head : GiantLeech
    {
        public override int BodyType => ModContent.NPCType<GiantLeech_Body>();
        public override int TailType => ModContent.NPCType<GiantLeech_Tail>();


        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 6000; // Head has more HP
            NPC.damage = 70;    // Head deals more damage
            NPC.width = 106;
            NPC.height = 120;
        }

        public override void AI()
        {
            if (NPC.ai[0] == 0 && NPC.localAI[0] == 0)  // Head segment logic
            {
                SpawnSegments();  // Spawn body and tail
                NPC.localAI[0] = 1; // Ensure this only runs once
            }

            // Head AI logic - Move toward the player
            NPC.TargetClosest(true);
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

            // Movement towards the player
            float speed = 10f;  // Adjust for worm speed
            float turnResistance = 10f;  // Adjust for smooth turning
            Vector2 targetPosition = player.Center;
            Vector2 direction = targetPosition - NPC.Center;
            direction.Normalize();
            direction *= speed;
            NPC.velocity = (NPC.velocity * (turnResistance - 1) + direction) / turnResistance;  // Smooth movement

            // Apply rotation to the head based on its velocity (direction)
            if (NPC.velocity.Length() > 0f)
            {
                NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;
            }
        }

        private void SpawnSegments()
        {
            int previousIndex = NPC.whoAmI;  // Start with the head

            for (int i = 0; i < 10; i++)  // Number of body segments
            {
                int type = i == 9 ? TailType : BodyType;  // Tail at the last segment
                int index = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, type);

                Main.npc[index].realLife = NPC.whoAmI;  // Link to the head
                Main.npc[index].ai[1] = previousIndex;  // Link to the previous segment
                Main.npc[previousIndex].ai[0] = index;  // Forward link for the current segment

                previousIndex = index;
            }
        }
    }
}