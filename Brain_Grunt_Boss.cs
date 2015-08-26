using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceWars
{
    class Brain_Grunt_Boss : Brain
    {
        public Ship mShip;

        public int mShootTracker;
        public int mSmokeTracker;
        public static int mShootTrackerMax = 30;
        public static int mSmokeTrackerMax = 10;
        public float mDistance;
        public Vector3 mShipDirection;
        public Vector3 mTargetDirection;
        public Vector3 mCrossValue;
        public bool nearDeath = false;

        public Brain_Grunt_Boss(Ship pEntity)
            : base(pEntity)
        {
            mShip = pEntity;
            mShootTracker = 0;
            mSmokeTracker = 0;
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

            if (mCrossValue.Z < -0.05f)
                mShip.ActivateAbility(AbilityType.TurnLeft);
            else if (mCrossValue.Z > 0.05f)
                mShip.ActivateAbility(AbilityType.TurnRight);


            if (mShip.aHull < 1000 && mSmokeTracker == mSmokeTrackerMax)
            {
                mShootTrackerMax = 20;
                mShip.aaGameWorld.aaGFXHandler.PlayEffect("Explosion_01", mShip.mXPos, mShip.mYPos, mShip.mAssetKit.COLOR_BLENDING);
                mSmokeTracker = 0;
            }

            if (mCrossValue.Z > -0.1f && mCrossValue.Z < 0.1f)
            {
                mDistance = (float)Math.Sqrt(Math.Pow((mShip.aaGameWorld.Player.mXPos - mShip.mXPos), 2) + Math.Pow((mShip.aaGameWorld.Player.mYPos - mShip.mYPos), 2));

                if (mDistance < 300)
                {
                    mShip.ActivateAbility(AbilityType.SlowShip);
                }
                else
                    mShip.ActivateAbility(AbilityType.ThrustShip);

                if (mShootTracker == mShootTrackerMax)
                {
                    mShip.ActivateAbility(AbilityType.FireMultipleBullets, mShip.mRotation, mShip.aHull < 1000);
                    mShip.ActivateAbility(AbilityType.BulletStar);
                    mShootTracker = 0;
                }
            }

            mShootTracker++;
            mSmokeTracker++;
            if (mShootTracker > mShootTrackerMax) { mShootTracker = mShootTrackerMax; }
            if (mSmokeTracker > mSmokeTrackerMax) { mSmokeTracker = mSmokeTrackerMax; }
        }

        public float GetAngleToPlayer()
        {
            return (float)Math.Atan2(mShip.aaGameWorld.Player.mYPos - mShip.mYPos, mShip.aaGameWorld.Player.mXPos - mShip.mXPos);
        }
    }
}
