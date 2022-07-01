using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;

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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Blood Mage");
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
                Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.BloodSpray>(), 1);
            base.OnCreate(context);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];
            var modPlayer = Player.GetModPlayer<ACMPlayer>();
            
            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", "[Hold 'W' to preview abilities]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                "-(P: Blood Well)-\n" +
                "Every second in battle gain a stack of 'Blood', each stack slightly increases your life regen.\n" +
                "-(A1: Transfusion)-\n" +
                "Throw a blob of your own blood that will seek out the nearest enemy, damage it, and come back to you with a bit of its blood, restoring your health.\n" +
                "-(A2: Blood Enchantment)-\n" +
                "Enchant your weapon with your own blood, constantly losing health per second but increasing damage dealt.\n" +
                "-(Ult: Regeneration)-\n" +
                "Regen yours and your allies' health every second over a duration.");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + "% Magic Damage p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + "% Max Mana p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + "% Max Health p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)(badStat * 100) + "% Defense p/lvl");

            var level = modPlayer.bloodMageLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + "% Magic Damage\n" +
                                                                      "+" + level * (decimal)(stat2 * 100 * modPlayer.classStatMultiplier) + "% Max Mana\n" +
                                                                      "+" + level * (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + "% Max Health");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)(badStat * 100) + "% Defense");

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

