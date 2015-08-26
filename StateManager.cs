using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceWars
{
    class State
    {
        public Game1 mGameReference;

        public State(Game1 pGame)
        {
            mGameReference = pGame;
        }

        public virtual void EnterState()
        {
            ClearDisplayLists();
        }

        public virtual void Update(float DT)
        {
        }

        public virtual void LeaveState()
        {
        }

        public void ClearDisplayLists()
        {
            if (mGameReference != null)
            {
                if (mGameReference.aaDisplay != null)
                {
                    if (mGameReference.aaDisplay.mDisplayObjects != null)
                    {
                        mGameReference.aaDisplay.mDisplayObjects.Clear();
                    }
                    if (mGameReference.aaDisplay.mMenuUIElements != null)
                    {
                        mGameReference.aaDisplay.mMenuUIElements.Clear();
                    }
                    if (mGameReference.aaDisplay.mUIElements != null)
                    {
                        mGameReference.aaDisplay.mUIElements.Clear();
                    }
                    if (mGameReference.aaDisplay.mVisibleUIElements != null)
                    {
                        mGameReference.aaDisplay.mVisibleUIElements.Clear();
                    }
                }
            }
        }
    }

    class StateManager
    {
        private State mCurrentState;

        public StateManager()
        {
        }

        public void ChangeState(State pNewState)
        {
            if (mCurrentState != null)
            {
                mCurrentState.LeaveState();
            }
            mCurrentState = pNewState;
            mCurrentState.EnterState();
        }

        public void UpdateState(float DT)
        {
            if (mCurrentState != null)
            {
                mCurrentState.Update(DT);
            }
        }

        public State GetCurrentState()
        {
            return mCurrentState;
        }
    }

    class State_GameState : State
    {
        public GameWorld aaGameWorld;

        public State_GameState(Game1 pGame)
            : base(pGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            mGameReference.IsMouseVisible = false;

            //Music plug
            mGameReference.mMusicMenu_Instance.Stop();
            mGameReference.mMusicGameplay_Instance.Play();

            aaGameWorld = new GameWorld(mGameReference.aaDisplay);
            aaGameWorld.Initialize();
            mGameReference.aaGameWorld = aaGameWorld;
        }

        public override void Update(float DT)
        {
            base.Update(DT);

            aaGameWorld.UpdateWorld(DT);
        }

        public override void LeaveState()
        {
            base.LeaveState();

            mGameReference.aaGameWorld.Destroy();

            mGameReference.aaGameWorld = null;
            aaGameWorld = null;

            mGameReference.IsMouseVisible = true;
        }
    }

    class State_SplashState : State
    {
        private float mDisplayTime = 5.0f;
        private float mFadeTime = 1.0f;
        private float mElapsedTime;

        private DisplayObject mSplashLogo;

        public State_SplashState(Game1 pGame)
            : base(pGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            mElapsedTime = 0.0f;

            mSplashLogo = new DisplayObject(mGameReference.aaDisplay, "Zelbyte", 0, 0, 0, 0, 1.0f, 0, 0, 1080, 720, 0, false);
            mSplashLogo.SetBlendColor(Color.White);
            mSplashLogo.FadeToAmount(0.0f);
        }

        public override void Update(float DT)
        {
            base.Update(DT);

            mElapsedTime += DT;

            if (mElapsedTime <= mFadeTime)
            {
                float fadeAmount = mElapsedTime / mFadeTime;
                mSplashLogo.FadeToAmount(fadeAmount);
            }
            else if (mElapsedTime >= (mDisplayTime - mFadeTime) && mElapsedTime <= mDisplayTime)
            {
                float fadeAmount = 1 - ((mElapsedTime - (mDisplayTime - mFadeTime)) / mFadeTime);
                mSplashLogo.FadeToAmount(fadeAmount);
            }
            else
            {
                mSplashLogo.FadeToAmount(1.0f);
            }

            if (mElapsedTime >= mDisplayTime || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
            }
        }

        public override void LeaveState()
        {
            base.LeaveState();

            mSplashLogo.Hide();
            mGameReference.aaDisplay.RemoveFromDisplayList(mSplashLogo);
        }
    }

    class State_Menu_Main : State
    {
        UIElement_StaticMenuElement gameTitle;
        AssetKit_Menu_Title titleKit;

        UIElement_MenuButton startButton;
        AssetKit_Menu_Button_StartGame startKit;

        UIElement_MenuButton instructionButton;
        AssetKit_Menu_Button_Instructions instructionKit;

        UIElement_MenuButton scoresButton;
        AssetKit_Menu_Button_Scores scoresKit;

        UIElement_MenuButton quitButton;
        AssetKit_Menu_Button_Quit quitKit;

        public State_Menu_Main(Game1 pGame)
            : base(pGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            //Music plug
            mGameReference.mMusicGameplay_Instance.Stop();
            mGameReference.mMusicMenu_Instance.Play();

            titleKit = new AssetKit_Menu_Title();
            gameTitle = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, titleKit.ASSET_NAME, 640, 180, (int)titleKit.ORIGIN.X, (int)titleKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, titleKit.SRC_RECTX, titleKit.SRC_RECTY, titleKit.SRC_RECTWIDTH, titleKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(gameTitle);

            startKit = new AssetKit_Menu_Button_StartGame();
            startButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, startKit.ASSET_NAME, 540, 260, (int)startKit.ORIGIN.X, (int)startKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, startKit.SRC_RECTX, startKit.SRC_RECTY, startKit.SRC_RECTWIDTH, startKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(startButton);

            instructionKit = new AssetKit_Menu_Button_Instructions();
            instructionButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, instructionKit.ASSET_NAME, 540, 350, (int)instructionKit.ORIGIN.X, (int)instructionKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, instructionKit.SRC_RECTX, instructionKit.SRC_RECTY, instructionKit.SRC_RECTWIDTH, instructionKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(instructionButton);

            scoresKit = new AssetKit_Menu_Button_Scores();
            scoresButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, scoresKit.ASSET_NAME, 540, 440, (int)scoresKit.ORIGIN.X, (int)scoresKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, scoresKit.SRC_RECTX, scoresKit.SRC_RECTY, scoresKit.SRC_RECTWIDTH, scoresKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(scoresButton);

            quitKit = new AssetKit_Menu_Button_Quit();
            quitButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, quitKit.ASSET_NAME, 540, 530, (int)quitKit.ORIGIN.X, (int)quitKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, quitKit.SRC_RECTX, quitKit.SRC_RECTY, quitKit.SRC_RECTWIDTH, quitKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(quitButton);
        }

        public override void LeaveState()
        {
            base.LeaveState();

            titleKit = null;
            gameTitle.MenuDestroy();
            gameTitle = null;

            startKit = null;
            startButton.MenuDestroy();
            startButton = null;

            instructionKit = null;
            instructionButton.MenuDestroy();
            instructionButton = null;

            scoresKit = null;
            scoresButton.MenuDestroy();
            scoresKit = null;

            quitKit = null;
            quitButton.MenuDestroy();
            quitButton = null;
        }

        public override void Update(float DT)
        {
            base.Update(DT);

            // TODO: Update menu stuff
            startButton.Update(DT);
            instructionButton.Update(DT);
            scoresButton.Update(DT);
            quitButton.Update(DT);

            if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
            {
                if (startButton.CheckButtonClick())
                {
                    mGameReference.mManager.ChangeState(mGameReference.mShipMenu);
                }
                else if (instructionButton.CheckButtonClick())
                {
                    mGameReference.mManager.ChangeState(mGameReference.mInstructionsMenu);
                }
                else if (scoresButton.CheckButtonClick())
                {
                    mGameReference.mManager.ChangeState(mGameReference.mHighScoreMenu);
                }
                else if (quitButton.CheckButtonClick())
                {
                    mGameReference.Exit();
                }
            }
        }
    }

    class State_Menu_Ship : State
    {
        public AbilityData Abilitydata;

        UIElement_MenuButton leftButton;
        AssetKit_LeftArrowButton leftButtonKit;
        UIElement_MenuButton rightButton;
        AssetKit_RightArrowButton rightButtonKit;
        UIElement_MenuButton backButton;
        AssetKit_Menu_BackButton backButtonKit;
        UIElement_MenuButton playButton;
        AssetKit_PlayButton playButtonKit;
        UIElement_MenuButton ability1Button;
        UIElement_MenuButton ability2Button;
        UIElement_MenuButton ability3Button;
        UIElement_MenuButton ability4Button;
        UIElement_MenuButton default1Button;
        UIElement_MenuButton default2Button;
        UIElement_MenuButton default3Button;
        UIElement_MenuButton default4Button;
        UIElement_MenuButton special1Button;
        UIElement_MenuButton special2Button;
        UIElement_MenuButton special3Button;
        UIElement_MenuButton special4Button;
        AssetKit_AbilityButton abilitykit;

        UIElement_StaticMenuElement ShipSelect;
        UIElement_StaticMenuElement AbilitySelect;
        UIElement_StaticMenuElement HelpPlaque;
        UIElement_StaticMenuElement ShipIcon;
        UIElement_StaticMenuElement Ability1;
        UIElement_StaticMenuElement Ability2;
        UIElement_StaticMenuElement Ability3;
        UIElement_StaticMenuElement Ability4;
        UIElement_StaticMenuElement DefaultAbility1;
        UIElement_StaticMenuElement DefaultAbility2;
        UIElement_StaticMenuElement DefaultAbility3;
        UIElement_StaticMenuElement DefaultAbility4;
        UIElement_StaticMenuElement SpecialAbility1;
        UIElement_StaticMenuElement SpecialAbility2;
        UIElement_StaticMenuElement SpecialAbility3;
        UIElement_StaticMenuElement SpecialAbility4;

        AssetKit_ShipSelect shipselectKit;
        AssetKit_ShipMenu_Help helpKit;
        AssetKit_AgilityIcon aiconKit;
        AssetKit_TankIcon ticonKit;
        AssetKit_CommandoIcon ciconKit;
        AssetKit_AbilitySelect abilityselectKit;
        AssetKit_Ability_QuickTurn quickturnKit;
        AssetKit_Ability_WarpDrive warpdriveKit;
        AssetKit_Ability_GunOvercharge gunoverchargeKit;
        AssetKit_Ability_BulletStar bulletstarKit;
        AssetKit_Ability_BulletFurry bulletfurryKit;
        AssetKit_Ability_DualWield dualwieldKit;
        AssetKit_Ability_OverchargeEngines overchargeenginesKit;
        AssetKit_Ability_MissileFlurry missileflurryKit;
        AssetKit_Ability_TighterTurns tighterturnsKit;
        AssetKit_Ability_EnergyShield energyshieldKit;
        AssetKit_Ability_ShotgunBlast shotgunblastKit;
        AssetKit_Ability_RearGuns reargunsKit;
        AssetKit_Ability_AntiBullets antibulletsKit;
        AssetKit_Ability_Satellite satelliteKit;
        AssetKit_Ability_Mine mineKit;
        AssetKit_Ability_FullShield fullshieldKit;
        AssetKit_Ability_Locked lockedKit;
        AssetKit_Ability_Slot1 slot1Kit;
        AssetKit_Ability_Slot2 slot2Kit;
        AssetKit_Ability_Slot3 slot3Kit;
        AssetKit_Ability_Slot4 slot4Kit;
        public AssetKit ability1;
        public AssetKit ability2;
        public AssetKit ability3;
        public AssetKit ability4;

        UIElement_Text mAbilityBox;
        UIElement_Text mDescriptionBox;
        UIElement_Text mHealth;
        UIElement_Text mMaxThrust;
        UIElement_Text mTurnSpeed;
        UIElement_Text mShipName;
        string mAbilityNameText;
        string mDesciprtionText;
        string health = "";
        string maxthrust = "";
        string turnspeed = "";
        string shipname = "";

        Vector2 textSize;

        public int shipCount;
        public int selectedAbility;
        public int special1ID;
        public int special2ID;
        public int special3ID;
        public int special4ID;
        public bool placingAbility = false;

        public State_Menu_Ship(Game1 pGame)
            : base(pGame)
        {
            shipCount = 0;
            mAbilityNameText = "";
            mDesciprtionText = "";
            selectedAbility = -1;
            special1ID = -1;
            special2ID = -1;
            special3ID = -2;
            special4ID = -3;
        }

        public override void EnterState()
        {
            base.EnterState();

            Abilitydata = mGameReference.aaDisplay.aaGame.aaHSManager.Abilitydata;

            lockedKit = new AssetKit_Ability_Locked();
            quickturnKit = new AssetKit_Ability_QuickTurn();
            warpdriveKit = new AssetKit_Ability_WarpDrive();
            gunoverchargeKit = new AssetKit_Ability_GunOvercharge();
            bulletstarKit = new AssetKit_Ability_BulletStar();
            bulletfurryKit = new AssetKit_Ability_BulletFurry();
            dualwieldKit = new AssetKit_Ability_DualWield();
            missileflurryKit = new AssetKit_Ability_MissileFlurry();
            overchargeenginesKit = new AssetKit_Ability_OverchargeEngines();
            tighterturnsKit = new AssetKit_Ability_TighterTurns();
            energyshieldKit = new AssetKit_Ability_EnergyShield();
            shotgunblastKit = new AssetKit_Ability_ShotgunBlast();
            reargunsKit = new AssetKit_Ability_RearGuns();
            antibulletsKit = new AssetKit_Ability_AntiBullets();
            satelliteKit = new AssetKit_Ability_Satellite();
            mineKit = new AssetKit_Ability_Mine();
            fullshieldKit = new AssetKit_Ability_FullShield();
            DefaultAbility1 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, quickturnKit.ASSET_NAME, 633, 335, (int)quickturnKit.ORIGIN.X, (int)quickturnKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, quickturnKit.SRC_RECTX, quickturnKit.SRC_RECTY, quickturnKit.SRC_RECTWIDTH, quickturnKit.SRC_RECTHEIGHT, 0.0f);
            DefaultAbility2 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, warpdriveKit.ASSET_NAME, 705, 335, (int)warpdriveKit.ORIGIN.X, (int)warpdriveKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, warpdriveKit.SRC_RECTX, warpdriveKit.SRC_RECTY, warpdriveKit.SRC_RECTWIDTH, warpdriveKit.SRC_RECTHEIGHT, 0.0f);
            DefaultAbility3 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, gunoverchargeKit.ASSET_NAME, 779, 335, (int)gunoverchargeKit.ORIGIN.X, (int)gunoverchargeKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, gunoverchargeKit.SRC_RECTX, gunoverchargeKit.SRC_RECTY, gunoverchargeKit.SRC_RECTWIDTH, gunoverchargeKit.SRC_RECTHEIGHT, 0.0f);
            DefaultAbility4 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, bulletstarKit.ASSET_NAME, 849, 335, (int)bulletstarKit.ORIGIN.X, (int)bulletstarKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, bulletstarKit.SRC_RECTX, bulletstarKit.SRC_RECTY, bulletstarKit.SRC_RECTWIDTH, bulletstarKit.SRC_RECTHEIGHT, 0.0f);
            SpecialAbility1 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, lockedKit.ASSET_NAME, 633, 482, (int)lockedKit.ORIGIN.X, (int)lockedKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, lockedKit.SRC_RECTX, lockedKit.SRC_RECTY, lockedKit.SRC_RECTWIDTH, lockedKit.SRC_RECTHEIGHT, 0.0f);
            SpecialAbility2 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, lockedKit.ASSET_NAME, 705, 482, (int)lockedKit.ORIGIN.X, (int)lockedKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, lockedKit.SRC_RECTX, lockedKit.SRC_RECTY, lockedKit.SRC_RECTWIDTH, lockedKit.SRC_RECTHEIGHT, 0.0f);
            SpecialAbility3 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, lockedKit.ASSET_NAME, 779, 482, (int)lockedKit.ORIGIN.X, (int)lockedKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, lockedKit.SRC_RECTX, lockedKit.SRC_RECTY, lockedKit.SRC_RECTWIDTH, lockedKit.SRC_RECTHEIGHT, 0.0f);
            SpecialAbility4 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, lockedKit.ASSET_NAME, 849, 482, (int)lockedKit.ORIGIN.X, (int)lockedKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, lockedKit.SRC_RECTX, lockedKit.SRC_RECTY, lockedKit.SRC_RECTWIDTH, lockedKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(DefaultAbility1);
            mGameReference.aaDisplay.AddMenuUIElement(DefaultAbility2);
            mGameReference.aaDisplay.AddMenuUIElement(DefaultAbility3);
            mGameReference.aaDisplay.AddMenuUIElement(DefaultAbility4);
            mGameReference.aaDisplay.AddMenuUIElement(SpecialAbility1);
            mGameReference.aaDisplay.AddMenuUIElement(SpecialAbility2);
            mGameReference.aaDisplay.AddMenuUIElement(SpecialAbility3);
            mGameReference.aaDisplay.AddMenuUIElement(SpecialAbility4);

            abilitykit = new AssetKit_AbilityButton();
            ability1Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 639, 206, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            ability2Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 711, 206, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            ability3Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 785, 206, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            ability4Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 855, 206, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            default1Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 639, 341, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            default2Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 711, 341, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            default3Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 785, 341, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            default4Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 855, 341, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            special1Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 639, 488, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            special2Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 711, 488, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            special3Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 785, 488, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            special4Button = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitykit.ASSET_NAME, 855, 488, (int)abilitykit.ORIGIN.X, (int)abilitykit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.2f, abilitykit.SRC_RECTX, abilitykit.SRC_RECTY, abilitykit.SRC_RECTWIDTH, abilitykit.SRC_RECTHEIGHT, 0.0f);
            ability1Button.highlightDisabled = true;
            ability2Button.highlightDisabled = true;
            ability3Button.highlightDisabled = true;
            ability4Button.highlightDisabled = true;
            mGameReference.aaDisplay.AddMenuUIElement(ability1Button);
            mGameReference.aaDisplay.AddMenuUIElement(ability2Button);
            mGameReference.aaDisplay.AddMenuUIElement(ability3Button);
            mGameReference.aaDisplay.AddMenuUIElement(ability4Button);
            mGameReference.aaDisplay.AddMenuUIElement(default1Button);
            mGameReference.aaDisplay.AddMenuUIElement(default2Button);
            mGameReference.aaDisplay.AddMenuUIElement(default3Button);
            mGameReference.aaDisplay.AddMenuUIElement(default4Button);
            mGameReference.aaDisplay.AddMenuUIElement(special1Button);
            mGameReference.aaDisplay.AddMenuUIElement(special2Button);
            mGameReference.aaDisplay.AddMenuUIElement(special3Button);
            mGameReference.aaDisplay.AddMenuUIElement(special4Button);

            backButtonKit = new AssetKit_Menu_BackButton();
            backButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, backButtonKit.ASSET_NAME, 1060, 370, (int)backButtonKit.ORIGIN.X, (int)backButtonKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, backButtonKit.SRC_RECTX, backButtonKit.SRC_RECTY, backButtonKit.SRC_RECTWIDTH, backButtonKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(backButton);
            
            rightButtonKit = new AssetKit_RightArrowButton();
            rightButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, rightButtonKit.ASSET_NAME, 325, 90, (int)rightButtonKit.ORIGIN.X, (int)rightButtonKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, rightButtonKit.SRC_RECTX, rightButtonKit.SRC_RECTY, rightButtonKit.SRC_RECTWIDTH, rightButtonKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(rightButton);

            leftButtonKit = new AssetKit_LeftArrowButton();
            leftButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, leftButtonKit.ASSET_NAME, 180, 90, (int)leftButtonKit.ORIGIN.X, (int)leftButtonKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, leftButtonKit.SRC_RECTX, leftButtonKit.SRC_RECTY, leftButtonKit.SRC_RECTWIDTH, leftButtonKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(leftButton);

            playButtonKit = new AssetKit_PlayButton();
            playButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, playButtonKit.ASSET_NAME, 1060, 270, (int)playButtonKit.ORIGIN.X, (int)playButtonKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, playButtonKit.SRC_RECTX, playButtonKit.SRC_RECTY, playButtonKit.SRC_RECTWIDTH, playButtonKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(playButton);

            aiconKit = new AssetKit_AgilityIcon();
            ticonKit = new AssetKit_TankIcon();
            ciconKit = new AssetKit_CommandoIcon();
            ShipIcon = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, aiconKit.ASSET_NAME, 235, 85, (int)aiconKit.ORIGIN.X, (int)aiconKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, aiconKit.SRC_RECTX, aiconKit.SRC_RECTY, aiconKit.SRC_RECTWIDTH, aiconKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(ShipIcon);

            shipselectKit = new AssetKit_ShipSelect();
            ShipSelect = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, shipselectKit.ASSET_NAME, 20, 10, (int)shipselectKit.ORIGIN.X, (int)shipselectKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, shipselectKit.SRC_RECTX, shipselectKit.SRC_RECTY, shipselectKit.SRC_RECTWIDTH, shipselectKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(ShipSelect);

            helpKit = new AssetKit_ShipMenu_Help();
            HelpPlaque = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, helpKit.ASSET_NAME, 20, 365, (int)helpKit.ORIGIN.X, (int)helpKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, helpKit.SRC_RECTX, helpKit.SRC_RECTY, helpKit.SRC_RECTWIDTH, helpKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(HelpPlaque);

            abilityselectKit = new AssetKit_AbilitySelect();
            AbilitySelect = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilityselectKit.ASSET_NAME, 535, 60, (int)abilityselectKit.ORIGIN.X, (int)abilityselectKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, abilityselectKit.SRC_RECTX, abilityselectKit.SRC_RECTY, abilityselectKit.SRC_RECTWIDTH, abilityselectKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(AbilitySelect);

            mAbilityBox = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mTextboxFont, 900 - (int)(textSize.X / 2), 560, Color.White, "");
            mDescriptionBox = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mDescriptionFont, 935 - (int)(textSize.X / 2), 600, Color.White, "");
            mGameReference.aaDisplay.AddMenuUIElement(mAbilityBox);
            mGameReference.aaDisplay.AddMenuUIElement(mDescriptionBox);

            mShipName = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mTextboxFont, 270 - (int)(textSize.X / 2), 185, Color.White, shipname);
            mHealth = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mDescription2Font, 131, 230, Color.White, "Health: " + health);
            mMaxThrust = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mDescription2Font, 132, 260, Color.White, "Max Thrust: " + maxthrust);
            mTurnSpeed = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mDescription2Font, 130, 290, Color.White, "Turn Speed: " + turnspeed);

            slot1Kit = new AssetKit_Ability_Slot1();
            slot2Kit = new AssetKit_Ability_Slot2();
            slot3Kit = new AssetKit_Ability_Slot3();
            slot4Kit = new AssetKit_Ability_Slot4();
            Ability1 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, slot1Kit.ASSET_NAME, 633, 200, (int)slot1Kit.ORIGIN.X, (int)slot1Kit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, slot1Kit.SRC_RECTX, slot1Kit.SRC_RECTY, slot1Kit.SRC_RECTWIDTH, slot1Kit.SRC_RECTHEIGHT, 0.0f);
            Ability2 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, slot1Kit.ASSET_NAME, 705, 200, (int)slot1Kit.ORIGIN.X, (int)slot1Kit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, slot1Kit.SRC_RECTX, slot1Kit.SRC_RECTY, slot1Kit.SRC_RECTWIDTH, slot1Kit.SRC_RECTHEIGHT, 0.0f);
            Ability3 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, slot1Kit.ASSET_NAME, 779, 200, (int)slot1Kit.ORIGIN.X, (int)slot1Kit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, slot1Kit.SRC_RECTX, slot1Kit.SRC_RECTY, slot1Kit.SRC_RECTWIDTH, slot1Kit.SRC_RECTHEIGHT, 0.0f);
            Ability4 = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, slot1Kit.ASSET_NAME, 849, 200, (int)slot1Kit.ORIGIN.X, (int)slot1Kit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH - 0.1f, slot1Kit.SRC_RECTX, slot1Kit.SRC_RECTY, slot1Kit.SRC_RECTWIDTH, slot1Kit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(Ability1);
            mGameReference.aaDisplay.AddMenuUIElement(Ability2);
            mGameReference.aaDisplay.AddMenuUIElement(Ability3);
            mGameReference.aaDisplay.AddMenuUIElement(Ability4);

            ShipChangeUpdate();
        }

        public override void LeaveState()
        {
            base.LeaveState();

            shipselectKit = null;
            aiconKit = null;
            ticonKit = null;
            ciconKit = null;
            ShipSelect.MenuDestroy();
            ShipSelect = null;

            leftButtonKit = null;
            leftButton.MenuDestroy();
            leftButton = null;

            rightButtonKit = null;
            rightButton.MenuDestroy();
            rightButton = null;

            backButtonKit = null;
            backButton.MenuDestroy();
            backButton = null;

            mAbilityBox.MenuDestroy();
            mAbilityBox = null;

            mGameReference.aaDisplay.mMenuUIElements.Clear();
        }

        public override void Update(float DT)
        {
            backButton.Update(DT);
            leftButton.Update(DT);
            rightButton.Update(DT);
            playButton.Update(DT);
            ability1Button.Update(DT);
            ability2Button.Update(DT);
            ability3Button.Update(DT);
            ability4Button.Update(DT);
            default1Button.Update(DT);
            default2Button.Update(DT);
            default3Button.Update(DT);
            default4Button.Update(DT);
            special1Button.Update(DT);
            special2Button.Update(DT);
            special3Button.Update(DT);
            special4Button.Update(DT);

            base.Update(DT);

            if (backButton.CheckButtonClick())
            {
                mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
                SaveData();
            }
            else if (playButton.CheckButtonClick())
            {
                if (!(Abilitydata.mAbility1[shipCount] < 0) && !(Abilitydata.mAbility2[shipCount] < 0) && !(Abilitydata.mAbility3[shipCount] < 0) && !(Abilitydata.mAbility4[shipCount] < 0))
                {
                    mGameReference.mManager.ChangeState(mGameReference.mGame);
                    mGameReference.aaDisplay.aaGame.aaHSManager.Abilitydata = Abilitydata;
                    mGameReference.aaHSManager.SaveAbilities();
                }
                else
                {
                    UIElement errorText = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.mDescription2Font, 570, 265, Color.Red, "You must fill all ability slots");
                    mGameReference.aaDisplay.AddMenuUIElement(errorText);
                }
            }
            else if (leftButton.CheckButtonClick())
            {
                shipCount--;
                FixShipCount();
                ShipChangeUpdate();
            }
            else if (rightButton.CheckButtonClick())
            {
                shipCount++;
                FixShipCount();
                ShipChangeUpdate();
            }
            else if (default1Button.CheckButtonClick())
            {
                UnhighlightAbilities();
                HighlightAbilities();
                default1Button.forceHighlight = true;
                selectedAbility = 0;
                placingAbility = true;
            }
            else if (default2Button.CheckButtonClick())
            {
                UnhighlightAbilities();
                HighlightAbilities();
                default2Button.forceHighlight = true;
                selectedAbility = 1;
                placingAbility = true;
            }
            else if (default3Button.CheckButtonClick())
            {
                UnhighlightAbilities();
                HighlightAbilities();
                default3Button.forceHighlight = true;
                selectedAbility = 2;
                placingAbility = true;
            }
            else if (default4Button.CheckButtonClick())
            {
                UnhighlightAbilities();
                HighlightAbilities();
                default4Button.forceHighlight = true;
                selectedAbility = 3;
                placingAbility = true;
            }
            else if (special1Button.CheckButtonClick() && special1ID >= 0)
            {
                UnhighlightAbilities();
                HighlightAbilities();
                special1Button.forceHighlight = true;
                selectedAbility = special1ID;
                placingAbility = true;
            }
            else if (special2Button.CheckButtonClick() && special2ID >= 0)
            {
                UnhighlightAbilities();
                HighlightAbilities();
                special2Button.forceHighlight = true;
                selectedAbility = special2ID;
                placingAbility = true;
            }
            else if (special3Button.CheckButtonClick() && special3ID >= 0)
            {
                UnhighlightAbilities();
                HighlightAbilities();
                special3Button.forceHighlight = true;
                selectedAbility = special3ID;
                placingAbility = true;
            }
            else if (special4Button.CheckButtonClick() && special4ID >= 0)
            {
                UnhighlightAbilities();
                HighlightAbilities();
                special4Button.forceHighlight = true;
                selectedAbility = special4ID;
                placingAbility = true;
            }

            if (default1Button.CheckButtonHover())
                UpdateDescriptionText(0);
            else if (default2Button.CheckButtonHover())
                UpdateDescriptionText(1);
            else if (default3Button.CheckButtonHover())
                UpdateDescriptionText(2);
            else if (default4Button.CheckButtonHover())
                UpdateDescriptionText(3);
            else if (special1Button.CheckButtonHover())
                UpdateDescriptionText(special1ID);
            else if (special2Button.CheckButtonHover())
                UpdateDescriptionText(special2ID);
            else if (special3Button.CheckButtonHover())
                UpdateDescriptionText(special3ID);
            else if (special4Button.CheckButtonHover())
                UpdateDescriptionText(special4ID);

            if (placingAbility)
            {
                if (ability1Button.CheckButtonClick() && selectedAbility >= 0)
                {
                    CheckExistance(selectedAbility, 1);
                    selectedAbility = -1;
                    UnhighlightAbilities();
                    ShipChangeUpdate();
                }
                else if (ability2Button.CheckButtonClick() && selectedAbility >= 0)
                {
                    CheckExistance(selectedAbility, 2);
                    selectedAbility = -1;
                    UnhighlightAbilities();
                    ShipChangeUpdate();
                }
                else if (ability3Button.CheckButtonClick() && selectedAbility >= 0)
                {
                    CheckExistance(selectedAbility, 3);
                    selectedAbility = -1;
                    UnhighlightAbilities();
                    ShipChangeUpdate();
                }
                else if (ability4Button.CheckButtonClick() && selectedAbility >= 0)
                {
                    CheckExistance(selectedAbility, 4);
                    selectedAbility = -1;
                    UnhighlightAbilities();
                    ShipChangeUpdate();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
            }
        }

        public void SaveData()
        {
            mGameReference.aaDisplay.aaGame.aaHSManager.Abilitydata = Abilitydata;
            mGameReference.aaHSManager.SaveAbilities();
        }

        public void CheckExistance(int abilityID, int slot)
        {
            switch (slot)
            {
                case 1:
                    if (Abilitydata.mAbility2[shipCount] == abilityID)
                        Abilitydata.mAbility2[shipCount] = -2;
                    else if (Abilitydata.mAbility3[shipCount] == abilityID)
                        Abilitydata.mAbility3[shipCount] = -3;
                    else if (Abilitydata.mAbility4[shipCount] == abilityID)
                        Abilitydata.mAbility4[shipCount] = -4;
                    Abilitydata.mAbility1[shipCount] = abilityID;
                    break;
                case 2:
                    if (Abilitydata.mAbility1[shipCount] == abilityID)
                        Abilitydata.mAbility1[shipCount] = -1;
                    if (Abilitydata.mAbility3[shipCount] == abilityID)
                        Abilitydata.mAbility3[shipCount] = -3;
                    if (Abilitydata.mAbility4[shipCount] == abilityID)
                        Abilitydata.mAbility4[shipCount] = -4;
                    Abilitydata.mAbility2[shipCount] = abilityID;
                    break;
                case 3:
                    if (Abilitydata.mAbility1[shipCount] == abilityID)
                        Abilitydata.mAbility1[shipCount] = -1;
                    if (Abilitydata.mAbility2[shipCount] == abilityID)
                        Abilitydata.mAbility2[shipCount] = -2;
                    if (Abilitydata.mAbility4[shipCount] == abilityID)
                        Abilitydata.mAbility4[shipCount] = -4;
                    Abilitydata.mAbility3[shipCount] = abilityID;
                    break;
                case 4:
                    if (Abilitydata.mAbility1[shipCount] == abilityID)
                        Abilitydata.mAbility1[shipCount] = -1;
                    if (Abilitydata.mAbility2[shipCount] == abilityID)
                        Abilitydata.mAbility2[shipCount] = -2;
                    if (Abilitydata.mAbility3[shipCount] == abilityID)
                        Abilitydata.mAbility3[shipCount] = -3;
                    Abilitydata.mAbility4[shipCount] = abilityID;
                    break;
            }
        }

        public void FixShipCount()
        {
            if (shipCount > 2)
                shipCount = 0;
            else if (shipCount < 0)
                shipCount = 2;
        }

        public void HighlightAbilities()
        {
            ability1Button.forceHighlight = true;
            ability2Button.forceHighlight = true;
            ability3Button.forceHighlight = true;
            ability4Button.forceHighlight = true;
        }

        public void UnhighlightAbilities()
        {
            default1Button.forceHighlight = false;
            default2Button.forceHighlight = false;
            default3Button.forceHighlight = false;
            default4Button.forceHighlight = false;
            special1Button.forceHighlight = false;
            special2Button.forceHighlight = false;
            special3Button.forceHighlight = false;
            special4Button.forceHighlight = false;
            ability1Button.forceHighlight = false;
            ability2Button.forceHighlight = false;
            ability3Button.forceHighlight = false;
            ability4Button.forceHighlight = false;
        }

        public void UpdateDescriptionText(int abilityID)
        {
            switch (abilityID)
            {
                case Constants.EMPTY_THREE:
                    mAbilityNameText = "Locked";
                    mDesciprtionText = "Reach wave 30 with this ship";
                    break;
                case Constants.EMPTY_TWO:
                    mAbilityNameText = "Locked";
                    mDesciprtionText = "Reach wave 20 with this ship";
                    break;
                case Constants.EMPTY_ONE:
                    mAbilityNameText = "Locked";
                    mDesciprtionText = "Reach wave 10 with this ship";
                    break;
                case Constants.QUICK_TURN:
                    mAbilityNameText = "Quick Turn";
                    mDesciprtionText = "Make a quick U-turn";
                    break;
                case Constants.WARP_DRIVE:
                    mAbilityNameText = "Warp Drive";
                    mDesciprtionText = "Jump forward a short distance";
                    break;
                case Constants.GUN_OVERCHARGE:
                    mAbilityNameText = "Overcharge Guns";
                    mDesciprtionText = "Increase fire rate";
                    break;
                case Constants.BULLET_STAR:
                    mAbilityNameText = "Bullet Star";
                    mDesciprtionText = "Fire a circle of bullets";
                    break;
                case Constants.BULLET_FURRY:
                    mAbilityNameText = "Bullet Fury";
                    mDesciprtionText = "Fire a barrage of bullets";
                    break;
                case Constants.DUAL_WIELD:
                    mAbilityNameText = "Dual Wield";
                    mDesciprtionText = "Dual wield your current weapon";
                    break;
                case Constants.OVERCHARGE_ENGINES:
                    mAbilityNameText = "Overcharge Engines";
                    mDesciprtionText = "Increase max thrust";
                    break;
                case Constants.MISSILE_FLURRY:
                    mAbilityNameText = "Missle Flurry";
                    mDesciprtionText = "Fire homing missiles";
                    break;
                case Constants.SHOTGUN_BLAST:
                    mAbilityNameText = "Shotgun Blast";
                    mDesciprtionText = "Shoot a wave of bullets";
                    break;
                case Constants.TIGHTER_TURNS:
                    mAbilityNameText = "Expand Wingspan";
                    mDesciprtionText = "Increase turning speed";
                    break;
                case Constants.ENERGY_SHIELD:
                    mAbilityNameText = "Energy Shield";
                    mDesciprtionText = "Absorbs ONLY bullets and missiles";
                    break;
                case Constants.REAR_GUN:
                    mAbilityNameText = "Rear Guns";
                    mDesciprtionText = "Fire a secondary gun opposite your primary gun";
                    break;
                case Constants.ANTI_BULLETS:
                    mAbilityNameText = "Anti-Bullets";
                    mDesciprtionText = "Fire weak bullets that destroy enemy bullets";
                    break;
                case Constants.SATELLITE:
                    mAbilityNameText = "Helper Satellite";
                    mDesciprtionText = "Deploy a satellite that helps shoot";
                    break;
                case Constants.MINES:
                    mAbilityNameText = "Mines";
                    mDesciprtionText = "Lay down active mines";
                    break;
                case Constants.FULL_SHIELD:
                    mAbilityNameText = "Full Shield";
                    mDesciprtionText = "Protection from bullets and collisions";
                    break;
            }

            textSize = mGameReference.mTextboxFont.MeasureString(mAbilityNameText);
            mAbilityBox.mPos.X = 775 - (int)(textSize.X / 2);
            mAbilityBox.mText = mAbilityNameText;
            textSize = mGameReference.mDescriptionFont.MeasureString(mDesciprtionText);
            mDescriptionBox.mPos.X = 775 - (int)(textSize.X / 2);
            mDescriptionBox.mText = mDesciprtionText;
        }

        public void ShipChangeUpdate()
        {
            mShipName.MenuDestroy();
            mHealth.MenuDestroy();
            mMaxThrust.MenuDestroy();
            mTurnSpeed.MenuDestroy();
            special2ID = -1;
            special3ID = -2;
            special4ID = -3;
            SpecialAbility2.src_RectX = lockedKit.SRC_RECTX;
            SpecialAbility2.src_RectY = lockedKit.SRC_RECTY;
            SpecialAbility3.src_RectX = lockedKit.SRC_RECTX;
            SpecialAbility3.src_RectY = lockedKit.SRC_RECTY;
            SpecialAbility4.src_RectX = lockedKit.SRC_RECTX;
            SpecialAbility4.src_RectY = lockedKit.SRC_RECTY;

            switch (shipCount)
            {
                case 0:
                    ShipIcon.src_RectX = aiconKit.SRC_RECTX;
                    ShipIcon.src_RectY = aiconKit.SRC_RECTY;
                    shipname = "AGILITY";
                    health = "100";
                    maxthrust = "300 N";
                    turnspeed = "5 rad/s";
                    if (CheckAbility1())
                        ability1 = SetAbilityIcon(Abilitydata.mAbility1[0]);
                    else
                        ability1 = SetAbilityIcon(Constants.EMPTY_ONE);
                    if (CheckAbility2())
                        ability2 = SetAbilityIcon(Abilitydata.mAbility2[0]);
                    else
                        ability2 = SetAbilityIcon(Constants.EMPTY_TWO);
                    if (CheckAbility3())
                        ability3 = SetAbilityIcon(Abilitydata.mAbility3[0]);
                    else
                        ability3 = SetAbilityIcon(Constants.EMPTY_THREE);
                    if (CheckAbility4())
                        ability4 = SetAbilityIcon(Abilitydata.mAbility4[0]);
                    else
                        ability4 = SetAbilityIcon(Constants.EMPTY_FOUR);
                    SpecialAbility1.src_RectX = tighterturnsKit.SRC_RECTX;
                    SpecialAbility1.src_RectY = tighterturnsKit.SRC_RECTY;
                    special1ID = 9;
                    if (Abilitydata.mUnlocks[0] == 2736)
                    {
                        SpecialAbility2.src_RectX = shotgunblastKit.SRC_RECTX;
                        SpecialAbility2.src_RectY = shotgunblastKit.SRC_RECTY;
                        special2ID = 8;
                    }
                    if (Abilitydata.mUnlocks[1] == 1967)
                    {
                        SpecialAbility3.src_RectX = energyshieldKit.SRC_RECTX;
                        SpecialAbility3.src_RectY = energyshieldKit.SRC_RECTY;
                        special3ID = 10;
                    }
                    if (Abilitydata.mUnlocks[2] == 8913)
                    {
                        SpecialAbility4.src_RectX = bulletfurryKit.SRC_RECTX;
                        SpecialAbility4.src_RectY = bulletfurryKit.SRC_RECTY;
                        special4ID = 4;
                    }
                    break;
                case 1:
                    ShipIcon.src_RectX = ticonKit.SRC_RECTX;
                    ShipIcon.src_RectY = ticonKit.SRC_RECTY;
                    shipname = "TANK";
                    health = "200";
                    maxthrust = "230 N";
                    turnspeed = "3 rad/s";
                    if (CheckAbility1())
                        ability1 = SetAbilityIcon(Abilitydata.mAbility1[1]);
                    else
                        ability1 = SetAbilityIcon(Constants.EMPTY_ONE);
                    if (CheckAbility2())
                        ability2 = SetAbilityIcon(Abilitydata.mAbility2[1]);
                    else
                        ability2 = SetAbilityIcon(Constants.EMPTY_TWO);
                    if (CheckAbility3())
                        ability3 = SetAbilityIcon(Abilitydata.mAbility3[1]);
                    else
                        ability3 = SetAbilityIcon(Constants.EMPTY_THREE);
                    if (CheckAbility4())
                        ability4 = SetAbilityIcon(Abilitydata.mAbility4[1]);
                    else
                        ability4 = SetAbilityIcon(Constants.EMPTY_FOUR);
                    SpecialAbility1.src_RectX = overchargeenginesKit.SRC_RECTX;
                    SpecialAbility1.src_RectY = overchargeenginesKit.SRC_RECTY;
                    special1ID = 6;
                    if (Abilitydata.mUnlocks[3] == 8432)
                    {
                        SpecialAbility2.src_RectX = missileflurryKit.SRC_RECTX;
                        SpecialAbility2.src_RectY = missileflurryKit.SRC_RECTY;
                        special2ID = 7;
                    }
                    if (Abilitydata.mUnlocks[4] == 6874)
                    {
                        SpecialAbility3.src_RectX = mineKit.SRC_RECTX;
                        SpecialAbility3.src_RectY = mineKit.SRC_RECTY;
                        special3ID = 14;
                    }
                    if (Abilitydata.mUnlocks[5] == 8794)
                    {
                        SpecialAbility4.src_RectX = fullshieldKit.SRC_RECTX;
                        SpecialAbility4.src_RectY = fullshieldKit.SRC_RECTY;
                        special4ID = 15;
                    }
                    break;
                case 2:
                    ShipIcon.src_RectX = ciconKit.SRC_RECTX;
                    ShipIcon.src_RectY = ciconKit.SRC_RECTY;
                    shipname = "COMMANDO";
                    health = "120";
                    maxthrust = "280 N";
                    turnspeed = "4 rad/s";
                    if (CheckAbility1())
                        ability1 = SetAbilityIcon(Abilitydata.mAbility1[2]);
                    else
                        ability1 = SetAbilityIcon(Constants.EMPTY_ONE);
                    if (CheckAbility2())
                        ability2 = SetAbilityIcon(Abilitydata.mAbility2[2]);
                    else
                        ability2 = SetAbilityIcon(Constants.EMPTY_TWO);
                    if (CheckAbility3())
                        ability3 = SetAbilityIcon(Abilitydata.mAbility3[2]);
                    else
                        ability3 = SetAbilityIcon(Constants.EMPTY_THREE);
                    if (CheckAbility4())
                        ability4 = SetAbilityIcon(Abilitydata.mAbility4[2]);
                    else
                        ability4 = SetAbilityIcon(Constants.EMPTY_FOUR);
                    SpecialAbility1.src_RectX = reargunsKit.SRC_RECTX;
                    SpecialAbility1.src_RectY = reargunsKit.SRC_RECTY;
                    special1ID = 11;
                    if (Abilitydata.mUnlocks[6] == 3252)
                    {
                        SpecialAbility2.src_RectX = antibulletsKit.SRC_RECTX;
                        SpecialAbility2.src_RectY = antibulletsKit.SRC_RECTY;
                        special2ID = 12;
                    }
                    if (Abilitydata.mUnlocks[7] == 3248)
                    {
                        SpecialAbility3.src_RectX = dualwieldKit.SRC_RECTX;
                        SpecialAbility3.src_RectY = dualwieldKit.SRC_RECTY;
                        special3ID = 5;
                    }
                    if (Abilitydata.mUnlocks[8] == 8942)
                    {
                        SpecialAbility4.src_RectX = satelliteKit.SRC_RECTX;
                        SpecialAbility4.src_RectY = satelliteKit.SRC_RECTY;
                        special4ID = 13;
                    }
                    break;
            }

            textSize = mGameReference.mTextboxFont.MeasureString(shipname);
            mShipName.mPos.X = 265 - (int)(textSize.X / 2);
            mShipName.mText = shipname;
            mHealth.mText = "Health: " + health;
            mMaxThrust.mText = "Max Thrust: " + maxthrust;
            mTurnSpeed.mText = "Turn Speed: " + turnspeed;
            Ability1.src_RectX = ability1.SRC_RECTX;
            Ability1.src_RectY = ability1.SRC_RECTY;
            Ability2.src_RectX = ability2.SRC_RECTX;
            Ability2.src_RectY = ability2.SRC_RECTY;
            Ability3.src_RectX = ability3.SRC_RECTX;
            Ability3.src_RectY = ability3.SRC_RECTY;
            Ability4.src_RectX = ability4.SRC_RECTX;
            Ability4.src_RectY = ability4.SRC_RECTY;
            mGameReference.aaDisplay.AddMenuUIElement(mShipName);
            mGameReference.aaDisplay.AddMenuUIElement(mHealth);
            mGameReference.aaDisplay.AddMenuUIElement(mMaxThrust);
            mGameReference.aaDisplay.AddMenuUIElement(mTurnSpeed);
            mGameReference.aaDisplay.AddMenuUIElement(ShipIcon);
            UpdateDescriptionText(0);
        }

        public AssetKit SetAbilityIcon(int abilityID)
        {
            switch (abilityID)
            {
                case Constants.EMPTY_FOUR:
                    return slot4Kit;
                case Constants.EMPTY_THREE:
                    return slot3Kit;
                case Constants.EMPTY_TWO:
                    return slot2Kit;
                case Constants.EMPTY_ONE:
                    return slot1Kit;
                case Constants.QUICK_TURN:
                    return quickturnKit;
                case Constants.WARP_DRIVE:
                    return warpdriveKit;
                case Constants.GUN_OVERCHARGE:
                    return gunoverchargeKit;
                case Constants.BULLET_STAR:
                    return bulletstarKit;
                case Constants.BULLET_FURRY:
                    return bulletfurryKit;
                case Constants.DUAL_WIELD:
                    return dualwieldKit;
                case Constants.OVERCHARGE_ENGINES:
                    return overchargeenginesKit;
                case Constants.MISSILE_FLURRY:
                    return missileflurryKit;
                case Constants.SHOTGUN_BLAST:
                    return shotgunblastKit;
                case Constants.TIGHTER_TURNS:
                    return tighterturnsKit;
                case Constants.ENERGY_SHIELD:
                    return energyshieldKit;
                case Constants.REAR_GUN:
                    return reargunsKit;
                case Constants.ANTI_BULLETS:
                    return antibulletsKit;
                case Constants.SATELLITE:
                    return satelliteKit;
                case Constants.MINES:
                    return mineKit;
                case Constants.FULL_SHIELD:
                    return fullshieldKit;
            }

            return null;
        }

        //Ugly Check
        public bool CheckAbility1()
        {
            if(shipCount == 0)
            {
                if (Abilitydata.mAbility1[0] < 5 || Abilitydata.mAbility1[0] == 9 || Abilitydata.mAbility1[0] == 8 || Abilitydata.mAbility1[0] == 10)
                    return true;
                else
                    Abilitydata.mAbility1[0] = Constants.EMPTY_ONE;
            }
            else if (shipCount == 1)
            {
                if (Abilitydata.mAbility1[1] < 4 || Abilitydata.mAbility1[1] == 6 || Abilitydata.mAbility1[1] == 7 || Abilitydata.mAbility1[1] == 15 || Abilitydata.mAbility1[1] == 14)
                    return true;
                else
                    Abilitydata.mAbility1[1] = Constants.EMPTY_ONE;
            }
            else if (shipCount == 2)
            {
                if (Abilitydata.mAbility1[2] < 4 || Abilitydata.mAbility1[2] == 5 || Abilitydata.mAbility1[2] == 12 || Abilitydata.mAbility1[2] == 11 || Abilitydata.mAbility1[2] == 13)
                    return true;
                else
                    Abilitydata.mAbility1[2] = Constants.EMPTY_ONE;
            }
            return false;
        }
        public bool CheckAbility2()
        {
            if (shipCount == 0)
            {
                if (Abilitydata.mAbility2[0] < 5 || Abilitydata.mAbility2[0] == 9 || Abilitydata.mAbility2[0] == 8 || Abilitydata.mAbility2[0] == 10)
                    return true;
                else
                    Abilitydata.mAbility2[0] = Constants.EMPTY_TWO;
            }
            else if (shipCount == 1)
            {
                if (Abilitydata.mAbility2[1] < 4 || Abilitydata.mAbility2[1] == 6 || Abilitydata.mAbility2[1] == 7 || Abilitydata.mAbility2[1] == 15 || Abilitydata.mAbility2[1] == 14)
                    return true;
                else
                    Abilitydata.mAbility2[1] = Constants.EMPTY_TWO;
            }
            else if (shipCount == 2)
            {
                if (Abilitydata.mAbility1[2] < 4 || Abilitydata.mAbility1[2] == 5 || Abilitydata.mAbility1[2] == 12 || Abilitydata.mAbility1[2] == 11 || Abilitydata.mAbility1[2] == 13)
                    return true;
                else
                    Abilitydata.mAbility2[2] = Constants.EMPTY_TWO;
            }
            return false;
        }
        public bool CheckAbility3()
        {
            if (shipCount == 0)
            {
                if (Abilitydata.mAbility3[0] < 5 || Abilitydata.mAbility3[0] == 9 || Abilitydata.mAbility3[0] == 8 || Abilitydata.mAbility3[0] == 10)
                    return true;
                else
                    Abilitydata.mAbility3[0] = Constants.EMPTY_THREE;
            }
            else if (shipCount == 1)
            {
                if (Abilitydata.mAbility3[1] < 4 || Abilitydata.mAbility3[1] == 6 || Abilitydata.mAbility3[1] == 7 || Abilitydata.mAbility3[1] == 15 || Abilitydata.mAbility3[1] == 14)
                    return true;
                else
                    Abilitydata.mAbility3[1] = Constants.EMPTY_THREE;
            }
            else if (shipCount == 2)
            {
                if (Abilitydata.mAbility1[2] < 4 || Abilitydata.mAbility1[2] == 5 || Abilitydata.mAbility1[2] == 12 || Abilitydata.mAbility1[2] == 11 || Abilitydata.mAbility1[2] == 13)
                    return true;
                else
                    Abilitydata.mAbility3[2] = Constants.EMPTY_THREE;
            }
            return false;
        }
        public bool CheckAbility4()
        {
            if (shipCount == 0)
            {
                if (Abilitydata.mAbility4[0] < 5 || Abilitydata.mAbility4[0] == 9 || Abilitydata.mAbility4[0] == 8 || Abilitydata.mAbility4[0] == 10)
                    return true;
                else
                    Abilitydata.mAbility4[0] = Constants.EMPTY_FOUR;
            }
            else if (shipCount == 1)
            {
                if (Abilitydata.mAbility4[1] < 4 || Abilitydata.mAbility4[1] == 6 || Abilitydata.mAbility4[1] == 7 || Abilitydata.mAbility4[1] == 15 || Abilitydata.mAbility4[1] == 14)
                    return true;
                else
                    Abilitydata.mAbility4[1] = Constants.EMPTY_FOUR;
            }
            else if (shipCount == 2)
            {
                if (Abilitydata.mAbility1[2] < 4 || Abilitydata.mAbility1[2] == 5 || Abilitydata.mAbility1[2] == 12 || Abilitydata.mAbility1[2] == 11 || Abilitydata.mAbility1[2] == 13)
                    return true;
                else
                    Abilitydata.mAbility4[2] = Constants.EMPTY_FOUR;
            }
            return false;
        }
    }

    class State_Menu_Instructions : State
    {
        UIElement_StaticMenuElement abilitiesPlaque;
        AssetKit_Rules_Abilities abilitiesKit;

        UIElement_StaticMenuElement movementPlaque;
        AssetKit_Rules_Movement movementKit;

        UIElement_StaticMenuElement pickupsPlaque;
        AssetKit_Rules_PickUps pickupsKit;

        UIElement_StaticMenuElement creditsPlaque;
        AssetKit_CreditsPlaque creditsKit;

        UIElement_MenuButton backButton;
        AssetKit_Menu_BackButton backButtonKit;

        public State_Menu_Instructions(Game1 pGame)
            : base(pGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            abilitiesKit = new AssetKit_Rules_Abilities();
            abilitiesPlaque = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, abilitiesKit.ASSET_NAME, 450, 311, (int)abilitiesKit.ORIGIN.X, (int)abilitiesKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, abilitiesKit.SRC_RECTX, abilitiesKit.SRC_RECTY, abilitiesKit.SRC_RECTWIDTH, abilitiesKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(abilitiesPlaque);

            movementKit = new AssetKit_Rules_Movement();
            movementPlaque = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, movementKit.ASSET_NAME, 450, 50, (int)movementKit.ORIGIN.X, (int)movementKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, movementKit.SRC_RECTX, movementKit.SRC_RECTY, movementKit.SRC_RECTWIDTH, movementKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(movementPlaque);

            pickupsKit = new AssetKit_Rules_PickUps();
            pickupsPlaque = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, pickupsKit.ASSET_NAME, 850, 50, (int)pickupsKit.ORIGIN.X, (int)pickupsKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, pickupsKit.SRC_RECTX, pickupsKit.SRC_RECTY, pickupsKit.SRC_RECTWIDTH, pickupsKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(pickupsPlaque);

            creditsKit = new AssetKit_CreditsPlaque();
            creditsPlaque = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, creditsKit.ASSET_NAME, 50, 50, (int)creditsKit.ORIGIN.X, (int)creditsKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, creditsKit.SRC_RECTX, creditsKit.SRC_RECTY, creditsKit.SRC_RECTWIDTH, creditsKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(creditsPlaque);

            backButtonKit = new AssetKit_Menu_BackButton();
            backButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, backButtonKit.ASSET_NAME, 15, 625, (int)backButtonKit.ORIGIN.X, (int)backButtonKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, backButtonKit.SRC_RECTX, backButtonKit.SRC_RECTY, backButtonKit.SRC_RECTWIDTH, backButtonKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(backButton);
        }

        public override void LeaveState()
        {
            base.LeaveState();

            abilitiesKit = null;
            abilitiesPlaque.MenuDestroy();
            abilitiesPlaque = null;

            movementKit = null;
            movementPlaque.MenuDestroy();
            movementPlaque = null;

            creditsKit = null;
            creditsPlaque.MenuDestroy();
            creditsPlaque = null;

            backButtonKit = null;
            backButton.MenuDestroy();
            backButton = null;
        }

        public override void Update(float DT)
        {
            base.Update(DT);

            backButton.Update(DT);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
            }

            if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
            {
                if (backButton.CheckButtonClick())
                {
                    mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
                }
            }
        }
    }

    class State_Menu_HighScore : State
    {
        ScoreData data;

        UIElement_Text oneName;
        UIElement_Text twoName;
        UIElement_Text threeName;
        UIElement_Text fourName;
        UIElement_Text fiveName;

        UIElement_Text oneScore;
        UIElement_Text twoScore;
        UIElement_Text threeScore;
        UIElement_Text fourScore;
        UIElement_Text fiveScore;

        UIElement_Text oneWave;
        UIElement_Text twoWave;
        UIElement_Text threeWave;
        UIElement_Text fourWave;
        UIElement_Text fiveWave;

        UIElement_StaticMenuElement highscorePlaque;
        AssetKit_HighScore_Plaque highscoreKit;

        UIElement_MenuButton backButton;
        AssetKit_Menu_BackButton backButtonKit;

        public State_Menu_HighScore(Game1 pGame)
            : base(pGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            data = mGameReference.aaDisplay.aaGame.aaHSManager.Scoredata;

            highscoreKit = new AssetKit_HighScore_Plaque();
            highscorePlaque = new UIElement_StaticMenuElement(mGameReference.aaDisplay, mGameReference.aaGameWorld, highscoreKit.ASSET_NAME, 640, 360, (int)highscoreKit.ORIGIN.X, (int)highscoreKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, highscoreKit.SRC_RECTX, highscoreKit.SRC_RECTY, highscoreKit.SRC_RECTWIDTH, highscoreKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(highscorePlaque);

            oneName = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 415, 290, Color.White, data.mName[0] + "");
            mGameReference.aaDisplay.AddMenuUIElement(oneName);
            twoName = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 415, 330, Color.White, data.mName[1] + "");
            mGameReference.aaDisplay.AddMenuUIElement(twoName);
            threeName = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 415, 370, Color.White, data.mName[2] + "");
            mGameReference.aaDisplay.AddMenuUIElement(threeName);
            fourName = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 415, 410, Color.White, data.mName[3] + "");
            mGameReference.aaDisplay.AddMenuUIElement(fourName);
            fiveName = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 415, 450, Color.White, data.mName[4] + "");
            mGameReference.aaDisplay.AddMenuUIElement(fiveName);

            if(data.mScore[0] != 0 && data.mWave[0] != 0)
            {
                oneScore = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 620, 290, Color.White, data.mScore[0] + "");
                mGameReference.aaDisplay.AddMenuUIElement(oneScore);
                oneWave = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 810, 290, Color.White, data.mWave[0] + "");
                mGameReference.aaDisplay.AddMenuUIElement(oneWave);
            }
            if (data.mScore[1] != 0 && data.mWave[1] != 0)
            {
                twoScore = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 620, 330, Color.White, data.mScore[1] + "");
                mGameReference.aaDisplay.AddMenuUIElement(twoScore);
                twoWave = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 810, 330, Color.White, data.mWave[1] + "");
                mGameReference.aaDisplay.AddMenuUIElement(twoWave);
            }
            if (data.mScore[2] != 0 && data.mWave[2] != 0)
            {
                threeScore = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 620, 370, Color.White, data.mScore[2] + "");
                mGameReference.aaDisplay.AddMenuUIElement(threeScore);
                threeWave = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 810, 370, Color.White, data.mWave[2] + "");
                mGameReference.aaDisplay.AddMenuUIElement(threeWave);
            }
            if (data.mScore[3] != 0 && data.mWave[3] != 0)
            {
                fourScore = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 620, 410, Color.White, data.mScore[3] + "");
                mGameReference.aaDisplay.AddMenuUIElement(fourScore);
                fourWave = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 810, 410, Color.White, data.mWave[3] + "");
                mGameReference.aaDisplay.AddMenuUIElement(fourWave);
            }
            if (data.mScore[4] != 0 && data.mWave[4] != 0)
            {
                fiveScore = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 620, 450, Color.White, data.mScore[4] + "");
                mGameReference.aaDisplay.AddMenuUIElement(fiveScore);
                fiveWave = new UIElement_Text(mGameReference.aaDisplay, mGameReference.aaGameWorld, mGameReference.aaDisplay.aaGame.mScoreFont, 810, 450, Color.White, data.mWave[4] + "");
                mGameReference.aaDisplay.AddMenuUIElement(fiveWave);
            }
            
            backButtonKit = new AssetKit_Menu_BackButton();
            backButton = new UIElement_MenuButton(mGameReference.aaDisplay, mGameReference.aaGameWorld, backButtonKit.ASSET_NAME, 15, 625, (int)backButtonKit.ORIGIN.X, (int)backButtonKit.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, backButtonKit.SRC_RECTX, backButtonKit.SRC_RECTY, backButtonKit.SRC_RECTWIDTH, backButtonKit.SRC_RECTHEIGHT, 0.0f);
            mGameReference.aaDisplay.AddMenuUIElement(backButton);
        }

        public override void LeaveState()
        {
            highscoreKit = null;
            highscorePlaque.MenuDestroy();
            highscorePlaque = null;

            oneName.MenuDestroy();
            twoName.MenuDestroy();
            threeName.MenuDestroy();
            fourName.MenuDestroy();
            fiveName.MenuDestroy();

            if (oneScore != null)
            {
                oneWave.MenuDestroy();
                oneScore.MenuDestroy();
            }
            if (twoScore != null)
            {
                twoScore.MenuDestroy();
                twoWave.MenuDestroy();
            }
            if (threeScore != null)
            {
                threeScore.MenuDestroy();
                threeWave.MenuDestroy();
            }
            if (fourScore != null)
            {
                fourScore.MenuDestroy();
                fourWave.MenuDestroy();
            }
            if (fiveScore != null)
            {
                fiveScore.MenuDestroy();
                fiveWave.MenuDestroy();
            }

            backButtonKit = null;
            backButton.MenuDestroy();
            backButton = null;

            base.LeaveState();

            // TODO: Remove the high score stuff from the screen
        }

        public override void Update(float DT)
        {
            base.Update(DT);

            backButton.Update(DT);

            // TODO: Check for key presses and menu transition stuff
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
            }

            if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
            {
                if (backButton.CheckButtonClick())
                {
                    mGameReference.mManager.ChangeState(mGameReference.mMainMenu);
                }
            }
        }
    }
}
