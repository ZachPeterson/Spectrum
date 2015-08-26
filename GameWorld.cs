using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWars
{
        class GameWorld
        {
            
            public Display aaDisplay;
            public List<Entity> mToSpawnEntityList;
            public List<Entity> mEntityList;
            public List<Entity> mEntityDeadList;
            public List<Projectile> mProjectileList;
            public List<Projectile> mProjDeadList;
            public List<Projectile> mRemovedProjectileList;
            public List<Pickup> mPickups;

            public Ship Player;

            public DisplayObject mBackground;

            public Director mAiDirector;

            public Random mRand;

            public List<CircleCollision> CollisionShapes_Circle;
            public List<PointCollision> CollisionShapes_Points;
            public List<LineCollision> CollisionShapes_Lines;

            public List<CircleCollision> CollisionShapes_Circle_ToRemove;
            public List<PointCollision> CollisionShapes_Points_ToRemove;
            public List<LineCollision> CollisionShapes_Lines_ToRemove;

            public UIElement mAbility1Indicator;
            public UIElement mAbility2Indicator;
            public UIElement mAbility3Indicator;
            public UIElement mAbility4Indicator;

            public UIElement score;
            public UIElement waveIndicator;

            public AssetKit_UI_Aimer mKit_Aimer;
            public AssetKit_UI_Crosshair mKit_Crosshair;
            public AssetKit_UI_HealthBar_Border mKit_HealthBorder;
            public AssetKit_UI_HealthBar_Bar mKit_HealthBar;
            public AssetKit_UI_HealthBar_BarWhite mKit_HealthWhite;
            public AssetKit_GameOver_Score mKit_GameOver;
            public AssetKit_Paused_Plaque mKit_Paused;

            public UIElement Crosshair;
            public UIElement Aimer;
            public UIElement HealthBarBorder;
            public UIElement HealthBar;
            public UIElement HealthWhite;
            public UIElement GameOverPlaque;
            public UIElement ScoreTextBox;
            public UIElement FinalScoreText;
            public UIElement PausedPlaque;
            public UIElement_Text UnlockText;

            public GraphicsEffectHandler aaGFXHandler;

            public int mPoints;

            public int mCurrentWave = 0;
            public int mEnemiesKilled = 0;

            public KeyboardState mInput;
            public KeyboardState mPrevInput;
            public bool isPaused = false;

            public EntityManager mEntityManager;

            public bool waveResting = false;
            public float waveRestDuration = 5.0f;
            public float waveRested = 0.0f;

            public GameWorld(Display pDisplay)
            {
                aaDisplay = pDisplay;

                mToSpawnEntityList = new List<Entity>();
                mEntityList = new List<Entity>();
                mEntityDeadList = new List<Entity>();
                mProjectileList = new List<Projectile>();
                mProjDeadList = new List<Projectile>();
                mRemovedProjectileList = new List<Projectile>();
                mPickups = new List<Pickup>();

                //Init CollisionLists
                CollisionShapes_Circle = new List<CircleCollision>();
                CollisionShapes_Points = new List<PointCollision>();
                CollisionShapes_Lines = new List<LineCollision>();

                CollisionShapes_Circle_ToRemove = new List<CircleCollision>();
                CollisionShapes_Points_ToRemove = new List<PointCollision>();
                CollisionShapes_Lines_ToRemove = new List<LineCollision>();

                mRand = new Random();

                mKit_Aimer = new AssetKit_UI_Aimer();
                mKit_Crosshair = new AssetKit_UI_Crosshair();
                mKit_HealthBorder = new AssetKit_UI_HealthBar_Border();
                mKit_HealthBar = new AssetKit_UI_HealthBar_Bar();
                mKit_HealthWhite = new AssetKit_UI_HealthBar_BarWhite();
                mKit_GameOver = new AssetKit_GameOver_Score();
                mKit_Paused = new AssetKit_Paused_Plaque();

                mEntityManager = new EntityManager(this);
            }

            public void Initialize()
            {
                aaDisplay.aaCamera = new DisplayCamera(this);
                aaGFXHandler = new GraphicsEffectHandler(this);
                if ((aaDisplay.aaGame.mShipMenu as State_Menu_Ship).shipCount == 0)
                    Player = new Ship(this, "Player_Agility", Constants.PLAYER_TEAM, 640, 360, new ShipKit_Player_Agility(), false, 0, 0, 0);
                else if((aaDisplay.aaGame.mShipMenu as State_Menu_Ship).shipCount == 2)
                    Player = new Ship(this, "Player_Commando", Constants.PLAYER_TEAM, 640, 360, new ShipKit_Player_Commando(), false, 0, 0, 0);
                else if ((aaDisplay.aaGame.mShipMenu as State_Menu_Ship).shipCount == 1)
                    Player = new Ship(this, "Player_Tank", Constants.PLAYER_TEAM, 640, 360, new ShipKit_Player_Tank(), false, 0, 0, 0);

                mEntityList.Add(Player);
                mAiDirector = new Director(this);

                mBackground = new DisplayObject(aaDisplay, "Back_Black_01", 0, 0, 0, 0, 1.0f, 0, 0, Constants.REGION_PLAYABLE_WIDTH, Constants.REGION_PLAYABLE_HEIGHT, 0, false);

                State_Menu_Ship smShip = (aaDisplay.aaGame.mShipMenu as State_Menu_Ship);
                mAbility1Indicator = new UIElement_AbilityIndicator(aaDisplay, this, ((Ship)Player).Ability1, smShip.ability1, 0, 658, 0);
                mAbility2Indicator = new UIElement_AbilityIndicator(aaDisplay, this, ((Ship)Player).Ability2, smShip.ability2, 60, 658, 0);
                mAbility3Indicator = new UIElement_AbilityIndicator(aaDisplay, this, ((Ship)Player).Ability3, smShip.ability3, 120, 658, 0);
                mAbility4Indicator = new UIElement_AbilityIndicator(aaDisplay, this, ((Ship)Player).Ability4, smShip.ability4, 180, 658, 0);

                Crosshair = new UIElement_ShipCrosshair(aaDisplay, this, KitManager.CrosshairUIAssetKit.ASSET_NAME, (int)Player.mXPos, (int)Player.mYPos, (int)mKit_Crosshair.ORIGIN.X, (int)mKit_Crosshair.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, mKit_Crosshair.SRC_RECTX, mKit_Crosshair.SRC_RECTY, mKit_Crosshair.SRC_RECTWIDTH, mKit_Crosshair.SRC_RECTHEIGHT, 0.0f);
                Aimer = new UIElement_ShipAimer(aaDisplay, this, Player, mKit_Aimer.ASSET_NAME, (int)Player.mXPos, (int)Player.mYPos, (int)mKit_Aimer.ORIGIN.X, (int)mKit_Aimer.ORIGIN.Y, Constants.LAYER_ENTITY_DEPTH, mKit_Aimer.SRC_RECTX, mKit_Aimer.SRC_RECTY, mKit_Aimer.SRC_RECTWIDTH, mKit_Aimer.SRC_RECTHEIGHT, 0.0f);
                HealthBarBorder = new UIElement_HealthBar_Border(aaDisplay, this, mKit_HealthBorder.ASSET_NAME, 992, 0, 0, 0, Constants.LAYER_UI_FRONT, mKit_HealthBorder.SRC_RECTX, mKit_HealthBorder.SRC_RECTY, mKit_HealthBorder.SRC_RECTWIDTH, mKit_HealthBorder.SRC_RECTHEIGHT, 0);
                HealthBar = new UIElement_HealthBar_Bar(aaDisplay, this, mKit_HealthBar.ASSET_NAME, 1030, 6, 0, 0, Constants.LAYER_UI_FRONT, Player, mKit_HealthBar.SRC_RECTX, mKit_HealthBar.SRC_RECTY, mKit_HealthBar.SRC_RECTWIDTH, mKit_HealthBar.SRC_RECTHEIGHT, 0);
                HealthWhite = new UIElement_HealthBar_BarWhite(aaDisplay, this, mKit_HealthWhite.ASSET_NAME, 1030, 6, 0, 0, Constants.LAYER_UI_BACK, Player, mKit_HealthWhite.SRC_RECTX, mKit_HealthWhite.SRC_RECTY, mKit_HealthWhite.SRC_RECTWIDTH, mKit_HealthWhite.SRC_RECTHEIGHT, 0);
                GameOverPlaque = new UIElement_StaticMenuElement(aaDisplay, this, mKit_GameOver.ASSET_NAME, 320, 184, (int)mKit_GameOver.ORIGIN.X, (int)mKit_GameOver.ORIGIN.Y, Constants.LAYER_UI_BACK, mKit_GameOver.SRC_RECTX, mKit_GameOver.SRC_RECTY, mKit_GameOver.SRC_RECTWIDTH, mKit_GameOver.SRC_RECTHEIGHT, 0.0f);

                score = new UIElement_PlayerScore(aaDisplay, this, aaDisplay.aaGame.mScoreFont, 0, 0, Color.White);
                waveIndicator = new UIElement_WaveDisplay(aaDisplay, this, aaDisplay.aaGame.mScoreFont, 0, 100, Color.White);

                aaDisplay.AddMenuUIElement(mAbility1Indicator);
                aaDisplay.AddMenuUIElement(mAbility2Indicator);
                aaDisplay.AddMenuUIElement(mAbility3Indicator);
                aaDisplay.AddMenuUIElement(mAbility4Indicator);
                aaDisplay.AddUIElement(Crosshair);
                aaDisplay.AddUIElement(Aimer);
                aaDisplay.AddUIElement(HealthBarBorder);
                aaDisplay.AddUIElement(HealthBar);
                aaDisplay.AddUIElement(HealthWhite);
                aaDisplay.AddUIElement(score);
                aaDisplay.AddUIElement(waveIndicator);
                
                mPoints = 0;
            }

            public void Destroy()
            {
                foreach (Entity e in mEntityList)
                {
                    e.Destroy();
                }
                mEntityList.Clear();

                foreach (Projectile p in mProjectileList)
                {
                    p.Destroy();
                }
                mProjectileList.Clear();

                mEntityDeadList.Clear();
                mProjDeadList.Clear();

                foreach (Pickup p in mPickups)
                {
                    p.Destroy();
                }
                mPickups.Clear();

                mRemovedProjectileList.Clear();

                mAbility1Indicator.Destroy();
                mAbility1Indicator = null;
                mAbility2Indicator.Destroy();
                mAbility2Indicator = null;
                mAbility3Indicator.Destroy();
                mAbility3Indicator = null;
                mAbility4Indicator.Destroy();
                mAbility4Indicator = null;
                Crosshair.Destroy();
                Crosshair = null;
                Aimer.Destroy();
                Aimer = null;
                score.Destroy();
                score = null;
                HealthBarBorder.Destroy();
                HealthBarBorder = null;
                HealthWhite.Destroy();
                HealthWhite = null;
                HealthBar.Destroy();
                HealthBar = null;

                mBackground.Destroy();
            }

            public bool UpdateWorld(float DT)
            {
                mPrevInput = mInput;
                mInput = Keyboard.GetState();
                if (mInput.IsKeyDown(Keys.P) && mPrevInput.IsKeyUp(Keys.P) && !isPaused && !Player.IsDead() || mInput.IsKeyDown(Keys.Escape) && mPrevInput.IsKeyUp(Keys.Escape) && !isPaused && !Player.IsDead())
                {
                    isPaused = true;
                    aaDisplay.aaGame.IsMouseVisible = true;
                    PausedPlaque = new UIElement_StaticMenuElement(aaDisplay, this, mKit_Paused.ASSET_NAME, 540, 320, (int)mKit_Paused.ORIGIN.X, (int)mKit_Paused.ORIGIN.Y, Constants.LAYER_UI_FRONT, mKit_Paused.SRC_RECTX, mKit_Paused.SRC_RECTY, mKit_Paused.SRC_RECTWIDTH, mKit_Paused.SRC_RECTHEIGHT, 0.0f);
                    aaDisplay.AddMenuUIElement(PausedPlaque);
                }
                else if (mInput.IsKeyDown(Keys.P) && mPrevInput.IsKeyUp(Keys.P) && isPaused || mInput.IsKeyDown(Keys.Escape) && mPrevInput.IsKeyUp(Keys.Escape) && isPaused)
                {
                    isPaused = false;
                    aaDisplay.aaGame.IsMouseVisible = false;
                    PausedPlaque.MenuDestroy();
                    PausedPlaque = null;
                }

                if (!isPaused)
                {
                    mAiDirector.Update(DT);

                    if (mAiDirector.DoneSpawning() && mAiDirector.mTotalEnemies <= mEnemiesKilled)
                    {
                        if (!waveResting)
                        {
                            waveResting = true;
                        }

                        if (waveResting)
                        {
                            waveRested += DT;
                        }

                        if (waveRested >= waveRestDuration)
                        {
                            waveIndicator.Show();
                            waveResting = false;
                            waveRested = 0.0f;
                            mEnemiesKilled = 0;
                            mCurrentWave++;
                            mAiDirector.StartWave(mCurrentWave);
                        }
                    }

                    foreach (Entity e in mToSpawnEntityList)
                    {
                        mEntityList.Add(e);
                    }
                    mToSpawnEntityList.Clear();

                    foreach (Entity e in mEntityList)
                    {
                        e.Update(DT);
                    }

                    foreach (Projectile p in mProjectileList)
                    {
                        p.Update(DT);
                    }

                    foreach (Projectile p in mProjDeadList)
                    {
                        if (!mProjectileList.Remove(p))
                        {
                            if (mProjectileList.Contains(p))
                            {
                                // Console.WriteLine("But the projectile list contains p");
                            }
                        }
                        mRemovedProjectileList.Add(p);

                        if (p.mCollisionType == CollisionType.Point)
                        {
                            CollisionShapes_Points.Remove(p.mCollision as PointCollision);
                        }
                        else if (p.mCollisionType == CollisionType.Circle)
                        {
                            CollisionShapes_Circle.Remove(p.mCollision as CircleCollision);
                        }

                        p.mDispObject.Hide();
                        /*
                        if (p.mDispObject != null)
                        {
                            Console.WriteLine("Display object not null, removing it");
                            p.mDispObject.Destroy();
                            aaDisplay.mDisplayObjects.Remove(p.mDispObject);
                            p.mDispObject = null;
                        }
                        p.aaGameWorld.aaDisplay.mDisplayObjects.Remove(p.mDispObject);*/
                    }
                    mProjDeadList.Clear();

                    foreach (Entity e in mEntityDeadList)
                    {
                        if (e != null)
                        {
                            if (e is Ship)
                            {
                                Ship s = e as Ship;

                                if (!Player.IsDead())
                                {
                                    mPoints += s.mKit.POINTS;
                                }
                                if (s.mTeam == Constants.ENEMY_TEAM)
                                {
                                    mEnemiesKilled++;
                                    if (s.mType == "Grunt_Boss" || s.mType == "Tank_Boss" || s.mType == "Bomb_Boss")
                                    {
                                        if (Player.FireBullet as Ability_FireBullet != null)
                                        {
                                            AssetKit_Tier2Pickup mAssetKit = new AssetKit_Tier2Pickup();
                                            mPickups.Add(new DualShotPickup(aaDisplay, this, mAssetKit.ASSET_NAME, new Vector2(s.mXPos, s.mYPos), mAssetKit.ORIGIN, Constants.LAYER_ENTITY_DEPTH, new Rectangle(mAssetKit.SRC_RECTX, mAssetKit.SRC_RECTY, mAssetKit.SRC_RECTWIDTH, mAssetKit.SRC_RECTHEIGHT), 0));
                                        }
                                        else if (Player.FireBullet as Ability_ShootTier2 != null)
                                        {
                                            AssetKit_Tier3Pickup mAssetKit = new AssetKit_Tier3Pickup();
                                            mPickups.Add(new TriShotPickup(aaDisplay, this, mAssetKit.ASSET_NAME, new Vector2(s.mXPos, s.mYPos), mAssetKit.ORIGIN, Constants.LAYER_ENTITY_DEPTH, new Rectangle(mAssetKit.SRC_RECTX, mAssetKit.SRC_RECTY, mAssetKit.SRC_RECTWIDTH, mAssetKit.SRC_RECTHEIGHT), 0));
                                        }
                                        else if (Player.FireBullet as Ability_ShootTier3 != null)
                                        {
                                            AssetKit_Tier4Pickup mAssetKit = new AssetKit_Tier4Pickup();
                                            mPickups.Add(new QuadShotPickup(aaDisplay, this, mAssetKit.ASSET_NAME, new Vector2(s.mXPos, s.mYPos), mAssetKit.ORIGIN, Constants.LAYER_ENTITY_DEPTH, new Rectangle(mAssetKit.SRC_RECTX, mAssetKit.SRC_RECTY, mAssetKit.SRC_RECTWIDTH, mAssetKit.SRC_RECTHEIGHT), 0));
                                        }
                                    }
                                    else
                                    {
                                        if (mRand.NextDouble() <= 0.02)
                                        {
                                            AssetKit_HealthPickup mAssetKit = new AssetKit_HealthPickup();
                                            mPickups.Add(new HealthPickup(aaDisplay, this, mAssetKit.ASSET_NAME, new Vector2(s.mXPos, s.mYPos), mAssetKit.ORIGIN, Constants.LAYER_ENTITY_DEPTH, new Rectangle(mAssetKit.SRC_RECTX, mAssetKit.SRC_RECTY, mAssetKit.SRC_RECTWIDTH, mAssetKit.SRC_RECTHEIGHT), 0));
                                        }
                                    }
                                }
                                s.Destroy();
                            }

                            aaDisplay.mDisplayObjects.Remove(e.mDispObj);
                            mEntityList.Remove(e);
                        }
                    }
                    mEntityDeadList.Clear();

                    ResolveWorldCollisions();

                    aaGFXHandler.Update(DT);

                    aaDisplay.aaCamera.ChangeFocus(Player.mXPos, Player.mYPos);
                    aaDisplay.aaCamera.Update(DT);

                    foreach (UIElement e in aaDisplay.mUIElements)
                    {
                        e.Update(DT);
                    }
                    foreach (UIElement e in aaDisplay.mMenuUIElements)
                    {
                        e.Update(DT);
                    }


                    if (Player.IsDead())
                    {
                        aaDisplay.aaGame.IsMouseVisible = true;
               
                        if (ScoreTextBox == null)
                        {
                            ScoreTextBox = new UIElement_ScoreTextBox(aaDisplay, this, 340, 315);
                        }
                        if (UnlockText == null)
                        {
                            UnlockText = new UIElement_Text(aaDisplay, this, aaDisplay.aaGame.mTextboxFont, 480, 230, Color.White, "");
                            aaDisplay.AddMenuUIElement(UnlockText);
                        }
                        if (FinalScoreText == null)
                        {
                            FinalScoreText = new UIElement_Text(aaDisplay, this, aaDisplay.aaGame.mTextboxFont, 640 - (int)(aaDisplay.aaGame.mTextboxFont.MeasureString(mPoints.ToString()).X / 2), 260, Color.White, mPoints.ToString());
                            aaDisplay.AddMenuUIElement(FinalScoreText);
                        }
                        aaDisplay.AddMenuUIElement(GameOverPlaque);
                        ScoreTextBox.Show();
                        FinalScoreText.Show();
                        ScoreTextBox.Update(DT);

                        if (((UIElement_ScoreTextBox)ScoreTextBox).AcceptClicked() || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            aaDisplay.aaGame.aaHSManager.AddScore(((UIElement_ScoreTextBox)ScoreTextBox).GetText(), mPoints, mCurrentWave);
                            aaDisplay.aaGame.aaHSManager.SaveScores();
                            GameOverPlaque.MenuDestroy();
                            GameOverPlaque = null;
                            FinalScoreText.MenuDestroy();
                            FinalScoreText = null;
                            ScoreTextBox.Destroy();
                            ScoreTextBox = null;
                            aaDisplay.aaGame.mManager.ChangeState(aaDisplay.aaGame.mHighScoreMenu);
                        }
                        else if (((UIElement_ScoreTextBox)ScoreTextBox).CancelClicked() || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            aaDisplay.aaGame.mManager.ChangeState(aaDisplay.aaGame.mHighScoreMenu);
                            GameOverPlaque.MenuDestroy();
                            GameOverPlaque = null;
                            FinalScoreText.MenuDestroy();
                            FinalScoreText = null;
                            ScoreTextBox.Destroy();
                            ScoreTextBox = null;
                        }

                        //Manage unlocked ships and abilities
                        if ((aaDisplay.aaGame.mShipMenu as State_Menu_Ship).shipCount == 0)
                        {
                            if (mCurrentWave >= 10 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[0] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[0] = 2736;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                            if (mCurrentWave >= 20 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[1] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[1] = 1967;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                            if (mCurrentWave >= 30 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[2] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[2] = 8913;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                        }
                        else if ((aaDisplay.aaGame.mShipMenu as State_Menu_Ship).shipCount == 1)
                        {
                            if (mCurrentWave >= 10 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[3] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[3] = 8432;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                            if (mCurrentWave >= 20 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[4] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[4] = 6874;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                            if (mCurrentWave >= 30 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[5] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[5] = 8794;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                        }
                        else if ((aaDisplay.aaGame.mShipMenu as State_Menu_Ship).shipCount == 2)
                        {
                            if (mCurrentWave >= 10 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[6] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[6] = 3252;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                            if (mCurrentWave >= 20 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[7] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[7] = 3248;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                            if (mCurrentWave >= 30 && (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[8] == 0)
                            {
                                (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mUnlocks[8] = 8942;
                                UnlockText.mText = "NEW ABILITY UNLOCKED";
                            }
                        }
                        (aaDisplay.aaGame.mShipMenu as State_Menu_Ship).SaveData();    
                    }

                    foreach (SoundEffectInstance sfx in aaDisplay.aaGame.mGameSoundFX)
                    {
                        if (sfx.State == SoundState.Stopped)
                        {
                            sfx.Dispose();
                            aaDisplay.aaGame.mGameSoundFXRemove.Add(sfx);
                        }
                    }

                    foreach (SoundEffectInstance sfx in aaDisplay.aaGame.mGameSoundFXRemove)
                    {
                        aaDisplay.aaGame.mGameSoundFX.Remove(sfx);
                    }
                    aaDisplay.aaGame.mGameSoundFXRemove.Clear();
                }

                return true;
            }

            public void ResolveWorldCollisions()
            {
                //Collision List
                foreach (CircleCollision circ in CollisionShapes_Circle)
                {
                    /*
                    foreach (LineCollision line in CollisionShapes_Lines)
                    {
                        //circ.isCollidingLine(line); 
                    }
                     */

                    foreach (PointCollision point in CollisionShapes_Points)
                    {
                        if (circ.mShipOwner != null)
                        {
                            if (circ.isCollidingPoint(point) && circ.mShipOwner.mTeam != point.mProjOwner.mTeam)
                            {
                                circ.mShipOwner.ResolveCollision(point.mProjOwner.mProjKit.DAMAGE);
                                point.mProjOwner.ResolveCollision();
                            }
                        }
                        if (circ.mShipOwner == null)
                        {
                            if (circ.isCollidingPoint(point) && circ.mTeam != point.mProjOwner.mTeam)
                            {
                                point.mProjOwner.ResolveCollision();
                            }
                        }
                        if (circ.mProjOwner != null)
                        {
                            if (circ.isCollidingPoint(point) && circ.mProjOwner.mTeam != point.mProjOwner.mTeam)
                            {
                                circ.mProjOwner.ResolveCollision(null, point.mProjOwner);
                                point.mProjOwner.ResolveCollision(null, circ.mProjOwner);
                            }
                        }
                        if (circ.mIsShield)
                        {
                            if (circ.isCollidingPoint(point) && circ.mTeam != point.mProjOwner.mTeam)
                            {
                                point.mProjOwner.ResolveCollision();
                            }
                        }
                    }

                    foreach (CircleCollision circTwo in CollisionShapes_Circle)
                    {
                        if (circ != circTwo)
                        {
                            if (circ.mShipOwner != null && circTwo.mShipOwner != null)
                            {
                                if (circ.isCollidingCircle(circTwo) && circ.mShipOwner.mTeam != circTwo.mShipOwner.mTeam)
                                {
                                    circ.mShipOwner.ResolveCollision(0, circTwo.mShipOwner);
                                }
                            }
                            else if (circ.mShipOwner == null && circ.mIsShield && circTwo.mShipOwner != null)
                            {
                                if (circ.isCollidingCircle(circTwo) && circ.mTeam != circTwo.mShipOwner.mTeam)
                                {
                                    circTwo.mShipOwner.ResolveCollision(0, Player);
                                }
                            }
                            if (circ.mShipOwner != null && circTwo.mProjOwner != null)
                            {
                                if (circ.isCollidingCircle(circTwo) && circ.mShipOwner.mTeam != circTwo.mProjOwner.mTeam)
                                {
                                    circ.mShipOwner.ResolveCollision(circTwo.mProjOwner.mProjKit.DAMAGE);
                                    circTwo.mProjOwner.ResolveCollision(circ.mShipOwner);
                                }
                            }
                            if (circ.mProjOwner != null && circTwo.mProjOwner != null && circ.mProjOwner.mType == ProjectileType.AntiBullet)
                            {
                                if (circ.isCollidingCircle(circTwo) && circ.mProjOwner.mTeam != circTwo.mProjOwner.mTeam)
                                {
                                    circ.mProjOwner.ResolveCollision();
                                    circTwo.mProjOwner.ResolveCollision();
                                }
                            }
                        }
                        if (circ.mIsPickup && circTwo.mShipOwner != null && circ.isCollidingCircle(circTwo) && (circTwo.mShipOwner.mType == "Player_Agility" || circTwo.mShipOwner.mType == "Player_Commando" || circTwo.mShipOwner.mType == "Player_Tank"))
                        {
                            if (circ.mPickupOwner != null)
                            {
                                circ.mPickupOwner.PickUp(circTwo.mShipOwner);
                                circ.mPickupOwner.Destroy();
                            }
                        }
                    }
                }

                foreach (CircleCollision circ in CollisionShapes_Circle_ToRemove)
                {
                    CollisionShapes_Circle.Remove(circ);
                }
                CollisionShapes_Circle_ToRemove.Clear();

                foreach (PointCollision point in CollisionShapes_Points_ToRemove)
                {
                    CollisionShapes_Points.Remove(point);
                }
                CollisionShapes_Points_ToRemove.Clear();
            }
    }
}
