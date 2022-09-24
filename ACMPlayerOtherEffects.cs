using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ApacchiisClassesMod2
{
    public class ACMPlayerOtherEffects : ModPlayer
	{
        #region Relics
        public bool hasRelic;

        public bool hasBrokenHeart;
        public bool hasBloodGem;
        public bool hasLeysMushroom;
        public bool hasStrangeMushroom;
        public bool hasMushroomConcentrate;
        public bool hasBerserkersBrew;
        public bool hasNessie;
        int nessieCooldown = -1;
        int nessieBaseCooldown = 60 * 65;
        #endregion

        int bloodGemProjectileTimer = 0;
        int bloodGemMeleeTimer = 0;

        float leysMushroomBuffChance = .14f;
        float leysMushroomHealChance = .03f;
        float leysMushroomHeal = .03f;

        string[] nessieProcText =
        {
            "Nessie!",
            "Little Nessie!",
            "Try as you might, you cant kill me, son!",
            "So cute!",
            "Squishy!",
            "*Squish*",
            "Ol' Nessie!",
            "^-^"
        };

        public override void ResetEffects()
        {
            hasRelic = false;

            hasBrokenHeart = false;
            hasBloodGem = false;
            hasLeysMushroom = false;
            hasStrangeMushroom = false;
            hasMushroomConcentrate = false;
            hasBerserkersBrew = false;
            hasNessie = false;
            nessieBaseCooldown = 60 * 65;

            base.ResetEffects();
        }

        public override void PreUpdate()
        {
            if (bloodGemProjectileTimer > 0)
                bloodGemProjectileTimer--;

            if (bloodGemMeleeTimer > 0)
                bloodGemMeleeTimer--;

            nessieCooldown--;
            base.PreUpdate();
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if (hasBrokenHeart && healValue >= 50 && item.type != ItemID.Mushroom)
                healValue += (int)(Player.statLifeMax2 * .07f);

            if (hasMushroomConcentrate && item.type == ItemID.Mushroom)
            {
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * .06f);
                healValue = 60 + heal;
            }

            if (hasBerserkersBrew && item.type != ItemID.Mushroom)
            {
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * .35f);
                healValue += heal;
            }

            base.GetHealLife(item, quickHeal, ref healValue);
        }

        //MOVE ALL THIS TO THE MAIN ACMPLAYER DUE TO HEALTH SCALING PROBLEMS
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            // Blood Gem
            if(bloodGemMeleeTimer <= 0 && hasBloodGem)
            {
                int heal = (int)(Player.statLifeMax2 * .02f);
                if(Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                    bloodGemMeleeTimer = 120;
                }  
            }

            // Ley's Mushroom Heal
            if(Main.rand.NextFloat() < leysMushroomHealChance && hasLeysMushroom)
            {
                if (Main.hardMode) leysMushroomHeal = .04f;
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * leysMushroomHeal);
                Player.statLife += heal;
                Player.HealEffect(heal);
            }

            //Ley's Mushroom Buffs
            if (Main.rand.NextFloat() < leysMushroomBuffChance && hasLeysMushroom)
            {
                int duration = 60 * 3;
                if (Main.hardMode) duration = 60 * 4;

                int buff = Main.rand.Next(4);
                if (buff == 0)
                    Player.AddBuff(BuffType<Buffs.LeysDamage>(), duration);
                if (buff == 1)
                    Player.AddBuff(BuffType<Buffs.LeysCrit>(), duration);
                if (buff == 2)
                    Player.AddBuff(BuffType<Buffs.LeysEndurance>(), duration);
                if (buff == 3)
                    Player.AddBuff(BuffType<Buffs.LeysAttackSpeed>(), duration);
            }
            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            base.OnHitByProjectile(proj, damage, crit);
        }

        // Modify hit by collision
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            damage = (int)(damage * GetInstance<Configs._ACMConfigServer>().enemyDamageMultiplier);

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.TrainingRapier>())
                    damage -= 6;

            if (hasNessie && nessieCooldown <= 0)
            {
                Player.NinjaDodge();
                nessieCooldown = nessieBaseCooldown;
                int nessieChosenText = Main.rand.Next(nessieProcText.Length);
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 140, Player.width, Player.height), Color.White, nessieProcText[nessieChosenText], true);
            }

            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }

        //MOVE ALL THIS TO THE MAIN ACMPLAYER DUE TO HEALTH SCALING PROBLEMS
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            // Blood Gem
            if (bloodGemProjectileTimer <= 0 && hasBloodGem)
            {
                int heal = (int)(Player.statLifeMax2 * .01f);
                if (Player.statLife < Player.statLifeMax2)
                {
                    Player.statLife += heal;
                    Player.HealEffect(heal);
                    bloodGemProjectileTimer = 240;
                }
            }

            // Ley's Mushroom Heal
            if (Main.rand.NextFloat() < leysMushroomHealChance && hasLeysMushroom)
            {
                if (Main.hardMode) leysMushroomHeal = .04f;
                int heal = (int)((Player.statLifeMax2 - Player.statLife) * leysMushroomHeal);
                Player.statLife += heal;
                Player.HealEffect(heal);
            }

            //Ley's Mushroom Buffs
            if (Main.rand.NextFloat() < leysMushroomBuffChance && hasLeysMushroom)
            {
                int duration = 60 * 3;
                if (Main.hardMode) duration = 60 * 4;

                int buff = Main.rand.Next(4);
                if (buff == 0)
                    Player.AddBuff(BuffType<Buffs.LeysDamage>(), duration);
                if(buff == 1)
                    Player.AddBuff(BuffType<Buffs.LeysCrit>(), duration);
                if (buff == 2)
                    Player.AddBuff(BuffType<Buffs.LeysEndurance>(), duration);
                if (buff == 3)
                    Player.AddBuff(BuffType<Buffs.LeysAttackSpeed>(), duration);
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        // Modify hit by projectile
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            damage = (int)(damage * GetInstance<Configs._ACMConfigServer>().enemyDamageMultiplier);

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.TrainingRapier>())
                    damage -= 6;

            if (hasNessie && nessieCooldown <= 0)
            {
                Player.NinjaDodge();
                nessieCooldown = nessieBaseCooldown;
                int nessieChosenText = Main.rand.Next(nessieProcText.Length);
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 140, Player.width, Player.height), Color.White, nessieProcText[nessieChosenText], true);
            }

            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {

            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.FadingDagger>() && proj.type == ProjectileType<Projectiles.Weapons.FadingDagger>() && crit)
                damage += 4;
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}