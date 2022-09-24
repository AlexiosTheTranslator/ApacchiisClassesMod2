using IL.Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2
{
    //note this command is effectively broken in multiplayer due to the lack of netcode around ExamplePlayer.score
    public class ACM2Commands : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "acr";

        public override string Usage
            => "/acr <command>";

        public override string Description
            => "Mostly used for debugging";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            int player;
            for (player = 0; player < 255; player++)
            {
                if (Main.player[player].active && /*Main.player[player].name == args[0]*/ Main.myPlayer == player)
                {
                    break;
                }
            }

            if (player == 255)
            {
                throw new UsageException("Could not find player: " + args[0]);
            }

            var modPlayer = Main.player[player].GetModPlayer<ACMPlayer>();

            if(args[0] == "cc")
            {
                modPlayer.ability1Cooldown = 0;
                modPlayer.ability2Cooldown = 0;
                modPlayer.ultCharge = modPlayer.ultChargeMax;
                modPlayer.InBattle();

                caller.Reply("All cooldowns removed from player: '" + Main.player[player].name + "'");
            }

            if(args[0] == "resetRunes" || args[0] == "resetCards")
            {
                modPlayer.card_ProwlerCount = 0;
                modPlayer.card_CarryCount = 0;
                modPlayer.card_DeadeyeCount = 0;
                modPlayer.card_FortifiedCount = 0;
                modPlayer.card_HealerCount = 0;
                modPlayer.card_HealthyCount = 0;
                modPlayer.card_ImpenetrableCount = 0;
                modPlayer.card_MagicalCount = 0;
                modPlayer.card_MasterfulCount = 0;
                modPlayer.card_MendingCount = 0;
                modPlayer.card_MightyCount = 0;
                modPlayer.card_NimbleHandsCount = 0;
                modPlayer.card_PowerfulCount = 0;
                modPlayer.card_SneakyCount = 0;
                modPlayer.card_SparkOfGeniusCount = 0;
                modPlayer.card_TimelessCount = 0;
                modPlayer.card_VeteranCount = 0;

                caller.Reply("All runes removed from player: '" + Main.player[player].name + "'");
            }

            if (args[0] == "cheatLevelUp")
            {
                if (modPlayer.hasBloodMage)
                {
                    modPlayer.bloodMageSkillPoints++;
                    modPlayer.bloodMageDefeatedBosses.Add("Cheat Level");
                }

                if (modPlayer.hasCommander)
                {
                    modPlayer.commanderSkillPoints++;
                    modPlayer.commanderDefeatedBosses.Add("Cheat Level");
                }

                if (modPlayer.hasScout)
                {
                    modPlayer.scoutSkillPoints++;
                    modPlayer.scoutDefeatedBosses.Add("Cheat Level");
                }

                if (modPlayer.hasSoulmancer)
                {
                    modPlayer.soulmancerSkillPoints++;
                    modPlayer.soulmancerDefeatedBosses.Add("Cheat Level");
                }

                if (modPlayer.hasVanguard)
                {
                    modPlayer.vanguardSkillPoints++;
                    modPlayer.vanguardDefeatedBosses.Add("Cheat Level");
                }

                caller.Reply($"Added a cheat level to the player's currently equipped class");
            }

            if (args[0] == "resetHUD")
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    ModContent.GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                    ModContent.GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
                }
                caller.Reply("HUD has been reset");
                return;
            }
        }
    }
}