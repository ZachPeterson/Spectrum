using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class KitManager
    {
        public static AssetKit_Ability_BulletFurry BulletFuryAbilityAssetKit = new AssetKit_Ability_BulletFurry();
        public static AssetKit_Ability_BulletStar BulletStarAbilityAssetKit = new AssetKit_Ability_BulletStar();
        public static AssetKit_Ability_DualWield DualWieldAbilityAssetKit = new AssetKit_Ability_DualWield();
        public static AssetKit_Ability_FullShield FullShieldAbilityAssetKit = new AssetKit_Ability_FullShield();
        public static AssetKit_Ability_GunOvercharge GunOverchargeAbilityAssetKit = new AssetKit_Ability_GunOvercharge();
        public static AssetKit_Ability_Locked LockedAbilityAssetKit = new AssetKit_Ability_Locked();
        public static AssetKit_Ability_MissileFlurry MissileFlurryAbilityAssetKit = new AssetKit_Ability_MissileFlurry();
        public static AssetKit_Ability_OverchargeEngines OverchargeEnginesAbilityAssetKit = new AssetKit_Ability_OverchargeEngines();
        public static AssetKit_Ability_QuickTurn QuickTurnAbilityAssetKit = new AssetKit_Ability_QuickTurn();
        public static AssetKit_Ability_Slot1 Slot1AbilityAssetKit = new AssetKit_Ability_Slot1();
        public static AssetKit_Ability_Slot2 Slot2AbilityAssetKit = new AssetKit_Ability_Slot2();
        public static AssetKit_Ability_Slot3 Slot3AbilityAssetKit = new AssetKit_Ability_Slot3();
        public static AssetKit_Ability_Slot4 Slot4AbilityAssetKit = new AssetKit_Ability_Slot4();
        public static AssetKit_Ability_WarpDrive WarpDriveAbilityAssetKit = new AssetKit_Ability_WarpDrive();
        public static AssetKit_AbilityButton AbilityButtonAssetKit = new AssetKit_AbilityButton();
        public static AssetKit_AbilitySelect AbilitySelectAssetKit = new AssetKit_AbilitySelect();
        public static AssetKit_AgilityIcon AgilityIconAssetKit = new AssetKit_AgilityIcon();
        public static AssetKit_BossShield BossShieldAssetKit = new AssetKit_BossShield();
        public static AssetKit_CommandoIcon CommandoIconAssetKit = new AssetKit_CommandoIcon();
        public static AssetKit_CreditsPlaque CreditsPlaqueAssetKit = new AssetKit_CreditsPlaque();
        public static AssetKit_GameOver_Score GameOverScoreAssetKit = new AssetKit_GameOver_Score();
        public static AssetKit_HealthPickup HealthPickupAssetKit = new AssetKit_HealthPickup();
        public static AssetKit_HighScore_Plaque HighScorePlaqueAssetKit = new AssetKit_HighScore_Plaque();
        public static AssetKit_LeftArrowButton LeftArrowButtonAssetKit = new AssetKit_LeftArrowButton();
        public static AssetKit_Menu_BackButton MenuBackButtonAssetKit = new AssetKit_Menu_BackButton();
        public static AssetKit_Menu_Button_Instructions MenuInstructionsButtonAssetKit = new AssetKit_Menu_Button_Instructions();
        public static AssetKit_Menu_Button_Quit MenuQuitButtonAssetKit = new AssetKit_Menu_Button_Quit();
        public static AssetKit_Menu_Button_Scores MenuScoresButtonAssetKit = new AssetKit_Menu_Button_Scores();
        public static AssetKit_Menu_Button_Settings SettingsMenuButtonAssetKit = new AssetKit_Menu_Button_Settings();
        public static AssetKit_Menu_Button_StartGame StartGameMenuButtonAssetKit = new AssetKit_Menu_Button_StartGame();
        public static AssetKit_Menu_Title MenuTitleAssetKit = new AssetKit_Menu_Title();
        public static AssetKit_Paused_Plaque PausedPlaqueAssetKit = new AssetKit_Paused_Plaque();
        public static AssetKit_PlayButton PlayButtonAssetKit = new AssetKit_PlayButton();
        public static AssetKit_Proj_Bullet_01 BulletProjectileAssetKit = new AssetKit_Proj_Bullet_01();
        public static AssetKit_Proj_Laser_01 LaserProjectileAssetKit = new AssetKit_Proj_Laser_01();
        public static AssetKit_Proj_Mine_01 MineProjectileAssetKit = new AssetKit_Proj_Mine_01();
        public static AssetKit_Proj_Missile_01 MissileProjectileAssetKit = new AssetKit_Proj_Missile_01();
        public static AssetKit_Proj_PlayerBullet_01 PlayerBulletProjectileAssetKit = new AssetKit_Proj_PlayerBullet_01();
        public static AssetKit_Proj_AntiBullet AntiBulletProjectileAssetKit = new AssetKit_Proj_AntiBullet();
        public static AssetKit_RightArrowButton RightArrowButtonAssetKit = new AssetKit_RightArrowButton();
        public static AssetKit_Rules_Abilities AbilitiesRulesAssetKit = new AssetKit_Rules_Abilities();
        public static AssetKit_Rules_Movement MovementRulesAssetKit = new AssetKit_Rules_Movement();
        public static AssetKit_Rules_PickUps PickUpsRulesAssetKit = new AssetKit_Rules_PickUps();
        public static AssetKit_Satellite SatelliteAssetKit = new AssetKit_Satellite();
        public static AssetKit_Ship_AGI_01 AgilityShipAssetKit = new AssetKit_Ship_AGI_01();
        public static AssetKit_Ship_BOMB_01 BombShipAssetKit = new AssetKit_Ship_BOMB_01();
        public static AssetKit_Ship_BOMB_BOSS_01 BombBossShipAssetKit = new AssetKit_Ship_BOMB_BOSS_01();
        public static AssetKit_Ship_Commando CommandoShipAssetKit = new AssetKit_Ship_Commando();
        public static AssetKit_Ship_GRUNT_01 GruntShipAssetKit = new AssetKit_Ship_GRUNT_01();
        public static AssetKit_Ship_GRUNT_BOSS GruntBossAssetKit = new AssetKit_Ship_GRUNT_BOSS();
        public static AssetKit_Ship_LASER_01 LaserAssetKit = new AssetKit_Ship_LASER_01();
        public static AssetKit_Ship_PTank PlayerTankAssetKit = new AssetKit_Ship_PTank();
        public static AssetKit_Ship_STEALTH_01 StealthShipAssetKit = new AssetKit_Ship_STEALTH_01();
        public static AssetKit_Ship_TANK_01 TankShipAssetKit = new AssetKit_Ship_TANK_01();
        public static AssetKit_Ship_TANK_BOSS TankBossShipAssetKit = new AssetKit_Ship_TANK_BOSS();
        public static AssetKit_ShipMenu_Help ShipMenuHelpAssetKit = new AssetKit_ShipMenu_Help();
        public static AssetKit_ShipSelect ShipSelectAssetKit = new AssetKit_ShipSelect();
        public static AssetKit_TankIcon TankIconAssetKit = new AssetKit_TankIcon();
        public static AssetKit_TankShield TankShieldAssetKit = new AssetKit_TankShield();
        public static AssetKit_Tier2Pickup Tier2PickupAssetKit = new AssetKit_Tier2Pickup();
        public static AssetKit_Tier3Pickup Tier3PickupAssetKit = new AssetKit_Tier3Pickup();
        public static AssetKit_Tier4Pickup Tier4PickupAssetKit = new AssetKit_Tier4Pickup();
        public static AssetKit_UI_Aimer AimerUIAssetKit = new AssetKit_UI_Aimer();
        public static AssetKit_UI_Crosshair CrosshairUIAssetKit = new AssetKit_UI_Crosshair();
        public static AssetKit_UI_HealthBar_Bar HealthBarUIAssetKit = new AssetKit_UI_HealthBar_Bar();
        public static AssetKit_UI_HealthBar_BarWhite HealthBarWhiteUIAssetKit = new AssetKit_UI_HealthBar_BarWhite();
        public static AssetKit_UI_HealthBar_Border HealthBarBorderUIAssetKit = new AssetKit_UI_HealthBar_Border();

        public static GameEffectKit_Explosion_01 Explosion01GameEffectKit = new GameEffectKit_Explosion_01();
        public static GameEffectKit_Explosion_02 Explosion02GameEffectKit = new GameEffectKit_Explosion_02();
        public static GameEffectKit_QuickFix QuickFixGameEffectKit = new GameEffectKit_QuickFix();

        public static ProjectileKit_Enemy_Bullet EnemyBulletProjectileKit = new ProjectileKit_Enemy_Bullet();
        public static ProjectileKit_Laser LaserProjectileKit = new ProjectileKit_Laser();
        public static ProjectileKit_Antibullet AntibulletProjectileKit = new ProjectileKit_Antibullet();
        public static ProjectileKit_Mine MineProjectileKit = new ProjectileKit_Mine();
        public static ProjectileKit_Missile MissileProjectileKit = new ProjectileKit_Missile();
        public static ProjectileKit_Player_Bullet PlayerBulletProjectileKit = new ProjectileKit_Player_Bullet();

        public static ShipKit_Bomb BombShipKit = new ShipKit_Bomb();
        public static ShipKit_Bomb_Boss BombBossShipKit = new ShipKit_Bomb_Boss();
        public static ShipKit_Grunt GruntShipKit = new ShipKit_Grunt();
        public static ShipKit_Grunt_Boss GruntBossShipKit = new ShipKit_Grunt_Boss();
        public static ShipKit_Laser LaserShipKit = new ShipKit_Laser();
        public static ShipKit_Player PlayerShipKit = new ShipKit_Player();
        public static ShipKit_Player_Agility AgilityPlayerShipKit = new ShipKit_Player_Agility();
        public static ShipKit_Player_Commando CommandoPlayerShipKit = new ShipKit_Player_Commando();
        public static ShipKit_Player_Tank TankPlayerShipKit = new ShipKit_Player_Tank();
        public static ShipKit_Satellite SatelliteShipKit = new ShipKit_Satellite();
        public static ShipKit_Stealth StealthShipKit = new ShipKit_Stealth();
        public static ShipKit_Tank TankShipKit = new ShipKit_Tank();
        public static ShipKit_Tank_Boss TankBossShipKit = new ShipKit_Tank_Boss();
    }
}
