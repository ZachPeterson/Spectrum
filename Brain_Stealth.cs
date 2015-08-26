using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceWars
{
    class Brain_Stealth : Brain
    {
        public Ship mShip;
        public bool isThereWaypoint;
        public PointCollision mWayPoint;
        public float mMiddleX;
        public float mMiddleY;
        public int mQuad, mTargetQuad;
        public float mRandX, mRandY;
        public float mHalfPoint;
        public float mDistance;

        public Vector3 mShipDirection;
        public Vector3 mTargetDirection;
        public Vector3 mCrossValue;

        public Random mRand;

        public Brain_Stealth(Ship pEntity)
            : base(pEntity)
        {
            mShip = pEntity;
            isThereWaypoint = false;
            mRand = mShip.aaGameWorld.mRand;
        }

        public override void UpdateBrain(float DT)
        {
            if (!isThereWaypoint)
            {
                SetWaypoint();
                mHalfPoint = Math.Abs((mWayPoint.mXPos - mShip.mXPos) / 2);
            }

            mShipDirection.X = (float)Math.Cos(mShip.mRotation);
            mShipDirection.Y = (float)Math.Sin(mShip.mRotation);
            mTargetDirection.X = (float)Math.Cos(GetAngleToWaypoint());
            mTargetDirection.Y = (float)Math.Sin(GetAngleToWaypoint());
            mCrossValue = Vector3.Cross(mShipDirection, mTargetDirection);

            if (mCrossValue.Z <= 0.1f)
                mShip.ActivateAbility(AbilityType.TurnLeft);
            else if (mCrossValue.Z >= 0.1f)
                mShip.ActivateAbility(AbilityType.TurnRight);

            if (mCrossValue.Z > -0.1f && mCrossValue.Z < 0.1f)
            {
                mShip.ActivateAbility(AbilityType.ThrustShip);
            }

            if (mShip.mCircleCollision.isCollidingPoint(mWayPoint))
            {
                isThereWaypoint = false;
            }

            if (mShip.mXPos > mHalfPoint)
                mShip.ActivateAbility(AbilityType.FireMine);

            mDistance = (float)Math.Sqrt(Math.Pow(mShip.mXPos - mShip.aaGameWorld.aaDisplay.aaCamera.mFocusX, 2) + Math.Pow(mShip.mYPos - mShip.aaGameWorld.aaDisplay.aaCamera.mFocusY, 2));

            if (mDistance < 1000)
                mShip.ActivateAbility(AbilityType.Cloak);

            base.UpdateBrain(DT);
        }

        public void SetWaypoint()
        {
            mQuad = GetQuadRegion();
            mTargetQuad = GetTargetQuad(mQuad);
            CreateWayPoint(mTargetQuad);

            isThereWaypoint = true;
        }

        public int GetQuadRegion()
        {
            mMiddleX = mShip.aaGameWorld.aaDisplay.aaCamera.mFocusX;
            mMiddleY = mShip.aaGameWorld.aaDisplay.aaCamera.mFocusY;

            mMiddleX = MathHelper.Clamp(mMiddleX, 0, Constants.REGION_PLAYABLE_WIDTH);
            mMiddleY = MathHelper.Clamp(mMiddleY, 0, Constants.REGION_PLAYABLE_HEIGHT);

            if (mShip.mXPos < mMiddleX && mShip.mYPos < mMiddleY)
                return 1;
            else if (mShip.mXPos > mMiddleX && mShip.mYPos < mMiddleY)
                return 2;
            else if (mShip.mXPos > mMiddleX && mShip.mYPos > mMiddleY)
                return 3;
            else if (mShip.mXPos < mMiddleX && mShip.mYPos > mMiddleY)
                return 4;

            return 0;
        }

        public int GetTargetQuad(int pQuad)
        {
            if (pQuad == 1)
                return 3;
            else if (pQuad == 2)
                return 4;
            else if (pQuad == 3)
                return 1;
            else if (pQuad == 4)
                return 2;

            return 0;
        }

        public void CreateWayPoint(int pTargetQuad)
        {
            if (pTargetQuad == 1)
            {
                mRandX = mRand.Next(0, (int)mMiddleX);
                mRandY = mRand.Next(0, (int)mMiddleY);
            }
            else if (pTargetQuad == 2)
            {
                mRandX = mRand.Next((int)mMiddleX, Constants.REGION_PLAYABLE_WIDTH);
                mRandY = mRand.Next(0, (int)mMiddleY);
            }
            else if (pTargetQuad == 3)
            {
                mRandX = mRand.Next((int)mMiddleX, Constants.REGION_PLAYABLE_WIDTH);
                mRandY = mRand.Next((int)mMiddleY, Constants.REGION_PLAYABLE_HEIGHT);
            }
            else if (pTargetQuad == 4)
            {
                mRandX = mRand.Next(0, (int)mMiddleX);
                mRandY = mRand.Next((int)mMiddleY, Constants.REGION_PLAYABLE_HEIGHT);
            }

            mWayPoint = new PointCollision(mRandX, mRandY);
        }

        public float GetAngleToWaypoint()
        {
            return (float)Math.Atan2(mWayPoint.mYPos - mShip.mYPos, mWayPoint.mXPos - mShip.mXPos);
        }
    }
}
