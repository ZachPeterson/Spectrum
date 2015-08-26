using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    enum BuffType
    {
        None,
        Cloak,
        DampenedHandling,
        Decloak,
        ImproveHandling,
        LaserBuff,
        ShipSlow,
        SuperShield,
        OverchargeEngines,
        TankShield,
        FullShield,
        AntiBullets,
        RapidFire,
    };

    class Buff
    {
        public Ship aaAffectedShip;
        public bool mBuffActive;

        public Buff(Ship pAffected)
        {
            aaAffectedShip = pAffected;
        }

        public virtual void Trigger()
        {
            mBuffActive = true;
        }

        public virtual void Tracker(float pDT)
        {
        }

        public virtual void Cease()
        {
            mBuffActive = false;
        }

        public bool IsBuffActive()
        {
            return mBuffActive;
        }
    }

    class Buff_ShipSlow : Buff
    {
        private float mCurrentTime;

        private float mSlowDuration;
        private float mSlowAmount;

        public Buff_ShipSlow(Ship pTarget)
            : base(pTarget)
        {
            mSlowDuration = 0.0f;
            mSlowAmount = 0.0f;
        }

        public void SetSlowAmount(float pSlowAmount)
        {
            mSlowAmount = pSlowAmount;
        }

        public void SetSlowDuration(float pSlowDuration)
        {
            mSlowDuration = pSlowDuration;
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mSlowDuration)
            {
                aaAffectedShip.mThrustVelocity = mSlowAmount * aaAffectedShip.mKit.THRUST_MAX;
            }
            else
            {
                Cease();
            }
        }
    }

    class Buff_DampenedHandling : Buff
    {
        private float mCurrentTime;

        private float mDampenDuration;
        private float mDampenAmount;

        public Buff_DampenedHandling(Ship pTarget)
            : base(pTarget)
        {
            mDampenDuration = 0.0f;
            mDampenAmount = 0.0f;
        }

        public void SetDampenAmount(float pDampenAmount)
        {
            mDampenAmount = pDampenAmount;
        }

        public void SetDampenDuration(float pDampenDuration)
        {
            mDampenDuration = pDampenDuration;
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mDampenDuration)
            {
                aaAffectedShip.aTurnPower *= mDampenAmount;
            }
            else
            {
                Cease();
            }
        }
    }

    class Buff_LaserBuff : Buff
    {
        private float mCurrentTime;

        private const float mLaserDuration = 2.0f;
        private const float mLaserSlowAmount = 0.7f;
        private const float mDamagePerSecond = 50.0f;

        public Buff_LaserBuff(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mLaserDuration)
            {
                aaAffectedShip.mVelXFinal *= mLaserSlowAmount;
                aaAffectedShip.mVelYFinal *= mLaserSlowAmount;

                aaAffectedShip.aHull -= (mDamagePerSecond * pDT);
            }
            else
            {
                Cease();
            }
        }
    }

    class Buff_ImproveHandling : Buff
    {
        private float mCurrentTime;

        private const float mImproveDuration = 5.0f;
        private const float mTurnImproved = 1.4f;

        public Buff_ImproveHandling(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mImproveDuration)
            {
                aaAffectedShip.aTurnPower *= mTurnImproved;
            }
            else
            {
                Cease();
            }
        }
    }

    class Buff_Cloak : Buff
    {
        private float mCurrentTime;
        private float mCurrentFade;

        private const float mCloakFadeTime = 1.0f;
        private const float mCloakDuration = 5.0f;
        private const float mMinCloakAlpha = 0.2f;
        private const float mMaxCloakAlpha = 1.0f;

        public Buff_Cloak(Ship pTarget)
            : base(pTarget)
        {
            mCurrentFade = 0.0f;
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mCloakFadeTime)
            {
                mCurrentFade = mMinCloakAlpha + (mMaxCloakAlpha - mMinCloakAlpha) * (1 - (mCurrentTime / mCloakFadeTime));

                aaAffectedShip.mDispObj.FadeToAmount(mCurrentFade);
            }
            else if (mCurrentTime >= mCloakFadeTime + mCloakDuration)
            {
                Cease();
                aaAffectedShip.TriggerBuff(BuffType.Decloak);
            }
            else
            {
                aaAffectedShip.mDispObj.FadeToAmount(mMinCloakAlpha);
            }
        }
    }

    class Buff_Decloak : Buff
    {
        private float mCurrentTime;
        private float mCurrentFade;

        private const float mCloakFadeTime = 1.0f;
        private const float mMinCloakAlpha = 0.01f;
        private const float mMaxCloakAlpha = 1.0f;

        public Buff_Decloak(Ship pTarget)
            : base(pTarget)
        {
            mCurrentFade = 0.0f;
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mCloakFadeTime)
            {
                mCurrentFade = mMinCloakAlpha + (mMaxCloakAlpha - mMinCloakAlpha) * ((mCurrentTime / mCloakFadeTime));

                aaAffectedShip.mDispObj.FadeToAmount(mCurrentFade);
            }
            else if (mCurrentTime >= mCloakFadeTime)
            {
                Cease();
                aaAffectedShip.mDispObj.FadeToAmount(mMaxCloakAlpha);
            }
        }
    }

    class Buff_SuperShield : Buff
    {
        private float mCurrentTime;
        private float mShieldDuration = 10.0f;

        public AssetKit_BossShield mShieldKit;
        public CircleCollision mShieldCollider;
        public DisplayObject mShield;

        public Buff_SuperShield(Ship pTarget)
            : base(pTarget)
        {
            mShieldKit = new AssetKit_BossShield();
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
            mShieldCollider = new CircleCollision(aaAffectedShip.mXPos, aaAffectedShip.mYPos, 90.0f, null, null, aaAffectedShip.mTeam);
            mShieldCollider.mIsShield = true;
            aaAffectedShip.aaGameWorld.CollisionShapes_Circle.Add(mShieldCollider);
            mShield = new DisplayObject(aaAffectedShip.aaGameWorld.aaDisplay, mShieldKit.ASSET_NAME, aaAffectedShip.mXPos, aaAffectedShip.mYPos, (int)mShieldKit.ORIGIN.X, (int)mShieldKit.ORIGIN.Y, 0.1f, mShieldKit.SRC_RECTX, mShieldKit.SRC_RECTY, mShieldKit.SRC_RECTWIDTH, mShieldKit.SRC_RECTHEIGHT);
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime < mShieldDuration)
            {
                mShield.posX = aaAffectedShip.mXPos;
                mShield.posY = aaAffectedShip.mYPos;
                mShieldCollider.mXPos = aaAffectedShip.mXPos;
                mShieldCollider.mYPos = aaAffectedShip.mYPos;
            }

            if (mCurrentTime >= mShieldDuration)
            {
                aaAffectedShip.aaGameWorld.CollisionShapes_Circle.Remove(mShieldCollider);
                mShield.Destroy();
                Cease();
            }

        }
    }

    class Buff_OverchargeEngines : Buff
    {
        private float mCurrentTime;

        private const float mOverchargeEnginesDuration = 2.5f;
        private const float mMaxThrustImproved = 1.6f;
        private const float mTurnHindered = 0.75f;

        public Buff_OverchargeEngines(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();

            aaAffectedShip.mThrustVelocity = (aaAffectedShip.mThrustVelocity / aaAffectedShip.aMaxThrust) * (mMaxThrustImproved * aaAffectedShip.aMaxThrust);

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime <= mOverchargeEnginesDuration)
            {
                aaAffectedShip.aMaxThrust *= mMaxThrustImproved;
                aaAffectedShip.aTurnPower *= mTurnHindered;
            }
            else
            {
                Cease();
            }
        }
    }

    class Buff_TankShield : Buff
    {
        private float mCurrentTime;
        public const float mShieldDuration = 20.0f;

        public AssetKit_TankShield mShieldKit;
        public CircleCollision mShieldCollider;
        public DisplayObject mShield;

        public Buff_TankShield(Ship pTarget)
            : base(pTarget)
        {
            mShieldKit = new AssetKit_TankShield();
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
            mShieldCollider = new CircleCollision(aaAffectedShip.mXPos, aaAffectedShip.mYPos, 25.0f, null, null, aaAffectedShip.mTeam, false);
            aaAffectedShip.aaGameWorld.CollisionShapes_Circle.Add(mShieldCollider);
            mShield = new DisplayObject(aaAffectedShip.aaGameWorld.aaDisplay, mShieldKit.ASSET_NAME, aaAffectedShip.mXPos, aaAffectedShip.mYPos, (int)mShieldKit.ORIGIN.X, (int)mShieldKit.ORIGIN.Y, 0.1f, mShieldKit.SRC_RECTX, mShieldKit.SRC_RECTY, mShieldKit.SRC_RECTWIDTH, mShieldKit.SRC_RECTHEIGHT);
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime < mShieldDuration)
            {
                mShield.posX = aaAffectedShip.mXPos;
                mShield.posY = aaAffectedShip.mYPos;
                mShieldCollider.mXPos = aaAffectedShip.mXPos;
                mShieldCollider.mYPos = aaAffectedShip.mYPos;
            }

            if (mCurrentTime >= mShieldDuration)
            {
                aaAffectedShip.aaGameWorld.CollisionShapes_Circle.Remove(mShieldCollider);
                mShield.Destroy();
                Cease();
            }

        }
    }

    class Buff_FullShield : Buff
    {
        private float mCurrentTime;
        public const float mShieldDuration = 20.0f;

        public AssetKit_FullSheild mShieldKit;
        public CircleCollision mShieldCollider;
        public DisplayObject mShield;

        public Buff_FullShield(Ship pTarget)
            : base(pTarget)
        {
            mShieldKit = new AssetKit_FullSheild();
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
            mShieldCollider = new CircleCollision(aaAffectedShip.mXPos, aaAffectedShip.mYPos, 35.0f, null, null, aaAffectedShip.mTeam, true);
            aaAffectedShip.aaGameWorld.CollisionShapes_Circle.Add(mShieldCollider);
            mShield = new DisplayObject(aaAffectedShip.aaGameWorld.aaDisplay, mShieldKit.ASSET_NAME, aaAffectedShip.mXPos, aaAffectedShip.mYPos, (int)mShieldKit.ORIGIN.X, (int)mShieldKit.ORIGIN.Y, 0.1f, mShieldKit.SRC_RECTX, mShieldKit.SRC_RECTY, mShieldKit.SRC_RECTWIDTH, mShieldKit.SRC_RECTHEIGHT);
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime < mShieldDuration)
            {
                mShield.posX = aaAffectedShip.mXPos;
                mShield.posY = aaAffectedShip.mYPos;
                mShieldCollider.mXPos = aaAffectedShip.mXPos;
                mShieldCollider.mYPos = aaAffectedShip.mYPos;
            }

            if (mCurrentTime >= mShieldDuration)
            {
                aaAffectedShip.aaGameWorld.CollisionShapes_Circle.Remove(mShieldCollider);
                mShield.Destroy();
                Cease();
            }

        }
    }

    class Buff_AntiBullets : Buff
    {
        private float mCurrentTime;
        public const float mAntiBulletDuration = 7.5f;

        public Buff_AntiBullets(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime >= mAntiBulletDuration)
            {
                Cease();
            }

        }
    }

    class Buff_RapidFire : Buff
    {
        private float mCurrentTime;
        public const float mRapidFireDurration = 5.0f;

        public Buff_RapidFire(Ship pTarget)
            : base(pTarget)
        {
        }

        public override void Trigger()
        {
            base.Trigger();

            mCurrentTime = 0.0f;
        }

        public override void Tracker(float pDT)
        {
            if (!mBuffActive)
            {
                return;
            }

            mCurrentTime += pDT;

            if (mCurrentTime >= mRapidFireDurration)
            {
                Cease();
            }

        }
    }
}
