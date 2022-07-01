using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace ApacchiisClassesMod2.Projectiles.Inventor
{
    public class Sentry : ModProjectile
    {
        bool flag = false;
        int firerate = 60;
        int firerateBase = 60;
        float range = 600;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inventor's Turret");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            Projectile.timeLeft = 60;

            //Projectile.direction = Projectile.spriteDirection = 1 or -1 depending on npc target;

            if (!flag)
            {


                firerate = acmPlayer.inventorSentryFirerate;
                firerateBase = firerate;
                range = acmPlayer.inventorSentryRange;
                flag = true;
            }

            // Circle Dust
            Vector2 origin = Projectile.Center;
            origin.X -= Projectile.width / 2;

            int locations = 100;
            for (int i = 0; i < locations; i++)
            {
                Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * range;
                var dust = Dust.NewDustPerfect(position, 2, Vector2.Zero, 0, Color.Orange, 1.25f);
                dust.noGravity = true;
            }

            firerate--;
            if (firerate <= 0)
            {
                ShootToTarget();
                firerate = firerateBase;
                Projectile.frameCounter = 1;
                Projectile.ai[1] = 4;
            }

            Projectile.ai[1]--;
            if(Projectile.ai[1] <= 0)
                Projectile.frameCounter = 0;
        }

        public override bool? CanHitNPC(NPC target) => false;

        void ShootToTarget()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                float shootToY = target.position.Y - Projectile.Center.Y;
                float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                if (distance < range && !target.friendly && target.active && target.type != NPCID.TargetDummy && target.lifeMax > 5)
                {
                    distance = 5f / distance;
                    shootToX *= distance * 5;
                    shootToY *= distance * 5;

                    if (Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
                    {
                        Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, shootToX, shootToY, ProjectileID.BulletHighVelocity, acmPlayer.inventorSentryDamage, 0, player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);
                    }
                }
            }
        }
    }
}
