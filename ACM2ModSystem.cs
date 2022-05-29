using System.IO;
using System;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ApacchiisClassesMod2.UI;
using ApacchiisClassesMod2.UI.HUD;
using ApacchiisClassesMod2.UI.Other;
using ApacchiisClassesMod2.UI.Specializations;
using Terraria.UI;
using System.Collections.Generic;

namespace ApacchiisClassesMod2
{
    public class ACM2ModSystem : ModSystem
    {
        public static ModKeybind ClassAbility1;
        public static ModKeybind ClassAbility2;
        public static ModKeybind ClassAbilityUltimate;
        public static ModKeybind Menu;

        
        private GameTime _lastUpdateUiGameTime;

        internal ClassesMenu ClassesMenu;
        internal UserInterface _ClassesMenu;

        internal HUD HUD;
        internal UserInterface _HUD;

        internal RelicsUI RelicsUI;
        internal UserInterface _RelicsUI;

        internal VanguardTalents VanguardTalents;
        internal UserInterface _VanguardTalents;
        internal VanguardSpecs VanguardSpecs;
        internal UserInterface _VanguardSpecs;

        internal BloodMageTalents BloodMageTalents;
        internal UserInterface _BloodMageTalents;
        internal BloodMageSpecs BloodMageSpecs;
        internal UserInterface _BloodMageSpecs;

        internal CommanderTalents CommanderTalents;
        internal UserInterface _CommanderTalents;
        internal CommanderSpecs CommanderSepcs;
        internal UserInterface _CommanderSpecs;

        internal ScoutTalents ScoutTalents;
        internal UserInterface _ScoutTalents;
        internal ScoutSpecs ScoutSpecs;
        internal UserInterface _ScoutSpecs;

        internal SoulmancerTalents SoulmancerTalents;
        internal UserInterface _SoulmancerTalents;
        internal SoulmancerSpecs SoulmancerSpecs;
        internal UserInterface _SoulmancerSpecs;


        public override void Load()
        {

            ClassAbility1 = KeybindLoader.RegisterKeybind(Mod, "Class Ability: 1", "Q");
            ClassAbility2 = KeybindLoader.RegisterKeybind(Mod, "Class Ability: 2", "C");
            ClassAbilityUltimate = KeybindLoader.RegisterKeybind(Mod, "Class Ability: Ultimate", "V");
            Menu = KeybindLoader.RegisterKeybind(Mod, "Menu", "N");

            if (!Main.dedServ)
            {
                ClassesMenu = new ClassesMenu();
                _ClassesMenu = new UserInterface();

                HUD = new HUD();
                _HUD = new UserInterface();

                RelicsUI = new RelicsUI();
                _RelicsUI = new UserInterface();

                VanguardTalents = new VanguardTalents();
                _VanguardTalents = new UserInterface();
                VanguardSpecs = new VanguardSpecs();
                _VanguardSpecs = new UserInterface();

                BloodMageTalents = new BloodMageTalents();
                _BloodMageTalents = new UserInterface();
                BloodMageSpecs = new BloodMageSpecs();
                _BloodMageSpecs = new UserInterface();

                CommanderTalents = new CommanderTalents();
                _CommanderTalents = new UserInterface();
                CommanderSepcs = new CommanderSpecs();
                _CommanderSpecs = new UserInterface();

                ScoutTalents = new ScoutTalents();
                _ScoutTalents = new UserInterface();
                ScoutSpecs = new ScoutSpecs();
                _ScoutSpecs = new UserInterface();

                SoulmancerTalents = new SoulmancerTalents();
                _SoulmancerTalents = new UserInterface();
                SoulmancerSpecs = new SoulmancerSpecs();
                _SoulmancerSpecs = new UserInterface();
            }

            base.Load();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;

            if (_ClassesMenu?.CurrentState != null)
                _ClassesMenu.Update(gameTime);

            if (_HUD?.CurrentState != null)
                _HUD.Update(gameTime);

            if (_RelicsUI?.CurrentState != null)
                _RelicsUI.Update(gameTime);

            if (_VanguardTalents?.CurrentState != null)
                _VanguardTalents.Update(gameTime);
            if (_VanguardSpecs?.CurrentState != null)
                _VanguardSpecs.Update(gameTime);

            if (_BloodMageTalents?.CurrentState != null)
                _BloodMageTalents.Update(gameTime);
            if (_BloodMageSpecs?.CurrentState != null)
                _BloodMageSpecs.Update(gameTime);

            if (_CommanderTalents?.CurrentState != null)
                _CommanderTalents.Update(gameTime);
            if (_CommanderSpecs?.CurrentState != null)
                _CommanderSpecs.Update(gameTime);

            if (_ScoutTalents?.CurrentState != null)
                _ScoutTalents.Update(gameTime);
            if (_ScoutSpecs?.CurrentState != null)
                _ScoutSpecs.Update(gameTime);

            if (_SoulmancerTalents?.CurrentState != null)
                _SoulmancerTalents.Update(gameTime);
            if (_SoulmancerSpecs?.CurrentState != null)
                _SoulmancerSpecs.Update(gameTime);

            base.UpdateUI(gameTime);
        }
        
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        
            if (mouseTextIndex != -1)
            {
                #region ClassMenu
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: ClassesMenu",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _ClassesMenu?.CurrentState != null)
                        {
                            _ClassesMenu.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: RelicsUI",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _RelicsUI?.CurrentState != null)
                        {
                            _RelicsUI.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion

                #region HUD
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: HUD",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _HUD?.CurrentState != null)
                        {
                            _HUD.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion

                #region Talents
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _VanguardTalents?.CurrentState != null)
                        {
                            _VanguardTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _BloodMageTalents?.CurrentState != null)
                        {
                            _BloodMageTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _CommanderTalents?.CurrentState != null)
                        {
                            _CommanderTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _ScoutTalents?.CurrentState != null)
                        {
                            _ScoutTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Talents",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _SoulmancerTalents?.CurrentState != null)
                        {
                            _SoulmancerTalents.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
                #endregion

                #region Specializations
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Specs",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _BloodMageSpecs?.CurrentState != null)
                            _BloodMageSpecs.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Specs",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _VanguardSpecs?.CurrentState != null)
                            _VanguardSpecs.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Specs",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _CommanderSpecs?.CurrentState != null)
                            _CommanderSpecs.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ApacchiisClassesMod2: Specs",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && _ScoutSpecs?.CurrentState != null)
                            _ScoutSpecs.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                        return true;
                    },
                       InterfaceScaleType.UI));

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                   "ApacchiisClassesMod2: Specs",
                   delegate
                   {
                       if (_lastUpdateUiGameTime != null && _SoulmancerSpecs?.CurrentState != null)
                           _SoulmancerSpecs.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                       return true;
                   },
                      InterfaceScaleType.UI));
                #endregion
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
}