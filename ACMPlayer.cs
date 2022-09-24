using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ApacchiisClassesMod2
{
	public class ACMPlayer : ModPlayer
	{
        public List<int> relicList = new List<int>()
        {
        };
        bool updatedRelicList = false;

        int resetHUD = 1800;
        public bool levelUpText = false;
        public string P_Name;
        public string P_Desc_1;
        public string P_Desc_2;
        public string P_Desc_3;
        public string P_Desc_4;
        public string P_Effect_1;
        public string P_Effect_2;
        public string P_Effect_3;
        public string P_Effect_4;

        public string A1_Name;
        public string A1_Desc_1;
        public string A1_Desc_2;
        public string A1_Desc_3;
        public string A1_Desc_4;
        public string A1_Effect_1;
        public string A1_Effect_2;
        public string A1_Effect_3;
        public string A1_Effect_4;

        public string A2_Name;
        public string A2_Desc_1;
        public string A2_Desc_2;
        public string A2_Desc_3;
        public string A2_Desc_4;
        public string A2_Effect_1;
        public string A2_Effect_2;
        public string A2_Effect_3;
        public string A2_Effect_4;

        public string Ult_Name;
        public string Ult_Desc_1;
        public string Ult_Desc_2;
        public string Ult_Desc_3;
        public string Ult_Desc_4;
        public string Ult_Effect_1;
        public string Ult_Effect_2;
        public string Ult_Effect_3;
        public string Ult_Effect_4;

        #region Cards
        public int cardsPoints = 0;

        // Basic
        public int card_CarryCount; //dmg
        public float card_CarryValue;
        public int card_HealthyCount; //+hp
        public float card_HealthyValue;
        public int card_PowerfulCount; //+ap
        public float card_PowerfulValue;
        public int card_MendingCount; //+heal
        public float card_MendingValue;
        public int card_TimelessCount; //+cdr
        public float card_TimelessValue;
        public int card_MightyCount; //+ucdr
        public float card_MightyValue;
        public int card_SneakyCount; //+dodge
        public float card_SneakyValue;
        public int card_NimbleHandsCount; //+as
        public float card_NimbleHandsValue;
        public int card_ImpenetrableCount; //+def
        public float card_ImpenetrableValue;
        public int card_MagicalCount; //+mana
        public float card_MagicalValue;
        public int card_DeadeyeCount; //+cdmg
        public float card_DeadeyeValue;


        // Complex
        public int card_FortifiedCount; //+hp +def
        public float card_FortifiedValue_1;
        public float card_FortifiedValue_2;
        public int card_MasterfulCount; //+ap +heal
        public float card_MasterfulValue_1;
        public float card_MasterfulValue_2;
        public int card_SparkOfGeniusCount; //+cdr +ucdr
        public float card_SparkOfGeniusValue_1;
        public float card_SparkOfGeniusValue_2;
        public int card_ProwlerCount; //+ms +jump
        public float card_ProwlerValue_1;
        public float card_ProwlerValue_2;
        public int card_VeteranCount; //+hp +mana
        public float card_VeteranValue_1;
        public float card_VeteranValue_2;
        public int card_HealerCount; //+cdr +heal
        public float card_HealerValue_1;
        public float card_HealerValue_2;
        public int card_MischievousCount; //+cdmg +dodge
        public float card_MischievousValue_1;
        public float card_MischievousValue_2;
        public int card_SeerCount; //+ap +ucdr
        public float card_SeerValue_1;
        public float card_SeerValue_2;
        public int card_FerociousCount; //+dmg +as
        public float card_FerociousValue_1;
        public float card_FerociousValue_2;
        #endregion

        bool gotFreeRelic = false;

        #region Relics
        public bool hasBleedingMoonStone;
        public bool hasAghanims;
        public bool hasUnstableConcoction;
        bool isUnstableConcoctionReady;
        public bool hasNeterihsToken;
        public bool hasSqueaker;
        public bool hasOldShield;
        public bool hasFlanPudding;
        public bool hasArcaneBlade;
        int arcaneBladeTimer = 30;
        public bool hasManaBag;
        public bool hasDarkSign;
        public bool hasEldenRing;
        public bool hasPocketSlime;
        public bool hasChocolateBar;
        int chocolateBarTimer = -1; // Start <0 so it doesnt instanly proc when joining a world
        int chocolateBarStoredHealth;
        public bool hasaccountantRat;
        int accountantRatDamageAccumulated = 0;
        int accountantRatTicks = 0;
        int accountantRatTimer = 30;
        public bool hasChaosAccelerant;
        public bool hasTearsOfLife;
        public bool hasOldBlood;
        public bool hasLuckyLeaf;
        public bool hasScalingWarbanner;
        public bool hasPeanut;
        public bool hasCactusRing;
        public bool hasPorcelainMask;
        #endregion

        public string[] classSpecsText = {"a", "b"};

        string[] accountantRatDeathText =
        {
            "'s accountant rat has forgot how to do basic maths!",
            "'s accountant rat quit their job!",
            "'s accountant rat has overdosed on math!",
            "'s accountant rat had a stroke trying to do math!",
            "'s accountant rat has given up hope!",
            "'s accountant rat has failed at doing their job!",
            "'s accountant rat had a 'bruh moment'!",
            "'s accountant rat has fell asleep!",
            "'s accountant rat got distracted by some cheese!"
        };

        public string equippedClass;
        public bool hasClass = false;
        public int spentSkillPointsGlobal;
        public bool hasRelic = false;
        public int globalLevel = 0;
        public int inBattleTimer = 0;
        int outOfBattleTimer = 480;
        int outOfBattle2 = 3;
        int inBattleTimeMax = 480;
        int outOfBattleTimeMax = 180;
        bool inBattle = false;
        public int ultCharge = 0;
        public int ultChargeMax = 12000;
        public float defenseMult = 1f;
        public float lifeMult = 1f;
        public float manaMult = 1f;
        public int pSecHealthRegen = 0;
        public float classStatMultiplier = 1f;
        int pSecHealthTimer = 60;
        int globalSingleSecondTimer = 60;
        bool ultSound = false;
        bool a1Sound = false;
        bool a2Sound = false;
        public int healthToRegen;
        public int healthToRegenMedium;
        int healthToRegenMediumTimer = 0;
        public int healthToRegenSlow;
        int healthToRegenSlowTimer = 0;
        public int healthToRegenSnail;
        int healthToRegenSnailTimer = 0;

        public bool devTool = false;

        public int ability1Cooldown;
        public int ability1MaxCooldown;
        public int ability2Cooldown;
        public int ability2MaxCooldown;
        public int ability3Cooldown;

        public float cooldownReduction = 1f;
        public float ultCooldownReduction = 1f;
        public float abilityPower = 1f;
        public float healingPower = 1f;
        public float abilityDuration = 1f;
        //public float attackSpeed = 1f;
        public float trueEndurance = 1f;
        public float dodgeChance = 0f;
        public float critDamageMult = 1f;

        #region Player Stats
        public int enemiesKilled;
        public int damageDealt;
        public int timesDied;
        public int highestDPS;
        public int highestCrit;
        bool canAddDeaths = true;
        public int totalDamageTaken;
        #endregion

        #region Vanguard
        public bool hasVanguard = false;
        public int vanguardLevel = 0;
        public int vanguardSkillPoints = 0;
        public int vanguardSpentSkillPoints = 0;

        public string vanguardTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string vanguardTalent_2 = "N";
        public string vanguardTalent_3 = "N";
        public string vanguardTalent_4 = "N";
        public string vanguardTalent_5 = "N";
        public string vanguardTalent_6 = "N";
        public string vanguardTalent_7 = "N";
        public string vanguardTalent_8 = "N";
        public string vanguardTalent_9 = "N";
        public string vanguardTalent_10 = "N";

        public int specVanguard_Defense;
        public float specVanguard_DefenseBase;
        public int specVanguard_ShieldDamageReduction;
        public float specVanguard_ShieldDamageReductionBase;
        public int specVanguard_MeleeDamage;
        public float specVanguard_MeleeDamageBase;
        public int specVanguard_UltCost;
        public float specVanguard_UltCostBase;
        public int specVanguard_SpearDamage;
        public int specVanguard_SpearDamageBase;

        bool vanguardShieldUp = false;
        bool vanguardShieldRegen = false;
        int vanguardShieldRegenTimer = 0;

        public float vanguardPassiveReflectAmount = 1f;

        public float vanguardShieldBaseDamageReduction;
        public float vanguardShieldDamageReduction;
        public int vanguardShieldBaseDuration;
        public int vanguardShieldDuration;
        public int vanguardShieldDurationPerLevel;
        int vanguardShieldCurrentDuration = 0;
        int vanguardDustLocations = 20;
        bool vanguardDustFlag = false;
        int vanguardDustTimer = 5;

        public int vanguardSpearBaseDamage;
        public int vanguardSpearDamage;
        bool vanguardSpearHeal = false;

        public float vanguardUltimateBossExecute;
        public int vanguardSwordBaseDamage;
        public int vanguardSwordDamage;
        #endregion

        #region Blood Mage
        public bool hasBloodMage = false;
        public int bloodMageLevel = 0;
        public int bloodMageSkillPoints = 0;
        public int bloodMageSpentSkillPoints = 0;

        public string bloodMageTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string bloodMageTalent_2 = "N";
        public string bloodMageTalent_3 = "N";
        public string bloodMageTalent_4 = "N";
        public string bloodMageTalent_5 = "N";
        public string bloodMageTalent_6 = "N";
        public string bloodMageTalent_7 = "N";
        public string bloodMageTalent_8 = "N";
        public string bloodMageTalent_9 = "N";
        public string bloodMageTalent_10 = "N";

        public int specBloodMage_MaxHealth;
        public float specBloodMage_MaxHealthBase;
        public int specBloodMage_TransfusionDamage;
        public int specBloodMage_TransfusionDamageBase;
        public int specBloodMage_UltHeal;
        public float specBloodMage_UltHealBase;
        public int specBloodMage_CooldownReduction;
        public float specBloodMage_CooldownReductionBase;
        public float specBloodMage_CooldownReductionUltBase;
        public int specBloodMage_EnchantDamage;
        public float specBloodMage_EnchantDamageBase;

        public int bloodMagePassiveBaseMaxStacks = 12;
        public int bloodMagePassiveMaxStacks;
        public int bloodMagePassiveCurrentStacks;
        public float bloodMageBasePassiveRegen = .0007f;
        public float bloodMagePassiveRegen;

        public float bloodMageSiphonHealMax = .15f;
        public int bloodMageSiphonBaseDamage = 20;
        public int bloodMageSiphonDamage;

        public bool bloodMageBloodEnchantment = false;
        public float bloodMageBaseHealthDrain = .07f;
        //public float bloodMageHealthDrain;
        public float bloodMageBaseDamageGain = .1f;
        public float bloodMageDamageGain;

        public float bloodMageBaseUltRegen = .02f;
        public float bloodMageUltRegen;
        public int bloodMageUltTicks = 8;
        int bloodMageCurUltTicks = 0;
        
        int bloodMageInBattle = 0;
        #endregion

        #region Commander
        public bool hasCommander;
        public int commanderLevel = 0;
        public int commanderSkillPoints = 0;
        public int commanderSpentSkillPoints = 0;

        public float commanderPassiveEndurance = .01f;

        public int commanderBannerRange = 200;
        public int commanderBannerDuration = 60 * 10;
        public float commanderBannerEndurance = .85f;
        public float commanderBannerDamage = 1.1f;
        public int commanderBannerBuffDuration = 0;
        public int commanderBannerPersist = 60;

        public int commanderCryRange = 400;
        public int commanderCryDamage;
        public int commanderCryBaseDamage = 20;
        public int commanderCryDamageLevel = 4;
        public int commanderCryDuration = 300;
        public float commanderCryBonusDamage = .15f;

        public int commanderUltDuration = 60 * 4;
        public bool commanderUltActive = false;

        public string commanderTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string commanderTalent_2 = "N";
        public string commanderTalent_3 = "N";
        public string commanderTalent_4 = "N";
        public string commanderTalent_5 = "N";
        public string commanderTalent_6 = "N";
        public string commanderTalent_7 = "N";
        public string commanderTalent_8 = "N";
        public string commanderTalent_9 = "N";
        public string commanderTalent_10 = "N";

        public int specCommander_BannerRange;
        public int specCommander_BannerRangeBase;
        public int specCommander_BannerDamageReduction;
        public float specCommander_BannerDamageReductionBase;
        public int specCommander_PassiveEndurance;
        public float specCommander_PassiveEnduranceBase;
        public int specCommander_WhipRange;
        public float specCommander_WhipRangeBase;
        public int specCommander_MinionDamage;
        public float specCommander_MinionDamageBase;
        #endregion

        #region Scout
        public bool hasScout = false;
        public int scoutLevel = 0;
        public int scoutSkillPoints = 0;
        public int scoutSpentSkillPoints = 0;

        public bool scoutCanDoubleJump = true;
        public bool scoutOtherJump = false;
        public float scoutPassiveSpeedBonus;

        public int scoutColaDuration;
        public int scoutColaCurDuration;
        public float scoutColaDamageBonus;
        public float scoutColaDamageBonusLevel;

        public int scoutTrapBaseDamage;
        public int scoutTrapDamage;
        public int scoutTrapDamageLevel;
        public int scoutTrapRange;
        public int scoutTrapChargeRate;

        public int scoutUltDuration;
        public int scoutUltInvDuration;
        public int scoutUltCurDuration;
        public int scoutUltInvCurDuration;
        public float scoutUltSpeed;
        public float scoutUltSpeedLevel;
        public float scoutUltJump;

        public string scoutTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string scoutTalent_2 = "N";
        public string scoutTalent_3 = "N";
        public string scoutTalent_4 = "N";
        public string scoutTalent_5 = "N";
        public string scoutTalent_6 = "N";
        public string scoutTalent_7 = "N";
        public string scoutTalent_8 = "N";
        public string scoutTalent_9 = "N";
        public string scoutTalent_10 = "N";

        public int specScout_TrapDamage;
        public float specScout_TrapDamageBase;
        public int specScout_Dodge;
        public float specScout_DodgeBase;
        public int specScout_UltCost;
        public float specScout_UltCostBase;
        public int specScout_CooldownReduction;
        public float specScout_CooldownReductionBase;
        public int specScout_ColaDuration;
        public float specScout_ColaDurationBase;
        #endregion

        #region Soulmancer
        public bool hasSoulmancer;
        public int soulmancerLevel = 0;
        public int soulmancerSkillPoints = 0;
        public int soulmancerSpentSkillPoints = 0;
        
        public float soulmancerSoulRipChance;
        public float soulmancerSoulRipChance_Base;
        public float soulmancerSoulRipChance_PerLevel;
        public int soulmancerSoulRipDamage;
        public int soulmancerSoulRipDamage_Base;
        public int soulmancerSoulRipDamage_PerLevel;

        public int soulmancerConsumeDuration;
        public int soulmancerConsumeDuration_Cur;
        public int soulmancerConsumeDuration_Base;
        public int soulmancerConsumeDuration_PerLevel;
        public float soulmancerConsumeHeal;
        public float soulmancerConsumeHeal_Base;

        public int soulmancerSoulShatterDamage;
        public int soulmancerSoulShatterDamage_Base;
        public int soulmancerShatterDamage_PerLevel;
        public int soulmancerSoulShatterRange;
        public Vector2 soulmancerSoulShatterCastTarget;

        public float soulmancerSacrificeHealthCost;
        public float soulmancerSacrificeHealthCost_Base;
        public int soulmancerSacrificeSoulCount;
        public int soulmancerSacrificeSoulCount_Base;
        public int soulmancerSacrificeSoulCount_Cur;
        public int soulmancerSacrificeTimer;

        public string soulmancerTalent_1 = "N"; // N (None), L (Left), R (Right)
        public string soulmancerTalent_2 = "N";
        public string soulmancerTalent_3 = "N";
        public string soulmancerTalent_4 = "N";
        public string soulmancerTalent_5 = "N";
        public string soulmancerTalent_6 = "N";
        public string soulmancerTalent_7 = "N";
        public string soulmancerTalent_8 = "N";
        public string soulmancerTalent_9 = "N";
        public string soulmancerTalent_10 = "N";

        public int specSoulmancer_SoulRipChance;
        public float specSoulmancer_SoulRipChanceBase;
        public int specSoulmancer_ConsumeDuration;
        public int specSoulmancer_ConsumeDurationBase;
        public int specSoulmancer_CooldownReduction;
        public float specSoulmancer_CooldownReductionBase;
        public int specSoulmancer_AbilityPower;
        public float specSoulmancer_AbilityPowerBase;
        public int specSoulmancer_MagicAttackSpeed;
        public float specSoulmancer_MagicAttackSpeedBase;
        #endregion

        #region Inventor
        public bool hasInventor;
        public int inventorLevel = 0;
        public int inventorSkillPoints = 0;
        public int inventorSpentSkillPoints = 0;

        public int inventorSentryFirerate;
        public int inventorSentryRange;
        public int inventorRangePerLevel;
        public int inventorSentryDamage;
        public int inventorSentryDamageBase;
        public int inventorSentryDamagePerLevel;
        public int inventorCogShotgunProjectiles;
        public int inventorCogShotgunDamage;
        public int inventorCogShotgunDamageBase;
        public int inventorCogShotgunDamagePerLevel;
        public int inventorOverclockCurDuration;
        public int inventorOverclockDuration;
        #endregion

        #region Crusader
        public bool hasCrusader;
        public int crusaderLevel = 0;

        public float crusaderEndurance;
        public float crusaderEnduranceBuff;

        public float crusaderHealing;
        public float crusaderHealingDuration;
        public float crusaderHealingCurDur = 0;
        #endregion

        public override void Initialize()
        {
            bloodMageDefeatedBosses = new List<string>();
            scoutDefeatedBosses = new List<string>();
            vanguardDefeatedBosses = new List<string>();
            commanderDefeatedBosses = new List<string>();
            soulmancerDefeatedBosses = new List<string>();
            crusaderDefeatedBosses = new List<string>();
            inventorDefeatedBosses = new List<string>();
            base.Initialize();
        }

        public override void ResetEffects()
        {
            #region Relics
            hasBleedingMoonStone = false;
            hasAghanims = false;
            hasUnstableConcoction = false;
            hasNeterihsToken = false;
            hasSqueaker = false;
            hasOldShield = false;
            hasFlanPudding = false;
            hasArcaneBlade = false;
            hasManaBag = false;
            hasDarkSign = false;
            hasEldenRing = false;
            hasPocketSlime = false;
            hasChocolateBar = false;
            hasaccountantRat = false;
            hasChaosAccelerant = false;
            hasTearsOfLife = false;
            hasOldBlood = false;
            hasLuckyLeaf = false;
            hasScalingWarbanner = false;
            hasPeanut = false;
            hasCactusRing = false;
            hasPorcelainMask = false;
            #endregion

            #region Player Stats
            abilityPower = 1f;
            healingPower = 1f;
            cooldownReduction = 1f;
            ultCooldownReduction = 1f;
            abilityDuration = 1f;
            //attackSpeed = 1f;
            trueEndurance = 1f;
            defenseMult = 1f;
            lifeMult = 1f;
            manaMult = 1f;
            pSecHealthRegen = 0;
            canAddDeaths = true;
            dodgeChance = 0f;
            critDamageMult = 1f;
            #endregion

            #region Class Menu Text
            P_Name = "";
            P_Desc_1 = "";
            P_Desc_2 = "";
            P_Desc_3 = "";
            P_Desc_4 = "";
            P_Effect_1 = "";
            P_Effect_2 = "";
            P_Effect_3 = "";
            P_Effect_4 = "";
            A1_Name = "";
            A1_Desc_1 = "";
            A1_Desc_2 = "";
            A1_Desc_3 = "";
            A1_Desc_4 = "";
            A1_Effect_1 = "";
            A1_Effect_2 = "";
            A1_Effect_3 = "";
            A1_Effect_4 = "";
            A2_Name = "";
            A2_Desc_1 = "";
            A2_Desc_2 = "";
            A2_Desc_3 = "";
            A2_Desc_4 = "";
            A2_Effect_1 = "";
            A2_Effect_2 = "";
            A2_Effect_3 = "";
            A2_Effect_4 = "";
            Ult_Name = "";
            Ult_Desc_1 = "";
            Ult_Desc_2 = "";
            Ult_Desc_3 = "";
            Ult_Desc_4 = "";
            Ult_Effect_1 = "";
            Ult_Effect_2 = "";
            Ult_Effect_3 = "";
            Ult_Effect_4 = "";
            #endregion

            #region Cards
            // Basic
            card_CarryValue = .0024f; //dmg
            card_HealthyValue = .002f; //hp
            card_PowerfulValue = .016f; //ap
            card_MendingValue = .011f; //heal
            card_TimelessValue = .0075f; //cdr
            card_MightyValue = .0066f; //ucdr
            card_SneakyValue = .0035f; //dodge
            card_NimbleHandsValue = .004f; //as
            card_ImpenetrableValue = .0033f; //def
            card_MagicalValue = .0034f; //mana
            card_DeadeyeValue = .0042f; //cdmg

            // Complex
            card_FortifiedValue_1 = .0016f; //hp
            card_FortifiedValue_2 = .0015f; //def
            card_MasterfulValue_1 = .01f; //ap
            card_MasterfulValue_2 = .008f; //heal
            card_SparkOfGeniusValue_1 = .0055f; //cdr
            card_SparkOfGeniusValue_2 = .005f; //ucdr
            card_ProwlerValue_1 = .0015f; //dmg
            card_ProwlerValue_2 = .0032f; //cdmg
            card_VeteranValue_1 = .0015f; //hp
            card_VeteranValue_2 = .0024f; //mana
            card_HealerValue_1 = .0065f; //heal
            card_HealerValue_2 = .0035f; //cdr
            card_MischievousValue_1 = .0026f; //cdmg
            card_MischievousValue_2 = .0025f; //dodge
            card_SeerValue_1 = .0085f; //ap
            card_SeerValue_2 = .004f; //ucdr
            card_FerociousValue_1 = .0013f; //dmg
            card_FerociousValue_2 = .003f; //as
            #endregion

            hasClass = false;
            hasRelic = false;
            equippedClass = "";

            devTool = false;

            #region Vanguard
            hasVanguard = false;
            ultChargeMax = 12000;
            ability1MaxCooldown = 1;
            ability2MaxCooldown = 1;
            vanguardShieldRegen = false;
            vanguardSpearHeal = false;
            vanguardPassiveReflectAmount = .75f;
            vanguardSpearBaseDamage = 22;
            vanguardSwordBaseDamage = 24;
            vanguardShieldBaseDuration = 480; //8s
            vanguardShieldBaseDamageReduction = .16f;
            vanguardUltimateBossExecute = .05f;
            vanguardShieldDamageReduction = vanguardShieldBaseDamageReduction;

            specVanguard_DefenseBase = .0025f;
            specVanguard_MeleeDamageBase = .002f;
            specVanguard_ShieldDamageReductionBase = .005f;
            specVanguard_SpearDamageBase = 8;
            specVanguard_UltCostBase = .003f;
            #endregion

            #region Blood Mage
            if (!hasBloodMage)
                bloodMageBloodEnchantment = false;
            hasBloodMage = false;
            //bloodMageHealthDrain = bloodMageBaseHealthDrain;
            bloodMageBaseHealthDrain = .08f;
            bloodMagePassiveBaseMaxStacks = 12;
            bloodMagePassiveMaxStacks = bloodMagePassiveBaseMaxStacks + 2 * bloodMageLevel;
            bloodMageBaseDamageGain = .01f;
            bloodMageBasePassiveRegen = .0007f;
            bloodMageBaseUltRegen = .02f;
            bloodMageSiphonBaseDamage = 20;
            bloodMageSiphonHealMax = .15f;
            bloodMagePassiveRegen = bloodMageBasePassiveRegen;
            bloodMageDamageGain = .1f;
            bloodMageUltTicks = 8;
            bloodMageUltRegen = bloodMageBaseUltRegen;

            specBloodMage_TransfusionDamageBase = 6;
            specBloodMage_MaxHealthBase = .0035f;
            specBloodMage_CooldownReductionBase = .005f;
            specBloodMage_CooldownReductionUltBase = .002f;
            specBloodMage_EnchantDamageBase = .005f;
            specBloodMage_UltHealBase = .001f;
            #endregion

            #region Commander
            hasCommander = false;
            commanderUltActive = false;
            commanderUltDuration = 60 * 4;
            commanderBannerEndurance = .85f;
            commanderBannerDamage = 1.1f;
            commanderBannerRange = 200;
            commanderBannerDuration = 60 * 10;
            commanderBannerPersist = 60;
            commanderCryRange = 325;
            commanderCryDamage = commanderCryBaseDamage;
            commanderCryBonusDamage = .15f;
            commanderCryDuration = 60 * 4;
            commanderPassiveEndurance = .01f;

            specCommander_BannerRangeBase = 8;
            specCommander_BannerDamageReductionBase = .01f;
            specCommander_PassiveEnduranceBase = .001f;
            specCommander_WhipRangeBase = .02f;
            specCommander_MinionDamageBase = .004f;
            #endregion

            #region Scout
            hasScout = false;
            scoutPassiveSpeedBonus = .15f;
            scoutOtherJump = false;
            scoutColaDuration = 60 * 4;
            scoutColaDamageBonus = 1.25f;
            scoutColaDamageBonusLevel = .015f;
            scoutTrapBaseDamage = 20;
            scoutTrapDamageLevel = 7;
            scoutTrapDamage = scoutTrapBaseDamage;
            scoutTrapRange = 125;
            scoutTrapChargeRate = 4;
            scoutUltDuration = 12 * 60;
            scoutUltInvDuration = 3 * 60;
            scoutUltSpeed = .5f;
            scoutUltSpeedLevel = .08f;
            scoutUltJump = 2f;

            specScout_ColaDurationBase = 9; //.15s
            specScout_CooldownReductionBase = .01f;
            specScout_DodgeBase = .0015f;
            specScout_TrapDamageBase = 14;
            specScout_UltCostBase = .012f;
            #endregion

            #region Soulmancer
            hasSoulmancer = false;

            soulmancerSoulRipChance_Base = .2f;
            soulmancerSoulRipChance = 0;
            soulmancerSoulRipChance_PerLevel = .01f;
            soulmancerSoulRipDamage_Base = 14;
            soulmancerSoulRipDamage = 0;
            soulmancerSoulRipDamage_PerLevel = 4;

            soulmancerConsumeDuration_Base = 60 * 5;
            soulmancerConsumeDuration = 0;
            soulmancerConsumeDuration_PerLevel = 8;
            soulmancerConsumeHeal_Base = .0065f;
            soulmancerConsumeHeal = 0;

            soulmancerSoulShatterDamage_Base = 25;
            soulmancerSoulShatterDamage = 0;
            soulmancerShatterDamage_PerLevel = 10;
            soulmancerSoulShatterRange = 380;
            soulmancerSoulShatterCastTarget = Player.Center;

            soulmancerSacrificeHealthCost_Base = .0075f;
            soulmancerSacrificeHealthCost = 0;
            soulmancerSacrificeSoulCount_Base = 20;
            soulmancerSacrificeSoulCount = soulmancerSacrificeSoulCount_Base;

            specSoulmancer_AbilityPowerBase = .0125f;
            specSoulmancer_ConsumeDurationBase = 6;
            specSoulmancer_CooldownReductionBase = .01f;
            specSoulmancer_MagicAttackSpeedBase = .0035f;
            specSoulmancer_SoulRipChanceBase = .01f;
            #endregion

            #region Inventor
            hasInventor = false;

            inventorSentryFirerate = 60;
            inventorSentryRange = 360;
            inventorRangePerLevel = 10;
            inventorSentryDamage = 5 + (int)(Player.HeldItem.damage * .2f);
            inventorSentryDamagePerLevel = 2;

            inventorCogShotgunProjectiles = 5;
            inventorCogShotgunDamage = 0;
            inventorCogShotgunDamageBase = 5;
            inventorCogShotgunDamagePerLevel = 3;

            inventorOverclockDuration = 60 * 5;
            #endregion
            #region Crusader
            hasCrusader = false;

            crusaderEndurance = .03f;
            crusaderEnduranceBuff = .04f;

            crusaderHealing = .01f;
            crusaderHealingDuration = 900;
            #endregion

            base.ResetEffects();
        }

        #region Level Lists
        public List<string> defeatedBosses = new List<string>()
        {
        };
        public List<string> vanguardDefeatedBosses = new List<string>()
        {
        };
        public List<string> bloodMageDefeatedBosses = new List<string>()
        {
        };
        public List<string> commanderDefeatedBosses = new List<string>()
        {
        };
        public List<string> scoutDefeatedBosses = new List<string>()
        {
        };
        public List<string> soulmancerDefeatedBosses = new List<string>()
        {
        };
        public List<string> inventorDefeatedBosses = new List<string>()
        {
        };
        public List<string> crusaderDefeatedBosses = new List<string>()
        {
        };
        #endregion

        #region MP Player Syncing
        public override void clientClone(ModPlayer clientClone)
        {
            ACMPlayer clone = clientClone as ACMPlayer;

            clone.vanguardSkillPoints = vanguardSkillPoints;
            clone.bloodMageSkillPoints = bloodMageSkillPoints;
            clone.commanderSkillPoints = commanderSkillPoints;
            clone.scoutSkillPoints = scoutSkillPoints;
            clone.soulmancerSkillPoints = soulmancerSkillPoints;

            base.clientClone(clientClone);
        }
        // /\ & \/
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            ACMPlayer clone = clientPlayer as ACMPlayer;

            if (clone.vanguardSkillPoints != vanguardSkillPoints ||
                clone.bloodMageSkillPoints != bloodMageSkillPoints ||
                clone.commanderSkillPoints != commanderSkillPoints ||
                clone.scoutSkillPoints != scoutSkillPoints ||
                clone.soulmancerSkillPoints != soulmancerSkillPoints)
            {        
                var packet = Mod.GetPacket();
                packet.Write((byte)ACM2.ACMHandlePacketMessage.SyncTalentPoints);
                packet.Write((byte)Player.whoAmI);
                packet.Write(equippedClass);
                packet.Send();
            }
        }

        //public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        //{
        //    string vanguardBosses = string.Join("\n", commanderDefeatedBosses);
        //
        //    ModPacket packet = Mod.GetPacket();
        //    packet.Write((byte)ACM2.ACMHandlePacketMessage.PlayerSyncPlayer);
        //    packet.Write((byte)Player.whoAmI);
        //    packet.Write(vanguardDefeatedBosses.Count);
        //    packet.Write(vanguardBosses);
        //    packet.Send(toWho, fromWho);
        //    base.SyncPlayer(toWho, fromWho, newPlayer);
        //}
        #endregion


        // Old attack speed, unused since tmodloader added their own player.GetAttackSpeed()
        //public override float UseTimeMultiplier(Item item)
        //{
        //    if(Player.HeldItem.pick > 0 || Player.HeldItem.axe > 0 || Player.HeldItem.hammer > 0)
        //        return 1f;
        //    else
        //        return 1f - (attackSpeed - 1f);
        //}

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            //mediumCoreDeath = false;
            
            return new[] {
            new Item(ItemType<Items.WhiteCloth>()),
            new Item(ItemType<Items.ClassBook>()),
            };
        }

        public override void UpdateDead()
        {
            ultCharge = 0;
            inBattleTimer = 0;
            if (canAddDeaths)
            {
                timesDied++;
                canAddDeaths = false;
            }

            healthToRegen = 0;
            healthToRegenMedium = 0;
            healthToRegenSlow = 0;
            healthToRegenSnail = 0;

            bloodMagePassiveCurrentStacks = 0;
            bloodMageBloodEnchantment = false;
            bloodMageCurUltTicks = 0;

            accountantRatDamageAccumulated = 0;
            accountantRatTicks = 0;
            base.UpdateDead();
        }

        //public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        //{
        //    healValue *= 2;
        //    base.GetHealLife(item, quickHeal, ref healValue);
        //}

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            //Dodge
            if (Main.rand.NextFloat() < dodgeChance)
                Player.NinjaDodge();

            damage = (int)(damage * trueEndurance);

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerEndurance);

            // This stays last in the bottom
            if(hasaccountantRat)
            {
                damage = (int)(damage * 1.22f);
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 15, Player.width, Player.height), Color.IndianRed, "" + damage);
                accountantRatDamageAccumulated += damage;
                accountantRatDamageAccumulated -= 1;
                accountantRatTicks = 10;
                damage = 1;
            }

            totalDamageTaken += damage;
            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            //Dodge
            if (Main.rand.NextFloat() < dodgeChance)
                Player.NinjaDodge();

            damage = (int)(damage * trueEndurance);

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerEndurance);

            if (hasOldShield)
                damage = (int)(damage * .86f);

            // This stays last in the bottom
            if (hasaccountantRat)
            {
                damage = (int)(damage * 1.22f);
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 15, Player.width, Player.height), Color.IndianRed, "" + damage);
                accountantRatDamageAccumulated += damage;
                accountantRatDamageAccumulated -= 1;
                accountantRatTicks = 10;
                damage = 1;
            }

            totalDamageTaken += damage;
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            InBattle();
            if (ultCharge < ultChargeMax && !hasCactusRing)
                ultCharge = (int)(ultCharge * .95f);

            int hitDir;
            if (npc.position.X < Player.position.X)
                hitDir = -1;
            else
                hitDir = 1;

            if (hasVanguard && !npc.dontTakeDamage)
                Player.ApplyDamageToNPC(npc, (int)(Player.statDefense * vanguardPassiveReflectAmount), 0, hitDir, false);

            if (hasEldenRing)
                Player.immuneTime -= 15;

            if (hasNeterihsToken)
                Player.immuneTime += 60;
            if (hasSqueaker)
            {
                Player.immuneTime += 15;
                SoundEngine.PlaySound(SoundID.Critter, Player.Center);
            }

            if (hasChocolateBar)
            {
                chocolateBarTimer = 120;
                chocolateBarStoredHealth = (int)(damage * .08f);
            }

            if (hasFlanPudding)
            {
                int heal = (int)(Player.statLifeMax2 * .04f);
                HealPlayer(1, 1, heal);
            }

            base.OnHitByNPC(npc, damage, crit);
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            InBattle();
            if (ultCharge < ultChargeMax && !hasCactusRing)
                ultCharge = (int)(ultCharge * .95f);

            if (hasEldenRing)
                Player.immuneTime -= 15;

            if (hasNeterihsToken)
                Player.immuneTime += 60;
            if (hasSqueaker)
            {
                Player.immuneTime += 15;
                SoundEngine.PlaySound(SoundID.Critter, Player.Center);
            }

            if (hasChocolateBar)
            {
                chocolateBarTimer = 120;
                chocolateBarStoredHealth = (int)(damage * .08f);
            }

            if (hasFlanPudding)
            {
                int heal = (int)(Player.statLifeMax2 * .04f);
                HealPlayer(1, 1, heal);
            }

            base.OnHitByProjectile(proj, damage, crit);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
                InBattle();

            if (Player.name == "Modding Man" || devTool)
                InBattle();

            if (target.life <= 0)
                enemiesKilled++;

            int damageDealtFinal;
            if (!target.friendly && target.type != NPCID.TargetDummy && !target.SpawnedFromStatue)
            {
                damageDealtFinal = damage;
                if (damageDealtFinal > target.life)
                    damageDealtFinal = target.life;
                damageDealt += damageDealtFinal;

                int dpsDealt = Player.getDPS();
                if (dpsDealt > highestDPS)
                    highestDPS = dpsDealt;

                int critDealt = 0;
                if (crit)
                    critDealt = damage;
                if (critDealt > highestCrit)
                    highestCrit = critDealt;
            }

            if (hasSoulmancer && Player.HeldItem.DamageType == DamageClass.Magic)
            {
                if(Main.rand.NextFloat() < soulmancerSoulRipChance)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                    speed.Normalize();
                    speed *= 15f;
                    Projectile.NewProjectile(default, target.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragment>(), (int)(soulmancerSoulRipDamage * abilityPower), 0, Player.whoAmI);
                }
            }

            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
                InBattle();

            if (Player.name == "Modding Man" || devTool)
                InBattle();

            if (target.life <= 0)
                enemiesKilled++;

            int damageDealtFinal;
            if (!target.friendly && target.type != NPCID.TargetDummy && !target.SpawnedFromStatue)
            {
                damageDealtFinal = damage;
                if (damageDealtFinal > target.life)
                    damageDealtFinal = target.life;
                damageDealt += damageDealtFinal;

                int dpsDealt = Player.getDPS();
                if (dpsDealt > highestDPS)
                    highestDPS = dpsDealt;

                int critDealt = 0;
                if (crit)
                    critDealt = damage;
                if (critDealt > highestCrit)
                    highestCrit = critDealt;
            }

            if (proj.type == ProjectileType<Projectiles.BloodMage.Transfusion>() && hasAghanims)
            {
                int heal = (int)(damage * .08f * healingPower);
                healthToRegen += heal;
                Player.HealEffect(heal);

            }

            if (hasSoulmancer && target.type != NPCID.TargetDummy)
            {
                if(Player.HeldItem.DamageType == DamageClass.Magic)
                {
                    if (Main.rand.NextFloat() < soulmancerSoulRipChance && proj.type != ProjectileType<Projectiles.Soulmancer.SoulFragment>() && proj.type != ProjectileType<Projectiles.Soulmancer.SoulShatter>())
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                        speed.Normalize();
                        speed *= 15f;
                        Projectile.NewProjectile(default, target.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragment>(), (int)(soulmancerSoulRipDamage * abilityPower), 0, Player.whoAmI);
                    }
                }
                
                if (proj.type == ProjectileType<Projectiles.Soulmancer.SoulFragment>() && soulmancerConsumeDuration_Cur > 0 && Player.statLife < Player.statLifeMax2)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                    speed.Normalize();
                    speed *= 18f;
                    Projectile.NewProjectile(default, target.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragmentAbsorb>(), 0, 0, Player.whoAmI);
                }
            }

            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //Vanguard Execute
            if (target.boss && target.life <= target.lifeMax * vanguardUltimateBossExecute && proj.type == ProjectileType<Projectiles.Vanguard.VanguardUltimate>())
                damage = target.lifeMax * 3;
            if (!target.boss && target.life <= target.lifeMax / 3 && proj.type == ProjectileType<Projectiles.Vanguard.VanguardUltimate>())
                damage = target.lifeMax * 3;

            if (isUnstableConcoctionReady)
            {
                damage *= 4;
                isUnstableConcoctionReady = false;
            }

            if (hasArcaneBlade && proj.DamageType == DamageClass.Melee && Player.statMana < Player.statManaMax2 && arcaneBladeTimer >= 30)
            {
                int manaToRestore = (int)(damage * .08f);
                if (manaToRestore < 1)
                    manaToRestore = 1;
                if (manaToRestore > 20)
                    manaToRestore = 20;
                Player.statMana += manaToRestore;
                Player.ManaEffect(manaToRestore);
                arcaneBladeTimer = 0;
            }

            if (bloodMageBloodEnchantment)
                damage = (int)(damage * (bloodMageDamageGain + 1f));

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerDamage);

            if (commanderUltActive && !crit)
                crit = true;

            if (hasScout && scoutColaCurDuration > 0)
                damage = (int)(damage * scoutColaDamageBonus);

            if (crit) damage = (int)(damage * critDamageMult);
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (isUnstableConcoctionReady)
            {
                damage *= 4;
                isUnstableConcoctionReady = false;
            }

            if (hasArcaneBlade && item.DamageType == DamageClass.Melee && Player.statMana < Player.statManaMax2 && arcaneBladeTimer >= 30)
            {  
                int manaToRestore = (int)(damage * .08f);
                if (manaToRestore < 1)
                    manaToRestore = 1;
                if (manaToRestore > 20)
                    manaToRestore = 20;
                Player.statMana += manaToRestore;
                Player.ManaEffect(manaToRestore);
                arcaneBladeTimer = 0;
            }


            if (bloodMageBloodEnchantment)
                damage = (int)(damage * (bloodMageDamageGain + 1f));

            if (commanderBannerBuffDuration > 0)
                damage = (int)(damage * commanderBannerDamage);

            if (commanderUltActive && !crit)
                crit = true;

            if (hasScout && scoutColaCurDuration > 0)
                    damage = (int)(damage * scoutColaDamageBonus);

            if (crit) damage = (int)(damage * critDamageMult);
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void PreUpdateBuffs()
        {
            if (Player.HeldItem.damage > 1 && Player.HeldItem.useTime > 0)
            {
                float shotsPerSecond = 60 / Player.HeldItem.useTime;
                float dps = shotsPerSecond * Player.HeldItem.damage;
                abilityPower += dps * Configs._ACMConfigServer.Instance.abilityPowerWeaponDPSMult / 100;
            }

            if (Player.HeldItem.type == ItemType<Items.ClassWeapons.SoulBurner>())
                soulmancerSoulRipChance += .25f;

            base.PreUpdateBuffs();
        }

        public override void PreUpdate()
        {
            chocolateBarTimer--;
            inventorOverclockCurDuration--;

            //if (Configs._ACMConfigServer.Instance.startWithRelic && !gotFreeRelic)
            //{
            //    Player.QuickSpawnItem(null, ItemType<Items.Relics.RandomRelic>(), 1);
            //    gotFreeRelic = true;
            //}

            if (levelUpText && Main.netMode != NetmodeID.Server)
            {
                if (hasBloodMage)
                    Main.NewText($"You have leveled up! You have {bloodMageSkillPoints} Skill Points to spend!");
                if (hasCommander)
                    Main.NewText($"You have leveled up! You have {commanderSkillPoints} Skill Points to spend!");
                if (hasScout)
                    Main.NewText($"You have leveled up! You have {scoutSkillPoints} Skill Points to spend!");
                if (hasVanguard)
                    Main.NewText($"You have leveled up! You have {vanguardSkillPoints} Skill Points to spend!");
                if (hasSoulmancer)
                    Main.NewText($"You have leveled up! You have {soulmancerSkillPoints} Skill Points to spend!");
                levelUpText = false;
            }

            if (Player.statLife <= 0 && !Player.dead && bloodMageBloodEnchantment && hasBloodMage)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " ran out of blood for their 'Blood Enchantment' ability!. Silly player!"), 1, 1);
            if (Player.statLife <= 0 && !Player.dead && soulmancerSacrificeSoulCount_Cur > 0 && hasSoulmancer)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " has sacrificed their own soul for the cause!. Now that's dedication!"), 1, 1);

            int selectedRatText = Main.rand.Next(accountantRatDeathText.Length);
            if (Player.statLife <= 0 && accountantRatDamageAccumulated > 0)
                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + accountantRatDeathText[selectedRatText]), 1, 1);

            arcaneBladeTimer++;

            // Old %hp based DoT
            //if (testRelicDamageAccumulated > 0 && testRelicTimer % 2 == 0)
            //{
            //    int damageToTake = (int)(Player.statLifeMax2 * .005f);
            //    if(damageToTake < 1)
            //        damageToTake = 1;
            //
            //    Player.statLife -= damageToTake;
            //    testRelicDamageAccumulated -= damageToTake;
            //    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 140, Player.width, Player.height), Color.IndianRed, "" + testRelicDamageAccumulated);
            //}

            accountantRatTimer++;
            if (accountantRatTicks == 0)
                accountantRatDamageAccumulated = 0;
            if(accountantRatTimer % 30 == 0 && accountantRatTicks > 0)
               if (accountantRatDamageAccumulated > 0)
               {
                    Player.statLife -= accountantRatDamageAccumulated / 10;
                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 15, Player.width, Player.height), Color.IndianRed, "" + accountantRatDamageAccumulated / 10);
                    accountantRatTicks--;
               }


            if (globalSingleSecondTimer == 0)
            {
                if (healthToRegen > 0 || healthToRegenMedium > 0 || healthToRegenSlow > 0 || healthToRegenSnail > 0)
                    for (int x = 0; x < 5; x++)
                        Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 1.75f));

                //Text for debugging overtime healing stuff
                //Main.NewText($"{healthToRegen}|{healthToRegenMedium}|{healthToRegenSlow}|{healthToRegenSnail}");
            }

            if(healthToRegen > 0)
            {
                if(Player.statLifeMax2 < 1000)
                {
                    Player.statLife++;
                    healthToRegen--;
                }

                if(Player.statLifeMax2 > 1000 && Player.statLifeMax2 < 2000)
                {
                    Player.statLife += 2;
                    healthToRegen -= 2;
                }

                if (Player.statLifeMax2 > 2000 && Player.statLifeMax2 < 3000)
                {
                    Player.statLife += 3;
                    healthToRegen -= 3;
                }

                if (Player.statLifeMax2 > 3000 && Player.statLifeMax2 < 4000)
                {
                    Player.statLife += 4;
                    healthToRegen -= 4;
                }

                if (Player.statLifeMax2 > 4000 && Player.statLifeMax2 < 5000)
                {
                    Player.statLife += 5;
                    healthToRegen -= 5;
                }

                if (Player.statLifeMax2 > 5000)
                {
                    Player.statLife += 6;
                    healthToRegen -= 6;
                }
            }

            healthToRegenMediumTimer++;
            if(healthToRegenMediumTimer % 4 == 0)
            {
                if(healthToRegenMedium > 0)
                {
                    if (Player.statLifeMax2 < 1000)
                    {
                        Player.statLife++;
                        healthToRegenMedium--;
                    }

                    if (Player.statLifeMax2 > 1000 && Player.statLifeMax2 < 2000)
                    {
                        Player.statLife += 2;
                        healthToRegenMedium -= 2;
                    }

                    if (Player.statLifeMax2 > 2000 && Player.statLifeMax2 < 3000)
                    {
                        Player.statLife += 3;
                        healthToRegenMedium -= 3;
                    }

                    if (Player.statLifeMax2 > 3000 && Player.statLifeMax2 < 4000)
                    {
                        Player.statLife += 4;
                        healthToRegenMedium -= 4;
                    }

                    if (Player.statLifeMax2 > 4000 && Player.statLifeMax2 < 5000)
                    {
                        Player.statLife += 5;
                        healthToRegenMedium -= 5;
                    }

                    if (Player.statLifeMax2 > 5000)
                    {
                        Player.statLife += 6;
                        healthToRegenMedium -= 6;
                    }
                }    
            }

            healthToRegenSlowTimer++;
            if (healthToRegenSlowTimer % 8 == 0)
            {
                if (healthToRegenSlow > 0)
                {
                    if (Player.statLifeMax2 < 1000)
                    {
                        Player.statLife++;
                        healthToRegenSlow--;
                    }

                    if (Player.statLifeMax2 > 1000 && Player.statLifeMax2 < 2000)
                    {
                        Player.statLife += 2;
                        healthToRegenSlow -= 2;
                    }

                    if (Player.statLifeMax2 > 2000 && Player.statLifeMax2 < 3000)
                    {
                        Player.statLife += 3;
                        healthToRegenSlow -= 3;
                    }

                    if (Player.statLifeMax2 > 3000 && Player.statLifeMax2 < 4000)
                    {
                        Player.statLife += 4;
                        healthToRegenSlow -= 4;
                    }

                    if (Player.statLifeMax2 > 4000 && Player.statLifeMax2 < 5000)
                    {
                        Player.statLife += 5;
                        healthToRegenSlow -= 5;
                    }

                    if (Player.statLifeMax2 > 5000)
                    {
                        Player.statLife += 6;
                        healthToRegenSlow -= 6;
                    }
                }
            }

            healthToRegenSnailTimer++;
            if (healthToRegenSnailTimer % 20 == 0)
            {
                if (healthToRegenSnail > 0)
                {
                    if (Player.statLifeMax2 < 1000)
                    {
                        Player.statLife++;
                        healthToRegenSnail--;
                    }

                    if (Player.statLifeMax2 > 1000 && Player.statLifeMax2 < 2000)
                    {
                        Player.statLife += 2;
                        healthToRegenSnail -= 2;
                    }

                    if (Player.statLifeMax2 > 2000 && Player.statLifeMax2 < 3000)
                    {
                        Player.statLife += 3;
                        healthToRegenSnail -= 3;
                    }

                    if (Player.statLifeMax2 > 3000 && Player.statLifeMax2 < 4000)
                    {
                        Player.statLife += 4;
                        healthToRegenSnail -= 4;
                    }

                    if (Player.statLifeMax2 > 4000 && Player.statLifeMax2 < 5000)
                    {
                        Player.statLife += 5;
                        healthToRegenSnail -= 5;
                    }

                    if (Player.statLifeMax2 > 5000)
                    {
                        Player.statLife += 6;
                        healthToRegenSnail -= 6;
                    }
                }
            }

            resetHUD--;
            if (resetHUD <= 0)
                resetHUD = 1800;
            if (globalSingleSecondTimer == 0)
                globalSingleSecondTimer = 60;
            globalSingleSecondTimer--;
            
            if (ability1Cooldown > 0)
                ability1Cooldown--;
            if(ability2Cooldown > 0)
                ability2Cooldown--;

            pSecHealthTimer--;

            outOfBattleTimer--;
            if (outOfBattleTimer <= 0)
                outOfBattle2--;
            if(outOfBattle2 <= 0)
            {
                outOfBattle2 = 3;
                if (ultCharge > 0)
                    ultCharge--;
            }

            if (inBattleTimer > 0)
            {
                inBattleTimer--;

                if (devTool)
                    ultCharge += 10;
                else
                    ultCharge++;

                inBattle = true;
            }
            else
            {
                inBattle = false;
            }

            if (inBattle)
            {
                bloodMageInBattle--;
                if(bloodMageInBattle <= 0)
                {
                    bloodMageInBattle = 60;
                    if(bloodMagePassiveCurrentStacks < bloodMagePassiveMaxStacks)
                        bloodMagePassiveCurrentStacks++;
                    //Main.NewText(bloodMagePassiveCurrentStacks);
                }
            }

            if (vanguardShieldCurrentDuration > 0)
            {
                vanguardShieldUp = true;
                vanguardShieldCurrentDuration--;
            }
            else
            {
                vanguardShieldUp = false;
            }

            if (vanguardShieldUp)
            {
                Vector2 origin = Player.Center;
                origin.X -= 4;
                float radius = 50;

                if (!vanguardDustFlag)
                {
                    vanguardDustLocations = 1;
                    vanguardDustFlag = true;
                }

                vanguardDustTimer--;
                if(vanguardDustTimer == 0 && vanguardDustLocations < 20)
                {
                    vanguardDustLocations++;
                    vanguardDustTimer = 5;
                }
                
                for (int i = 0; i < vanguardDustLocations; i++)
                {
                    Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / vanguardDustLocations * i)) * radius;

                    var dust = Dust.NewDustDirect(position, 1, 1, 6, 0f, 0f, 0, Color.LightYellow, 1f);
                    dust.noGravity = true;
                }

                trueEndurance -= vanguardShieldDamageReduction;

                if(vanguardShieldRegen)
                    vanguardShieldRegenTimer++;
                if(vanguardShieldRegenTimer == 60)
                {
                    vanguardShieldRegenTimer = 0;
                    Player.statLife += (int)(Player.statLifeMax2 * .012f * healingPower);
                    Player.HealEffect((int)(Player.statLifeMax2 * .012f * healingPower));
                    for (int x = 0; x < 3; x++)
                        Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                }

            }
            else
            {
                vanguardDustFlag = false;
                vanguardDustTimer = 5;
                vanguardDustLocations = 1;
            }

            if (vanguardTalent_4 == "R" || vanguardTalent_4 == "B")
                vanguardShieldBaseDuration += 30;
            if (vanguardTalent_8 == "R" || vanguardTalent_8 == "B")
                vanguardShieldBaseDuration += 48;

            if (commanderBannerBuffDuration > 0)
                commanderBannerBuffDuration--;

            if (hasScout)
            {
                if (scoutColaCurDuration > 0)
                    scoutColaCurDuration--;

                if (scoutUltCurDuration > 0)
                    scoutUltCurDuration--;
                    
                if (scoutUltInvCurDuration > 0)
                    scoutUltInvCurDuration--;

                if (scoutColaCurDuration > 0)
                {
                    var dustScout = Dust.NewDustDirect(Player.position, Player.width, Player.height, 63, Player.velocity.X * .5f, Player.velocity.Y * .5f, Scale: 1.5f);
                    dustScout.noGravity = true;
                    dustScout.noLight = true;

                    if(scoutColaCurDuration == 1)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(12f, 12f);
                            var dust = Dust.NewDustPerfect(Player.Center, 63, speed, Scale: 1.5f);
                            dust.noGravity = true;
                        }

                        CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Poof!", true);
                    }
                }
            }

            if (soulmancerConsumeDuration_Cur > 0)
                soulmancerConsumeDuration_Cur--;
            if(soulmancerConsumeDuration_Cur > 0)
            {
                // Circle Dust
                Vector2 origin = Player.Center;
                //origin.X += 8;
                //origin.X -= Player.width / 2;
                float radius = 75;

                int locations = 20;
                for (int i = 0; i < locations; i++)
                {
                    Vector2 position = origin + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / locations * i)) * radius;
                    var dust = Dust.NewDustPerfect(position, 63, Vector2.Zero, 0, Color.Red, 2f);
                    dust.noGravity = true;
                    dust.noLight = true;
                }

                soulmancerSoulRipChance += 5;
            }

            if (soulmancerSacrificeTimer > 0)
                soulmancerSacrificeTimer--;

            if (soulmancerSacrificeTimer == 0 && soulmancerSacrificeSoulCount_Cur > 0)
            {
                soulmancerSacrificeSoulCount_Cur--;
                soulmancerSacrificeTimer = 3;

                Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                speed.Normalize();
                speed *= 15f;
                Projectile.NewProjectile(default, Player.Center, speed, ProjectileType<Projectiles.Soulmancer.SoulFragment>(), (int)(soulmancerSoulRipDamage * abilityPower * 1.5f), 0, Player.whoAmI);

                SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, Player.position);

                int healthCost = (int)(Player.statLifeMax2 * soulmancerSacrificeHealthCost);
                if (healthCost < 1)
                    healthCost = 1;
                Player.statLife -= healthCost;
                Player.HealEffect(-healthCost);
            }

            #region Global Skill Points Spent
            if (equippedClass == "Blood Mage")
                spentSkillPointsGlobal = bloodMageSpentSkillPoints;
            if (equippedClass == "Commander")
                spentSkillPointsGlobal = commanderSpentSkillPoints;
            if (equippedClass == "Scout")
                spentSkillPointsGlobal = scoutSpentSkillPoints;
            if (equippedClass == "Vanguard")
                spentSkillPointsGlobal = vanguardSpentSkillPoints;
            if (equippedClass == "Soulmancer")
                spentSkillPointsGlobal = soulmancerSpentSkillPoints;
            #endregion
            base.PreUpdate();
        }

        public override void PostUpdateBuffs()
        {
            base.PostUpdateBuffs();
        }

        public override void UpdateEquips()
        {
            base.UpdateEquips();
        }

        public override void PostUpdateEquips()
        {
            if (hasScalingWarbanner)
            {
                classStatMultiplier += .1f;
                if (Main.hardMode)
                    classStatMultiplier += .08f;
            }
            //globalLevel = defeatedBosses.Count;   

            #region Card Stats
            lifeMult += card_HealthyCount * card_HealthyValue;
            trueEndurance += card_ImpenetrableCount * card_ImpenetrableValue;
            Player.GetAttackSpeed(DamageClass.Generic) += card_NimbleHandsCount * card_NimbleHandsValue;
            Player.GetDamage(DamageClass.Generic) += card_CarryCount * card_CarryValue;
            critDamageMult += card_DeadeyeCount * card_DeadeyeValue;
            dodgeChance += card_SneakyCount * card_SneakyValue;
            abilityPower += card_PowerfulCount * card_PowerfulValue;
            cooldownReduction -= card_TimelessCount * card_TimelessValue;
            ultCooldownReduction -= card_MightyCount * card_MightyValue;
            healingPower += card_MendingCount * card_MendingValue;
            manaMult += card_MagicalCount * card_MagicalValue;

            lifeMult += card_VeteranCount * card_VeteranValue_1;
            manaMult += card_VeteranCount * card_VeteranValue_2;

            lifeMult += card_FortifiedCount * card_FortifiedValue_1;
            trueEndurance += card_FortifiedCount * card_FortifiedValue_2;

            Player.GetDamage(DamageClass.Generic) += card_FerociousCount * card_FerociousValue_1;
            Player.GetAttackSpeed(DamageClass.Generic) += card_FerociousCount * card_FerociousValue_2;

            critDamageMult += card_ProwlerCount * card_ProwlerValue_2;
            Player.GetDamage(DamageClass.Generic) += card_ProwlerCount * card_ProwlerValue_1;

            critDamageMult += card_MischievousCount * card_MischievousValue_1;
            dodgeChance += card_MischievousCount * card_MischievousValue_2;

            abilityPower += card_MasterfulCount * card_MasterfulValue_1;
            healingPower += card_MasterfulCount + card_MasterfulValue_2;

            abilityPower += card_SeerCount * card_SeerValue_1;
            ultCooldownReduction -= card_SeerCount * card_SeerValue_2;

            healingPower += card_HealerCount * card_HealerValue_1;
            cooldownReduction -= card_HealerCount * card_HealerValue_2;

            cooldownReduction -= card_SparkOfGeniusCount * card_SparkOfGeniusValue_1;
            ultCooldownReduction -= card_SparkOfGeniusCount * card_SparkOfGeniusValue_2;
            #endregion

            #region Vanguard
            vanguardLevel = vanguardDefeatedBosses.Count;
            if (vanguardLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                vanguardLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasVanguard)
            {
                #region Vanguard Specs
                vanguardSpearBaseDamage += specVanguard_SpearDamage * specVanguard_SpearDamageBase;
                vanguardShieldBaseDamageReduction += specVanguard_ShieldDamageReduction * specVanguard_ShieldDamageReductionBase;
                defenseMult += specVanguard_Defense * specVanguard_DefenseBase;
                Player.GetDamage(DamageClass.Melee) += specVanguard_MeleeDamage * specVanguard_MeleeDamageBase;
                ultCooldownReduction -= specVanguard_UltCost * specVanguard_UltCostBase;
                #endregion

                #region Vanguard Talents
                if (vanguardTalent_1 == "L" || vanguardTalent_1 == "B")
                    Player.statDefense += 2;
                else if (vanguardTalent_1 == "R" || vanguardTalent_1 == "B")
                    cooldownReduction -= .06f;

                if (vanguardTalent_2 == "L" || vanguardTalent_2 == "B")
                    lifeMult += .015f;
                else if (vanguardTalent_2 == "R" || vanguardTalent_2 == "B")
                    Player.GetDamage(DamageClass.Melee) += .01f;

                if (vanguardTalent_3 == "L" || vanguardTalent_3 == "B")
                    vanguardShieldDamageReduction += .05f;
                else if (vanguardTalent_3 == "R" || vanguardTalent_3 == "B")
                    vanguardSpearBaseDamage += 18;

                if (vanguardTalent_4 == "L" || vanguardTalent_4 == "B")
                    Player.statDefense += 4;

                if (vanguardTalent_5 == "L" || vanguardTalent_5 == "B")
                    lifeMult += .02f;
                else if (vanguardTalent_5 == "R" || vanguardTalent_5 == "B")
                    ultCooldownReduction -= .08f;

                if (vanguardTalent_6 == "R" || vanguardTalent_6 == "B")
                    vanguardPassiveReflectAmount += .35f;

                if (vanguardTalent_7 == "L" || vanguardTalent_7 == "B")
                    Player.GetCritChance(DamageClass.Melee) += 2;
                else if (vanguardTalent_7 == "R" || vanguardTalent_7 == "B")
                    vanguardSpearHeal = true;

                if (vanguardTalent_8 == "L" || vanguardTalent_8 == "B")
                    vanguardSwordBaseDamage += 28;

                if (vanguardTalent_9 == "L" || vanguardTalent_9 == "B")
                    vanguardShieldRegen = true;
                else if (vanguardTalent_9 == "R" || vanguardTalent_9 == "B")
                    cooldownReduction -= .17f;

                if (vanguardTalent_10 == "L" || vanguardTalent_10 == "B")
                    lifeMult += .08f;
                else if (vanguardTalent_10 == "R" || vanguardTalent_10 == "B")
                    vanguardUltimateBossExecute += .04f;
                #endregion

                vanguardSpearDamage = vanguardSpearBaseDamage + 10 * vanguardLevel;
                vanguardShieldDuration = vanguardShieldBaseDuration + 15 * vanguardLevel;
                vanguardSwordDamage = vanguardSwordBaseDamage + 9 * vanguardLevel;
            }
            #endregion

            #region Blood Mage
            bloodMageLevel = bloodMageDefeatedBosses.Count;
            if (bloodMageLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                bloodMageLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasBloodMage)
            {
                #region Blood Mage Specs
                lifeMult += specBloodMage_MaxHealth * specBloodMage_MaxHealthBase;
                bloodMageSiphonBaseDamage += specBloodMage_TransfusionDamage * specBloodMage_TransfusionDamageBase;
                bloodMageDamageGain += specBloodMage_EnchantDamage * specBloodMage_EnchantDamageBase;
                cooldownReduction -= specBloodMage_CooldownReduction * specBloodMage_CooldownReductionBase;
                ultCooldownReduction -= specBloodMage_CooldownReduction * specBloodMage_CooldownReductionUltBase;
                bloodMageUltRegen += specBloodMage_UltHeal * specBloodMage_UltHealBase;
                #endregion

                #region Blood Mage Talents
                if (bloodMageTalent_1 == "R" || bloodMageTalent_1 == "B")
                    bloodMageSiphonHealMax += .05f;
                if (bloodMageTalent_1 == "L" || bloodMageTalent_1 == "B")
                    cooldownReduction -= .05f;

                if (bloodMageTalent_2 == "L" || bloodMageTalent_2 == "B")
                    bloodMageBaseDamageGain += .03f;
                else if (bloodMageTalent_2 == "R" || bloodMageTalent_2 == "B") 
                    bloodMageBaseHealthDrain -= .02f;

                if (bloodMageTalent_3 == "L" || bloodMageTalent_3 == "B")
                    lifeMult += .05f;
                else if (bloodMageTalent_3 == "R" || bloodMageTalent_3 == "B")
                    cooldownReduction -= .1f;

                if (bloodMageTalent_4 == "L" || bloodMageTalent_4 == "B")
                    ultCooldownReduction -= .12f;
                else if (bloodMageTalent_4 == "R" || bloodMageTalent_4 == "B")
                    bloodMagePassiveRegen += .0002f;

                if (bloodMageTalent_5 == "L" || bloodMageTalent_5 == "B")
                    bloodMageSiphonBaseDamage += 35;
                else if (bloodMageTalent_5 == "R" || bloodMageTalent_5 == "B")
                    ultCooldownReduction -= .05f;

                if (bloodMageTalent_6 == "L" || bloodMageTalent_6 == "B")
                    manaMult += .18f;
                else if (bloodMageTalent_6 == "R" || bloodMageTalent_6 == "B")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (bloodMageTalent_7 == "L" || bloodMageTalent_7 == "B")
                    Player.manaCost -= .15f;
                else if (bloodMageTalent_7 == "R" || bloodMageTalent_7 == "B")
                    bloodMageBaseDamageGain += .04f;

                if (bloodMageTalent_8 == "L" || bloodMageTalent_8 == "B")
                    bloodMageBaseHealthDrain -= .01f;
                else if (bloodMageTalent_8 == "R" || bloodMageTalent_8 == "B")
                    ultCooldownReduction -= .05f;

                if (bloodMageTalent_9 == "L" || bloodMageTalent_9 == "B")
                    Player.GetDamage(DamageClass.Magic) += bloodMagePassiveCurrentStacks * .0008f;
                else if (bloodMageTalent_9 == "R" || bloodMageTalent_9 == "B")
                    bloodMageBaseUltRegen += .005f;

                if (bloodMageTalent_10 == "L" || bloodMageTalent_10 == "B")
                    cooldownReduction -= .12f;
                else if (bloodMageTalent_10 == "R" || bloodMageTalent_10 == "B")
                    bloodMageUltTicks += 3;
                #endregion

                pSecHealthRegen += (int)(bloodMagePassiveCurrentStacks * bloodMagePassiveRegen);
                bloodMageUltRegen = bloodMageBaseUltRegen + .001f * bloodMageLevel;
                bloodMageSiphonDamage = bloodMageSiphonBaseDamage + 6 * bloodMageLevel;
                bloodMageDamageGain += bloodMageBaseDamageGain * bloodMageLevel;
                //bloodMagePassiveMaxStacks += 2 * bloodMageLevel;
                //bloodMageHealthDrain += bloodMageBaseHealthDrain;

                if (bloodMageBloodEnchantment)
                {
                    if (globalSingleSecondTimer == 0)
                        Player.statLife -= (int)(Player.statLifeMax2 * bloodMageBaseHealthDrain);
                }
            }
            #endregion

            #region Commander
            commanderLevel = commanderDefeatedBosses.Count;
            if (commanderLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                commanderLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasCommander)
            {
                #region Commander Specs
                commanderBannerEndurance += specCommander_BannerDamageReduction * specCommander_BannerDamageReductionBase;
                commanderBannerRange += specCommander_BannerRange * specCommander_BannerRangeBase;
                Player.GetDamage(DamageClass.Summon) += specCommander_MinionDamage * specCommander_MinionDamageBase;
                Player.whipRangeMultiplier += specCommander_WhipRange * specCommander_WhipRangeBase;
                commanderPassiveEndurance += specCommander_PassiveEndurance * specCommander_PassiveEnduranceBase;
                #endregion

                #region Commander Talents
                if (commanderTalent_1 == "L" || commanderTalent_1 == "B")
                    commanderBannerDamage += .03f;
                else if (commanderTalent_1 == "R" || commanderTalent_1 == "B")
                    Player.whipRangeMultiplier += .04f;

                if (commanderTalent_2 == "L" || commanderTalent_2 == "B")
                    commanderBannerRange = (int)(commanderBannerRange * 1.28f);
                else if (commanderTalent_2 == "R" || commanderTalent_2 == "B")
                    cooldownReduction -= .15f;

                if (commanderTalent_3 == "L" || commanderTalent_3 == "B")
                    commanderPassiveEndurance += .002f;
                else if (commanderTalent_3 == "R" || commanderTalent_3 == "B")
                    ultCooldownReduction -= .1f;

                if (commanderTalent_4 == "L" || commanderTalent_4 == "B")
                    commanderBannerDamage += .04f;
                else if (commanderTalent_4 == "R" || commanderTalent_4 == "B")
                    commanderCryRange = (int)(commanderCryRange * 1.25f);

                if (commanderTalent_5 == "L" || commanderTalent_5 == "B")
                    Player.maxMinions += 1;
                else if (commanderTalent_5 == "R" || commanderTalent_5 == "B")
                    commanderBannerEndurance -= .05f;

                if (commanderTalent_6 == "L" || commanderTalent_6 == "B")
                    commanderCryDuration += 120;
                else if (commanderTalent_6 == "R" || commanderTalent_6 == "B")
                    commanderBannerPersist += 120;

                if (commanderTalent_7 == "L" || commanderTalent_7 == "B")
                    commanderUltDuration += 60;
                else if (commanderTalent_7 == "R" || commanderTalent_7 == "B")
                    commanderCryDuration += 60;

                if (commanderTalent_8 == "L" || commanderTalent_8 == "B")
                    commanderCryBonusDamage += .05f;
                else if (commanderTalent_8 == "R" || commanderTalent_8 == "B")
                    Player.whipRangeMultiplier += .08f;

                if (commanderTalent_9 == "L" || commanderTalent_9 == "B")
                    ultCooldownReduction -= .1f;
                else if (commanderTalent_9 == "R" || commanderTalent_9 == "B")
                    commanderPassiveEndurance += .002f;

                if (commanderTalent_10 == "L" || commanderTalent_10 == "B")
                    Player.whipRangeMultiplier += .05f;
                else if (commanderTalent_10 == "R" || commanderTalent_10 == "B")
                    commanderBannerEndurance -= .15f;
                #endregion


            trueEndurance -= Player.maxMinions * commanderPassiveEndurance;
            commanderBannerDuration += 24 * commanderLevel;
            commanderUltDuration += 15 * commanderLevel;
            commanderCryDamage += commanderCryDamageLevel * commanderLevel;
            }
            #endregion

            #region Scout
            scoutLevel = scoutDefeatedBosses.Count;
            if (scoutLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                scoutLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasScout)
            {
                #region Scout Talents
                if (scoutTalent_1 == "L" || scoutTalent_1 == "B")
                    scoutPassiveSpeedBonus += .05f;
                else if (scoutTalent_1 == "R" || scoutTalent_1 == "B")
                    scoutTrapDamage += 25;

                if (scoutTalent_2 == "L" || scoutTalent_2 == "B")
                    Player.GetCritChance(DamageClass.Ranged) += 1;

                if (scoutTalent_3 == "L" || scoutTalent_3 == "B")
                    scoutTrapRange += 25;
                else if (scoutTalent_3 == "R" || scoutTalent_3 == "B")
                    cooldownReduction -= .16f;

                if (scoutTalent_4 == "L" || scoutTalent_4 == "B")
                    scoutColaDamageBonus += .04f;
                else if (scoutTalent_4 == "R" || scoutTalent_4 == "B")
                    scoutTrapChargeRate += 2;

                if (scoutTalent_5 == "L" || scoutTalent_5 == "B")
                    defenseMult += .05f;
                else if (scoutTalent_5 == "R" || scoutTalent_5 == "B")
                    Player.GetCritChance(DamageClass.Ranged) += 1;

                if (scoutTalent_6 == "L" || scoutTalent_6 == "B")
                    scoutColaDuration += 120;
                else if (scoutTalent_6 == "R" || scoutTalent_6 == "B")
                    ability2MaxCooldown -= 3;

                if (scoutTalent_7 == "L" || scoutTalent_7 == "B")
                    scoutPassiveSpeedBonus += .06f;
                else if (scoutTalent_7 == "R" || scoutTalent_7 == "B")
                    scoutColaDamageBonus += .06f;

                if (scoutTalent_8 == "L" || scoutTalent_8 == "B")
                    scoutTrapRange += 20;
                else if (scoutTalent_8 == "R" || scoutTalent_8 == "B")
                    ultCooldownReduction -= .1f;

                if (scoutTalent_9 == "L" || scoutTalent_9 == "B")
                    Player.GetDamage(DamageClass.Ranged) += .03f;
                else if (scoutTalent_9 == "R" || scoutTalent_9 == "B")
                    scoutTrapDamage += 120;

                if (scoutTalent_10 == "L" || scoutTalent_10 == "B")
                    scoutTrapRange = (int)(scoutTrapRange * 1.45f);
                else if (scoutTalent_10 == "R" || scoutTalent_10 == "B")
                    scoutUltInvDuration += 60;
                #endregion

                #region Specs
                scoutColaDuration += (int)(specScout_ColaDuration * specScout_ColaDurationBase);
                cooldownReduction -= specScout_CooldownReduction * specScout_CooldownReductionBase;
                dodgeChance += specScout_Dodge * specScout_DodgeBase;
                scoutTrapBaseDamage += (int)(specScout_TrapDamage * specScout_TrapDamageBase);
                ultCooldownReduction -= specScout_UltCost * specScout_UltCostBase;
                #endregion

                if (scoutUltCurDuration > 0)
                {
                    Player.moveSpeed += scoutUltSpeed;
                    Player.autoJump = true;
                    Player.jumpSpeedBoost += 3f;
                    Player.armorEffectDrawShadow = true;
                }

                if (scoutUltInvCurDuration > 0)
                {
                    Player.immune = true;
                }

                scoutTrapDamage += scoutTrapDamageLevel * scoutLevel;
                scoutUltSpeed += scoutUltSpeedLevel * scoutLevel;
            }
            #endregion

            #region Soulmancer
            soulmancerLevel = soulmancerDefeatedBosses.Count;
            if (soulmancerLevel > Configs._ACMConfigServer.Instance.maxClassLevel)
                soulmancerLevel = Configs._ACMConfigServer.Instance.maxClassLevel;

            if (hasSoulmancer)
            {
                #region Soulmancer Talents
                if (soulmancerTalent_1 == "L")
                    soulmancerSoulRipChance_Base += .01f;
                if (soulmancerTalent_1 == "R")
                    abilityPower += .1f;

                if (soulmancerTalent_2 == "L")
                    cooldownReduction -= .06f;
                if (soulmancerTalent_2 == "R")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (soulmancerTalent_3 == "L")
                    ultCooldownReduction -= .06f;
                if (soulmancerTalent_3 == "R")
                    soulmancerSoulRipChance_Base += .02f;

                if (soulmancerTalent_4 == "L")
                    soulmancerSoulRipDamage_Base += 11;
                if (soulmancerTalent_4 == "R")
                    soulmancerSoulShatterRange += 65;

                if (soulmancerTalent_5 == "L")
                    ultCooldownReduction -= .04f;
                if (soulmancerTalent_5 == "R")
                    soulmancerSoulShatterDamage_Base += 24;

                if (soulmancerTalent_6 == "L")
                    soulmancerConsumeDuration_Base += 60;
                if (soulmancerTalent_6 == "R")
                    soulmancerSoulRipDamage_Base += 12;

                if (soulmancerTalent_7 == "L")
                    soulmancerSoulRipChance_Base += .02f;
                if (soulmancerTalent_7 == "R")
                    Player.GetCritChance(DamageClass.Magic) += 2;

                if (soulmancerTalent_8 == "L")
                    soulmancerSoulShatterRange += 95;
                if (soulmancerTalent_8 == "R")
                    soulmancerSoulRipChance_Base += .03f;

                if (soulmancerTalent_9 == "L")
                    abilityPower += .16f;
                if (soulmancerTalent_9 == "R")
                    cooldownReduction -= .08f;

                if (soulmancerTalent_9 == "L")
                    soulmancerSoulRipChance_Base += .04f;
                if (soulmancerTalent_9 == "R")
                    soulmancerConsumeDuration_Base += 60;
                #endregion

                #region Specs
                soulmancerSoulRipChance_Base += specSoulmancer_SoulRipChance * specSoulmancer_SoulRipChanceBase;
                abilityPower += specSoulmancer_AbilityPower * specSoulmancer_AbilityPowerBase;
                Player.GetAttackSpeed(DamageClass.Magic) += specSoulmancer_MagicAttackSpeed * specSoulmancer_MagicAttackSpeedBase;
                cooldownReduction -= specSoulmancer_CooldownReduction * specSoulmancer_CooldownReductionBase;
                soulmancerConsumeDuration += specSoulmancer_ConsumeDuration * specSoulmancer_ConsumeDurationBase;
                #endregion

                soulmancerSoulRipDamage += soulmancerSoulRipDamage_Base + soulmancerSoulRipDamage_PerLevel * soulmancerLevel;
                soulmancerSoulRipChance += soulmancerSoulRipChance_Base + soulmancerSoulRipChance_PerLevel * soulmancerLevel;
                soulmancerConsumeHeal += soulmancerConsumeHeal_Base;
                soulmancerConsumeDuration += soulmancerConsumeDuration_Base + soulmancerConsumeDuration_PerLevel * soulmancerLevel;
                soulmancerSoulShatterDamage += soulmancerSoulShatterDamage_Base + soulmancerShatterDamage_PerLevel * soulmancerLevel;
                //soulmancerSacrificeSoulCount += soulmancerSacrificeSoulCount_Base;
                soulmancerSacrificeHealthCost += soulmancerSacrificeHealthCost_Base;
            }
            #endregion

            #region Inventor
            if (inventorOverclockCurDuration > 0)
                Player.GetAttackSpeed(DamageClass.Generic) += .25f;
            #endregion

                #region Ability Ready Sounds
                if (ability1Cooldown == 0 && !a1Sound)
            {
                a1Sound = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }

            if (ability2Cooldown == 0 && !a2Sound)
            {
                a2Sound = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }

            if (ultCharge == ultChargeMax && !ultSound)
            {
                ultSound = true;
                SoundEngine.PlaySound(SoundID.MaxMana);
            }
            #endregion

            if (hasDarkSign)
                lifeMult -= .15f;

            if (hasPocketSlime)
                lifeMult -= .05f;

            if (hasPeanut)
                lifeMult -= .16f;

            if (hasOldBlood)
                defenseMult -= .1f;

            if(hasLuckyLeaf)
                lifeMult -= .16f;

            if (hasChaosAccelerant)
            {
                lifeMult -= .5f;
                manaMult -= .5f;
            }

            if (hasTearsOfLife)
                lifeMult -= .03f;

            if (hasSqueaker)
                lifeMult -= .8f;

            if (hasFlanPudding)
                lifeMult += .04f;

            if (hasPorcelainMask)
                defenseMult -= .25f;

            if(hasScout)
                lifeMult -= scoutLevel * .0045f;

            ultChargeMax = (int)(ultChargeMax * ultCooldownReduction);
            Player.statLifeMax2 = (int)(Player.statLifeMax2 * lifeMult);
            Player.statDefense = (int)(Player.statDefense * defenseMult);
            Player.statManaMax2 = (int)(Player.statManaMax2 * manaMult);
            if (ultCharge > ultChargeMax)
                ultCharge = ultChargeMax;

            if (pSecHealthTimer == 0)
            {
                if (!inBattle && bloodMagePassiveCurrentStacks > 0 && outOfBattleTimer <= 0)
                    bloodMagePassiveCurrentStacks--;

                if (!Player.dead)
                    Player.statLife += (int)(Player.statLifeMax2 * pSecHealthRegen);

                if(hasManaBag)
                {
                    int manaToRegen = (int)(Player.statManaMax2 * .03f);
                    if(Player.statMana < Player.statLifeMax2)
                        Player.statMana += manaToRegen;
                }

                if (bloodMageCurUltTicks > 0)
                {
                    int heal = (int)(Player.statLifeMax2 * bloodMageUltRegen * healingPower);
                    HealPlayer(1, 3, heal);
                    bloodMageCurUltTicks--;
                    //Main.NewText($"{bloodMageCurUltTicks}/{bloodMageUltTicks}");
                }
                pSecHealthTimer = 60;
            }

            if (cooldownReduction < 0f)
                cooldownReduction = 0f;

            if (hasScout && scoutColaCurDuration > 0 && hasAghanims)
                Player.GetCritChance(DamageClass.Ranged) += 15;

            if (hasSoulmancer)
            {
                if (hasAghanims)
                    soulmancerSoulShatterCastTarget = Main.MouseWorld;
                else
                    soulmancerSoulShatterCastTarget = Player.Center;
            }

            if (chocolateBarTimer == 0)
            {
                Player.statLife += (int)(chocolateBarStoredHealth + Player.statLifeMax2 * .02f);
                Player.HealEffect((int)(chocolateBarStoredHealth + Player.statLifeMax2 * .02f));
                chocolateBarStoredHealth = 0;

                for (int x = 0; x < 4; x++)
                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
            }

            

            base.PostUpdateEquips();
        }

        public override void PostUpdateMiscEffects()
        {
            base.PostUpdateMiscEffects();
        }

        public override void PostUpdate()
        {
            if (!updatedRelicList)
            {
                for (int i = 0; i < ItemLoader.ItemCount; i++)
                {
                    Item newItem = new Item();
                    try
                    {
                        newItem.SetDefaults(i);
                    }
                    catch
                    {
                        newItem.netDefaults(i);
                    }

                    ACMGlobalItem acm = newItem.GetGlobalItem<ACMGlobalItem>();

                    if (acm != null)
                        if (acm.isRelic && !relicList.Contains(i))
                            relicList.Add(i);
                }
                updatedRelicList = true;
            }

            if (Main.netMode != NetmodeID.Server)
            {
                if(Player.whoAmI == Main.myPlayer)
                {
                    if (equippedClass != "" && GetInstance<ACM2ModSystem>()._HUD.CurrentState == null)
                        GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());

                    if (resetHUD == 1 && equippedClass != "")
                    {
                        GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                        GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
                    }

                    if (equippedClass == "" || Main.playerInventory)
                    {
                        GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                        GetInstance<ACM2ModSystem>()._Specs.SetState(null);

                    }
                }
            }

            base.PostUpdate();
        }



        public override void OnRespawn(Player player)
        {
            if(Main.myPlayer == player.whoAmI && Main.netMode != NetmodeID.Server)
            {
                GetInstance<ACM2ModSystem>()._HUD.SetState(null);
                GetInstance<ACM2ModSystem>()._HUD.SetState(new UI.HUD.HUD());
            }
                
            base.OnRespawn(player);
        }

        public override void UpdateLifeRegen()
        {
            if(bloodMageBloodEnchantment)
                Player.lifeRegenTime = 0;
            if (bloodMageBloodEnchantment && Player.lifeRegen > 0)
            {
                Player.lifeRegen = 0;
                Player.lifeRegen -= 2;
            }

            base.UpdateLifeRegen();
        }

        public override void UpdateBadLifeRegen()
        {
            if (bloodMageBloodEnchantment)
                Player.lifeRegenTime = 0;
            if (bloodMageBloodEnchantment && Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                    Player.lifeRegen -= 2;
                }

            base.UpdateBadLifeRegen();
        }

        public override void OnEnterWorld(Player player)
        {
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new UI.ClassesMenu());
            GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
            base.OnEnterWorld(player);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                #region Press Key: Escape(Inventory) to close menus
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._ClassesMenu.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._RelicsUI.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._RelicsUI.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._BloodMageTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._BloodMageTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._BloodMageSpecs.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._BloodMageSpecs.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._CommanderTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._CommanderTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._ScoutTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._ScoutTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                if (Player.controlInv && GetInstance<ACM2ModSystem>()._VanguardTalents.CurrentState != null)
                {
                    GetInstance<ACM2ModSystem>()._VanguardTalents.SetState(null);
                    if (Main.playerInventory == false)
                        Main.playerInventory = false;
                }
                #endregion

                if (ACM2.Menu.JustReleased)
                {
                    if (GetInstance<ACM2ModSystem>()._ClassesMenu.CurrentState == null)
                    {
                        GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(new UI.ClassesMenu());
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                    }
                    else
                    {
                        GetInstance<ACM2ModSystem>()._ClassesMenu.SetState(null);
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                }
            }

            if (Main.myPlayer == Player.whoAmI && !Player.dead)
            {
                if (ACM2.ClassAbility1.JustReleased && ability1Cooldown <= 0)
                {
                    if (hasUnstableConcoction)
                        if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;


                    #region Relic Effects
                    if (ability1MaxCooldown > 1)
                    {
                        if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                        {
                            int heal = (int)(Player.statLifeMax2 * .03f);
                            healthToRegenSlow += heal;
                            Player.HealEffect(heal);
                            for (int x = 0; x < 3; x++)
                                Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                        }
                    }
                    #endregion

                    Vector2 PointToCursor = Main.MouseWorld - Player.position;
                    switch (equippedClass)
                    {
                        case "Vanguard":
                            
                            PointToCursor.Normalize();
                            PointToCursor *= 21f;

                            if (vanguardSpearHeal && Player.statLife < Player.statLifeMax2)
                            {
                                Player.statLife += (int)(Player.statLifeMax2 * .026f * healingPower);
                                Player.HealEffect((int)(Player.statLifeMax2 * .026f * healingPower));
                                for (int x = 0; x < 3; x++)
                                    Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                            }


                            SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot);
                            Projectile.NewProjectile(null, new Vector2(Player.position.X + Player.width / 2, Player.position.Y), new Vector2(PointToCursor.X, PointToCursor.Y), ProjectileType<Projectiles.Vanguard.VanguardSpear>(), (int)(vanguardSpearDamage * abilityPower), 0, Player.whoAmI);

                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Spear Of Light!", true);
                            break;

                        case "Blood Mage":
                            PointToCursor.Normalize();
                            PointToCursor *= 18f;

                            SoundEngine.PlaySound(SoundID.Item21);
                            Projectile.NewProjectile(null, new Vector2(Player.position.X + Player.width / 2, Player.position.Y), new Vector2(PointToCursor.X, PointToCursor.Y), ProjectileType<Projectiles.BloodMage.Transfusion>(), (int)(bloodMageSiphonDamage * abilityPower), 0, Player.whoAmI);

                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Transfusion!", true);
                            break;

                        case "Commander":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Commander.WarBanner>(), 0, 0, Player.whoAmI);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "War Banner!", true);
                            break;

                        case "Scout":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Hit-a-Soda!", true);
                            scoutColaCurDuration = scoutColaDuration;

                            SoundEngine.PlaySound(SoundID.Item3, Player.position);
                            break;

                        case "Soulmancer":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Consume!", true);
                            soulmancerConsumeDuration_Cur = soulmancerConsumeDuration;

                            SoundEngine.PlaySound(SoundID.DD2_WitherBeastAuraPulse, Player.position);
                            break;

                        case "Inventor":
                            AddAbilityCooldown(1, ability1MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Deploy Sentry!", true);

                            for (int p = 0; p < Main.maxProjectiles; p++)
                            {
                                if (Player.ownedProjectileCounts[ProjectileType<Projectiles.Inventor.Sentry>()] > 0)
                                    if (Main.projectile[p].type == ProjectileType<Projectiles.Inventor.Sentry>() && Main.projectile[p].owner == Main.myPlayer)
                                        Main.projectile[p].Kill();
                            }
                            
                               
                            int xx = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
                            int yy = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
                            int bump = -32;
                            if (Player.gravDir == -1f)
                                yy = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
                            Player.FindSentryRestingSpot(ProjectileType<Projectiles.Inventor.Sentry>(), out xx, out yy, out bump);
                            Projectile.NewProjectile(null, xx, yy - 22, 0, 0, ProjectileType<Projectiles.Inventor.Sentry>(), 0, 0, Player.whoAmI);

                            SoundEngine.PlaySound(SoundID.DD2_DefenseTowerSpawn, Player.position);
                            break;
                    }
                    a1Sound = false;
                }

                if (ACM2.ClassAbility2.JustReleased && ability2Cooldown <= 0)
                {
                    if (hasUnstableConcoction && equippedClass != "Blood Mage")
                        if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                    #region Relic Effects
                    if (ability2MaxCooldown > 1)
                    {
                        if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2 /*&& equippedClass != "Blood Mage"*/)
                        {
                            int heal = (int)(Player.statLifeMax2 * .03f);
                            healthToRegenSlow += heal;
                            Player.HealEffect(heal);
                            for (int x = 0; x < 3; x++)
                                Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                        }
                    }
                    #endregion

                    switch (equippedClass)
                    {
                        case "Vanguard":
                            vanguardShieldCurrentDuration = (int)(vanguardShieldDuration * abilityDuration);
                            SoundEngine.PlaySound(SoundID.Mech);

                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Light Barrier!", true);
                            break;

                        case "Blood Mage":
                            if (bloodMageBloodEnchantment)
                            {
                                bloodMageBloodEnchantment = false;
                            }
                            else
                            {
                                bloodMageBloodEnchantment = true;
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment>(), 0, 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment2>(), 0, 0, Player.whoAmI);
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.BloodMage.BloodEnchantment3>(), 0, 0, Player.whoAmI);
                            }
                                
                            if (!bloodMageBloodEnchantment)
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Blood Enchantment: Off!", true);
                            else
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Blood Enchantment: On!", true);
                            break;

                        case "Commander":
                            Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Commander.BattleCry>(), 0, 0, Player.whoAmI);
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Battle Cry!", true);
                            break;

                        case "Scout":
                            if (Collision.CanHitLine(Player.Center, 1, 1, Main.MouseWorld, 1, 1))
                            {
                                AddAbilityCooldown(2, ability2MaxCooldown);
                                Projectile.NewProjectile(null, Main.MouseWorld, Vector2.Zero, ProjectileType<Projectiles.Scout.ScoutTrap>(), 0, 0, Player.whoAmI);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Explosive Trap!", true);
                                SoundEngine.PlaySound(SoundID.Item37, Player.position);
                            }
                            break;

                        case "Soulmancer":
                            AddAbilityCooldown(2, ability2MaxCooldown);
                            if (hasAghanims)
                                Projectile.NewProjectile(null, Main.MouseWorld, Vector2.Zero, ProjectileType<Projectiles.Soulmancer.SoulShatter>(), 0, 0, Player.whoAmI);
                            else
                                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Projectiles.Soulmancer.SoulShatter>(), 0, 0, Player.whoAmI);
                            SoundEngine.PlaySound(SoundID.DD2_LightningBugZap, Player.position);
                            break;

                        case "Inventor":
                            Vector2 vel = Main.MouseWorld - Player.Center;
                            vel.Normalize();
                            vel *= 28f;

                            for (int i = 0; i < inventorCogShotgunProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(vel.X, vel.Y).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-5f, 5f)));

                                if(inventorOverclockCurDuration > 0)
                                    Projectile.NewProjectile(null, Player.Center, perturbedSpeed, ProjectileType<Projectiles.Inventor.Cog>(), inventorSentryDamage * 2, 0, Player.whoAmI);
                                else
                                    Projectile.NewProjectile(null, Player.Center, perturbedSpeed, ProjectileType<Projectiles.Inventor.Cog>(), inventorSentryDamage, 0, Player.whoAmI);
                            }
                            SoundEngine.PlaySound(SoundID.Item36, Player.position);
                            break;
                    }
                    a2Sound = false;
                }

                if (ACM2.ClassAbilityUltimate.JustReleased)
                {
                    if(ultCharge >= ultChargeMax)
                    {   
                        ultCharge -= ultChargeMax;

                        #region Relic Effects
                        if (hasUnstableConcoction)
                            if (!isUnstableConcoctionReady) isUnstableConcoctionReady = true;

                        if (hasBleedingMoonStone && Player.statLife < Player.statLifeMax2)
                        {
                            int heal = (int)(Player.statLifeMax2 * .075f);
                            healthToRegen += heal;
                            Player.HealEffect(heal);
                            for (int x = 0; x < 3; x++)
                                Dust.NewDustDirect(Player.position, Player.width, Player.height, DustType<Dusts.HealingDust>(), 0f, 0f, 0, default, Main.rand.NextFloat(1f, 2f));
                        }
                        #endregion

                        switch (equippedClass)
                        {
                            case "Vanguard":
                                Projectile.NewProjectile(null, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 1000), new Vector2(0f, 50f), ProjectileType<Projectiles.Vanguard.VanguardUltimate>(), (int)(vanguardSwordDamage * abilityPower), 0, Player.whoAmI);
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Sword Of Judgement!", true);
                                //NetworkText text;
                                //text = NetworkText.FromKey(player.name + " was healed for " + healAmount + " health", acmPlayer.player.name);
                                //NetMessage.(text, new Color(25, 225, 25));
                                break;

                            case "Blood Mage":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Regeneration!", true);
                                SoundEngine.PlaySound(SoundID.Item4, Player.position);
                                bloodMageCurUltTicks = bloodMageUltTicks;
                                break;

                            case "Commander":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Inspire!", true);
                                SoundEngine.PlaySound(SoundID.Thunder, Player.position);
                                ApplyBuffToAllPlayers(BuffType<Buffs.Commander.Inspire>(), commanderUltDuration);
                                break;

                            case "Scout":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Atomic-Slap (TM)!", true);
                                SoundEngine.PlaySound(SoundID.Item3, Player.position);

                                scoutUltCurDuration = scoutUltDuration;
                                scoutUltInvCurDuration = scoutUltInvDuration;
                                break;

                            case "Soulmancer":
                                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y + 20, Player.width, Player.height), Color.White, "Self Sacrifice!", true);
                                soulmancerSacrificeTimer = 3;
                                soulmancerSacrificeSoulCount_Cur = soulmancerSacrificeSoulCount_Base;
                                break;

                            case "Inventor":
                                inventorOverclockCurDuration = inventorOverclockDuration;
                                SoundEngine.PlaySound(SoundID.DD2_KoboldIgnite, Player.Center);
                                break;
                        }
                        ultSound = false;
                    }
                }
            }
            base.ProcessTriggers(triggersSet);
        }

        public override void SaveData(TagCompound tag)
        {
            //tag["defeatedBosses"] = defeatedBosses;
            tag.Add("enemiesKilled", enemiesKilled);
            tag.Add("damageDealt", damageDealt);
            tag.Add("timesDied", timesDied);
            tag.Add("highestDPS", highestDPS);
            tag.Add("highestCrit", highestCrit);
            tag.Add("totalDamageTaken", totalDamageTaken);

            tag.Add("gotFreeRelic", gotFreeRelic);

            #region Cards
            tag.Add("cardsPoints", cardsPoints);
            tag.Add("card_ProwlerCount", card_ProwlerCount);
            tag.Add("card_CarryCount", card_CarryCount);
            tag.Add("card_DeadeyeCount", card_DeadeyeCount);
            tag.Add("card_FortifiedCount", card_FortifiedCount);
            tag.Add("card_HealerCount", card_HealerCount);
            tag.Add("card_HealthyCount", card_HealthyCount);
            tag.Add("card_ImpenetrableCount", card_ImpenetrableCount);
            tag.Add("card_MagicalCount", card_MagicalCount);
            tag.Add("card_MasterfulCount", card_MasterfulCount);
            tag.Add("card_MendingCount", card_MendingCount);
            tag.Add("card_MightyCount", card_MightyCount);
            tag.Add("card_NimbleHandsCount", card_NimbleHandsCount);
            tag.Add("card_PowerfulCount", card_PowerfulCount);
            tag.Add("card_SneakyCount", card_SneakyCount);
            tag.Add("card_SparkOfGeniusCount", card_SparkOfGeniusCount);
            tag.Add("card_TimelessCount", card_TimelessCount);
            tag.Add("card_VeteranCount", card_VeteranCount);
            tag.Add("card_MischievousCount", card_MischievousCount);
            tag.Add("card_SeerCount", card_SeerCount);
            tag.Add("card_FerociousCount", card_FerociousCount);
            #endregion

            #region Vanguard
            tag.Add("vanguardDefeatedBosses", vanguardDefeatedBosses);
            tag.Add("vanguardSkillPoints", vanguardSkillPoints);
            tag.Add("vanguardSpentSkillPoints", vanguardSpentSkillPoints);

            tag.Add("vanguardTalent_1", vanguardTalent_1);
            tag.Add("vanguardTalent_2", vanguardTalent_2);
            tag.Add("vanguardTalent_3", vanguardTalent_3);
            tag.Add("vanguardTalent_4", vanguardTalent_4);
            tag.Add("vanguardTalent_5", vanguardTalent_5);
            tag.Add("vanguardTalent_6", vanguardTalent_6);
            tag.Add("vanguardTalent_7", vanguardTalent_7);
            tag.Add("vanguardTalent_8", vanguardTalent_8);
            tag.Add("vanguardTalent_9", vanguardTalent_9);
            tag.Add("vanguardTalent_10", vanguardTalent_10);

            tag.Add("specVanguard_Defense", specVanguard_Defense);
            tag.Add("specVanguard_MeleeDamage", specVanguard_MeleeDamage);
            tag.Add("specVanguard_ShieldDamageReduction", specVanguard_ShieldDamageReduction);
            tag.Add("specVanguard_SpearDamage", specVanguard_SpearDamage);
            tag.Add("specVanguard_UltCost", specVanguard_UltCost);
            #endregion

            #region Blood Mage
            tag.Add("bloodMageDefeatedBosses", bloodMageDefeatedBosses);
            tag.Add("bloodMageSkillPoints", bloodMageSkillPoints);
            tag.Add("bloodMageSpentSkillPoints", bloodMageSpentSkillPoints);

            tag.Add("bloodMageTalent_1", bloodMageTalent_1);
            tag.Add("bloodMageTalent_2", bloodMageTalent_2);
            tag.Add("bloodMageTalent_3", bloodMageTalent_3);
            tag.Add("bloodMageTalent_4", bloodMageTalent_4);
            tag.Add("bloodMageTalent_5", bloodMageTalent_5);
            tag.Add("bloodMageTalent_6", bloodMageTalent_6);
            tag.Add("bloodMageTalent_7", bloodMageTalent_7);
            tag.Add("bloodMageTalent_8", bloodMageTalent_8);
            tag.Add("bloodMageTalent_9", bloodMageTalent_9);
            tag.Add("bloodMageTalent_10", bloodMageTalent_10);

            tag.Add("specBloodMage_MaxHealth", specBloodMage_MaxHealth);
            tag.Add("specBloodMage_CooldownReduction", specBloodMage_CooldownReduction);
            tag.Add("specBloodMage_EnchantDamage", specBloodMage_EnchantDamage);
            tag.Add("specBloodMage_TransfusionDamage", specBloodMage_TransfusionDamage);
            tag.Add("specBloodMage_UltHeal", specBloodMage_UltHeal);
            #endregion

            #region Commander
            tag.Add("commanderDefeatedBosses", commanderDefeatedBosses);
            tag.Add("commanderSkillPoints", commanderSkillPoints);
            tag.Add("commanderSpentSkillPoints", commanderSpentSkillPoints);

            tag.Add("commanderTalent_1", commanderTalent_1);
            tag.Add("commanderTalent_2", commanderTalent_2);
            tag.Add("commanderTalent_3", commanderTalent_3);
            tag.Add("commanderTalent_4", commanderTalent_4);
            tag.Add("commanderTalent_5", commanderTalent_5);
            tag.Add("commanderTalent_6", commanderTalent_6);
            tag.Add("commanderTalent_7", commanderTalent_7);
            tag.Add("commanderTalent_8", commanderTalent_8);
            tag.Add("commanderTalent_9", commanderTalent_9);
            tag.Add("commanderTalent_10", commanderTalent_10);

            tag.Add("specCommander_BannerDamageReduction", specCommander_BannerDamageReduction);
            tag.Add("specCommander_BannerRange", specCommander_BannerRange);
            tag.Add("specCommander_MinionDamage", specCommander_MinionDamage);
            tag.Add("specCommander_PassiveEndurance", specCommander_PassiveEndurance);
            tag.Add("specCommander_WhipRange", specCommander_WhipRange);
            #endregion

            #region Scout
            tag.Add("scoutDefeatedBosses", scoutDefeatedBosses);
            tag.Add("scoutSkillPoints", scoutSkillPoints);
            tag.Add("scoutSpentSkillPoints", scoutSpentSkillPoints);
            
            tag.Add("scoutCanDoubleJump", scoutCanDoubleJump);
            
            tag.Add("scoutTalent_1", scoutTalent_1);
            tag.Add("scoutTalent_2", scoutTalent_2);
            tag.Add("scoutTalent_3", scoutTalent_3);
            tag.Add("scoutTalent_4", scoutTalent_4);
            tag.Add("scoutTalent_5", scoutTalent_5);
            tag.Add("scoutTalent_6", scoutTalent_6);
            tag.Add("scoutTalent_7", scoutTalent_7);
            tag.Add("scoutTalent_8", scoutTalent_8);
            tag.Add("scoutTalent_9", scoutTalent_9);
            tag.Add("scoutTalent_10", scoutTalent_10);

            tag.Add("specScout_ColaDuration", specScout_ColaDuration);
            tag.Add("specScout_CooldownReduction", specScout_CooldownReduction);
            tag.Add("specScout_Dodge", specScout_Dodge);
            tag.Add("specScout_TrapDamage", specScout_TrapDamage);
            tag.Add("specScout_UltCost", specScout_UltCost);

            tag.Add("specSoulmancer_AbilityPower", specSoulmancer_AbilityPower);
            tag.Add("specSoulmancer_ConsumeDuration", specSoulmancer_ConsumeDuration);
            tag.Add("specSoulmancer_CooldownReduction", specSoulmancer_CooldownReduction);
            tag.Add("specSoulmancer_MagicAttackSpeed", specSoulmancer_MagicAttackSpeed);
            tag.Add("specSoulmancer_SoulRipChance", specSoulmancer_SoulRipChance);
            #endregion

            #region Soulmancer
            tag.Add("soulmancerDefeatedBosses", soulmancerDefeatedBosses);
            tag.Add("soulmancerSkillPoints", soulmancerSkillPoints);
            tag.Add("soulmancerSpentSkillPoints", soulmancerSpentSkillPoints);

            tag.Add("soulmancerTalent_1", soulmancerTalent_1);
            tag.Add("soulmancerTalent_2", soulmancerTalent_2);
            tag.Add("soulmancerTalent_3", soulmancerTalent_3);
            tag.Add("soulmancerTalent_4", soulmancerTalent_4);
            tag.Add("soulmancerTalent_5", soulmancerTalent_5);
            tag.Add("soulmancerTalent_6", soulmancerTalent_6);
            tag.Add("soulmancerTalent_7", soulmancerTalent_7);
            tag.Add("soulmancerTalent_8", soulmancerTalent_8);
            tag.Add("soulmancerTalent_9", soulmancerTalent_9);
            tag.Add("soulmancerTalent_10", soulmancerTalent_10);
            #endregion

            base.SaveData(tag);
        }   

        public override void LoadData(TagCompound tag)
        {
            //defeatedBosses.Clear();
            //defeatedBosses.AddRange(tag.GetList<string>("defeatedBosses"));
            enemiesKilled = tag.GetInt("enemiesKilled");
            damageDealt = tag.GetInt("damageDealt");
            timesDied = tag.GetInt("timesDied");
            highestDPS = tag.GetInt("highestDPS");
            highestCrit = tag.GetInt("highestCrit");
            totalDamageTaken = tag.GetInt("totalDamageTaken");

            gotFreeRelic = tag.GetBool("gotFreeRelic");

            #region Cards
            cardsPoints = tag.GetInt("cardsPoints");
            card_ProwlerCount = tag.GetInt("card_ProwlerCount");
            card_CarryCount = tag.GetInt("card_CarryCount");
            card_DeadeyeCount = tag.GetInt("card_DeadeyeCount");
            card_FortifiedCount = tag.GetInt("card_FortifiedCount");
            card_HealerCount = tag.GetInt("card_HealerCount");
            card_HealthyCount = tag.GetInt("card_HealthyCount");
            card_ImpenetrableCount = tag.GetInt("card_ImpenetrableCount");
            card_MagicalCount = tag.GetInt("card_MagicalCount");
            card_MasterfulCount = tag.GetInt("card_MasterfulCount");
            card_MendingCount = tag.GetInt("card_MendingCount");
            card_MightyCount = tag.GetInt("card_MightyCount");
            card_NimbleHandsCount = tag.GetInt("card_NimbleHandsCount");
            card_PowerfulCount = tag.GetInt("card_PowerfulCount");
            card_SneakyCount = tag.GetInt("card_SneakyCount");
            card_SparkOfGeniusCount = tag.GetInt("card_SparkOfGeniusCount");
            card_TimelessCount = tag.GetInt("card_TimelessCount");
            card_VeteranCount = tag.GetInt("card_VeteranCount");
            card_MischievousCount = tag.GetInt("card_MischievousCount");
            card_SeerCount = tag.GetInt("card_SeerCount");
            card_FerociousCount = tag.GetInt("card_FerociousCount");
            #endregion

            #region Vanguard
            vanguardDefeatedBosses.AddRange(tag.GetList<string>("vanguardDefeatedBosses"));
            vanguardSkillPoints = tag.GetInt("vanguardSkillPoints");
            vanguardSpentSkillPoints = tag.GetInt("vanguardSpentSkillPoints");

            vanguardTalent_1 = tag.GetString("vanguardTalent_1");
            vanguardTalent_2 = tag.GetString("vanguardTalent_2");
            vanguardTalent_3 = tag.GetString("vanguardTalent_3");
            vanguardTalent_4 = tag.GetString("vanguardTalent_4");
            vanguardTalent_5 = tag.GetString("vanguardTalent_5");
            vanguardTalent_6 = tag.GetString("vanguardTalent_6");
            vanguardTalent_7 = tag.GetString("vanguardTalent_7");
            vanguardTalent_8 = tag.GetString("vanguardTalent_8");
            vanguardTalent_9 = tag.GetString("vanguardTalent_9");
            vanguardTalent_10 = tag.GetString("vanguardTalent_10");

            specVanguard_Defense = tag.GetInt("specVanguard_Defense");
            specVanguard_MeleeDamage = tag.GetInt("specVanguard_MeleeDamage");
            specVanguard_ShieldDamageReduction = tag.GetInt("specVanguard_ShieldDamageReduction");
            specVanguard_SpearDamage = tag.GetInt("specVanguard_SpearDamage");
            specVanguard_UltCost = tag.GetInt("specVanguard_UltCost");
            #endregion

            #region Blood Mage
            bloodMageDefeatedBosses.AddRange(tag.GetList<string>("bloodMageDefeatedBosses"));
            bloodMageSkillPoints = tag.GetInt("bloodMageSkillPoints");
            bloodMageSpentSkillPoints = tag.GetInt("bloodMageSpentSkillPoints");

            bloodMageTalent_1 = tag.GetString("bloodMageTalent_1");
            bloodMageTalent_2 = tag.GetString("bloodMageTalent_2");
            bloodMageTalent_3 = tag.GetString("bloodMageTalent_3");
            bloodMageTalent_4 = tag.GetString("bloodMageTalent_4");
            bloodMageTalent_5 = tag.GetString("bloodMageTalent_5");
            bloodMageTalent_6 = tag.GetString("bloodMageTalent_6");
            bloodMageTalent_7 = tag.GetString("bloodMageTalent_7");
            bloodMageTalent_8 = tag.GetString("bloodMageTalent_8");
            bloodMageTalent_9 = tag.GetString("bloodMageTalent_9");
            bloodMageTalent_10 = tag.GetString("bloodMageTalent_10");

            specBloodMage_MaxHealth = tag.GetInt("specBloodMage_MaxHealth");
            specBloodMage_CooldownReduction = tag.GetInt("specBloodMage_CooldownReduction");
            specBloodMage_EnchantDamage = tag.GetInt("specBloodMage_EnchantDamage");
            specBloodMage_TransfusionDamage = tag.GetInt("specBloodMage_TransfusionDamage");
            specBloodMage_UltHeal = tag.GetInt("specBloodMage_UltHeal");
            #endregion

            #region Commander
            commanderDefeatedBosses.AddRange(tag.GetList<string>("commanderDefeatedBosses"));
            commanderSkillPoints = tag.GetInt("commanderSkillPoints");
            commanderSpentSkillPoints = tag.GetInt("commanderSpentSkillPoints");

            commanderTalent_1 = tag.GetString("commanderTalent_1");
            commanderTalent_2 = tag.GetString("commanderTalent_2");
            commanderTalent_3 = tag.GetString("commanderTalent_3");
            commanderTalent_4 = tag.GetString("commanderTalent_4");
            commanderTalent_5 = tag.GetString("commanderTalent_5");
            commanderTalent_6 = tag.GetString("commanderTalent_6");
            commanderTalent_7 = tag.GetString("commanderTalent_7");
            commanderTalent_8 = tag.GetString("commanderTalent_8");
            commanderTalent_9 = tag.GetString("commanderTalent_9");
            commanderTalent_10 = tag.GetString("commanderTalent_10");

            specCommander_BannerDamageReduction = tag.GetInt("specCommander_BannerDamageReduction");
            specCommander_BannerRange = tag.GetInt("specCommander_BannerRange");
            specCommander_MinionDamage = tag.GetInt("specCommander_MinionDamage");
            specCommander_PassiveEndurance = tag.GetInt("specCommander_PassiveEndurance");
            specCommander_WhipRange = tag.GetInt("specCommander_WhipRange");
            #endregion

            #region Scout
            scoutDefeatedBosses.AddRange(tag.GetList<string>("scoutDefeatedBosses"));
            scoutSkillPoints = tag.GetInt("scoutSkillPoints");
            scoutSpentSkillPoints = tag.GetInt("scoutSpentSkillPoints");
            
            scoutCanDoubleJump = tag.GetBool("scoutCanDoubleJump");
            
            scoutTalent_1 = tag.GetString("scoutTalent_1");
            scoutTalent_2 = tag.GetString("scoutTalent_2");
            scoutTalent_3 = tag.GetString("scoutTalent_3");
            scoutTalent_4 = tag.GetString("scoutTalent_4");
            scoutTalent_5 = tag.GetString("scoutTalent_5");
            scoutTalent_6 = tag.GetString("scoutTalent_6");
            scoutTalent_7 = tag.GetString("scoutTalent_7");
            scoutTalent_8 = tag.GetString("scoutTalent_8");
            scoutTalent_9 = tag.GetString("scoutTalent_9");
            scoutTalent_10 = tag.GetString("scoutTalent_10");

            specScout_ColaDuration = tag.GetInt("specScout_ColaDuration");
            specScout_CooldownReduction = tag.GetInt("specScout_CooldownReduction");
            specScout_Dodge = tag.GetInt("specScout_Dodge");
            specScout_TrapDamage = tag.GetInt("specScout_TrapDamage");
            specScout_UltCost = tag.GetInt("specScout_UltCost");
            #endregion

            #region Soulmancer
            soulmancerDefeatedBosses.AddRange(tag.GetList<string>("soulmancerDefeatedBosses"));
            soulmancerSkillPoints = tag.GetInt("soulmancerSkillPoints");
            soulmancerSpentSkillPoints = tag.GetInt("soulmancerSpentSkillPoints");

            soulmancerTalent_1 = tag.GetString("soulmancerTalent_1");
            soulmancerTalent_2 = tag.GetString("soulmancerTalent_2");
            soulmancerTalent_3 = tag.GetString("soulmancerTalent_3");
            soulmancerTalent_4 = tag.GetString("soulmancerTalent_4");
            soulmancerTalent_5 = tag.GetString("soulmancerTalent_5");
            soulmancerTalent_6 = tag.GetString("soulmancerTalent_6");
            soulmancerTalent_7 = tag.GetString("soulmancerTalent_7");
            soulmancerTalent_8 = tag.GetString("soulmancerTalent_8");
            soulmancerTalent_9 = tag.GetString("soulmancerTalent_9");
            soulmancerTalent_10 = tag.GetString("soulmancerTalent_10");

            specSoulmancer_AbilityPower = tag.GetInt("specSoulmancer_AbilityPower");
            specSoulmancer_ConsumeDuration = tag.GetInt("specSoulmancer_ConsumeDuration");
            specSoulmancer_CooldownReduction = tag.GetInt("specSoulmancer_CooldownReduction");
            specSoulmancer_MagicAttackSpeed = tag.GetInt("specSoulmancer_MagicAttackSpeed");
            specSoulmancer_SoulRipChance = tag.GetInt("specSoulmancer_SoulRipChance");
            #endregion

            base.LoadData(tag);
        }

        /// <summary>
        /// Add ability cooldowns. 'ability' corresponds to ability number (1/2).
        /// </summary>
        void AddAbilityCooldown(int ability, int timeInSeconds)
        {
            if (ability == 1)
                ability1Cooldown = (int)(60 * timeInSeconds * cooldownReduction);
            if (ability == 2)
                ability2Cooldown = (int)(60 * timeInSeconds * cooldownReduction);
        }

        /// <summary>
        /// Sets the Player in combat for 3s, increasing ult charge.
        /// </summary>
        public void InBattle()
        {
            inBattleTimer = outOfBattleTimeMax;
            outOfBattleTimer = inBattleTimeMax;
        }

        /// <summary>
        /// Call this to easily heal players.
        /// <para>healAmount is automatically set to 1 if it is lower than 1.</para>
        /// </summary>
        /// <param name="healType">Determines who is healed<para>0 = Heal Self</para><para>1 = Heal All Players</para>2 = Heal Lowest Health(WIP)</param>
        /// <param name="healSpeed">Determines how fast the player is healed<para>0 = Instant</para><para>1 = Snail</para><para>2 = Slow</para><para>3 = Medium</para><para>4 = Fast</para></param>
        /// <param name="healAmount">The amount the player is healed</param>
        public void HealPlayer(int healType, int healSpeed, int healAmount)
        {
            if (healAmount < 1)
                healAmount = 1;

            //Multiplayer
            if (Main.netMode == NetmodeID.MultiplayerClient) 
            {
                // Heal Self Only
                if (healType == 0)
                {
                    switch(healSpeed)
                    {
                        case 0: // Instant
                            Player.statLife += healAmount;
                            Player.HealEffect(healAmount);
                            break;

                        case 1: // Snail Speed
                            healthToRegenSnail += healAmount;
                            Player.HealEffect(healAmount);
                            break;

                        case 2: // Slow Speed
                            healthToRegenSlow += healAmount;
                            Player.HealEffect(healAmount);
                            break;

                        case 3: // Medium Speed
                            healthToRegenMedium += healAmount;
                            Player.HealEffect(healAmount);
                            break;

                        case 4: // Fast Speed
                            healthToRegen += healAmount;
                            Player.HealEffect(healAmount);
                            break;
                    }
                }

                // Heal All Players
                if (healType == 1)
                {
                    switch (healSpeed)
                    {
                        case 0: // Instant
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayer);
                                    packet.Write(i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 1: // Snail Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerSnail);
                                    packet.Write(i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 2: // Slow Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerSlow);
                                    packet.Write(i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 3: // Medium Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerMedium);
                                    packet.Write(i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;

                        case 4: // Fast Speed
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    ModPacket packet = Mod.GetPacket();
                                    packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayerFast);
                                    packet.Write(i);
                                    packet.Write(healAmount);
                                    packet.Send(-1, -1);
                                }
                            break;
                    }
                }

                // Heal Lowest Health
                if (healType == 2)
                {
                    switch (healSpeed)
                    {
                        case 0: // Instant
                            int curHP = 99999999;
                            int selectedPlayer = -1;
                            for (int i = 0; i < 255; i++)
                                if (Main.player[i].active && !Main.player[i].dead)
                                {
                                    if(Main.player[i].statLife < curHP)
                                    {
                                        curHP = Main.player[i].statLife;
                                        selectedPlayer = i;
                                    }

                                    if(i == 255)
                                    {
                                        ModPacket packet = Mod.GetPacket();
                                        packet.Write((byte)ACM2.ACMHandlePacketMessage.HealPlayer);
                                        packet.Write(selectedPlayer);
                                        packet.Write(healAmount);
                                        packet.Send(-1, -1);
                                    }
                                }
                            break;

                        case 1: // Snail Speed

                            break;

                        case 2: // Slow Speed

                            break;

                        case 3: // Medium Speed

                            break;

                        case 4: // Fast Speed

                            break;
                    }
                }
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)// Singleplayer
            {
                // Heal Self Only
                switch (healSpeed)
                {
                    case 0: // Instant
                        Player.statLife += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 1: // Snail Speed
                        healthToRegenSnail += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 2: // Slow Speed
                        healthToRegenSlow += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 3: // Medium Speed
                        healthToRegenMedium += healAmount;
                        Player.HealEffect(healAmount);
                        break;

                    case 4: // Fast Speed
                        healthToRegen += healAmount;
                        Player.HealEffect(healAmount);
                        break;
                }
            }
        }

        void ApplyBuffToAllPlayers(int buffType, int duration)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead)
                    {
                        ModPacket packet = Mod.GetPacket();
                        packet.Write((byte)ACM2.ACMHandlePacketMessage.BuffPlayer);
                        packet.Write(i);
                        packet.Write(buffType);
                        packet.Write(duration);
                        packet.Send(-1, -1);
                    }
                }
            }

            if(Main.netMode == NetmodeID.SinglePlayer)
            {
                Player.AddBuff(buffType, duration);
            }
        }
    }
}