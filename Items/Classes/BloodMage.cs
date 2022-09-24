using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;
using Terraria.Localization;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class BloodMage : ModItem
	{
        float baseStat1 = .0082f;
        float stat1; // Magic Damage

        float baseStat2 = .011f;
        float stat2; // Max Mana

        float baseStat3 = .0055f;
        float stat3; // Health

        float baseBadStat = .005f;
        float badStat; // Defense

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

        //public override void OnCreate(ItemCreationContext context)
        //{
        //    if(_ACMConfigServer.Instance.classWeaponsEnabled)
        //        Main.player[Main.myPlayer].QuickSpawnItemDirect(null, ModContent.ItemType<ClassWeapons.BloodSpray>(), 1);
        //    base.OnCreate(context);
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", $"[{Language.GetTextValue("Mods.ApacchiisClassesMod2.HoldToPreviewAbilities")}]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                $"-(P: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_P_Name")})-\n" + //Blood Well
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_P_Prev")}\n" +
                $"-(A1: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A1_Name")})-\n" + //Transfusion
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A1_Prev")}\n" +
                $"-(A2: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Name")})-\n" + //Blood Enchantment
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_A2_Prev")}\n" +
                $"-(Ult: {Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_Ult_Name")})-\n" + //Regeneration
                $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.BloodMage_Ult_Prev")}");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")} p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMana")} p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")} p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")} p/lvl");

            var level = modPlayer.bloodMageLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MagicDamage")}\n" +
                                                                      "+" + level * (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxMana")}\n" +
                                                                      "+" + level * (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.MaxHealth")}");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + $"% {Language.GetTextValue("Mods.ApacchiisClassesMod2.Defense")}");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.bloodMageLevel == 0)
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
            acmPlayer.hasBloodMage = true;
            acmPlayer.equippedClass = "Blood Mage";
            acmPlayer.ultChargeMax = 3780;
            acmPlayer.ability1MaxCooldown = 35;
            acmPlayer.ability2MaxCooldown = 0;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Magic Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; // Magic Crit
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Health
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult; // Defense

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Magic) += acmPlayer.bloodMageLevel * stat1 * acmPlayer.classStatMultiplier;
                    acmPlayer.manaMult += stat2 * acmPlayer.bloodMageLevel* acmPlayer.classStatMultiplier;
                    acmPlayer.lifeMult += stat3 * acmPlayer.bloodMageLevel * acmPlayer.classStatMultiplier;
                    acmPlayer.defenseMult -= acmPlayer.bloodMageLevel * badStat;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Magic) += acmPlayer.bloodMageLevel * stat1 * acmPlayer.classStatMultiplier;
                acmPlayer.manaMult += stat2 * acmPlayer.bloodMageLevel* acmPlayer.classStatMultiplier;
                acmPlayer.lifeMult += stat3 * acmPlayer.bloodMageLevel * acmPlayer.classStatMultiplier;
                acmPlayer.defenseMult -= acmPlayer.bloodMageLevel * badStat;
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

