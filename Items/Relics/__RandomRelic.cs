using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
 
namespace ApacchiisClassesMod2.Items.Relics
{
    public class RandomRelic : ModItem
    {
        public override void SetStaticDefaults(){
			DisplayName.SetDefault($"{Language.GetTextValue("Mods.ApacchiisClassesMod2.RandomRelic")}");
			Tooltip.SetDefault("Gives you a random relic\n{$CommonItemTooltip.RightClickToOpen}");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 0;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = 11;
            Item.maxStack = 99;
            Item.value = 0;
        }


        //public override void AddRecipes()
        //{
        //    var recipe = CreateRecipe(1);
        //    recipe.AddIngredient(ModContent.ItemType<LostRelic>(), 1);
        //    recipe.AddTile(TileID.Anvils);
        //    recipe.Register();
        //}

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            int relicCount = player.GetModPlayer<ACMPlayer>().relicList.Count; //22 | (040222:1730)
            int choice = Main.rand.Next(relicCount);

            player.QuickSpawnItem(player.GetSource_OpenItem(Type), player.GetModPlayer<ACMPlayer>().relicList[choice]);
            //if(player.GetModPlayer<ACMPlayer>().relicList[choice] == ModContent.ItemType<Nessie>())
            //{
            //    SoundEngine.PlaySound(...);
            //}

            base.RightClick(player);
        }
    }
}
