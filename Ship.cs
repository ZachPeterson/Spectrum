using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceWars
{
    class Ship : Entity
    {
        //Physics Variables
        //public float mThrustAccel;
        public float mVelXFinal;
        public float mVelYFinal;
        public float mXAccel;
        public float mYAccel;
        public float mMass;
        public float mCollisionAngle;

        //Ship Kit
        public ShipKit mKit;

        //Force Variables
        //public float mExternalForce;
        public float mThrustVelocity;
        public float mXForce, mYForce;

        public float aThrustPower;
        public float aMaxThrust;
        public float aTurnPower;
        public float aHull;

        public uint mTeam;

        public bool mDead;
        public bool mBoss;

        public Ability BulletSpam;
        public Ability Cloak;
        public Ability FireBullet;
        public Ability FireMultipleBullets;
        public Ability BulletStar;
        public Ability FireMine;
        public Ability FireMissile;
        public Ability FireMultipleMissles;
        public Ability ImmediateAboutFace;
        public Ability ImproveHandling;
        public Ability MissileFlurry;
        public Ability PathLaser;
        public Ability SlowShip;
        public Ability SuicideBomb;
        public Ability ThrustShip;
        public Ability TurnLeft;
        public Ability TurnRight;
        public Ability ChargeLaser;
        public Ability Shield;
        public Ability DeployBombGrunts;
        public Ability SpawnSatellite;
        public Ability QuickFix;
        public Ability OverchargeEngines;
        public Ability SiegeMode;
        public Ability TankShield;
        public Ability DualWield;
        public Ability WaveBlast;
        public Ability WarpDrive;
        public Ability FullShield;
        public Ability RearGuns;
        public Ability AntiBullets;
        public Ability RapidFire;

        public Ability Ability1;
        public Ability Ability2;
        public Ability Ability3;
        public Ability Ability4;

        public List<Behavior> mActiveBehaviors;
        public List<Buff> mActiveBuffs;

        public List<Behavior> mBehaviorsToRemove;
        public List<Buff> mBuffsToRemove;
        
        public List<Buff> mBuffsToTrigger;

        public List<Ability> mAllAbilities;

        public Ship mMySatellite;

        public SoundEffectInstance mSlowBeepInstance;
        public SoundEffectInstance mFastBeepInstance;

        public Ship(GameWorld pGameWorld, string pType, uint pTeam, float pXPos, float pYPos, ShipKit pKit, bool pBoss = false, float pRotation = 0, float pXVel = 0, float pYVel = 0, float pMass = 0.001f) : base(pGameWorld, pType, pXPos, pYPos, pRotation, pXVel, pYVel)
        {
            mType = pType;
            mMass = pMass;
            mXForce = 0;
            mYForce = 0;

            mTeam = pTeam;

            aTurnPower = 5;
            mKit = pKit;

            aThrustPower = mKit.THRUST_POWER;
            aMaxThrust = mKit.THRUST_MAX;
            aTurnPower = mKit.TURN_POWER;
            aHull = mKit.HULL;
            mDead = false;
            mBoss = pBoss;

            BulletSpam = new Ability_BulletSpam(this);
            Cloak = new Ability_Cloak(this);
            FireBullet = new Ability_FireBullet(this);
            FireMultipleBullets = new Ability_FireMultipleBullets(this);
            BulletStar = new Ability_BulletStar(this);
            FireMine = new Ability_FireMine(this);
            FireMissile = new Ability_FireMissile(this);
            FireMultipleMissles = new Ability_FireMultipleMissles(this);
            ImmediateAboutFace = new Ability_ImmediateAboutFace(this);
            ImproveHandling = new Ability_ImproveHandling(this);
            MissileFlurry = new Ability_MissileFlurry(this);
            PathLaser = new Ability_PathLaser(this);
            SlowShip = new Ability_SlowShip(this);
            SuicideBomb = new Ability_SuicideBomb(this);
            ThrustShip = new Ability_ThrustShip(this);
            TurnLeft = new Ability_TurnLeft(this);
            TurnRight = new Ability_TurnRight(this);
            ChargeLaser = new Ability_ChargeLaser(this);
            Shield = new Ability_SuperShield(this);
            DeployBombGrunts = new Ability_DeployBombGrunts(this);
            SpawnSatellite = new Ability_SpawnSatellite(this);
            QuickFix = new Ability_QuickFix(this);
            OverchargeEngines = new Ability_OverchargeEngines(this);
            TankShield = new Ability_TankShield(this);
            DualWield = new Ability_DualWield(this);
            WaveBlast = new Ability_WaveBlast(this);
            WarpDrive = new Ability_WarpDrive(this);
            FullShield = new Ability_FullShield(this);
            RearGuns = new Ability_RearGuns(this);
            AntiBullets = new Ability_ShootAntiBullets(this);
            RapidFire = new Ability_RapidFire(this);

            if (pType == "Player_Agility")
            {
                Ability1 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility1[0]);
                Ability2 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility2[0]);
                Ability3 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility3[0]);
                Ability4 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility4[0]);
            }
            else if (pType == "Player_Tank")
            {
                Ability1 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility1[1]);
                Ability2 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility2[1]);
                Ability3 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility3[1]);
                Ability4 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility4[1]);
            }
            else if (pType == "Player_Commando")
            {
                Ability1 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility1[2]);
                Ability2 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility2[2]);
                Ability3 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility3[2]);
                Ability4 = GetAbility((aaGameWorld.aaDisplay.aaGame.mShipMenu as State_Menu_Ship).Abilitydata.mAbility4[2]);
            }

            mAllAbilities = new List<Ability>();

            mAllAbilities.Add(BulletSpam);
            mAllAbilities.Add(Cloak);
            mAllAbilities.Add(FireBullet);
            mAllAbilities.Add(FireMultipleBullets);
            mAllAbilities.Add(BulletStar);
            mAllAbilities.Add(FireMine);
            mAllAbilities.Add(FireMissile);
            mAllAbilities.Add(FireMultipleMissles);
            mAllAbilities.Add(ImmediateAboutFace);
            mAllAbilities.Add(ImproveHandling);
            mAllAbilities.Add(MissileFlurry);
            mAllAbilities.Add(PathLaser);
            mAllAbilities.Add(SlowShip);
            mAllAbilities.Add(SuicideBomb);
            mAllAbilities.Add(ThrustShip);
            mAllAbilities.Add(TurnLeft);
            mAllAbilities.Add(TurnRight);
            mAllAbilities.Add(ChargeLaser);
            mAllAbilities.Add(Shield);
            mAllAbilities.Add(DeployBombGrunts);
            mAllAbilities.Add(SpawnSatellite);
            mAllAbilities.Add(QuickFix);
            mAllAbilities.Add(DualWield);
            mAllAbilities.Add(OverchargeEngines);
            mAllAbilities.Add(TankShield);
            mAllAbilities.Add(WaveBlast);
            mAllAbilities.Add(WarpDrive);
            mAllAbilities.Add(FullShield);
            mAllAbilities.Add(RearGuns);
            mAllAbilities.Add(AntiBullets);
            mAllAbilities.Add(RapidFire);

            mActiveBehaviors = new List<Behavior>();
            mActiveBuffs = new List<Buff>();

            mBehaviorsToRemove = new List<Behavior>();
            mBuffsToRemove = new List<Buff>();

            mBuffsToTrigger = new List<Buff>();

            mCircleCollision = new CircleCollision(mXPos, mYPos, mKit.COLLISION_SIZE, null, this);
            aaGameWorld.CollisionShapes_Circle.Add(mCircleCollision);
        }

        public Ability GetAbility(int AbilityID)
        {
            switch (AbilityID)
            {
                case Constants.QUICK_TURN:
                    return ImmediateAboutFace;
                case Constants.WARP_DRIVE:
                    return WarpDrive;
                case Constants.GUN_OVERCHARGE:
                    return RapidFire;
                case Constants.BULLET_STAR: 
                    return BulletStar;
                case Constants.BULLET_FURRY:
                    return BulletSpam;
                case Constants.DUAL_WIELD:
                    return DualWield;
                case Constants.OVERCHARGE_ENGINES:
                    return OverchargeEngines;
                case Constants.MISSILE_FLURRY:
                    return MissileFlurry;
                case Constants.SHOTGUN_BLAST:
                    return WaveBlast;
                case Constants.TIGHTER_TURNS:
                    return ImproveHandling;
                case Constants.ENERGY_SHIELD:
                    return TankShield;
                case Constants.REAR_GUN:
                    return RearGuns;
                case Constants.ANTI_BULLETS:
                    return AntiBullets;
                case Constants.SATELLITE:
                    return SpawnSatellite;
                case Constants.MINES:
                    return FireMine;
                case Constants.FULL_SHIELD:
                    return FullShield;

            }
            return null;
        }

        public override void Destroy()
        {
            base.Destroy();

            BulletSpam = null;
            Cloak = null;
            FireBullet = null;
            FireMultipleBullets = null;
            BulletStar = null;
            FireMine = null;
            FireMissile = null;
            FireMultipleMissles = null;
            ImmediateAboutFace = null;
            ImproveHandling = null;
            MissileFlurry = null;
            PathLaser = null;
            SlowShip = null;
            SuicideBomb = null;
            ThrustShip = null;
            TurnLeft = null;
            TurnRight = null;
            Shield = null;
            DeployBombGrunts = null;
            SpawnSatellite = null;
            QuickFix = null;
            OverchargeEngines = null;
            SiegeMode = null;
            TankShield = null;
            DualWield = null;
            WaveBlast = null;
            WarpDrive = null;
            FullShield = null;
            RearGuns = null;
            AntiBullets = null;
            RapidFire = null;

            Ability1 = null;
            Ability2 = null;
            Ability3 = null;
            Ability4 = null;

            mActiveBehaviors.Clear();
            mActiveBuffs.Clear();

            mBehaviorsToRemove.Clear();
            mBuffsToRemove.Clear();

            mBuffsToTrigger.Clear();

            mAllAbilities.Clear();

            if (mFastBeepInstance != null)
            {
                mFastBeepInstance.Stop();
                mFastBeepInstance.Dispose();
                mFastBeepInstance = null;
            }

            if (mSlowBeepInstance != null)
            {
                mSlowBeepInstance.Stop();
                mSlowBeepInstance.Dispose();
                mSlowBeepInstance = null;
            }
        }

        public void TriggerBehavior(BehaviorType pType)
        {
            if (pType == BehaviorType.None)
            {
                Console.WriteLine("ERROR: No behavior type specified for target ship");
            }
            else if (pType == BehaviorType.VelocityReversal)
            {
                Behavior_VelocityReversal b = new Behavior_VelocityReversal(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.PathLaser)
            {
                Behavior_PathLaser b = new Behavior_PathLaser(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.ThrustShip)
            {
                Behavior_ThrustShip b = new Behavior_ThrustShip(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.TurnLeft)
            {
                Behavior_TurnLeft b = new Behavior_TurnLeft(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.TurnRight)
            {
                Behavior_TurnRight b = new Behavior_TurnRight(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.SlowShip)
            {
                Behavior_SlowShip b = new Behavior_SlowShip(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.BulletSpam)
            {
                Behavior_BulletSpam b = new Behavior_BulletSpam(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.SuicideBomb)
            {
                Behavior_SuicideBomb b = new Behavior_SuicideBomb(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.ChargeLaser)
            {
                Behavior_ChargeLaser b = new Behavior_ChargeLaser(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.DualWield)
            {
                Behavior_DualWield b = new Behavior_DualWield(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else if (pType == BehaviorType.RearGuns)
            {
                Behavior_RearGuns b = new Behavior_RearGuns(this);
                b.Trigger();
                mActiveBehaviors.Add(b);
            }
            else
            {
                Console.WriteLine("ERROR: Behavior type was not a recognized BehaviorType");
            }

        }

        public void TriggerBuff(BuffType pType, float mParam = 0.0f)
        {
            if (pType == BuffType.None)
            {
                Console.WriteLine("ERROR: No buff type specified for target ship");
            }
            else if (pType == BuffType.ShipSlow)
            {
                Buff_ShipSlow e = new Buff_ShipSlow(this);
                e.SetSlowAmount(mParam);
                e.SetSlowDuration(1);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.LaserBuff)
            {
                Buff_LaserBuff e = new Buff_LaserBuff(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.ImproveHandling)
            {
                Buff_ImproveHandling e = new Buff_ImproveHandling(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.Cloak)
            {
                Buff_Cloak e = new Buff_Cloak(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.Decloak)
            {
                Buff_Decloak e = new Buff_Decloak(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.SuperShield)
            {
                Buff_SuperShield e = new Buff_SuperShield(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.OverchargeEngines)
            {
                Buff_OverchargeEngines e = new Buff_OverchargeEngines(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.TankShield)
            {
                Buff_TankShield e = new Buff_TankShield(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.FullShield)
            {
                Buff_FullShield e = new Buff_FullShield(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.AntiBullets)
            {
                Buff_AntiBullets e = new Buff_AntiBullets(this);
                mBuffsToTrigger.Add(e);
            }
            else if (pType == BuffType.RapidFire)
            {
                Buff_RapidFire e = new Buff_RapidFire(this);
                mBuffsToTrigger.Add(e);
            }
            else
            {
                Console.WriteLine("ERROR: Buff type was not a recognized BuffType");
            }
        }

        public void UpdateBehaviorAndBuffs(float pDT)
        {
            foreach (Buff e in mBuffsToTrigger)
            {
                e.Trigger();
                mActiveBuffs.Add(e);
            }
            mBuffsToTrigger.Clear();


            foreach (Buff e in mActiveBuffs)
            {
                e.Tracker(pDT);

                if (!e.IsBuffActive())
                {
                    mBuffsToRemove.Add(e);
                }
            }

            foreach (Behavior b in mActiveBehaviors)
            {
                b.Tracker(pDT);

                if (!b.IsBehaviorActive())
                {
                    mBehaviorsToRemove.Add(b);
                }
            }

            foreach (Behavior b in mBehaviorsToRemove)
            {
                mActiveBehaviors.Remove(b);
            }
            mBehaviorsToRemove.Clear();

            foreach (Buff b in mBuffsToRemove)
            {
                mActiveBuffs.Remove(b);
            }
            mBuffsToRemove.Clear();
        }

        public override void Update(float DT)
        {
            base.UpdateBrain(DT);

            foreach (Ability a in mAllAbilities)
            {
                a.Update(DT);
            }

            ApplyVelocity(mThrustVelocity, mRotation, DT);
            CalcVelComp(DT);
            UpdatePosition(DT);
            ApplyFriction();
            //TODO: Fix this Math.PI/2 shit
            mDispObj.RotateToAngle(mRotation + (float)(Math.PI/2));

            if (aHull < 0)
            {
                Die();
            }

            if (mThrustVelocity > aMaxThrust)
            {
                mThrustVelocity = aMaxThrust;
            }

            mCircleCollision.Update(mXPos, mYPos);

            mXVel = MathHelper.Clamp(mXVel, -1 * Constants.SHIP_MAX_VELOCITY, Constants.SHIP_MAX_VELOCITY);
            mYVel = MathHelper.Clamp(mYVel, -1 * Constants.SHIP_MAX_VELOCITY, Constants.SHIP_MAX_VELOCITY);
            mXForce = 0;
            mYForce = 0;
            aMaxThrust = mKit.THRUST_MAX;
            aTurnPower = mKit.TURN_POWER;
            base.Update(DT);

            // Make sure the ship does not go out of the playable area
            if (mXPos < mKit.COLLISION_SIZE)
            {
                mXPos = mKit.COLLISION_SIZE;
            }
            else if (mXPos > (Constants.REGION_PLAYABLE_WIDTH - mKit.COLLISION_SIZE))
            {
                mXPos = Constants.REGION_PLAYABLE_WIDTH - mKit.COLLISION_SIZE;
            }

            if (mYPos < mKit.COLLISION_SIZE)
            {
                mYPos = mKit.COLLISION_SIZE;
            }
            else if (mYPos > (Constants.REGION_PLAYABLE_HEIGHT - mKit.COLLISION_SIZE))
            {
                mYPos = Constants.REGION_PLAYABLE_HEIGHT - mKit.COLLISION_SIZE;
            }

            UpdateBehaviorAndBuffs(DT);
        }

        public void ApplyForce(float pForce, float pAngle)
        {
            CalcForceComp(pForce, pAngle);
        }

        public void ApplyVelocity(float pVelocity, float pAngle, float DT)
        {
            mXPos += pVelocity * (float)Math.Cos(pAngle) * DT;
            mYPos += pVelocity * (float)Math.Sin(pAngle) * DT;
        }

        public void CalcForceComp(float pForce, float pAngle)
        {
            //Break force into components and add them
            mXForce += (float)Math.Cos(pAngle) * pForce;
            mYForce += (float)Math.Sin(pAngle) * pForce;
        }

        public void ApplyFriction()
        {
            if (mXVel < 0)
                mXVel += Constants.WORLD_FRICTION;
            else if (mXVel > 0)
                mXVel -= Constants.WORLD_FRICTION;

            if (mYVel < 0)
                mYVel += Constants.WORLD_FRICTION;
            else if (mYVel > 0)
                mYVel -= Constants.WORLD_FRICTION;

            if (Math.Abs(mXVel) < Constants.WORLD_FRICTION)
                mXVel = 0;
            if (Math.Abs(mYVel) < Constants.WORLD_FRICTION)
                mYVel = 0;
        }

        public void CalcVelComp(float pDT)
        {
            mXAccel = mXForce / mMass;
            mYAccel = mYForce / mMass;
            mXVel += mXAccel * pDT;
            mYVel += mYAccel * pDT;
        }

        public void UpdatePosition(float pDT)
        {
            mXPos += mXVel * pDT;
            mYPos += mYVel * pDT;
        }

        public void ActivateAbility(AbilityType pAType, float param = 0.0f, bool param2 = false)
        {
            if (pAType == AbilityType.ThrustShip)
            {
                ThrustShip.AttemptActivate();
            }
            else if (pAType == AbilityType.SlowShip)
            {
                SlowShip.AttemptActivate();
            }
            else if (pAType == AbilityType.TurnLeft)
            {
                TurnLeft.AttemptActivate();
            }
            else if (pAType == AbilityType.TurnRight)
            {
                TurnRight.AttemptActivate();
            }
            else if (pAType == AbilityType.ImmediateAboutFace)
            {
                ImmediateAboutFace.AttemptActivate();
            }
            else if (pAType == AbilityType.Cloak)
            {
                Cloak.AttemptActivate();
            }
            else if (pAType == AbilityType.SuperShield)
            {
                Shield.AttemptActivate();
            }
            else if (pAType == AbilityType.ImproveHandling)
            {
                ImproveHandling.AttemptActivate();
            }
            else if (pAType == AbilityType.PathLaser)
            {
                PathLaser.AttemptActivate();
            }
            else if (pAType == AbilityType.FireBullet)
            {
                FireBullet.AttemptActivate(param, mXPos, mYPos, true);
            }
            else if (pAType == AbilityType.FireMultipleBullets)
            {
                FireMultipleBullets.AttemptActivate(param, param2);
            }
            else if (pAType == AbilityType.BulletStar)
            {
                BulletStar.AttemptActivate(param);
            }
            else if (pAType == AbilityType.BulletSpam)
            {
                BulletSpam.AttemptActivate();
            }
            else if (pAType == AbilityType.FireMine)
            {
                FireMine.AttemptActivate();
            }
            else if (pAType == AbilityType.FireMissile)
            {
                FireMissile.AttemptActivate(param);
            }
            else if (pAType == AbilityType.FireMultipleMissles)
            {
                FireMultipleMissles.AttemptActivate(param, param2);
            }
            else if (pAType == AbilityType.SuicideBomb)
            {
                SuicideBomb.AttemptActivate();
            }
            else if (pAType == AbilityType.ChargeLaser)
            {
                ChargeLaser.AttemptActivate();
            }
            else if (pAType == AbilityType.DeployBombGrunts)
            {
                DeployBombGrunts.AttemptActivate(param, param2);
            }
            else if (pAType == AbilityType.OverchargeEngines)
            {
                OverchargeEngines.AttemptActivate();
            }
            else if (pAType == AbilityType.SiegeMode)
            {
                SiegeMode.AttemptActivate();
            }
            else if (pAType == AbilityType.TankShield)
            {
                TankShield.AttemptActivate();
            }
            else if (pAType == AbilityType.FullShield)
            {
                FullShield.AttemptActivate();
            }
            else if (pAType == AbilityType.DualWield)
            {
                DualWield.AttemptActivate(param);
            }
            else if (pAType == AbilityType.WaveBlast)
            {
                WaveBlast.AttemptActivate();
            }
            else if (pAType == AbilityType.WarpDrive)
            {
                WarpDrive.AttemptActivate();
            }
            else if (pAType == AbilityType.AntiBullets)
            {
                AntiBullets.AttemptActivate();
            }
            else if (pAType == AbilityType.RearGuns)
            {
                RearGuns.AttemptActivate();
            }
            else if (pAType == AbilityType.RapidFire)
            {
                RapidFire.AttemptActivate();
            }
            else if (pAType == AbilityType.PlayerAbility1)
            {
                Ability1.AttemptActivate();
            }
            else if (pAType == AbilityType.PlayerAbility2)
            {
                Ability2.AttemptActivate();
            }
            else if (pAType == AbilityType.PlayerAbility3)
            {
                Ability3.AttemptActivate();
            }
            else if (pAType == AbilityType.PlayerAbility4)
            {
                Ability4.AttemptActivate();
            }
        }

        public override void ResolveCollision(int pDamage = 0, Ship pShip = null)
        {
            aHull -= pDamage;
            SoundEffectInstance mHitInstance = aaGameWorld.aaDisplay.aaGame.mHitSound.CreateInstance();
            mHitInstance.Volume = 0.2f;
            mHitInstance.Play();
            aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mHitInstance);
            mHitInstance = null;

            if (pShip != null)
            {
                if (this.mType != "Bomb_Ship")
                {
                    aHull -= (pShip.mKit.MASS / mKit.MASS) * Constants.COLLISION_DAMAGE;
                }
                mCollisionAngle = (float)Math.Atan2(pShip.mYPos - mYPos, pShip.mXPos - mXPos);
                ApplyForce((pShip.mKit.MASS / mKit.MASS) * Constants.COLLISION_FORCE, mCollisionAngle + (float)Math.PI);
                mHitInstance = aaGameWorld.aaDisplay.aaGame.mHitSound.CreateInstance();
                mHitInstance.Volume = 0.2f;
                mHitInstance.Play();
                aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mHitInstance);
            }

            base.ResolveCollision(pDamage, pShip);
        }

        public bool IsDead()
        {
            return mDead;
        }

        public void Die()
        {
            if (!mBoss)
            {
                aaGameWorld.aaGFXHandler.PlayEffect("Explosion_01", mXPos, mYPos, mAssetKit.COLOR_BLENDING);
            }
            else
            {
                aaGameWorld.aaGFXHandler.PlayEffect("Explosion_02", mXPos, mYPos, mAssetKit.COLOR_BLENDING);
            }

            SoundEffectInstance mExplosionInstance = aaGameWorld.aaDisplay.aaGame.mExplosion.CreateInstance();
            mExplosionInstance.Volume = 0.2f;
            mExplosionInstance.Play();
            aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mExplosionInstance);
            aaGameWorld.mEntityDeadList.Add(this);
            aaGameWorld.CollisionShapes_Circle_ToRemove.Add(mCircleCollision);
            mDead = true;
        }
    }
}
