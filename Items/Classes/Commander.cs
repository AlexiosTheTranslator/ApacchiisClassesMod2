using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ApacchiisClassesMod2.Configs;

namespace ApacchiisClassesMod2.Items.Classes
{
	public class Commander : ModItem
	{
        float baseStat1 = .0078f;
        float stat1; // Minion Dmg

        float baseStat2 = .1f;
        float stat2; // Minion Slots

        float baseStat3 = .0125f;
        float stat3;// Whip Range

        float baseBadStat = .003f;
        float badStat; // Acceleration

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Class: Commander");
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = 0;
			Item.rare = ItemRarityID.Pink;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult;
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult;
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult;
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult;

            Item.GetGlobalItem<ACMGlobalItem>().isClass = true;
        }

		public override void AddRecipes()
		{
            var recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<OrangeCloth>());
            recipe.Register();
        }

        public override void OnCreate(ItemCreationContext context)
        {
            if (_ACMConfigServer.Instance.classWeaponsEnabled)
                Main.player[Main.myPlayer].QuickSpawnItem(Main.player[Main.myPlayer].GetSource_GiftOrReward(), ModContent.ItemType<ClassWeapons.SeekingSpirit>(), 1);
            base.OnCreate(context);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player Player = Main.player[Main.myPlayer];

            var modPlayer = Player.GetModPlayer<ACMPlayer>();

            TooltipLine HoldSToPreview = new TooltipLine(Mod, "HoldPreview", "[Hold 'W' to preview abilities]");
            TooltipLine AbilityPreview = new TooltipLine(Mod, "AbilityPreview",
                "-(P: Commander's Will)-\n" +
                "For each minion slot you have gain a small amount of damage reduction.\n" +
                "-(A1: War Banner)-\n" +
                "Place a war banner at your feet, players standing inside its area will deal increased damage and take reduced damage\n" +
                "-(A2: Battle Cry)-\n" +
                "Scream loudly, intimidating enemies around you and increasing the damage they take for a period of time.\n" +
                "-(Ult: Inspire)-\n" +
                "Inspire yourself and your teammates, granting everyone guaranteed critical hits for a period of time.");

            HoldSToPreview.OverrideColor = Color.CadetBlue;
            AbilityPreview.OverrideColor = Color.CadetBlue;

            TooltipLine lineStatsPreview = new TooltipLine(Mod, "Stats", "+" + (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + "% Summon Damage p/lvl\n" +
                                                                         "+" + (decimal)(stat2 * modPlayer.classStatMultiplier) + " Minion Slots p/lvl\n" +
                                                                         "+" + (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + "% Whip Range p/lvl");
            TooltipLine lineBadStatPreview = new TooltipLine(Mod, "BadStat", "-" + (decimal)badStat * 100 + "% Movement Acceleration p/lvl");

            var level = modPlayer.commanderLevel;

            TooltipLine lineLevel = new TooltipLine(Mod, "Level", "Level: " + level);
            TooltipLine lineStats = new TooltipLine(Mod, "Stats", "+" + level * (decimal)(stat1 * 100 * modPlayer.classStatMultiplier) + "% Summon Damage\n" +
                                                                      "+" + level * (decimal)(stat2 * modPlayer.classStatMultiplier) + " Minion Slots\n" +
                                                                      "+" + level * (decimal)(stat3 * 100 * modPlayer.classStatMultiplier) + "% Whip Range");
            TooltipLine lineBadStat = new TooltipLine(Mod, "BadStat", "-" + level * (decimal)badStat * 100 + "% Movement Acceleration");

            lineLevel.OverrideColor = new Color(200, 150, 25);
            lineBadStat.OverrideColor = new Color(200, 50, 25);
            lineBadStatPreview.OverrideColor = new Color(200, 50, 25);

            if (modPlayer.commanderLevel == 0)
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
            acmPlayer.hasCommander = true;
            acmPlayer.equippedClass = "Commander";
            acmPlayer.ultChargeMax = 3000;
            acmPlayer.ability1MaxCooldown = 50;
            acmPlayer.ability2MaxCooldown = 28;

            stat1 = baseStat1 * _ACMConfigServer.Instance.classStatMult; // Minion Damage
            stat2 = baseStat2 * _ACMConfigServer.Instance.classStatMult; ; // Minion Slots
            stat3 = baseStat3 * _ACMConfigServer.Instance.classStatMult; // Whip Range
            badStat = baseBadStat * _ACMConfigServer.Instance.classStatMult; // Acceleration

            if (_ACMConfigServer.Instance.configHidden)
            {
                if (!hideVisual)
                {
                    Player.GetDamage(DamageClass.Summon) += acmPlayer.commanderLevel * stat1 * acmPlayer.classStatMultiplier;
                    Player.maxMinions += (int)(stat2 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier);
                    Player.whipRangeMultiplier += stat3 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier;
                    Player.runAcceleration -= badStat;
                }
            }
            else
            {
                Player.GetDamage(DamageClass.Magic) += acmPlayer.commanderLevel * stat1 * acmPlayer.classStatMultiplier;
                Player.maxMinions += (int)(stat2 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier);
                Player.whipRangeMultiplier += stat3 * acmPlayer.commanderLevel * acmPlayer.classStatMultiplier;
                Player.runAcceleration -= badStat;
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

