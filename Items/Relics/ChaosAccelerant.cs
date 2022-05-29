using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class ChaosAccelerant : ModItem
	{
        public string desc = "Increases ability power by 65%\n" +
                             "Increases cooldown reduction by 35%\n" +
                             "Decreases ult cost by 20%\n" +
                             "Reduces healing power by 75%\n" +
                             "Decreases all weapon damage by 25%\n" +
                             "Decreases max health by 50%\n" +
                             "Decreases max mana by 50%";
        string donator = "Eloraeon";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Chaos Accelerant");
            Tooltip.SetDefault(desc + $"\n[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, {donator}!][c/e796e8:]]");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.accessory = true;	
			Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Quest;

            Item.GetGlobalItem<ACMGlobalItem>().isRelic = true;
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc + $"\n[c/e796e8:> Donator Item <]\n[c/e796e8:[Thank you for your support, {donator}!][c/e796e8:]]";
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasChaosAccelerant = true;
            acmPlayer.abilityPower += .65f;
            acmPlayer.cooldownReduction -= .35f;
            acmPlayer.ultCooldownReduction -= .2f;
            acmPlayer.healingPower -= .75f;
            player.GetDamage(DamageClass.Generic) -= .25f;


            base.UpdateVanity(player);
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<ACMPlayer>().hasRelic == true)
                return false;

            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

