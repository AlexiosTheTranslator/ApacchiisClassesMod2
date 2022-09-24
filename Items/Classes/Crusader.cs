/*using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Crusader : ModItem
	{
        float baseStat1 = .0065f;
        float stat1; // Melee Dmg

        float baseStat2 = .005f;
        float stat2; // Healing Power

        float baseStat3 = .0088f;
        float stat3; // Defense

        float baseBadStat = .004f;
        float badStat; // HP

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
                                                                         "+" + (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.HealingPower")} p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");

            var level = modPlayer.vanguardLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + (decimal)(level * stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MeleeDamage")}\n" +
                                                                      "+" + (decimal)(level * stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.HealingPower")}\n" +
                                                                      "+" + (decimal)(level * stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + (decimal)(level * badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");

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
            acmPlayer.equippedClass = "Crusader";
            acmPlayer.ability1MaxCooldown = 20;
            acmPlayer.ability2MaxCooldown = 65;
            acmPlayer.ultChargeMax = 3600;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Melee) += acmPlayer.crusaderLevel * stat1 * acmPlayer.classStatMultiplier;
                    acmPlayer.defenseMult += acmPlayer.crusaderLevel * stat2 * acmPlayer.classStatMultiplier;
                    acmPlayer.lifeMult += acmPlayer.crusaderLevel * stat3 * acmPlayer.classStatMultiplier;
                    Player.GetAttackSpeed(DamageClass.Melee) -= acmPlayer.crusaderLevel * badStat;
                    if (acmPlayer.defenseMult * acmPlayer.crusaderLevel * stat2 * acmPlayer.classStatMultiplier < 1)
                        Player.statDefense++;
                }
            }
            else
            {
                //Stats here ^
            }

            acmPlayer.classStatMultiplier = 1f;


            // Class Menu Text
            acmPlayer.P_Name = "Tenacity";
            acmPlayer.P_Desc_1 = $"Reduce damage taken by {acmPlayer.crusaderEndurance * 100}%.";
            acmPlayer.P_Desc_2 = $"This effect is increased by an additional {acmPlayer.crusaderEnduranceBuff * 100}% while under the effects of a healing buff.";

            acmPlayer.A1_Name = "Protection";
            acmPlayer.A1_Desc_1 = "Apply a healing buff to all players that slowly heals them each second and reduces damage taken.";
            acmPlayer.A1_Effect_1 = $"Healing: {(decimal)(100 * acmPlayer.healingPower)}% p/sec";
            acmPlayer.A1_Effect_2 = $"Duration: {acmPlayer.crusaderHealingDuration / 60}s";
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

*/