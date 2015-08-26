using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWars
{

    class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Display aaDisplay;
        public GameWorld aaGameWorld;
        public GameIO aaHSManager;

        public StateManager mManager;
        public State mGame;
        public State mSplash;
        public State mMainMenu;
        public State mInstructionsMenu;
        public State mHighScoreMenu;
        public State mShipMenu;

        public SpriteFont mScoreFont;
        public SpriteFont mTextboxFont;
        public SpriteFont mDescriptionFont;
        public SpriteFont mDescription2Font;

        public SoundEffect mMusicMenu;
        public SoundEffect mMusicGameplay;
        public SoundEffect mExplosion;
        public SoundEffect mShootSound;
        public SoundEffect mHitSound;
        public SoundEffect mBulletSpamSound;
        public SoundEffect mDualWieldSound;
        public SoundEffect mMissileFlurrySound;
        public SoundEffect mOverchargeEnginesSound;
        public SoundEffect mQuickFixSound;
        public SoundEffect mQuickTurnSound;
        public SoundEffect mSatelliteSound;
        public SoundEffect mSiegeModeSound;
        public SoundEffect mTankShieldSound;
        public SoundEffect mWarpDriveSound;
        public SoundEffect mWaveBlastSound;
        public SoundEffect mPickUpSound;
        public SoundEffect mMineSound;
        public SoundEffect mAntiBulletSound;
        public SoundEffect mBombBeepSlow;
        public SoundEffect mBombBeepFast;
        public SoundEffect mTighterTurns;
        public SoundEffectInstance mMusicMenu_Instance;
        public SoundEffectInstance mMusicGameplay_Instance;
        public List<SoundEffectInstance> mGameSoundFX;
        public List<SoundEffectInstance> mGameSoundFXRemove;
        private Int64 previousTimeStamp = 0;
        private Int64 currentTimeStamp = 0;
        private float secondsPerCount = 0;

        public Game1()
        {
            this.Window.Title = "Spectrum";
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            aaDisplay = new Display(this);
            mManager = new StateManager();
            mGame = new State_GameState(this);
            mSplash = new State_SplashState(this);
            mMainMenu = new State_Menu_Main(this);
            mShipMenu = new State_Menu_Ship(this);
            mInstructionsMenu = new State_Menu_Instructions(this);
            mHighScoreMenu = new State_Menu_HighScore(this);
            aaHSManager = new GameIO();
            mGameSoundFX = new List<SoundEffectInstance>();
            mGameSoundFXRemove = new List<SoundEffectInstance>();

            this.IsMouseVisible = true;

            Int64 performanceFrequency = 0;
            PerformanceTimer.QueryPerformanceFrequency(out performanceFrequency);
            secondsPerCount = 1.0f / (float)performanceFrequency;
            PerformanceTimer.QueryPerformanceCounter(out currentTimeStamp);
            previousTimeStamp = currentTimeStamp;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mScoreFont = Content.Load<SpriteFont>("scoreFont");
            mTextboxFont = Content.Load<SpriteFont>("textboxFont");
            mDescriptionFont = Content.Load<SpriteFont>("detailFont");
            mDescription2Font = Content.Load<SpriteFont>("detailFont2");

            mMusicMenu = Content.Load<SoundEffect>("Music_Menu");
            mMusicGameplay = Content.Load<SoundEffect>("Music_Gameplay");

            mExplosion = Content.Load<SoundEffect>("Explosion77");
            mShootSound = Content.Load<SoundEffect>("Laser_Shoot14");
            mHitSound = Content.Load<SoundEffect>("Hit_Hurt33");

            mBulletSpamSound = Content.Load<SoundEffect>("BulletSpam");
            mDualWieldSound = Content.Load<SoundEffect>("DualWield");
            mMissileFlurrySound = Content.Load<SoundEffect>("MissileFlurry");
            mOverchargeEnginesSound = Content.Load<SoundEffect>("OverchargeEngines");
            mQuickFixSound = Content.Load<SoundEffect>("QuickFix");
            mQuickTurnSound = Content.Load<SoundEffect>("QuickTurn");
            mSatelliteSound = Content.Load<SoundEffect>("Satellite");
            mSiegeModeSound = Content.Load<SoundEffect>("SiegeMode");
            mTankShieldSound = Content.Load<SoundEffect>("TankShield");
            mWarpDriveSound = Content.Load<SoundEffect>("WarpDrive");
            mWaveBlastSound = Content.Load<SoundEffect>("WaveBlast");
            mPickUpSound = Content.Load<SoundEffect>("PickUpSound");
            mBombBeepSlow = Content.Load<SoundEffect>("BombBeepSlow");
            mBombBeepFast = Content.Load<SoundEffect>("BombBeepFast");
            mMineSound = Content.Load<SoundEffect>("Mine");
            mAntiBulletSound = Content.Load<SoundEffect>("Antibullets");
            mTighterTurns = Content.Load<SoundEffect>("TighterTurns");

            mMusicMenu_Instance = mMusicMenu.CreateInstance();
            mMusicMenu_Instance.IsLooped = true;
            mMusicGameplay_Instance = mMusicGameplay.CreateInstance();
            mMusicGameplay_Instance.IsLooped = true;
            aaDisplay.LoadAssets();
        }

        protected override void UnloadContent()
        {
            aaDisplay.UnloadAssets();
            mMusicGameplay.Dispose();
            mMusicMenu.Dispose();
            mExplosion.Dispose();
            mShootSound.Dispose();
            mHitSound.Dispose();
            mBulletSpamSound.Dispose();
            mDualWieldSound.Dispose();
            mMissileFlurrySound.Dispose();
            mOverchargeEnginesSound.Dispose();
            mQuickFixSound.Dispose();
            mQuickTurnSound.Dispose();
            mSatelliteSound.Dispose();
            mSiegeModeSound.Dispose();
            mTankShieldSound.Dispose();
            mWarpDriveSound.Dispose();
            mWaveBlastSound.Dispose();
            mPickUpSound.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            PerformanceTimer.QueryPerformanceCounter(out currentTimeStamp);
            float dt = (currentTimeStamp - previousTimeStamp) * secondsPerCount;

            // Allows the game to exitClass1.cs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (mManager.GetCurrentState() == null)
            {
                mManager.ChangeState(mSplash);
            }

            mManager.UpdateState(dt);

            base.Update(gameTime);

            previousTimeStamp = currentTimeStamp;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            aaDisplay.DrawScreen(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
