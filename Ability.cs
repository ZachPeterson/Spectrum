using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceWars
{
    enum AbilityType
    {
        None,
        ImmediateAboutFace,
        PathLaser,
        ThrustShip,
        TurnLeft,
        TurnRight,
        SlowShip,
        BulletSpam,
        BulletStar,
        ImproveHandling,
        FireBullet,
        FireMultipleBullets,
        FireLaser,
        FireMissile,
        FireMultipleMissles,
        FireMine,
        Cloak,
        SuicideBomb,
        MissileFlurry,
        ChargeLaser,
        PlayerAbility1,
        PlayerAbility2,
        PlayerAbility3,
        PlayerAbility4,
        SuperShield,
        DeployBombGrunts,
        SpawnSatellite,
        QuickFix,
        OverchargeEngines,
        SiegeMode,
        TankShield,
        DualWield,
        WarpDrive,
        WaveBlast,
        RearGuns,
        AntiBullets,
        FullShield,
        RapidFire,
    };

    class Ability
    {
        public float mCooldown;
        public float mElapsedTime;

        public Ship aaTargetShip;
        public SoundEffectInstance mShootInstance;

        public Ability(Ship pTarget, float pAbilityCooldown)
        {
            aaTargetShip = pTarget;

            mCooldown = pAbilityCooldown;
            mElapsedTime = pAbilityCooldown;
        }

        public virtual void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public virtual void Update(float pElapsedTime)
        {
            mElapsedTime += pElapsedTime;
        }

        public virtual void ActivateAbility()
        {
        }

        public virtual void AttemptActivate(float param)
        {
        }

        public virtual void AttemptActivate(float param, bool param2 = false)
        {
        }

        public virtual void AttemptActivate(float param, float pXPos, float pYPos, bool pPlaySound)
        {
        }

        public virtual void ActivateAbility(float param)
        {
        }

        public virtual void ActivateAbility(float param, bool param2 = false)
        {
        }

        public virtual void ActivateAbility(float param, float pXPos, float pYPos, bool pPlaySound)
        {
        }

        public float GetCooldownPercentage()
        {
            return MathHelper.Clamp(mElapsedTime / mCooldown, 0.0f, 1.0f);
        }

        public void PlayShootSound(float pVolume)
        {
            mShootInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mShootSound.CreateInstance();
            mShootInstance.Volume = pVolume;
            mShootInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mShootInstance);
        }
    }

    class Ability_ImmediateAboutFace : Ability
    {
        private const float mImmediateAboutFaceCooldown = 10.0f;

        public Ability_ImmediateAboutFace(Ship pTarget)
            : base(pTarget, mImmediateAboutFaceCooldown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the quick turn sound
            SoundEffectInstance mQuickTurnInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mQuickTurnSound.CreateInstance();
            mQuickTurnInstance.Volume = 0.8f;
            mQuickTurnInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mQuickTurnInstance);

            aaTargetShip.TriggerBehavior(BehaviorType.VelocityReversal);
        }
    }

    class Ability_PathLaser : Ability
    {
        private const float mPathLaserCooldown = 30.0f;

        public Ability_PathLaser(Ship pTarget)
            : base(pTarget, mPathLaserCooldown)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.PathLaser);
            aaTargetShip.TriggerBuff(BuffType.LaserBuff);
        }
    }

    class Ability_ThrustShip : Ability
    {
        public Ability_ThrustShip(Ship pTarget)
            : base(pTarget, 0.0f)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.ThrustShip);
        }
    }

    class Ability_TurnLeft : Ability
    {
        public Ability_TurnLeft(Ship pTarget)
            : base(pTarget, 0.0f)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.TurnLeft);
        }
    }

    class Ability_TurnRight : Ability
    {
        public Ability_TurnRight(Ship pTarget)
            : base(pTarget, 0.0f)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.TurnRight);
        }
    }

    class Ability_SlowShip : Ability
    {
        public Ability_SlowShip(Ship pTarget)
            : base(pTarget, 0.0f)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.SlowShip);
        }
    }

    class Ability_BulletSpam : Ability
    {
        // 1 full rotation, four streams, 3-4 seconds
        private const float mBulletSpamCoolDown = 30.0f;

        public Ability_BulletSpam(Ship pTarget)
            : base(pTarget, mBulletSpamCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the bullet spam sound
            SoundEffectInstance mBulletSpamInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mBulletSpamSound.CreateInstance();
            mBulletSpamInstance.Volume = 0.8f;
            mBulletSpamInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mBulletSpamInstance);

            aaTargetShip.TriggerBehavior(BehaviorType.BulletSpam);
        }
    }

    class Ability_BulletStar : Ability
    {
        private const float mBulletStarCoolDown = 8.0f;

        public Ability_BulletStar(Ship pTarget)
            : base(pTarget, mBulletStarCoolDown)
        {
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            for (float i = 0; i < 2 * Math.PI; i += (float)(Math.PI / 20))
            {
                if (aaTargetShip.mTeam == Constants.PLAYER_TEAM)
                {
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), i, ProjectileKitType.PlayerBullet);
                }
                else
                {
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), i, ProjectileKitType.EnemyBullet);
                }
                PlayShootSound(0.01f);
            }
        }
    }

    class Ability_ImproveHandling : Ability
    {
        private const float mImproveHandlingCoolDown = 15.0f;

        public Ability_ImproveHandling(Ship pTarget)
            : base(pTarget, mImproveHandlingCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            SoundEffectInstance mTighterTurns = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mTighterTurns.CreateInstance();
            mTighterTurns.Volume = 0.8f;
            mTighterTurns.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mTighterTurns);
            aaTargetShip.TriggerBuff(BuffType.ImproveHandling);
        }
    }

    class Ability_FireBullet : Ability
    {
        private const float mFireRate = 0.1f;

        public Ability_FireBullet(Ship pTarget)
            : base(pTarget, mFireRate)
        {
        }

        public override void AttemptActivate(float param, float pPosX, float pPosY, bool pPlaySound)
        {
            ActivateAbility(param, pPosX, pPosY, pPlaySound);
        }

        public override void ActivateAbility(float param, float pXPos, float pYPos, bool pPlaySound)
        {
            double variance = ((2 * aaTargetShip.aaGameWorld.mRand.NextDouble()) - 1.0d) * (Math.PI / 64.0d);
            if (aaTargetShip.mTeam == Constants.PLAYER_TEAM)
            {
                bool antiBulletsActive = false;
                if(aaTargetShip == aaTargetShip.aaGameWorld.Player)
                    ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 8;
                foreach (Buff b in aaTargetShip.mActiveBuffs)
                {
                    if (b as Buff_AntiBullets != null)
                    {
                        antiBulletsActive = true;
                    }
                    if (b as Buff_RapidFire != null && aaTargetShip == aaTargetShip.aaGameWorld.Player)
                        ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 4;
                }
                if (antiBulletsActive)
                {
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos, pYPos), param + (float)variance, ProjectileKitType.AntiBullet);
                }
                else
                {
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), param + (float)variance, ProjectileKitType.PlayerBullet);
                }

                if(pPlaySound)
                    PlayShootSound(0.2f);
            }
            else
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), param + (float)variance, ProjectileKitType.EnemyBullet);
                if(pPlaySound)
                    PlayShootSound(0.1f);
            }
        }
    }

    class Ability_FireMultipleBullets : Ability
    {
        private const float mFireRate = 0.1f;

        private Vector2[] mBulletOffsets;

        public Ability_FireMultipleBullets(Ship pTarget)
            : base(pTarget, mFireRate)
        {
            mBulletOffsets = new Vector2[4];
        }

        public override void AttemptActivate(float param, bool param2)
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility(param, param2);
            }
        }

        public override void ActivateAbility(float param, bool param2)
        {
            double variance = ((2 * aaTargetShip.aaGameWorld.mRand.NextDouble()) - 1.0d) * (Math.PI / 64.0d);

            mBulletOffsets[0].Y = -30 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[0].X = -30 * (float)Math.Sin(aaTargetShip.mRotation);
            mBulletOffsets[1].Y = -45 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[1].X = -45 * (float)Math.Sin(aaTargetShip.mRotation);
            mBulletOffsets[2].Y = 45 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[2].X = 45 * (float)Math.Sin(aaTargetShip.mRotation);
            mBulletOffsets[3].Y = 30 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[3].X = 30 * (float)Math.Sin(aaTargetShip.mRotation);

            aaTargetShip.FireBullet.AttemptActivate(param, aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y, false);
            aaTargetShip.FireBullet.AttemptActivate(param, aaTargetShip.mXPos + mBulletOffsets[3].X, aaTargetShip.mYPos + mBulletOffsets[3].Y, false);

            if (param2)
            {
                aaTargetShip.FireBullet.AttemptActivate(param, aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[1].Y, false);
                aaTargetShip.FireBullet.AttemptActivate(param, aaTargetShip.mXPos + mBulletOffsets[3].X, aaTargetShip.mYPos + mBulletOffsets[2].Y, false);
                aaTargetShip.FireBullet.AttemptActivate(param, aaTargetShip.mXPos, aaTargetShip.mYPos, false);
            }

            PlayShootSound(0.1f);
        }
    }

    class Ability_FireMissile : Ability
    {
        private const float mFireRate = 2.5f;

        public Ability_FireMissile(Ship pTarget)
            : base(pTarget, mFireRate)
        {
        }

        public override void AttemptActivate(float param)
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility(param);
            }
        }

        public override void ActivateAbility(float param)
        {
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), aaTargetShip.mRotation, ProjectileKitType.Missile);
        }
    }

    class Ability_FireMultipleMissles : Ability
    {
        private const float mFireRate = 2.5f;
        private Vector2[] mBulletOffsets;

        public Ability_FireMultipleMissles(Ship pTarget)
            : base(pTarget, mFireRate)
        {
            mBulletOffsets = new Vector2[2];
        }

        public override void AttemptActivate(float param, bool param2)
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility(param, param2);
            }
        }

        public override void ActivateAbility(float param, bool param2)
        {
            //Calculate missle spawn offset
            mBulletOffsets[0].Y = -55 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[0].X = -55 * (float)Math.Sin(aaTargetShip.mRotation);
            mBulletOffsets[1].Y = 55 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[1].X = 55 * (float)Math.Sin(aaTargetShip.mRotation);

            //Fire 3 missles per wing
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y), param, ProjectileKitType.Missile);
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y), param + 0.2f, ProjectileKitType.Missile);
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y), param - 0.2f, ProjectileKitType.Missile);
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y), param, ProjectileKitType.Missile);
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y), param + 0.2f, ProjectileKitType.Missile);
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y), param - 0.2f, ProjectileKitType.Missile);

            if (param2)
            {
                //Shoot 5 missles per wing if almost about to die
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y), param - 0.4f, ProjectileKitType.Missile);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y), param - 0.4f, ProjectileKitType.Missile);

                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y), param + 0.4f, ProjectileKitType.Missile);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y), param - 0.4f, ProjectileKitType.Missile);
            }
        }
    }

    class Ability_FireLaser : Ability
    {
        private const float mFireRate = 0.0f;

        public Ability_FireLaser(Ship pTarget)
            : base(pTarget, mFireRate)
        {
        }

        public override void ActivateAbility()
        {
        }
    }

    class Ability_FireMine : Ability
    {
        private const float mFireRate = 10.0f;

        public Ability_FireMine(Ship pTarget)
            : base(pTarget, mFireRate)
        {
            if (aaTargetShip.mTeam == 0)
                mCooldown = 5.0f;
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Mine, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), 0, ProjectileKitType.Mine);
            if (aaTargetShip.mTeam == 0)
            {
                SoundEffectInstance mMineInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mMineSound.CreateInstance();
                mMineInstance.Volume = 0.8f;
                mMineInstance.Play();
                aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mMineInstance);
            }
        }
    }

    class Ability_Cloak : Ability
    {
        private const float mCloakCoolDown = 30.0f;

        public Ability_Cloak(Ship pTarget)
            : base(pTarget, mCloakCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            // Activate cloak effect
            aaTargetShip.TriggerBuff(BuffType.Cloak);
        }
    }

    class Ability_SuperShield : Ability
    {
        private const float mShieldCoolDown = 25.0f;

        public Ability_SuperShield(Ship pTarget)
            : base(pTarget, mShieldCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBuff(BuffType.SuperShield);
        }
    }

    class Ability_SuicideBomb : Ability
    {
        private const float mSuidiceRecharge = 100.0f;

        public Ability_SuicideBomb(Ship pTarget)
            : base(pTarget, mSuidiceRecharge)
        {
            mCooldown = mSuidiceRecharge;
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.SuicideBomb);
        }

    }

    class Ability_ChargeLaser : Ability
    {
        private const float mChargeLaserRecharge = 3.0f;

        public Ability_ChargeLaser(Ship pTarget)
            : base(pTarget, mChargeLaserRecharge)
        {
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBehavior(BehaviorType.ChargeLaser);
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
                (aaTargetShip.mBrain as Brain_Laser).isCharging = true;
            }
        }
    }

    class Ability_DeployBombGrunts : Ability
    {
        private const float mCoolDown = 7.0f;

        private Vector2[] mBulletOffsets;

        public Ability_DeployBombGrunts(Ship pTarget)
            : base(pTarget, mCoolDown)
        {
            mBulletOffsets = new Vector2[2];
            mElapsedTime = 0.0f;
        }

        public override void AttemptActivate(float param, bool param2)
        {
            if (param2)
            {
                mCooldown = 2.0f;
            }
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility(param);
            }
        }

        public override void ActivateAbility(float param)
        {
            //Calculate bomb spawn offset
            mBulletOffsets[0].Y = -65 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[0].X = -65 * (float)Math.Sin(aaTargetShip.mRotation);
            mBulletOffsets[1].Y = 65 * -(float)Math.Cos(aaTargetShip.mRotation);
            mBulletOffsets[1].X = 65 * (float)Math.Sin(aaTargetShip.mRotation);

            Ship Bomber1 = new Ship(aaTargetShip.aaGameWorld, "Bomb_Ship", Constants.ENEMY_TEAM, aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y, KitManager.BombShipKit);
            Ship Bomber2 = new Ship(aaTargetShip.aaGameWorld, "Bomb_Ship", Constants.ENEMY_TEAM, aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y, KitManager.BombShipKit);
            Bomber1.mRotation = aaTargetShip.mRotation;
            Bomber1.ApplyForce(100, aaTargetShip.mRotation);
            Bomber2.mRotation = aaTargetShip.mRotation;
            Bomber2.ApplyForce(100, aaTargetShip.mRotation);
            aaTargetShip.aaGameWorld.mToSpawnEntityList.Add(Bomber1);
            aaTargetShip.aaGameWorld.mToSpawnEntityList.Add(Bomber2);
        }
    }

    class Ability_MissileFlurry : Ability
    {
        private const float mMissileFlurryCoolDown = 15.0f;

        public Ability_MissileFlurry(Ship pTarget)
            : base(pTarget, mMissileFlurryCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the missile flurry sound
            SoundEffectInstance mMissileFlurryInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mMissileFlurrySound.CreateInstance();
            mMissileFlurryInstance.Volume = 0.8f;
            mMissileFlurryInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mMissileFlurryInstance);

            List<Ship> shipsToHit = new List<Ship>();
            int currentShip = 0;
            int shipsFound = 0;

            while (shipsToHit.Count < 4)
            {
                if (currentShip < aaTargetShip.aaGameWorld.mEntityList.Count)
                {
                    if (aaTargetShip.aaGameWorld.mEntityList[currentShip] is Ship && (aaTargetShip.aaGameWorld.mEntityList[currentShip] as Ship).mTeam != aaTargetShip.mTeam)
                    {
                         shipsToHit.Add(aaTargetShip.aaGameWorld.mEntityList[currentShip] as Ship);
                         shipsFound++;
                    }
                }
                else
                {
                    break;
                }

                currentShip++;
            }

            shipsToHit = SortShipDistanceList(shipsToHit);

            foreach (Entity e in aaTargetShip.aaGameWorld.mEntityList)
            {
                bool shouldContinue = true;
                if (e != aaTargetShip && e is Ship && (e as Ship).mTeam != aaTargetShip.mTeam)
                {
                    foreach (Ship s in shipsToHit)
                    {
                        if (e as Ship == s)
                        {
                            shouldContinue = false;
                            break;
                        }
                    }

                    if (shouldContinue)
                    {
                        for (int i = 0; i < shipsToHit.Count; i++)
                        {
                            if (Vector2.Distance(new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), new Vector2(e.mXPos, e.mYPos)) < Vector2.Distance(new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), new Vector2(shipsToHit[i].mXPos, shipsToHit[i].mYPos)))
                            {
                                shipsToHit[i] = e as Ship;
                                shipsToHit = SortShipDistanceList(shipsToHit);
                                break;
                            }
                        }
                    }
                }
            }

            foreach (Ship s in shipsToHit)
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Missile, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), aaTargetShip.mRotation, ProjectileKitType.Missile, s);
            }
        }

        private List<Ship> SortShipDistanceList(List<Ship> pShipDistanceList)
        {
            List<Ship> sortedList = pShipDistanceList;

            for (int i = 2; i < sortedList.Count; i++)
            {
                for (int k = i; k > 1; k--)
                {
                    if (Vector2.Distance(new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), new Vector2(sortedList[k].mXPos, sortedList[k].mYPos)) < Vector2.Distance(new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), new Vector2(sortedList[k - 1].mXPos, sortedList[k - 1].mYPos)))
                    {
                        Ship temp = sortedList[k];
                        sortedList[k] = sortedList[k - 1];
                        sortedList[k - 1] = temp;
                    }
                }
            }
            return sortedList;
        }
    }

    class Ability_SpawnSatellite : Ability
    {
        private Ship mTarget;
        private const float mCoolDown = 60.0f;
        private Ship mSatellite = null;

        public Ability_SpawnSatellite(Ship pTarget)
            : base(pTarget, mCoolDown)
        {
            mTarget = pTarget;
        }

        public override void Update(float pElapsedTime)
        {
            if(mTarget.aaGameWorld.mEntityList.Contains(mSatellite))
            {
                mElapsedTime = 0.0f;
            }

            base.Update(pElapsedTime);
        }

        public override void AttemptActivate()
        {
            if (!mTarget.aaGameWorld.mEntityList.Contains(mSatellite))
            {
                mSatellite = null;
            }
            
            if (mElapsedTime >= mCooldown && mSatellite == null)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            // Play the satellite sound
            SoundEffectInstance mSatelliteInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mSatelliteSound.CreateInstance();
            mSatelliteInstance.Volume = 0.8f;
            mSatelliteInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mSatelliteInstance);

            mSatellite = new Ship(aaTargetShip.aaGameWorld, "Satellite", Constants.PLAYER_TEAM, mTarget.mXPos, mTarget.mYPos, KitManager.SatelliteShipKit);
            mTarget.mMySatellite = mSatellite;

            if (mTarget.FireBullet as Ability_ShootTier2 != null)
            {
                mSatellite.FireBullet = new Ability_ShootTier2(mSatellite);
            }
            else if (mTarget.FireBullet as Ability_ShootTier3 != null)
            {
                mSatellite.FireBullet = new Ability_ShootTier3(mSatellite);
            }
            else if (mTarget.FireBullet as Ability_ShootTier4 != null)
            {
                mSatellite.FireBullet = new Ability_ShootTier4(mSatellite);
            }

            mTarget.aaGameWorld.mToSpawnEntityList.Add(mSatellite);
        }
    }

    class Ability_QuickFix : Ability
    {
        private const float mCoolDown = 30.0f;

        private Ship mTarget;

        public Ability_QuickFix(Ship pTarget)
            : base(pTarget, mCoolDown)
        {
            mTarget = pTarget;
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown && mTarget.aHull < 50)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            // Play the quick fix sound
            SoundEffectInstance mQuickFixInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mQuickFixSound.CreateInstance();
            mQuickFixInstance.Volume = 0.8f;
            mQuickFixInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mQuickFixInstance);

            mTarget.aHull += mTarget.aHull * 0.2f;
            //mTarget.aaGameWorld.aaGFXHandler.PlayEffect("QuickFix", mTarget.mXPos, mTarget.mYPos, mTarget.mAssetKit.COLOR_BLENDING);
        }
    }

    class Ability_OverchargeEngines : Ability
    {
        private const float mOverchargeEnginesCoolDown = 10.0f;

        public Ability_OverchargeEngines(Ship pTarget)
            : base(pTarget, mOverchargeEnginesCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the overcharge engines sound
            SoundEffectInstance mOverchargeEnginesInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mOverchargeEnginesSound.CreateInstance();
            mOverchargeEnginesInstance.Volume = 0.8f;
            mOverchargeEnginesInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mOverchargeEnginesInstance);

            aaTargetShip.TriggerBuff(BuffType.OverchargeEngines);
        }
    }

    class Ability_TankShield : Ability
    {
        private const float mTankShieldCoolDown = 20.0f;

        public Ability_TankShield(Ship pTarget)
            : base(pTarget, mTankShieldCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the tank shield sound
            SoundEffectInstance mTankShieldInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mTankShieldSound.CreateInstance();
            mTankShieldInstance.Volume = 0.8f;
            mTankShieldInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mTankShieldInstance);

            aaTargetShip.TriggerBuff(BuffType.TankShield);
            mElapsedTime -= Buff_TankShield.mShieldDuration;
        }
    }

    class Ability_FullShield : Ability
    {
        private const float mTankShieldCoolDown = 20.0f;

        public Ability_FullShield(Ship pTarget)
            : base(pTarget, mTankShieldCoolDown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the tank shield sound
            SoundEffectInstance mTankShieldInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mTankShieldSound.CreateInstance();
            mTankShieldInstance.Volume = 0.8f;
            mTankShieldInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mTankShieldInstance);

            aaTargetShip.TriggerBuff(BuffType.FullShield);
            mElapsedTime -= Buff_FullShield.mShieldDuration;
        }
    }

    class Ability_DualWield : Ability
    {
        public static float mCoolDown = 20.0f;

        public Ability_DualWield(Ship pTarget)
            : base(pTarget, mCoolDown)
        {

        }

        public override void AttemptActivate()
        {
            if (mElapsedTime > mCoolDown)
            {
                mElapsedTime = 0;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            // Play the dual wield sound
            SoundEffectInstance mDualWieldInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mDualWieldSound.CreateInstance();
            mDualWieldInstance.Volume = 0.8f;
            mDualWieldInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mDualWieldInstance);
            aaTargetShip.TriggerBehavior(BehaviorType.DualWield);
        }
    }

    class Ability_WaveBlast : Ability
    {
        private const float mWaveBlastCooldown = 15.0f;
        private const int mNumberOfBullets = 50;
        private const float mAngleBetweenBullets = (float)(Math.PI / 256);

        public Ability_WaveBlast(Ship pTarget)
            : base(pTarget, mWaveBlastCooldown)
        {
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            // Play the wave blast sound
            SoundEffectInstance mWaveBlastInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mWaveBlastSound.CreateInstance();
            mWaveBlastInstance.Volume = 0.8f;
            mWaveBlastInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mWaveBlastInstance);

            // Spawn the bullet in the middle of the wave
            aaTargetShip.aaGameWorld.mProjectileList.Add(new Projectile(aaTargetShip.aaGameWorld, aaTargetShip.mTeam, ProjectileType.Bullet, aaTargetShip.mXPos, aaTargetShip.mYPos, aaTargetShip.mRotation, KitManager.PlayerBulletProjectileKit));

            // Spawn the wave of bullets
            for (int i = 0; i < mNumberOfBullets / 2; i++)
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), aaTargetShip.mRotation + i * mAngleBetweenBullets, ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), aaTargetShip.mRotation - i * mAngleBetweenBullets, ProjectileKitType.PlayerBullet);
            }
        }
    }

    class Ability_WarpDrive : Ability
    {
        private static float mWarpDriveCooldown = 4.0f;
        private static float mWarpDistance = 100.0f;

        public Ability_WarpDrive(Ship pTarget)
            : base(pTarget, mWarpDriveCooldown)
        {
        }

        public override void ActivateAbility()
        {
            // Play the warp drive sound
            SoundEffectInstance mWarpDriveInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mWarpDriveSound.CreateInstance();
            mWarpDriveInstance.Volume = 0.5f;
            mWarpDriveInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mWarpDriveInstance);

            float delX = (float)(Math.Cos(aaTargetShip.mRotation) * mWarpDistance);
            float delY = (float)(Math.Sin(aaTargetShip.mRotation) * mWarpDistance);
            aaTargetShip.mXPos += delX;
            aaTargetShip.mYPos += delY;
        }
    }

    class Ability_ShootTier2 : Ability
    {
        private const float mFireRate = 0.04f;
        private const int mSeperationDistance = 10;

        public Ability_ShootTier2(Ship pTarget)
            : base(pTarget, mFireRate)
        {
        }

        public override void AttemptActivate(float param, float pPosX, float pPosY, bool pPlaySound)
        {
            ActivateAbility(param, pPosX, pPosY, pPlaySound);
        }

        public override void ActivateAbility(float param, float pXPos, float pYPos, bool pPlaySound)
        {
            double variance = ((2 * aaTargetShip.aaGameWorld.mRand.NextDouble()) - 1.0d) * (Math.PI / 64.0d);
            float angle = param + (float)variance;
            float tangent = angle + (float)(Math.PI / 2);
            int seperationX = (int)(Math.Cos(tangent) * mSeperationDistance);
            int seperationY = (int)(Math.Sin(tangent) * mSeperationDistance);

            bool antiBulletsActive = false;
                if(aaTargetShip == aaTargetShip.aaGameWorld.Player)
                    ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 8;
                foreach (Buff b in aaTargetShip.mActiveBuffs)
                {
                    if (b as Buff_AntiBullets != null)
                    {
                        antiBulletsActive = true;
                    }
                    if (b as Buff_RapidFire != null && aaTargetShip == aaTargetShip.aaGameWorld.Player)
                        ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 4;
                }
            if (antiBulletsActive)
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos + seperationX, pYPos + seperationY), angle, ProjectileKitType.AntiBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos - seperationX, pYPos - seperationY), angle, ProjectileKitType.AntiBullet);
            }
            else
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos + seperationX, pYPos + seperationY), angle, ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos - seperationX, pYPos - seperationY), angle, ProjectileKitType.PlayerBullet);
            }

            if (pPlaySound)
                PlayShootSound(0.2f);
        }
    }

    class Ability_ShootTier3 : Ability
    {
        private const float mFireRate = 0.05f;

        public Ability_ShootTier3(Ship pTarget)
            : base(pTarget, mFireRate)
        {
        }

        public override void AttemptActivate(float param, float pPosX, float pPosY, bool pPlaySound)
        {
            ActivateAbility(param, pPosX, pPosY, pPlaySound);
        }

        public override void ActivateAbility(float param, float pXPos, float pYPos, bool pPlaySound)
        {
            double variance = ((2 * aaTargetShip.aaGameWorld.mRand.NextDouble()) - 1.0d) * (Math.PI / 64.0d);
            float angle = param + (float)variance;

            bool antiBulletsActive = false;
                if(aaTargetShip == aaTargetShip.aaGameWorld.Player)
                    ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 8;
                foreach (Buff b in aaTargetShip.mActiveBuffs)
                {
                    if (b as Buff_AntiBullets != null)
                    {
                        antiBulletsActive = true;
                    }
                    if (b as Buff_RapidFire != null && aaTargetShip == aaTargetShip.aaGameWorld.Player)
                        ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 4;
                }
            if (antiBulletsActive)
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos, pYPos), angle - (float)(Math.PI / 8), ProjectileKitType.AntiBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos, pYPos), angle, ProjectileKitType.AntiBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos, pYPos), angle + (float)(Math.PI / 8), ProjectileKitType.AntiBullet);
            }
            else
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), angle - (float)(Math.PI / 8), ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), angle, ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), angle + (float)(Math.PI / 8), ProjectileKitType.PlayerBullet);
            }

            if (pPlaySound)
                PlayShootSound(0.2f);
        }
    }

    class Ability_ShootTier4 : Ability
    {
        private const float mFireRate = 0.04f;
        private const int mSeperationDistance = 10;

        public Ability_ShootTier4(Ship pTarget)
            : base(pTarget, mFireRate)
        {
        }

        public override void AttemptActivate(float param, float pPosX, float pPosY, bool pPlaySound)
        {
            ActivateAbility(param, pPosX, pPosY, pPlaySound);
        }

        public override void ActivateAbility(float param, float pXPos, float pYPos, bool pPlaySound)
        {
            double variance = ((2 * aaTargetShip.aaGameWorld.mRand.NextDouble()) - 1.0d) * (Math.PI / 64.0d);
            float angle = param + (float)variance;
            float tangent = angle + (float)(Math.PI / 2);
            int seperationX = (int)(Math.Cos(tangent) * mSeperationDistance);
            int seperationY = (int)(Math.Sin(tangent) * mSeperationDistance);

            bool antiBulletsActive = false;
                if(aaTargetShip == aaTargetShip.aaGameWorld.Player)
                    ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 8;
                foreach (Buff b in aaTargetShip.mActiveBuffs)
                {
                    if (b as Buff_AntiBullets != null)
                    {
                        antiBulletsActive = true;
                    }
                    if (b as Buff_RapidFire != null && aaTargetShip == aaTargetShip.aaGameWorld.Player)
                        ((aaTargetShip.mBrain) as Brain_Player).mShootTrackerMax = 4;
                }
            if (antiBulletsActive)
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos, pYPos), angle - (float)(Math.PI / 8), ProjectileKitType.AntiBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos + seperationX, pYPos + seperationY), angle, ProjectileKitType.AntiBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos - seperationX, pYPos - seperationY), angle, ProjectileKitType.AntiBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.AntiBullet, new Vector2(pXPos, pYPos), angle + (float)(Math.PI / 8), ProjectileKitType.AntiBullet);
            }
            else
            {
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), angle - (float)(Math.PI / 8), ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos + seperationX, pYPos + seperationY), angle, ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos - seperationX, pYPos - seperationY), angle, ProjectileKitType.PlayerBullet);
                aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(pXPos, pYPos), angle + (float)(Math.PI / 8), ProjectileKitType.PlayerBullet);
            }

            if (pPlaySound)
                PlayShootSound(0.2f);
        }
    }

    class Ability_RearGuns : Ability
    {
        public static float mCoolDown = 15.0f;

        public Ability_RearGuns(Ship pTarget)
            : base(pTarget, mCoolDown)
        {

        }

        public override void AttemptActivate()
        {
            if (mElapsedTime > mCoolDown)
            {
                mElapsedTime = 0;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            // Play the dual wield sound
            SoundEffectInstance mRearGunsInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mDualWieldSound.CreateInstance();
            mRearGunsInstance.Volume = 0.8f;
            mRearGunsInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mRearGunsInstance);
            aaTargetShip.TriggerBehavior(BehaviorType.RearGuns);
        }
    }

    class Ability_ShootAntiBullets : Ability
    {
        private const float mAntiBulletsDuration = 15.0f;

        public Ability_ShootAntiBullets(Ship pTarget)
            : base(pTarget, mAntiBulletsDuration)
        {
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            SoundEffectInstance mAntiBulletInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mAntiBulletSound.CreateInstance();
            mAntiBulletInstance.Volume = 0.8f;
            mAntiBulletInstance.Play();
            aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mAntiBulletInstance);
            aaTargetShip.TriggerBuff(BuffType.AntiBullets);
        }
    }

    class Ability_RapidFire : Ability
    {
        private const float mRapidFireDuration = 15.0f;

        public Ability_RapidFire(Ship pTarget)
            : base(pTarget, mRapidFireDuration)
        {
        }

        public override void AttemptActivate()
        {
            if (mElapsedTime >= mCooldown)
            {
                mElapsedTime = 0.0f;
                ActivateAbility();
            }
        }

        public override void ActivateAbility()
        {
            aaTargetShip.TriggerBuff(BuffType.RapidFire);
        }
    }
}