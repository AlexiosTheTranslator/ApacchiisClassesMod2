//using Terraria;
//using Terraria.Audio;
//using Terraria.ID;
//using Terraria.ModLoader;
//
//namespace ApacchiisClassesMod2.Items
//{
//	public class LostRelic : ModItem
//	{
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Lost Relic");
//            Tooltip.SetDefault("An ancient relic, maybe it can be of use to you");
//        }
//
//		public override void SetDefaults()
//		{
//            Item.maxStack = 999;
//			Item.width = 30;
//			Item.height = 30;
//            Item.useStyle = ItemUseStyleID.EatFood;
//			Item.value = Item.sellPrice(0, 2, 0, 0);
//            Item.rare = 11;
//            Item.consumable = true;
//		}
//
//        public override bool? UseItem(Player player)
//        {
//            if (player.whoAmI == Main.myPlayer)
//                if (player.whoAmI == Main.myPlayer)
//                    Item.stack--;
//
//            return base.UseItem(player);
//        }
//
//        public override void UseAnimation(Player player)
//        {
//            if (player.whoAmI == Main.myPlayer)
//            {
//                var modPlayer = player.GetModPlayer<ACMPlayer>();
//                if (modPlayer.hasVanguard)
//                {
//                    modPlayer.vanguardSpentSkillPoints = 0;
//
//                    if (modPlayer.vanguardTalent_1 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_2 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_3 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_4 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_5 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_6 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_7 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_8 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_9 != "N")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_10 != "N")
//                        modPlayer.vanguardSkillPoints++;
//
//                    if (modPlayer.vanguardTalent_1 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_2 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_3 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_4 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_5 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_6 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_7 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_8 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_9 == "B")
//                        modPlayer.vanguardSkillPoints++;
//                    if (modPlayer.vanguardTalent_10 == "B")
//                        modPlayer.vanguardSkillPoints++;
//
//                    modPlayer.vanguardTalent_1 = "N";
//                    modPlayer.vanguardTalent_2 = "N";
//                    modPlayer.vanguardTalent_3 = "N";
//                    modPlayer.vanguardTalent_4 = "N";
//                    modPlayer.vanguardTalent_5 = "N";
//                    modPlayer.vanguardTalent_6 = "N";
//                    modPlayer.vanguardTalent_7 = "N";
//                    modPlayer.vanguardTalent_8 = "N";
//                    modPlayer.vanguardTalent_9 = "N";
//                    modPlayer.vanguardTalent_10 = "N";
//                }
//
//                if (modPlayer.hasBloodMage)
//                {
//                    modPlayer.bloodMageSpentSkillPoints = 0;
//
//                    if (modPlayer.bloodMageTalent_1 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_2 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_3 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_4 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_5 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_6 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_7 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_8 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_9 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_10 != "N")
//                        modPlayer.bloodMageSkillPoints++;
//
//                    if (modPlayer.bloodMageTalent_1 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_2 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_3 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_4 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_5 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_6 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_7 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_8 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_9 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//                    if (modPlayer.bloodMageTalent_10 == "B")
//                        modPlayer.bloodMageSkillPoints++;
//
//                    modPlayer.bloodMageTalent_1 = "N";
//                    modPlayer.bloodMageTalent_2 = "N";
//                    modPlayer.bloodMageTalent_3 = "N";
//                    modPlayer.bloodMageTalent_4 = "N";
//                    modPlayer.bloodMageTalent_5 = "N";
//                    modPlayer.bloodMageTalent_6 = "N";
//                    modPlayer.bloodMageTalent_7 = "N";
//                    modPlayer.bloodMageTalent_8 = "N";
//                    modPlayer.bloodMageTalent_9 = "N";
//                    modPlayer.bloodMageTalent_10 = "N";
//                }
//
//                if (modPlayer.hasCommander)
//                {
//                    modPlayer.commanderSpentSkillPoints = 0;
//
//                    if (modPlayer.commanderTalent_1 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_2 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_3 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_4 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_5 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_6 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_7 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_8 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_9 != "N")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_10 != "N")
//                        modPlayer.commanderSkillPoints++;
//
//                    if (modPlayer.commanderTalent_1 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_2 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_3 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_4 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_5 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_6 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_7 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_8 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_9 == "B")
//                        modPlayer.commanderSkillPoints++;
//                    if (modPlayer.commanderTalent_10 == "B")
//                        modPlayer.commanderSkillPoints++;
//
//                    modPlayer.commanderTalent_1 = "N";
//                    modPlayer.commanderTalent_2 = "N";
//                    modPlayer.commanderTalent_3 = "N";
//                    modPlayer.commanderTalent_4 = "N";
//                    modPlayer.commanderTalent_5 = "N";
//                    modPlayer.commanderTalent_6 = "N";
//                    modPlayer.commanderTalent_7 = "N";
//                    modPlayer.commanderTalent_8 = "N";
//                    modPlayer.commanderTalent_9 = "N";
//                    modPlayer.commanderTalent_10 = "N";
//                }
//
//                if (modPlayer.hasScout)
//                {
//                    modPlayer.scoutSpentSkillPoints = 0;
//
//                    if (modPlayer.scoutTalent_1 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_2 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_3 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_4 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_5 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_6 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_7 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_8 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_9 != "N")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_10 != "N")
//                        modPlayer.scoutSkillPoints++;
//
//                    if (modPlayer.scoutTalent_1 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_2 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_3 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_4 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_5 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_6 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_7 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_8 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_9 == "B")
//                        modPlayer.scoutSkillPoints++;
//                    if (modPlayer.scoutTalent_10 == "B")
//                        modPlayer.scoutSkillPoints++;
//
//                    modPlayer.scoutTalent_1 = "N";
//                    modPlayer.scoutTalent_2 = "N";
//                    modPlayer.scoutTalent_3 = "N";
//                    modPlayer.scoutTalent_4 = "N";
//                    modPlayer.scoutTalent_5 = "N";
//                    modPlayer.scoutTalent_6 = "N";
//                    modPlayer.scoutTalent_7 = "N";
//                    modPlayer.scoutTalent_8 = "N";
//                    modPlayer.scoutTalent_9 = "N";
//                    modPlayer.scoutTalent_10 = "N";
//                }
//
//                SoundEngine.PlaySound(SoundID.Item3, player.position);
//                Main.NewText("ZIP: " + player.GetModPlayer<ACMPlayer>().equippedClass + "'s talents have been reset");
//
//                //if (player.itemAnimation == 2)
//                //{
//                //    
//                //}  
//            }
//            base.UseAnimation(player);
//        }
//    }
//}

