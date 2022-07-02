using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Soulmancer : ModItem
	{
        float baseStat1 = .0225f;
        float stat1; // Ability Power

        float baseStat2 = .5f;
        float stat2; // Magic Crit

        float baseStat3 = .006f;
        float stat3; // Mana Cost

        float baseBadStat = .007f;
        float badStat; // Magic Damage

        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.ClassPrefix")}: Soulmancer");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Blue;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<BlueCloth>());
            recipe.Register();
        }

        public override void OnCreate(ItemCreationContext context)
        {
            if(_ACMConfigServer.Instance.classWeaponsEnabled)
                Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.SoulBurner>(), 1);
                base.OnCreate(context);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            
            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Prev_1")}\n{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_P_Prev_1")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Soulmancer_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")} p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicCrit")} p/lvl\n" +
                                                                         "-" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")} p/lvl");

            var level = modPlayer.soulmancerLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.AbilityPower")}\n" +
                                                                      "+" + level * (decimal)(stat2 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicCrit")}\n" +
                                                                      "-" + level * (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.ManaCost")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.soulmancerLevel == 0)
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
            acmPlayer.hasSoulmancer = true;
            acmPlayer.equippedClass = "Soulmancer";
            acmPlayer.ultChargeMax = 1920;
            acmPlayer.ability1MaxCooldown = 27;
            acmPlayer.ability2MaxCooldown = 12;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Magic Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Magic Crit
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Health
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult; // Defense

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    acmPlayer.abilityPower += acmPlayer.soulmancerLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.GetCritChance(DamageClass.Magic) += (int)(stat2 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier);
                    Player.manaCost -= stat3 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier;
                    Player.GetDamage(DamageClass.Magic) -= acmPlayer.soulmancerLevel * badStat;
                }
            }
            else
            {
                acmPlayer.abilityPower += acmPlayer.soulmancerLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.GetCritChance(DamageClass.Magic) += (int)(stat2 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier);
                Player.manaCost -= stat3 * acmPlayer.soulmancerLevel * acmPlayer.classStatMultiplier;
                Player.GetDamage(DamageClass.Magic) -= acmPlayer.soulmancerLevel * badStat;
            }

            acmPlayer.classStatMultiplier = 1f;
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

