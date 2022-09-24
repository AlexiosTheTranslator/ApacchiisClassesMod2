using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2.Items
{
	public class LostRuneFragment : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lost Rune Fragmet");
            Tooltip.SetDefault("");
        }

		public override void SetDefaults()
		{
            Item.maxStack = 999;
			Item.width = 30;
			Item.height = 30;
			Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
		}
    }
}

