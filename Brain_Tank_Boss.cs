using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceWars
{
    class Brain_Tank_Boss : Brain
    {
        public Ship mShip;
        //public Ship mTarget;

        public int mShootTracker;
        public int mSmokeTracker;
        public static int mShootTrackerMax = 15;
        public static int mSmokeTrackerMax = 10;
        public float mDistance;
        public Vector3 mShipDirection;
        public Vector3 mTargetDirection;
        public Vector3 mCrossValue;

        public Brain_Tank_Boss(Ship pEntity)
            : base(pEntity)
        {
            mShip = pEntity;
            mShootTracker = 0;
            mShipDirection = Vector3.Zero;
            mTargetDirection = Vector3.Zero;
        }

        public override void UpdateBrain(float DT)
        {
            mShipDirection.X = (float)Math.Cos(mShip.mRotation);
            mShipDirection.Y = (float)Math.Sin(mShip.mRotation);
            mTargetDirection.X = (float)Math.Cos(GetAngleToPlayer());
            mTargetDirection.Y = (float)Math.Sin(GetAngleToPlayer());
            mCrossValue = Vector3.Cross(mShipDirection, mTargetDirection);

            if (mCrossValue.Z < -0.01f)
                mShip.ActivateAbility(AbilityType.TurnLeft);
            else if (mCrossValue.Z > 0.01f)
                mShip.ActivateAbility(AbilityType.TurnRight);

            if (mShip.aHull < 1000 && mSmokeTracker == mSmokeTrackerMax)
            {
                mShootTrackerMax = 20;
                mShip.aaGameWorld.aaGFXHandler.PlayEffect("Explosion_01", mShip.mXPos, mShip.mYPos, mShip.mAssetKit.COLOR_BLENDING);
                mSmokeTracker = 0;
            }

            if (mCrossValue.Z > -0.2f && mCrossValue.Z < 0.2f)
            {
                //Get distance between you an the dirty, evil player
                mDistance = (float)Math.Sqrt(Math.Pow((mShip.aaGameWorld.Player.mXPos - mShip.mXPos), 2) + Math.Pow((mShip.aaGameWorld.Player.mYPos - mShip.mYPos), 2));
                mShip.ActivateAbility(AbilityType.FireMultipleMissles, mShip.mRotation, (mShip.aHull < 1000));

                if (mDistance < 350)
                {
                    mShip.ActivateAbility(AbilityType.SlowShip);
                }
                else
                {
                    mShip.ActivateAbility(AbilityType.ThrustShip);
                }
            }

            mShip.ActivateAbility(AbilityType.SuperShield);

            mSmokeTracker++;
            if (mSmokeTracker > mSmokeTrackerMax) { mSmokeTracker = mSmokeTrackerMax; }

            base.UpdateBrain(DT);
        }

        public float GetAngleToPlayer()
        {
            return (float)Math.Atan2(mShip.aaGameWorld.Player.mYPos - mShip.mYPos, mShip.aaGameWorld.Player.mXPos - mShip.mXPos);
        }
    }
}
