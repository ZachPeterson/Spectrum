using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceWars
{
    class Brain_Laser : Brain
    {
        public Ship mShip;
        //public Ship mTarget;

        public int mShootTracker;
        public static int mShootTrackerMax = 15;
        public float mDistance;
        public Vector3 mShipDirection;
        public Vector3 mTargetDirection;
        public Vector3 mCrossValue;
        public bool isCharging;

        public Brain_Laser(Ship pEntity)
            : base(pEntity)
        {
            mShip = pEntity;
            mShootTracker = 0;
            mShipDirection = Vector3.Zero;
            mTargetDirection = Vector3.Zero;
            isCharging = false;
        }

        public override void UpdateBrain(float DT)
        {
            mShipDirection.X = (float)Math.Cos(mShip.mRotation);
            mShipDirection.Y = (float)Math.Sin(mShip.mRotation);
            mTargetDirection.X = (float)Math.Cos(GetAngleToPlayer());
            mTargetDirection.Y = (float)Math.Sin(GetAngleToPlayer());
            mCrossValue = Vector3.Cross(mShipDirection, mTargetDirection);

            if (mCrossValue.Z < 0)
                mShip.ActivateAbility(AbilityType.TurnLeft);
            else if (mCrossValue.Z > 0)
                mShip.ActivateAbility(AbilityType.TurnRight);

            mDistance = (float)Math.Sqrt(Math.Pow((mShip.aaGameWorld.Player.mXPos - mShip.mXPos), 2) + Math.Pow((mShip.aaGameWorld.Player.mYPos - mShip.mYPos), 2));

            if (mCrossValue.Z > -0.1f && mCrossValue.Z < 0.1f)
            {
                if (mDistance < 250)
                    mShip.ActivateAbility(AbilityType.SlowShip);
                else
                    mShip.ActivateAbility(AbilityType.ThrustShip);

                if (!isCharging)
                {
                    if (mDistance < 500)
                    {
                        mShip.ActivateAbility(AbilityType.ChargeLaser);
                    }
                }
                
                mShootTracker++;
                if (mShootTracker > mShootTrackerMax) { mShootTracker = mShootTrackerMax; }
            }

            base.UpdateBrain(DT);
        }

        public float GetAngleToPlayer()
        {
            return (float)Math.Atan2(mShip.aaGameWorld.Player.mYPos - mShip.mYPos, mShip.aaGameWorld.Player.mXPos - mShip.mXPos);
        }
    }
}
