using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWars
{
    class AssetKit
    {
        public string ASSET_NAME = "ERROR_ASSETKIT_NOT_INTIALIZED";

        public int SRC_RECTX = 0;
        public int SRC_RECTY = 0;
        public int SRC_RECTWIDTH = 0;
        public int SRC_RECTHEIGHT = 0;

        public Color COLOR_BLENDING = Color.White;

        public Vector2 ORIGIN = Vector2.Zero;
    }

    class AssetKit_Ship_AGI_01 : AssetKit
    {
        public AssetKit_Ship_AGI_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 19;
            SRC_RECTY = 25;
            SRC_RECTWIDTH = 26;
            SRC_RECTHEIGHT = 19;

            COLOR_BLENDING = new Color(255, 230, 23);

            ORIGIN = new Vector2(13, 7);
        }
    }

    class AssetKit_Ship_Commando : AssetKit
    {
        public AssetKit_Ship_Commando()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 402;
            SRC_RECTY = 22;
            SRC_RECTWIDTH = 26;
            SRC_RECTHEIGHT = 22;

            COLOR_BLENDING = new Color(255, 230, 23);

            ORIGIN = new Vector2(13, 11);
        }
    }

    class AssetKit_Ship_PTank : AssetKit
    {
        public AssetKit_Ship_PTank()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 465;
            SRC_RECTY = 22;
            SRC_RECTWIDTH = 30;
            SRC_RECTHEIGHT = 22;

            COLOR_BLENDING = new Color(255, 230, 23);

            ORIGIN = new Vector2(15, 11);
        }
    }

    class AssetKit_Ship_GRUNT_01 : AssetKit
    {
        public AssetKit_Ship_GRUNT_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 85;
            SRC_RECTY = 20;
            SRC_RECTWIDTH = 22;
            SRC_RECTHEIGHT = 23;

            COLOR_BLENDING = new Color(0, 132, 255);

            ORIGIN = new Vector2(11, 12);
        }
    }

    class AssetKit_Ship_GRUNT_BOSS : AssetKit
    {
        public AssetKit_Ship_GRUNT_BOSS()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 192;
            SRC_RECTWIDTH = 256;
            SRC_RECTHEIGHT = 128;

            COLOR_BLENDING = new Color(0, 132, 255);

            ORIGIN = new Vector2(128, 64);
        }
    }

    class AssetKit_Ship_TANK_01 : AssetKit
    {
        public AssetKit_Ship_TANK_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 336;
            SRC_RECTY = 19;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 36;

            COLOR_BLENDING = new Color(106, 236, 10);

            ORIGIN = new Vector2(16, 13);
        }
    }

    class AssetKit_Ship_TANK_BOSS : AssetKit
    {
        public AssetKit_Ship_TANK_BOSS()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 320;
            SRC_RECTWIDTH = 256;
            SRC_RECTHEIGHT = 128;

            COLOR_BLENDING = new Color(106, 236, 10);

            ORIGIN = new Vector2(128, 64);
        }
    }

    class AssetKit_Ship_BOMB_01 : AssetKit
    {
        public AssetKit_Ship_BOMB_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 212;
            SRC_RECTY = 21;
            SRC_RECTWIDTH = 24;
            SRC_RECTHEIGHT = 24;

            COLOR_BLENDING = new Color(255, 42, 0);

            ORIGIN = new Vector2(12, 11);

        }
    }

    class AssetKit_Ship_BOMB_BOSS_01 : AssetKit
    {
        public AssetKit_Ship_BOMB_BOSS_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 296;
            SRC_RECTY = 216;
            SRC_RECTWIDTH = 176;
            SRC_RECTHEIGHT = 82;

            COLOR_BLENDING = new Color(255, 42, 0);

            ORIGIN = new Vector2(88, 41);
        }
    }

    class AssetKit_Ship_STEALTH_01 : AssetKit
    {
        public AssetKit_Ship_STEALTH_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 139;
            SRC_RECTY = 21;
            SRC_RECTWIDTH = 42;
            SRC_RECTHEIGHT = 24;

            COLOR_BLENDING = new Color(106, 10, 203);

            ORIGIN = new Vector2(21, 11);

        }
    }

    class AssetKit_Ship_LASER_01 : AssetKit
    {
        public AssetKit_Ship_LASER_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 276;
            SRC_RECTY = 18;
            SRC_RECTWIDTH = 24;
            SRC_RECTHEIGHT = 29;

            COLOR_BLENDING = new Color(23,232,161);

            ORIGIN = new Vector2(12, 18);

        }
    }

    class AssetKit_Proj_Bullet_01 : AssetKit
    {
        public AssetKit_Proj_Bullet_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 93;
            SRC_RECTY = 93;
            SRC_RECTWIDTH = 6;
            SRC_RECTHEIGHT = 20;

            ORIGIN = new Vector2(3, 3);
        }
    }

    class AssetKit_Proj_PlayerBullet_01 : AssetKit
    {
        public AssetKit_Proj_PlayerBullet_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 349;
            SRC_RECTY = 93;
            SRC_RECTWIDTH = 6;
            SRC_RECTHEIGHT = 20;

            ORIGIN = new Vector2(3, 3);
        }
    }

    class AssetKit_Proj_AntiBullet : AssetKit
    {
        public AssetKit_Proj_AntiBullet()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 477;
            SRC_RECTY = 93;
            SRC_RECTWIDTH = 6;
            SRC_RECTHEIGHT = 20;

            ORIGIN = new Vector2(3, 3);
        }
    }

    class AssetKit_Proj_Missile_01 : AssetKit
    {
        public AssetKit_Proj_Missile_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 156;
            SRC_RECTY = 89;
            SRC_RECTWIDTH = 8;
            SRC_RECTHEIGHT = 18;

            ORIGIN = new Vector2(4, 7);
        }
    }

    class AssetKit_Proj_Mine_01 : AssetKit
    {
        public AssetKit_Proj_Mine_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 217;
            SRC_RECTY = 89;
            SRC_RECTWIDTH = 14;
            SRC_RECTHEIGHT = 14;
            ORIGIN = new Vector2(7, 7);

            COLOR_BLENDING = new Color(106, 10, 203);
        }
    }

    class AssetKit_Proj_Laser_01 : AssetKit
    {
        public AssetKit_Proj_Laser_01()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 277;
            SRC_RECTY = 85;
            SRC_RECTWIDTH = 22;
            SRC_RECTHEIGHT = 22;
            ORIGIN = new Vector2(11, 11);
        }
    }

    class AssetKit_UI_Aimer : AssetKit
    {
        public AssetKit_UI_Aimer()
            : base()
        {
            ASSET_NAME = "UI_Element_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 25;
            SRC_RECTWIDTH = 80;
            SRC_RECTHEIGHT = 80;
            ORIGIN = new Vector2(40, 45);
        }
    }

    class AssetKit_UI_Crosshair : AssetKit
    {
        public AssetKit_UI_Crosshair()
            : base()
        {
            ASSET_NAME = "UI_Element_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 24;
            SRC_RECTHEIGHT = 24;
            ORIGIN = new Vector2(12, 12);
        }
    }

    class AssetKit_Menu_Title : AssetKit
    {
        public AssetKit_Menu_Title()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 131;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 130;
            SRC_RECTHEIGHT = 37;
            ORIGIN = new Vector2(65, 27);
        }
    }

    class AssetKit_Menu_Button_StartGame : AssetKit
    {
        public AssetKit_Menu_Button_StartGame()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 37;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0,0);
        }
    }
    class AssetKit_Menu_Button_Instructions : AssetKit
    {
        public AssetKit_Menu_Button_Instructions()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 77;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Menu_Button_Scores : AssetKit
    {
        public AssetKit_Menu_Button_Scores()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 117;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Menu_Button_Settings : AssetKit
    {
        public AssetKit_Menu_Button_Settings()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 201;
            SRC_RECTY = 37;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }
    class AssetKit_Menu_Button_Quit : AssetKit
    {
        public AssetKit_Menu_Button_Quit()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 201;
            SRC_RECTY = 77;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_UI_HealthBar_Border : AssetKit
    {
        public AssetKit_UI_HealthBar_Border()
            : base()
        {
            ASSET_NAME = "UI_Element_Health";

            SRC_RECTX = 0;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 288;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(144, 16);
        }
    }

    class AssetKit_UI_HealthBar_Bar : AssetKit
    {
        public AssetKit_UI_HealthBar_Bar()
            : base()
        {
            ASSET_NAME = "UI_Element_Health";

            SRC_RECTX = 0;
            SRC_RECTY = 32;
            SRC_RECTWIDTH = 244;
            SRC_RECTHEIGHT = 20;
            ORIGIN = new Vector2(122, 10);
        }
    }

    class AssetKit_UI_HealthBar_BarWhite : AssetKit
    {
        public AssetKit_UI_HealthBar_BarWhite()
            : base()
        {
            ASSET_NAME = "UI_Element_Health";

            SRC_RECTX = 0;
            SRC_RECTY = 52;
            SRC_RECTWIDTH = 244;
            SRC_RECTHEIGHT = 20;
            ORIGIN = new Vector2(122, 10);
        }
    }

    class AssetKit_Rules_Abilities : AssetKit
    {
        public AssetKit_Rules_Abilities()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 364;
            SRC_RECTY = 243;
            SRC_RECTWIDTH = 180;
            SRC_RECTHEIGHT = 165;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Rules_Movement : AssetKit
    {
        public AssetKit_Rules_Movement()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 182;
            SRC_RECTY = 242;
            SRC_RECTWIDTH = 181;
            SRC_RECTHEIGHT = 118;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Menu_BackButton : AssetKit
    {
        public AssetKit_Menu_BackButton()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 201;
            SRC_RECTY = 117;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_GameOver_Score : AssetKit
    {
        public AssetKit_GameOver_Score()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 182;
            SRC_RECTY = 450;
            SRC_RECTWIDTH = 320;
            SRC_RECTHEIGHT = 176;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_HighScore_Plaque : AssetKit
    {
        public AssetKit_HighScore_Plaque()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 402;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 238;
            SRC_RECTHEIGHT = 156;
            ORIGIN = new Vector2(119, 78);
        }
    }

    class AssetKit_CreditsPlaque : AssetKit
    {
        public AssetKit_CreditsPlaque()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 362;
            SRC_RECTWIDTH = 181;
            SRC_RECTHEIGHT = 264;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_BossShield : AssetKit
    {
        public AssetKit_BossShield()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 271;
            SRC_RECTY = 333;
            SRC_RECTWIDTH = 165;
            SRC_RECTHEIGHT = 165;
            ORIGIN = new Vector2(82, 82);
        }
    }

    class AssetKit_Satellite : AssetKit
    {
        public AssetKit_Satellite()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 280;
            SRC_RECTY = 152;
            SRC_RECTWIDTH = 17;
            SRC_RECTHEIGHT = 17;

            COLOR_BLENDING = new Color(255, 230, 23);

            ORIGIN = new Vector2(8, 8);
        }
    }

    class AssetKit_TankShield : AssetKit
    {
        public AssetKit_TankShield()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 320;
            SRC_RECTY = 128;
            SRC_RECTWIDTH = 64;
            SRC_RECTHEIGHT = 64;
            ORIGIN = new Vector2(32, 32);
        }
    }

    class AssetKit_ShipSelect : AssetKit
    {
        public AssetKit_ShipSelect()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 238;
            SRC_RECTHEIGHT = 174;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_AgilityIcon : AssetKit
    {
        public AssetKit_AgilityIcon()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 192;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_TankIcon : AssetKit
    {
        public AssetKit_TankIcon()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 32;
            SRC_RECTY = 192;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_CommandoIcon : AssetKit
    {
        public AssetKit_CommandoIcon()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 64;
            SRC_RECTY = 192;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_RightArrowButton : AssetKit
    {
        public AssetKit_RightArrowButton()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 225;
            SRC_RECTWIDTH = 13;
            SRC_RECTHEIGHT = 26;
            ORIGIN = new Vector2(0, 0);
        }
    }
    
    class AssetKit_LeftArrowButton : AssetKit
    {
        public AssetKit_LeftArrowButton()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 252;
            SRC_RECTWIDTH = 13;
            SRC_RECTHEIGHT = 26;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_AbilityButton : AssetKit
    {
        public AssetKit_AbilityButton()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 28;
            SRC_RECTY = 225;
            SRC_RECTWIDTH = 26;
            SRC_RECTHEIGHT = 26;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_PlayButton : AssetKit
    {
        public AssetKit_PlayButton()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 402;
            SRC_RECTY = 157;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_HealthPickup : AssetKit
    {
        public AssetKit_HealthPickup()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 384;
            SRC_RECTY = 64;
            SRC_RECTWIDTH = 22;
            SRC_RECTHEIGHT = 22;
            ORIGIN = new Vector2(11, 11);
        }
    }

    class AssetKit_Tier2Pickup : AssetKit
    {
        public AssetKit_Tier2Pickup()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 406;
            SRC_RECTY = 64;
            SRC_RECTWIDTH = 22;
            SRC_RECTHEIGHT = 22;
            ORIGIN = new Vector2(11, 11);
        }
    }

    class AssetKit_Tier3Pickup : AssetKit
    {
        public AssetKit_Tier3Pickup()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 384;
            SRC_RECTY = 87;
            SRC_RECTWIDTH = 22;
            SRC_RECTHEIGHT = 22;
            ORIGIN = new Vector2(11, 11);
        }
    }

    class AssetKit_Tier4Pickup : AssetKit
    {
        public AssetKit_Tier4Pickup()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 406;
            SRC_RECTY = 87;
            SRC_RECTWIDTH = 22;
            SRC_RECTHEIGHT = 22;
            ORIGIN = new Vector2(11, 11);
        }
    }

    class AssetKit_Rules_PickUps : AssetKit
    {
        public AssetKit_Rules_PickUps()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 242;
            SRC_RECTWIDTH = 181;
            SRC_RECTHEIGHT = 118;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Paused_Plaque : AssetKit
    {
        public AssetKit_Paused_Plaque()
            : base()
        {
            ASSET_NAME = "Menu_MASTER";

            SRC_RECTX = 182;
            SRC_RECTY = 361;
            SRC_RECTWIDTH = 100;
            SRC_RECTHEIGHT = 39;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_AbilitySelect : AssetKit
    {
        public AssetKit_AbilitySelect()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 239;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 238;
            SRC_RECTHEIGHT = 295;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_ShipMenu_Help : AssetKit
    {
        public AssetKit_ShipMenu_Help()
            : base()
        {
            ASSET_NAME = "ShipMenu_MASTER";

            SRC_RECTX = 239;
            SRC_RECTY = 297;
            SRC_RECTWIDTH = 238;
            SRC_RECTHEIGHT = 174;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_QuickTurn : AssetKit
    {
        public AssetKit_Ability_QuickTurn()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_WarpDrive : AssetKit
    {
        public AssetKit_Ability_WarpDrive()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 32;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_GunOvercharge : AssetKit
    {
        public AssetKit_Ability_GunOvercharge()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 128;
            SRC_RECTY = 33;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_BulletStar : AssetKit
    {
        public AssetKit_Ability_BulletStar()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 33;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_BulletFurry : AssetKit
    {
        public AssetKit_Ability_BulletFurry()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 96;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Locked : AssetKit
    {
        public AssetKit_Ability_Locked()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 128;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_DualWield : AssetKit
    {
        public AssetKit_Ability_DualWield()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 64;
            SRC_RECTY = 33;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_OverchargeEngines : AssetKit
    {
        public AssetKit_Ability_OverchargeEngines()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 66;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_FullShield : AssetKit
    {
        public AssetKit_Ability_FullShield()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 96;
            SRC_RECTY = 66;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_TighterTurns : AssetKit
    {
        public AssetKit_Ability_TighterTurns()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 99;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_EnergyShield : AssetKit
    {
        public AssetKit_Ability_EnergyShield()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 32;
            SRC_RECTY = 99;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_ShotgunBlast : AssetKit
    {
        public AssetKit_Ability_ShotgunBlast()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 64;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_RearGuns : AssetKit
    {
        public AssetKit_Ability_RearGuns()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 64;
            SRC_RECTY = 99;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_AntiBullets : AssetKit
    {
        public AssetKit_Ability_AntiBullets()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 96;
            SRC_RECTY = 99;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Satellite : AssetKit
    {
        public AssetKit_Ability_Satellite()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 96;
            SRC_RECTY = 33;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_MissileFlurry : AssetKit
    {
        public AssetKit_Ability_MissileFlurry()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 64;
            SRC_RECTY = 66;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Mine : AssetKit
    {
        public AssetKit_Ability_Mine()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 128;
            SRC_RECTY = 66;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Slot1 : AssetKit
    {
        public AssetKit_Ability_Slot1()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 160;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Slot2 : AssetKit
    {
        public AssetKit_Ability_Slot2()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 192;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Slot3 : AssetKit
    {
        public AssetKit_Ability_Slot3()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 224;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_Ability_Slot4 : AssetKit
    {
        public AssetKit_Ability_Slot4()
            : base()
        {
            ASSET_NAME = "Ability_Icons_MASTER";

            SRC_RECTX = 256;
            SRC_RECTY = 0;
            SRC_RECTWIDTH = 32;
            SRC_RECTHEIGHT = 32;
            ORIGIN = new Vector2(0, 0);
        }
    }

    class AssetKit_FullSheild : AssetKit
    {
        public AssetKit_FullSheild()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 384;
            SRC_RECTY = 128;
            SRC_RECTWIDTH = 64;
            SRC_RECTHEIGHT = 64;
            ORIGIN = new Vector2(32, 32);
        }
    }

    class AssetKit_PlayerMine : AssetKit
    {
        public AssetKit_PlayerMine()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 473;
            SRC_RECTY = 153;
            SRC_RECTWIDTH = 14;
            SRC_RECTHEIGHT = 14;
            ORIGIN = new Vector2(7, 7);

            COLOR_BLENDING = new Color(255, 230, 23);
        }
    }
}
