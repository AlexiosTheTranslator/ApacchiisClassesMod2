using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Projectiles.Vanguard
{
	public class VanguardSpear : ModProjectile
	{
        //public override string Texture => "ApacchiisClassesMod2/Projectiles/Invisible";

        bool flag = false;
        int range;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vanguard's Spear");
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 60;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 2;
            Projectile.tileCollide = false;
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            var acmPlayer = player.GetModPlayer<ACMPlayer>();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;

            var d1 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * -.2f, Projectile.velocity.Y * -.2f, 0, Color.White, 1.25f);
            var d2 = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.AmberBolt, Projectile.velocity.X * .33f, Projectile.velocity.Y * .33f, 0, Color.White, .75f);
            d1.noGravity = true;
            d2.noGravity = true;

            if (acmPlayer.vanguardTalent_4 == "L")
                range = 600;
            else
                range = 300;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    if (Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= 64 && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i].type != NPCID.DD2Bartender && Main.npc[i].type != NPCID.DD2EterniaCrystal && Main.npc[i].type != NPCID.DD2LanePortal && !Main.npc[i].friendly)
                    {
                        int hitDir;
                        if (Main.npc[i].position.X < Main.player[Projectile.owner].position.X)
                            hitDir = -1;
                        else
                            hitDir = 1;

                        if (acmPlayer.vanguardTalent_6 == "L") // Double range
                        {
                            Projectile.width = 600;
                            Projectile.height = 600;
                            range = 600;
                        }
                        else
                        {
                            Projectile.width = 300;
                            Projectile.height = 300;
                            range = 300;
                        }

                        if (!flag)
                        {
                            if (acmPlayer.vanguardTalent_6 == "L")
                            {
                                Projectile.position.X -= 300;
                                Projectile.position.Y -= 300;
                            }
                            else
                            {
                                Projectile.position.X -= 150;
                                Projectile.position.Y -= 150;
                            }
                            flag = true;
                        }

                        for (int i2 = 0; i2 < Main.maxNPCs; i2++)
                            if (Vector2.Distance(Projectile.Center, Main.npc[i2].Center) <= range && !Main.npc[i2].townNPC && !Main.npc[i2].dontTakeDamage && Main.npc[i2].type != NPCID.DD2Bartender && Main.npc[i2].type != NPCID.DD2EterniaCrystal && Main.npc[i2].type != NPCID.DD2LanePortal && !Main.npc[i2].friendly)
                                player.ApplyDamageToNPC(Main.npc[i2], (int)(acmPlayer.vanguardSpearDamage * acmPlayer.abilityPower * player.GetModPlayer<ACMPlayer>().abilityPower), 5f, hitDir, false);
                        
                        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

                        if (acmPlayer.vanguardTalent_6 == "L")
                        {
                            for (int x = 0; x < 30; x++)
                            {
                                var d3 = Dust.NewDustDirect(Projectile.position, 600, 600, DustID.AmberBolt, Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, Color.White, 2f);
                                var d4 = Dust.NewDustDirect(Projectile.position, 600, 600, DustID.AmberBolt, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 0, Color.White, 1f);
                                d3.noGravity = true;
                                d4.noGravity = false;
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 70; y++)
                            {
                                var d3 = Dust.NewDustDirect(Projectile.position, 300, 300, DustID.AmberBolt, Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, Color.White, 2f);
                                var d4 = Dust.NewDustDirect(Projectile.position, 300, 300, DustID.AmberBolt, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 0, Color.White, 1f);
                                d3.noGravity = true;
                                d4.noGravity = false;
                            }
                        }

                        Projectile.Kill();
                    }
                }
            }

            base.AI();
        }

        public override bool? CanHitNPC(NPC target) => false;
    }
}

