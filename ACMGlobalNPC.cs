using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApacchiisClassesMod2
{
	public class ACMGlobalNPC : GlobalNPC
	{
        
        public int battleCryBoost = 0;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        //public bool vanguardSpeared = false;
        //public Vector2 vanguardSpearPos;

        public override void ResetEffects(NPC npc)
        {
            npc.takenDamageMultiplier = 1f;
            base.ResetEffects(npc);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(!npc.boss && !npc.friendly && npc.lifeMax > 5)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Relics.RandomRelic>(), 200, 1, 1));

            base.ModifyNPCLoot(npc, npcLoot);
        }

        //public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        //{
        //    // Dropping relics
        //    //globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Relics.RandomRelic>(), 300, 1, 1));
        //
        //    base.ModifyGlobalLoot(globalLoot);
        //}

        public override void PostAI(NPC npc)
        {
            Player Player = Main.player[Main.myPlayer];

            if (battleCryBoost > 0)
            {
                battleCryBoost--;
                npc.takenDamageMultiplier += Player.GetModPlayer<ACMPlayer>().commanderCryBonusDamage;
            }
                
            base.PostAI(npc);
        }

        public override void OnKill(NPC npc)
        {
            if (npc.boss)
            {
                for (int playerToUpdate = 0; playerToUpdate < 255; playerToUpdate++)
                {
                    if(Main.player[playerToUpdate].active)
                    {
                        var acmPlayer = Main.player[playerToUpdate].GetModPlayer<ACMPlayer>();

                        //Vanguard List
                        if (acmPlayer.hasVanguard && !acmPlayer.vanguardDefeatedBosses.Contains(npc.TypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.TypeName);
                                packet.Send();
                            }
                            else
                            {
                                acmPlayer.vanguardDefeatedBosses.Add(npc.TypeName);
                                acmPlayer.vanguardSkillPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Blood Mage List
                        if (acmPlayer.hasBloodMage && !acmPlayer.bloodMageDefeatedBosses.Contains(npc.TypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.TypeName);
                                packet.Send();
                            }
                            else
                            {
                                acmPlayer.bloodMageDefeatedBosses.Add(npc.TypeName);
                                acmPlayer.bloodMageSkillPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Commander List
                        if (acmPlayer.hasCommander && !acmPlayer.commanderDefeatedBosses.Contains(npc.TypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.TypeName);
                                packet.Send();
                            }
                            else
                            {
                                acmPlayer.commanderDefeatedBosses.Add(npc.TypeName);
                                acmPlayer.commanderSkillPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Scout List
                        if (acmPlayer.hasScout && !acmPlayer.scoutDefeatedBosses.Contains(npc.TypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.TypeName);
                                packet.Send();
                            }
                            else
                            {
                                acmPlayer.scoutDefeatedBosses.Add(npc.TypeName);
                                acmPlayer.scoutSkillPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }

                        //Soulmancer List
                        if (acmPlayer.hasSoulmancer && !acmPlayer.soulmancerDefeatedBosses.Contains(npc.TypeName))
                        {
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                var packet = Mod.GetPacket();
                                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncBosses);
                                packet.Write((byte)playerToUpdate);
                                packet.Write(acmPlayer.equippedClass);
                                packet.Write(npc.TypeName);
                                packet.Send();
                            }
                            else
                            {
                                acmPlayer.soulmancerDefeatedBosses.Add(npc.TypeName);
                                acmPlayer.soulmancerSkillPoints++;
                                acmPlayer.levelUpText = true;
                            }
                        }
                    }
                } 
            }

            base.OnKill(npc);
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //if (battleCryBoost > 0)
            //    spriteBatch.Draw(ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/BattleCry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, new Vector2(npc.Center.X, npc.position.Y - 20), default);

            if (battleCryBoost > 0)
            {
                Texture2D texture = ModContent.Request<Texture2D>("ApacchiisClassesMod2/Draw/BattleCry", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        npc.position.X - Main.screenPosition.X + npc.width * 0.5f,
                        npc.position.Y - Main.screenPosition.Y - npc.height * .5f - 24
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    0,
                    texture.Size() * 0.5f,
                    npc.scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}