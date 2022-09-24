using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Vanguard : ModItem
	{
        float baseStat1 = .0078f;
        float stat1; // Melee Dmg

        float baseStat2 = .0092f;
        float stat2; // Defense

        float baseStat3 = .0088f;
        float stat3; // HP

        float baseBadStat = .003f;
        float badStat; // Attack Speed

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Red;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<RedCloth>());
            recipe.Register();
        }

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if (_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.TrainingRapier>(), 1);
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MeleeDamage")} p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AttackSpeed")} p/lvl (does not apply to tools)");

            var level = modPlayer.vanguardLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (decimal)(level * stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MeleeDamage")}\n" +
                                                                      "+" + (decimal)(level * stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")}\n" +
                                                                      "+" + (decimal)(level * stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (decimal)(level * badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AttackSpeed")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.vanguardLevel == 0)
            {
                tooltips.Add(lineLevel);
                tooltips.Add(lineStatsPreview);
                tooltips.Add(lineBadStatPreview);
            }
            else
            {
                tooltips.Add(lineLevel);
                tooltips.Add(lineStats);
                tooltips.Add(lineBadStat);
            }

            if (Player.controlUp)
                tooltips.Add(AbilityPreview); 
            else
                tooltips.Add(HoldSToPreview);
            

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory (Player Player, bool hideVisual)
		{
            var acmPlayer = Player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasClass = true;
            acmPlayer.hasVanguard = true;
            acmPlayer.equippedClass = "Vanguard";
            acmPlayer.ability1MaxCooldown = 22;
            acmPlayer.ability2MaxCooldown = 48;
            acmPlayer.ultChargeMax = 2640;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Melee) += acmPlayer.vanguardLevel * stat1 * acmPlayer.classStatMultiplier;
                    acmPlayer.defenseMult += acmPlayer.vanguardLevel * stat2 * acmPlayer.classStatMultiplier;
                    acmPlayer.lifeMult += acmPlayer.vanguardLevel * stat3 * acmPlayer.classStatMultiplier;
                    Player.GetAttackSpeed(DamageClass.Melee) -= acmPlayer.vanguardLevel * badStat;
                    if (acmPlayer.defenseMult * acmPlayer.crusaderLevel * stat2 * acmPlayer.classStatMultiplier < 1)
                        Player.statDefense++;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Melee) += acmPlayer.vanguardLevel * stat1 * acmPlayer.classStatMultiplier;
                acmPlayer.defenseMult += acmPlayer.vanguardLevel * stat2 * acmPlayer.classStatMultiplier;
                acmPlayer.lifeMult += acmPlayer.vanguardLevel * stat3 * acmPlayer.classStatMultiplier;
                Player.GetAttackSpeed(DamageClass.Melee) -= acmPlayer.vanguardLevel * badStat;
                if (acmPlayer.defenseMult * acmPlayer.crusaderLevel * stat2 * acmPlayer.classStatMultiplier < 1)
                    Player.statDefense++;
            }

            acmPlayer.classStatMultiplier = 1f;


            // Class Menu Text
            acmPlayer.P_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_P_Name");
            acmPlayer.P_Desc_1 = $"Your armor is enchanted with light properties.";
            acmPlayer.P_Desc_2 = $"Enemies that directly attack you take a percentage of your defense as damage.";
            acmPlayer.P_Effect_1 = $"Reflected Damage: {acmPlayer.vanguardPassiveReflectAmount * 100 + acmPlayer.vanguardLevel * 2.5f}% = {acmPlayer.vanguardPassiveReflectAmount * 100}% + 2.5% p/Level({acmPlayer.vanguardLevel * 2.5f}%)";

            acmPlayer.A1_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A1_Name");
            acmPlayer.A1_Desc_1 = "Throw a spear of light that will explode if an enemy is nearby, dealing damage to all enemies around.";
            acmPlayer.A1_Effect_1 = $"Explosion Damage: {(int)(acmPlayer.vanguardSpearBaseDamage + acmPlayer.vanguardLevel * 10 * acmPlayer.abilityPower)} = {acmPlayer.vanguardSpearBaseDamage} + 10 p/Level({acmPlayer.vanguardLevel * 10})";

            acmPlayer.A2_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_A2_Name");
            acmPlayer.A2_Desc_1 = "Surround yourself in a barrier of light. Any damage taken when the barrier is active will be";
            acmPlayer.A2_Desc_2 = "reduced by a percentage.";
            acmPlayer.A2_Effect_1 = $"Damage Reduction: {(decimal)(acmPlayer.vanguardShieldDamageReduction * 100)}%";
            acmPlayer.A2_Effect_2 = $"Duration: {(decimal)(acmPlayer.vanguardShieldBaseDuration / 60)}s + 0.25s p/Level({(decimal)(acmPlayer.vanguardLevel * 0.25)}s) = {(decimal)(acmPlayer.vanguardShieldBaseDuration / 60 + acmPlayer.vanguardLevel * 0.25f)}s";

            acmPlayer.Ult_Name = Language.GetTextValue("Mods.ApacchiisClassesMod2.Vanguard_Ult_Name");
            acmPlayer.Ult_Desc_1 = "Call in a giant sword from the heavens. The sword hits enemies all around it, dealing massive damage.";
            acmPlayer.Ult_Desc_2 = "and executing enemies below 50% health.";
            acmPlayer.Ult_Desc_3 = $"(Bosses are executed below {acmPlayer.vanguardUltimateBossExecute * 100}% health)";
            acmPlayer.Ult_Effect_1 = $"Damage: {acmPlayer.vanguardSwordBaseDamage} + 9 p/Level({acmPlayer.vanguardLevel * 9}) = {(int)(acmPlayer.vanguardSwordBaseDamage + (acmPlayer.vanguardLevel * 9))}";
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<ACMPlayer>().hasClass == true)
                return false;

            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

