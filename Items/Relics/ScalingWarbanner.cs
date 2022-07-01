using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class ScalingWarbanner : ModItem
	{
        public string desc = "Increases non-negative stats gained from class banners by 10%\n" +
                             "Increases by an additional 8% in Hardmode";
        string donator = "Derin";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[Relic] Scaling Warbanner");
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
            acmPlayer.hasScalingWarbanner = true;
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

