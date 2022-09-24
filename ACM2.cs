using System.IO;
using System;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ApacchiisClassesMod2.UI;
using Terraria.UI;
using System.Collections.Generic;
using Terraria.Localization;

namespace ApacchiisClassesMod2
{
    public class ACM2 : Mod
    {
        public static ModKeybind ClassAbility1;
        public static ModKeybind ClassAbility2;
        public static ModKeybind ClassAbilityUltimate;
        public static ModKeybind Menu;

        //public ACM2()
        //{
        //    Properties = new ModProperties()
        //    {
        //        Autoload = true,
        //        AutoloadGores = true,
        //        AutoloadSounds = true
        //    };
        //}

        

        public override void Load()
        {
            ClassAbility1 = KeybindLoader.RegisterKeybind(this, "Class Ability: 1", "Q");
            ClassAbility2 = KeybindLoader.RegisterKeybind(this, "Class Ability: 2", "C");
            ClassAbilityUltimate = KeybindLoader.RegisterKeybind(this, "Class Ability: Ultimate", "V");
            Menu = KeybindLoader.RegisterKeybind(this, "Menu", "N");



            base.Load();
        }

        internal enum ACMHandlePacketMessage : byte
        {
            SyncBosses,
            SyncTalentPoints,

            PlayerSyncPlayer,
            HealPlayer,
            HealPlayerFast,
            HealPlayerMedium,
            HealPlayerSlow,
            HealPlayerSnail,
            SyncRegenStats,
            SyncPlayerHealth,
            BuffPlayer,
            SyncPlayerBuffs,
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ACMHandlePacketMessage msgType = (ACMHandlePacketMessage)reader.ReadByte();

            switch (msgType)
            {
                case ACMHandlePacketMessage.SyncBosses:
                    int playernumber = reader.ReadInt32();
                    string playerClass = reader.ReadString();
                    string bossDefeated = reader.ReadString();

                    ACMPlayer acmPlayer = Main.player[playernumber].GetModPlayer<ACMPlayer>();


                    switch (playerClass)
                    {
                        case "Vanguard":
                            if (!acmPlayer.vanguardDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.vanguardDefeatedBosses.Add(bossDefeated);
                                acmPlayer.vanguardSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Blood Mage":
                            if (!acmPlayer.bloodMageDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.bloodMageDefeatedBosses.Add(bossDefeated);
                                acmPlayer.bloodMageSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Commander":
                            if (!acmPlayer.commanderDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.commanderDefeatedBosses.Add(bossDefeated);
                                acmPlayer.commanderSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Scout":
                            if (!acmPlayer.scoutDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.scoutDefeatedBosses.Add(bossDefeated);
                                acmPlayer.scoutSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;

                        case "Soulmancer":
                            if (!acmPlayer.soulmancerDefeatedBosses.Contains(bossDefeated))
                            {
                                acmPlayer.soulmancerDefeatedBosses.Add(bossDefeated);
                                acmPlayer.soulmancerSkillPoints++;
                                acmPlayer.cardsPoints += 2;
                            }
                            break;
                    }
                    //acmPlayer.levelUpText = true;
                    break;

                case ACMHandlePacketMessage.BuffPlayer:
                    int playerToBuff = reader.ReadInt32();
                    int buffType = reader.ReadInt32();
                    int duration = reader.ReadInt32();

                    Main.player[playerToBuff].AddBuff(buffType, duration);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.BuffPlayer);
                        packet.Write(playerToBuff);
                        packet.Write(buffType);
                        packet.Write(duration);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerFast:

                    int PlayerNumber2 = reader.ReadInt32();
                    int totalHealAmount = reader.ReadInt32();

                    Main.player[PlayerNumber2].GetModPlayer<ACMPlayer>().healthToRegen += totalHealAmount;
                    Main.player[PlayerNumber2].HealEffect(totalHealAmount);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((int)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber2);
                        packet.Write(totalHealAmount);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerMedium:

                    int PlayerNumber3 = reader.ReadInt32();
                    int totalHealAmountMedium = reader.ReadInt32();

                    Main.player[PlayerNumber3].GetModPlayer<ACMPlayer>().healthToRegenMedium += totalHealAmountMedium;
                    Main.player[PlayerNumber3].HealEffect(totalHealAmountMedium);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber3);
                        packet.Write(0);
                        packet.Write(totalHealAmountMedium);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerSlow:

                    int PlayerNumber4 = reader.ReadInt32();
                    int totalHealAmountSlow = reader.ReadInt32();

                    Main.player[PlayerNumber4].GetModPlayer<ACMPlayer>().healthToRegenSlow += totalHealAmountSlow;
                    Main.player[PlayerNumber4].HealEffect(totalHealAmountSlow);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber4);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(totalHealAmountSlow);
                        packet.Write(0);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayerSnail:

                    int PlayerNumber5 = reader.ReadInt32();
                    int totalHealAmountSnail = reader.ReadInt32();

                    Main.player[PlayerNumber5].GetModPlayer<ACMPlayer>().healthToRegenSnail += totalHealAmountSnail;
                    Main.player[PlayerNumber5].HealEffect(totalHealAmountSnail);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncRegenStats);
                        packet.Write(PlayerNumber5);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(0);
                        packet.Write(totalHealAmountSnail);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.HealPlayer:

                    int PlayerNumber = reader.ReadInt32();
                    int healAmount = reader.ReadInt32();

                    Main.player[PlayerNumber].statLife += healAmount;
                    Main.player[PlayerNumber].HealEffect(healAmount);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)ACMHandlePacketMessage.SyncPlayerHealth);
                        packet.Write((byte)PlayerNumber);
                        packet.Write(Main.player[PlayerNumber].statLife);
                        packet.Send(-1, -1);
                    }
                    break;

                case ACMHandlePacketMessage.SyncPlayerHealth:

                    PlayerNumber = reader.ReadInt32();
                    int PlayerHealth = reader.ReadInt32();

                    Main.player[PlayerNumber].statLife = PlayerHealth;
                    break;

                case ACMHandlePacketMessage.SyncRegenStats:

                    PlayerNumber = reader.ReadInt32();
                    int regenFast = reader.ReadInt32();
                    int regenMedium = reader.ReadInt32();
                    int regenSlow = reader.ReadInt32();
                    int regenSnail = reader.ReadInt32();

                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegen += regenFast;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenMedium += regenMedium;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenSlow += regenSlow;
                    Main.player[PlayerNumber].GetModPlayer<ACMPlayer>().healthToRegenSnail += regenSnail;

                    break;

                default:
                    Logger.WarnFormat("ACM2:Message type unknown: {0}", msgType);
                    break;
            }
        }
    }
}