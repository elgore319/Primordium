using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Primordium.Content.Projectiles.Bosses.The_Primordial
{
    public class PrimordialOrb : ModProjectile
    {
        private int shootCooldown = 0;
        private const int ShootInterval = 30;
        private const float DetectionRange = 300f;

        // Store the target NPC using Projectile.ai[0]
        private NPC HomingTarget
        {
            get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
            set
            {
                Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1;
            }
        }

        public ref float DelayTimer => ref Projectile.ai[1];


        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 400;
            Projectile.tileCollide = false;
            Projectile.light = 1f;
            Projectile.timeLeft = 600;

            Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.3f);
        }

        // Custom AI
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = Main.MouseWorld;

            Projectile.rotation += 0.1f; // Rotate slowly for a visual effect

            // Optional: Ensure the projectile stays active as long as the player is using the item
            if (player.channel) // Check if the player is holding the use button
            {
                Projectile.timeLeft = 2; // Reset the timeLeft to keep the projectile alive
            }
            else
            {
                Projectile.Kill(); // Kill the projectile if the player stops using the item
            }

            if (shootCooldown > 0)
            {
                shootCooldown--; // Decrease cooldown
            }
            else
            {
                // Find the nearest enemy in range
                NPC target = FindNearestEnemy(DetectionRange);
                if (target != null)
                {
                    // Shoot a projectile at the target
                    ShootAtTarget(target);

                    // Reset cooldown
                    shootCooldown = ShootInterval;
                }
            }
        }
        private NPC FindNearestEnemy(float range)
        {
            NPC nearest = null;
            float nearestDistance = range;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance < nearestDistance)
                    {
                        nearest = npc;
                        nearestDistance = distance;
                    }
                }
            }

            return nearest;
        }

        // Helper method to shoot a projectile at the target
        private void ShootAtTarget(NPC target)
        {
            Vector2 direction = target.Center - Projectile.Center;
            direction.Normalize(); // Normalize to get a unit vector

            float speed = 10f; 
            int damage = Projectile.damage; 
            float knockback = Projectile.knockBack; 

            // Spawn projectile
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(), 
                Projectile.Center, 
                direction * speed, //Velocity
                ProjectileID.RainbowRodBullet, 
                damage, // Damage
                knockback, // Knockback
                Projectile.owner // Owner (player who shot the projectile)
            );

            // Optional: Play a sound effect when shooting
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
        }
    }
}