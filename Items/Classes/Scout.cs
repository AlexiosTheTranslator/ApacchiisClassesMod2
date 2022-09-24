using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Scout : ModItem
	{
        float baseStat1 = .0086f;
        float stat1; // Ranged Dmg

        float baseStat2 = .002f;
        float stat2; // Acceleration

        float baseStat3 = .0035f;
        float stat3; // Dodge

        float baseBadStat = .0045f;
        float badStat; // Health

		public override void SetDefaults()
		{
            Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Green;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GreenCloth>());
            recipe.Register();
        }

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if (_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.FadingDagger>(), 1);
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A1_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A2_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Name")})-\n" +
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Prev_1")} " + modPlayer.scoutUltInvDuration / 60 + $" {Language.GetTextValue("Mods.ApacchiisClassesMod2.Scout_Ult_Prev_2")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedDamage")} p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MovementAcceleration")} p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.DodgeChance")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");

            var level = modPlayer.scoutLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.RangedDamage")}\n" +
                                                                      "+" + level * (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MovementAcceleration")}\n" +
                                                                      "+" + level * (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.DodgeChance")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.scoutLevel == 0)
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
            acmPlayer.hasScout = true;
            acmPlayer.equippedClass = "Scout";
            acmPlayer.ultChargeMax = 1680;
            acmPlayer.ability1MaxCooldown = 38;
            acmPlayer.ability2MaxCooldown = 10;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.runAcceleration += stat2 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
                    acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Ranged) += acmPlayer.scoutLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.runAcceleration += stat2 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
                acmPlayer.dodgeChance += stat3 * acmPlayer.scoutLevel * acmPlayer.classStatMultiplier;
            }

            if (acmPlayer.scoutTalent_2 == "R" || acmPlayer.scoutTalent_2 == "B")
            {
                if (acmPlayer.scoutCanDoubleJump)
                    Player.hasJumpOption_Blizzard = true;
                else
                    Player.hasJumpOption_Blizzard = false;
            }
            else
            {
                if (acmPlayer.scoutCanDoubleJump)
                    Player.hasJumpOption_Cloud = true;
                else
                    Player.hasJumpOption_Cloud = false;
            }
            
            Player.moveSpeed += acmPlayer.scoutPassiveSpeedBonus;

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

