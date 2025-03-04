using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WarriorsPath.Content.Items.Weapons.Melee.Hardmode;

namespace WarriorsPath.Content.NPCs.Bosses.HumanBoss
{
    public class HumanBoss : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 24; // Match player size
            NPC.height = 42;
            NPC.damage = 25;
            NPC.defense = 10;
            NPC.lifeMax = 5000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 5, 0, 0); // Drops 5 gold on death
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1; // Custom AI
            Music = MusicID.Boss2; // Set boss music

            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void AI()
        {
            // Custom AI for walking and flying
            HandleMovement();
            HandlePhases();
        }

        private void HandleMovement()
        {
            Player player = Main.player[NPC.target];

            // Acquire a target if not already done
            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
            {
                NPC.TargetClosest();
            }

            if (player.dead) // End the fight if the player dies
            {
                NPC.velocity.Y += 0.1f; // Drift upward when despawning
                if (NPC.timeLeft > 10)
                    NPC.timeLeft = 10;
                return;
            }

            // Movement logic (walking/flying)
            if (NPC.life > NPC.lifeMax * 0.5) // Walk during the first phase
            {
                NPC.TargetClosest();
                float speed = 3f;
                NPC.velocity.X = NPC.direction * speed;
            }
            else // Fly during later phases
            {
                FlyToPlayer(player);
            }
        }

        private void FlyToPlayer(Player player)
        {
            float speed = 6f;
            Vector2 targetPosition = player.Center;
            Vector2 direction = targetPosition - NPC.Center;
            direction.Normalize();
            NPC.velocity = direction * speed;
        }

        private void HandlePhases()
        {
            if (NPC.life <= NPC.lifeMax * 0.75 && NPC.ai[0] == 0) // Phase 2 at 75% HP
            {
                EnterPhase2();
            }
            else if (NPC.life <= NPC.lifeMax * 0.5 && NPC.ai[0] == 1) // Phase 3 at 50% HP
            {
                EnterPhase3();
            }
            else if (NPC.life <= NPC.lifeMax * 0.25 && NPC.ai[0] == 2) // Phase 4 at 25% HP
            {
                EnterPhase4();
            }
        }

        private void EnterPhase2()
        {
            NPC.ai[0] = 1;
            Main.NewText("The boss enters Phase 2!", 175, 75, 255); // Announce phase change
            NPC.defense += 5; // Buff defense
                              // Add custom behavior (e.g., new attack or pattern)
        }

        private void EnterPhase3()
        {
            NPC.ai[0] = 2;
            Main.NewText("The boss enters Phase 3!", 175, 50, 255);
            NPC.damage += 10; // Increase damage
        }

        private void EnterPhase4()
        {
            NPC.ai[0] = 3;
            Main.NewText("The boss enters Phase 4!", 200, 0, 50);
            NPC.knockBackResist = 0.5f; // Change KB resistance for a tougher finish
        }
        public override void FindFrame(int frameHeight)
        {
            // Define the frame range for the boss
            int startFrame = 0;
            int finalFrame = 16;

            // Adjust frames for the second phase
            if (NPC.ai[0] >= 1) // Assuming SecondStage is determined by ai[0]
            {
                startFrame = 17;
                finalFrame = Main.npcFrameCount[NPC.type] - 1;

                // Transition smoothly to the second stage frames
                if (NPC.frame.Y < startFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }

            // Control frame speed and adjust animation rate based on movement
            int frameSpeed = 5; // Lower = faster animation
            NPC.frameCounter += 0.5f;
            NPC.frameCounter += NPC.velocity.Length() / 10f; // Increase speed with NPC movement

            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight; // Move to the next frame

                // Loop back to the start frame if exceeding the final frame
                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordOfScripture>(), 1)); // Drops a custom weapon
        }
    }
}