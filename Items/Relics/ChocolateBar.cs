using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items.Relics
{
	public class ChocolateBar : ModItem
	{
        public string desc = "2 Seconds after taking damage from an enemy, regen 8% of the damage you took + 2% of your max health\n" +
                             "[Does not stack, only the last hit you take before the heal will have effect]";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault($"[Relic] Chocolate Bar");
            Tooltip.SetDefault(desc);
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
            Item.GetGlobalItem<ACMGlobalItem>().desc = desc;
        }

        public override void UpdateVanity(Player player)
        {
            var acmPlayer = player.GetModPlayer<ACMPlayer>();
            acmPlayer.hasRelic = true;
            acmPlayer.hasChocolateBar = true;

            base.UpdateVanity(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
                if (line.Mod == "Terraria" && line.Name == "Equipable")
                    line.Text = $"{Language.GetTextValue("Mods.ApacchiisClassesMod2.EquipableRelic")}";

            base.ModifyTooltips(tooltips);
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (!modded)
                return false;

            return base.CanEquipAccessory(player, slot, modded);
        }
    }
}

