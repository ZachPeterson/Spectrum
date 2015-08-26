using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace SpaceWars
{
    enum BehaviorType
    {
        None,
        BulletSpam,
        PathLaser,
        SlowShip,
        SuicideBomb,
        ThrustShip,
        TurnLeft,
        TurnRight,
        VelocityReversal,
        ChargeLaser,
        SiegeMode,
        DualWield,
        AntiBullets,
        RearGuns,
    };

    class Behavior
    {
        public Ship aaTargetShip;
        public bool mBehaviorActive;

        public Behavior(Ship pTarget)
        {
            aaTargetShip = pTarget;
        }

        public virtual void Trigger()
        {
            mBehaviorActive = true;
        }

        public virtual void Tracker(float pDT)
        {
        }

        public virtual void Cease()
        {
            mBehaviorActive = false;
        }

        public virtual bool IsBehaviorActive()
        {
            return mBehaviorActive;
        }
    }

    class Behavior_VelocityReversal : Behavior
    {
        public Behavior_VelocityReversal(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();

            aaTargetShip.mRotation -= (float)Math.PI;

            Cease();
        }
    }

    class Behavior_PathLaser : Behavior
    {
        public Behavior_PathLaser(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();
        }
    }

    class Behavior_ThrustShip : Behavior
    {
        public Behavior_ThrustShip(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Tracker(float pDT)
        {
            aaTargetShip.mThrustVelocity += aaTargetShip.aThrustPower * pDT;

            if (aaTargetShip.mThrustVelocity > aaTargetShip.aMaxThrust)
            {
                aaTargetShip.mThrustVelocity = aaTargetShip.aMaxThrust;
            }

            Cease();
        }
    }

    class Behavior_SlowShip : Behavior
    {
        public Behavior_SlowShip(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Tracker(float pDT)
        {
            aaTargetShip.mThrustVelocity -= aaTargetShip.aThrustPower * pDT;

            if (aaTargetShip.mThrustVelocity <= 0.0f)
            {
                aaTargetShip.mThrustVelocity = 0.0f;
            }

            Cease();
        }
    }

    class Behavior_TurnLeft : Behavior
    {
        public Behavior_TurnLeft(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Tracker(float pDT)
        {
            aaTargetShip.mRotation -= aaTargetShip.aTurnPower * pDT;

            Cease();
        }
    }

    class Behavior_TurnRight : Behavior
    {
        public Behavior_TurnRight(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Tracker(float pDT)
        {
            aaTargetShip.mRotation += aaTargetShip.aTurnPower * pDT;

            Cease();
        }
    }

    class Behavior_BulletSpam : Behavior
    {
        private float mBulletSpamDuration;
        private float mElapsedTime;
        private float mRotationAmount;
        private float mFireRate;
        private float mFireElapsed;
        private float mNumberOfRotations;
        private float mBaseRotation;

        public Behavior_BulletSpam(Ship pTarget)
            : base(pTarget)
        {
            mBulletSpamDuration = 3.0f;
            mElapsedTime = 0.0f;
            mRotationAmount = 0.0f;
            mFireRate = 0.08f;
            mFireElapsed = 0.0f;
            mNumberOfRotations = 0.5f;

            mBaseRotation = pTarget.mRotation;
        }

        public override void Tracker(float pDT)
        {
            mElapsedTime += pDT;
            mFireElapsed += pDT;

            if (mElapsedTime >= mBulletSpamDuration)
            {
                Cease();
            }
            else
            {
                mRotationAmount = (mNumberOfRotations * mElapsedTime / mBulletSpamDuration) * 2.0f * (float)Math.PI;

                if (mFireElapsed >= mFireRate)
                {
                    mFireElapsed = 0.0f;

                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation + mRotationAmount, ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation - mRotationAmount + (float)(Math.PI / 4), ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation + mRotationAmount + (float)(Math.PI / 2), ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation - mRotationAmount + (float)(3 * Math.PI / 4), ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation + mRotationAmount + (float)(Math.PI), ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation - mRotationAmount + (float)(5 * Math.PI / 4), ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation + mRotationAmount + (float)(3 * Math.PI / 2), ProjectileKitType.PlayerBullet);
                    aaTargetShip.aaGameWorld.mEntityManager.AddProjectile(aaTargetShip.mTeam, ProjectileType.Bullet, new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), mBaseRotation - mRotationAmount + (float)(7 * Math.PI / 4), ProjectileKitType.PlayerBullet);

                    aaTargetShip.FireBullet.PlayShootSound(0.2f);
                }
            }
        }
    }

    class Behavior_SuicideBomb : Behavior
    {
        private const float mSuicideDelay = 1.0f;
        private float mElapsedTime;
        private const float mExplosionRadius = 100.0f;
        private const int mExplosionDamage = 40;

        public Behavior_SuicideBomb(Ship pTarget)
            : base(pTarget)
        {
            mElapsedTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            mElapsedTime += pDT;
            if (mElapsedTime >= mSuicideDelay)
            {
                // TODO: Explode here and deal damage
                // Explode

                foreach (Entity e in aaTargetShip.aaGameWorld.mEntityList)
                {
                    Ship s = e as Ship;

                    if (s != null && s != aaTargetShip)
                    {
                        if (Vector2.Distance(new Vector2(aaTargetShip.mXPos, aaTargetShip.mYPos), new Vector2(s.mXPos, s.mYPos)) <= mExplosionRadius)
                        {
                            s.aHull -= mExplosionDamage;
                        }
                    }
                }

                aaTargetShip.aHull = -1;

                Cease();
            }
            else
            {
                // TODO: Flash, beep, something to let the player know the ship is gonna blow
            }
        }
    }

    class Behavior_ChargeLaser : Behavior
    {
        Projectile mLaserCharge;
        Ship mTarget;
        float mSize = 0.1f;
        float mSlow = 1.0f;
        int offset = 10;

        public Behavior_ChargeLaser(Ship pTarget)
            : base(pTarget)
        {
            mTarget = pTarget;
            mLaserCharge = new Projectile(aaTargetShip.aaGameWorld, aaTargetShip.mTeam, ProjectileType.LaserBall, aaTargetShip.mXPos, aaTargetShip.mYPos, pTarget.mRotation, KitManager.LaserProjectileKit, null, mTarget);
            aaTargetShip.aaGameWorld.mProjectileList.Add(mLaserCharge);
        }

        public override void Tracker(float pDT)
        {
            if (mTarget != null)
            {
                int offsetX = (int)(Math.Cos(mTarget.mRotation) * offset);
                int offsetY = (int)(Math.Sin(mTarget.mRotation) * offset);
                mLaserCharge.mXPos = aaTargetShip.mXPos;
                mLaserCharge.mYPos = aaTargetShip.mYPos;
                mLaserCharge.mRotation = mTarget.mRotation;
                mLaserCharge.mProjKit.SLOW_EFFECT = mSlow;

                if (mLaserCharge.mDispObject != null)
                    mLaserCharge.mDispObject.SetZoom(mSize);

                mSize += 0.01f;
                mSlow -= 0.005f;
            }

            if (mSize > 1.0f)
            {
                (mTarget.mBrain as Brain_Laser).isCharging = false;
                mTarget = null;
                Cease();
            }
        }
    }

    class Behavior_DualWield : Behavior
    {
        private float mDualWieldDuration = 10.0f;
        private float mElapsedTime;
        private Vector2[] mBulletOffsets;
        private float mShootAngle;

        public Behavior_DualWield(Ship pTarget)
            : base(pTarget)
        {
            mElapsedTime = 0;
            mBulletOffsets = new Vector2[2];
        }

        public override void Tracker(float pDT)
        {
            mElapsedTime += pDT;

            if (mElapsedTime >= mDualWieldDuration)
            {
                Cease();
            }

            if ((aaTargetShip.mBrain as Brain_Player).mShootTracker == (aaTargetShip.mBrain as Brain_Player).mShootTrackerMax && (aaTargetShip.mBrain as Brain_Player).mInput_Mouse.LeftButton.Equals(ButtonState.Pressed))
            {
                mShootAngle = (aaTargetShip.mBrain as Brain_Player).GetAngleToMouse();

                mBulletOffsets[0].Y = -15 * -(float)Math.Cos(mShootAngle);
                mBulletOffsets[0].X = -15 * (float)Math.Sin(mShootAngle);
                mBulletOffsets[1].Y = 15 * -(float)Math.Cos(mShootAngle);
                mBulletOffsets[1].X = 15 * (float)Math.Sin(mShootAngle);

                aaTargetShip.FireBullet.ActivateAbility(mShootAngle, aaTargetShip.mXPos + mBulletOffsets[0].X, aaTargetShip.mYPos + mBulletOffsets[0].Y, false);
                aaTargetShip.FireBullet.ActivateAbility(mShootAngle, aaTargetShip.mXPos + mBulletOffsets[1].X, aaTargetShip.mYPos + mBulletOffsets[1].Y, false);
                aaTargetShip.FireBullet.PlayShootSound(0.2f);

                (aaTargetShip.mBrain as Brain_Player).mShootTracker = 0;
            }
        }
    }

    class Behavior_RearGuns : Behavior
    {
        private float mRearGunsDuration;
        private float mElapsedTime;
        private float mFireRate;
        private float mFireElapsed;

        public Behavior_RearGuns(Ship pTarget)
            : base(pTarget)
        {
            mRearGunsDuration = 6.0f;
            mElapsedTime = 0.0f;
            mFireRate = 0.1f;
            mFireElapsed = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            mElapsedTime += pDT;
            mFireElapsed += pDT;

            if (mElapsedTime >= mRearGunsDuration)
            {
                Cease();
            }
            else
            {
                if (mFireElapsed >= mFireRate)
                {
                    SoundEffectInstance mShootInstance = aaTargetShip.aaGameWorld.aaDisplay.aaGame.mShootSound.CreateInstance();
                    mShootInstance.Volume = 0.1f;
                    mShootInstance.Play();
                    aaTargetShip.aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mShootInstance);

                    mFireElapsed = 0.0f;

                    float shootAngle = (aaTargetShip.mBrain as Brain_Player).GetAngleToMouse();

                    // Shoot in front and behind
                    aaTargetShip.FireBullet.ActivateAbility(shootAngle, aaTargetShip.mXPos, aaTargetShip.mYPos, false);
                    aaTargetShip.FireBullet.ActivateAbility((float)(shootAngle + Math.PI), aaTargetShip.mXPos, aaTargetShip.mYPos, false);
                }
            }
        }
    }
}
