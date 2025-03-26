using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primordium.Content.Items.Boss_Items.Drops.ThePrimordial;
using Primordium.Content.NPCs.Bosses.The_Primordial.HealthBar;
using Primordium.Content.Projectiles.Bosses.The_Primordial;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Primordium.Content.NPCs.Bosses.The_Primordial;

public class ThePrimordial : ModNPC
{
    public override void SetDefaults()
    {
        NPC.width = 110;
        NPC.height = 110;
        NPC.scale = 2f;
        NPC.damage = 120; 
        NPC.defense = 50; 
        NPC.lifeMax = 50000; 
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0f;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.value = Item.buyPrice(platinum: 5); 
        NPC.SpawnWithHigherTime(30);
        NPC.boss = true;
        NPC.npcSlots = 10f;
        NPC.BossBar = ModContent.GetInstance<PrimordialBossBar>(); 
        Music = MusicLoader.GetMusicSlot(Mod, "Content/Assets/Music/Cosmic_Reign"); 
    }
    private enum AttackState
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Death
    }
    private AttackState CurrentPhase = AttackState.Phase1;
    private int attackTimer;
    private int phaseTransitionTimer;


    public override void AI()
    {
        // Don't do anything if despawning
        if (NPC.localAI[3] >= 1f)
        {
            NPC.velocity = Vector2.Zero;
            return;
        }
        // Phase transitions
        if (phaseTransitionTimer > 0)
        {
            phaseTransitionTimer--;
            DoPhaseTransitionEffects();
            return;
        }
        // Execute current phase behavior
        switch (CurrentPhase)
        {
            case AttackState.Phase1:
                Phase1Behavior();
                break;
            case AttackState.Phase2:
                Phase2Behavior();
                break;
            case AttackState.Phase3:
                Phase3Behavior();
                break;
            case AttackState.Phase4:
                Phase4Behavior();
                break;
            case AttackState.Death:
                DeathSequence();
                break;
        }
        attackTimer++;

        // Check for phase transitions based on health
        CheckPhaseTransitions();
    }
    private void CheckPhaseTransitions()
    {
        if (CurrentPhase == AttackState.Phase1 && NPC.life < NPC.lifeMax * 0.75f)
        {
            CurrentPhase = AttackState.Phase2;
            phaseTransitionTimer = 120; // 2 second transition
            EnterPhase2();
        }
        else if (CurrentPhase == AttackState.Phase2 && NPC.life < NPC.lifeMax * 0.5f)
        {
            CurrentPhase = AttackState.Phase3;
            phaseTransitionTimer = 120;
            EnterPhase3();
        }
        else if (CurrentPhase == AttackState.Phase3 && NPC.life < NPC.lifeMax * 0.25f)
        {
            CurrentPhase = AttackState.Phase4;
            phaseTransitionTimer = 120;
            EnterPhase4();
        }
        else if (CurrentPhase == AttackState.Phase4 && NPC.life < NPC.lifeMax * 0.05f)
        {
            CurrentPhase = AttackState.Death;
            phaseTransitionTimer = 180;
        }
    }



    private void Phase1Behavior()
    {
        NPC.TargetClosest(true);
        Player player = Main.player[NPC.target];

        // Basic movement - hover near player
        Vector2 targetPosition = player.Center + new Vector2(0, -200);
        Vector2 direction = targetPosition - NPC.Center;
        float distance = direction.Length();

        if (distance > 20f)
        {
            direction.Normalize();
            NPC.velocity = direction * 8f;
        }
        else
        {
            NPC.velocity *= 0.95f;
        }

        // Periodic attacks
        if (attackTimer % 120 == 0) // Every 2 seconds
        {
            // Shoot a spread of projectiles
            int projectileCount = 5;
            float spread = MathHelper.PiOver2;
            float startAngle = NPC.rotation - spread / 2;

            for (int i = 0; i < projectileCount; i++)
            {
                Vector2 velocity = Vector2.UnitX.RotatedBy(startAngle + spread * i / (projectileCount - 1)) * 10f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity,
                    ModContent.ProjectileType<PrimordialEnergy>(), 30, 2f, Main.myPlayer);
            }
        }
    }

    private void Phase2Behavior()
    {
        // More aggressive movement
        NPC.TargetClosest(true);
        Player player = Main.player[NPC.target];

        // Charge at player periodically
        if (attackTimer % 180 == 0)
        {
            Vector2 direction = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitY);
            NPC.velocity = direction * 20f;
        }

        // Continuous projectile ring
        if (attackTimer % 30 == 0)
        {
            int projectileCount = 8;
            for (int i = 0; i < projectileCount; i++)
            {
                Vector2 velocity = Vector2.UnitX.RotatedBy(MathHelper.TwoPi * i / projectileCount) * 8f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity,
                    ModContent.ProjectileType<PrimordialOrb>(), 25, 2f, Main.myPlayer);
            }
        }
    }
    private void Phase3Behavior()
    {
        NPC.TargetClosest(true);
    }
    private void Phase4Behavior()
    {
        NPC.TargetClosest(true);
    }
    private void EnterPhase2()
    {
        // Visual and sound effects for phase transition
        SoundEngine.PlaySound(SoundID.Item100, NPC.Center); // Custom sound later

        for (int i = 0; i < 30; i++)
        {
            Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
            dust.velocity = Main.rand.NextVector2Circular(10f, 10f);
            dust.scale = 2f;
            dust.noGravity = true;
        }

        // Increase stats for phase 2
        NPC.damage = 150;
        NPC.defense = 60;
    }
    private void EnterPhase3()
    {

    }
    private void EnterPhase4()
    {

    }

    private void DoPhaseTransitionEffects()
    {
        // Continuous effects during transition
        if (Main.rand.NextBool(5))
        {
            Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height,
                CurrentPhase == AttackState.Phase2 ? DustID.PurpleTorch :
                CurrentPhase == AttackState.Phase3 ? DustID.BlueTorch : DustID.RedTorch);
            dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
            dust.scale = 1.5f;
            dust.noGravity = true;
        }
    }
    public override void FindFrame(int frameHeight)
    {
        NPC.frameCounter++;

        // Simple animation cycle
        if (NPC.frameCounter >= 10)
        {
            NPC.frameCounter = 0;
            NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[NPC.type]);
        }

        // Rotate based on velocity for more dynamic look
        if (NPC.velocity.X != 0)
        {
            NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;
        }
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        // Custom drawing for glow effects
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Vector2 drawPos = NPC.Center - screenPos;
        Vector2 origin = new Vector2(NPC.width, NPC.height) / 2f;

        // Draw glow behind main sprite
        Color glowColor = CurrentPhase switch
        {
            AttackState.Phase1 => Color.Purple,
            AttackState.Phase2 => Color.Blue,
            AttackState.Phase3 => Color.Red,
            AttackState.Phase4 => Color.Orange,
            _ => Color.White
        };

        for (int i = 0; i < 3; i++)
        {
            Vector2 offset = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f));
            spriteBatch.Draw(texture, drawPos + offset, NPC.frame, glowColor * 0.5f, NPC.rotation, origin, NPC.scale,
                NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
        }

        // Main sprite
        spriteBatch.Draw(texture, drawPos, NPC.frame, drawColor, NPC.rotation, origin, NPC.scale,
            NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

        return false;
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        // Guaranteed drops
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PrimordialEssence>(), 1));

        // Expert/master mode drops
        npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<PrimordialRelic>()));
        npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<PrimordialPetItem>(), 4));

        // Normal drops
        LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
        notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PrimordialShard>(), 1, 15, 25));

        // Weapons/tools
        //npcLoot.Add(ItemDropRule.OneFromOptions(1,
            //ModContent.ItemType<PrimordialStaff>(),
            //ModContent.ItemType<PrimordialBow>(),
            //ModContent.ItemType<PrimordialBlade>()));
    }
    private void DeathSequence()
    {
        // Freeze the boss during death animation
        NPC.velocity = Vector2.Zero;
        NPC.dontTakeDamage = true;

        // Visual effects
        if (phaseTransitionTimer % 5 == 0) // Spawn effects every 5 ticks
        {
            // Explosive dust effects
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height,
                    CurrentPhase == AttackState.Phase4 ? DustID.LifeDrain : DustID.ShadowbeamStaff);
                dust.velocity = Main.rand.NextVector2Circular(15f, 15f);
                dust.scale = 2.5f;
                dust.noGravity = true;
            }

            // Lightning effects
            if (Main.rand.NextBool(3))
            {
                Vector2 spawnPos = NPC.Center + new Vector2(Main.rand.Next(-NPC.width, NPC.width),
                                                          Main.rand.Next(-NPC.height, NPC.height));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPos, Vector2.Zero,
                    ProjectileID.CultistBossLightningOrbArc, 0, 0f, Main.myPlayer);
            }
        }

        // Final explosion
        if (phaseTransitionTimer == 30) // Halfway through transition
        {
            SoundEngine.PlaySound(SoundID.Item100 with { Pitch = -0.5f, Volume = 1.5f }, NPC.Center);

            // Large explosion
            for (int i = 0; i < 100; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Vortex);
                dust.velocity = Main.rand.NextVector2Circular(20f, 20f);
                dust.scale = 3f;
                dust.noGravity = true;
            }

            // Shockwave effect
            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
                ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 0f, Main.myPlayer);

            // Screen shake for dramatic effect
            if (Main.netMode != NetmodeID.Server)
            {
                Main.LocalPlayer.GetModPlayer<MyModPlayer>().ScreenShakeIntensity = 10f;
            }
        }

        // Spawn loot and finish
        if (phaseTransitionTimer <= 0)
        {
            // Spawn loot normally (handled by Terraria's system)
            NPC.active = false;
            NPC.netUpdate = true;

            // Special post-boss effects
            StartPostBossEvent();
        }
    }

    private void StartPostBossEvent()
    {
        // Unlock achievements or flags
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            MyModWorld.downedPrimordial = true;
        }
        else
        {
            // Sync in multiplayer
            NetMessage.SendData(MessageID.WorldData);
        }

        // Spawn special NPC or item
        if (Main.rand.NextFloat() < 0.1f) // 10% chance
        {
            int item = Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(),
                ModContent.ItemType<PrimordialCore>());

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item);
            }
        }

        // Broadcast a message
        string deathMessage = "The fabric of reality stabilizes as The Primordial dissolves into the void...";
        if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(deathMessage), Color.Purple);
        }
        else
        {
            Main.NewText(deathMessage, Color.Purple);
        }

        // Start a custom post-boss event (optional)
        if (!Main.dayTime && Main.rand.NextBool(3)) // 33% chance at night
        {
            MyModWorld.StartPrimordialRemnantEvent();
        }
    }
}