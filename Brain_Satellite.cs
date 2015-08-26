using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceWars
{
    class Brain_Satellite : Brain
    {
        public Ship mShip;
        public Ship mEnemyTarget;

        public int mShootTracker;
        public static int mShootTrackerMax = 20;
        public float mAngleToPlayer;
        public float mRotationSpeed;
        public float mShootAngle;
        public int mOrbitDistance = 40;
        public float mDistance2Enemy;
        public float mTempDistance;

        public List<Ship> mTargetList;

        public Brain_Satellite(Ship pEntity)
            : base(pEntity)
        {
            mShip = pEntity;
            mShootTracker = 0;
            mAngleToPlayer = 0.0f;
            mRotationSpeed = (float)(2 * Math.PI) / 80;
            mTargetList = new List<Ship>();
        }

        public override void UpdateBrain(float DT)
        {
            mDistance2Enemy = float.MaxValue;

            mAngleToPlayer += mRotationSpeed;

            mShip.mXPos = (float)Math.Cos(mAngleToPlayer) * mOrbitDistance + mShip.aaGameWorld.Player.mXPos;
            mShip.mYPos = (float)Math.Sin(mAngleToPlayer) * mOrbitDistance + mShip.aaGameWorld.Player.mYPos;

            if (mAngleToPlayer >= 2 * Math.PI)
            {
                mAngleToPlayer = 0;
            }

            foreach (Entity e in mShip.aaGameWorld.mEntityList)
            {
                mTempDistance = GetDistance(e);
                if (mTempDistance < mDistance2Enemy && (e as Ship).mTeam != mShip.mTeam)
                {
                    mDistance2Enemy = mTempDistance;
                    mEnemyTarget = (e as Ship);
                }
            }

            mShootAngle = GetAngleToEnemy();
            if (mShootTracker == mShootTrackerMax)
            {
                mShip.ActivateAbility(AbilityType.FireBullet, mShootAngle);
                mShootTracker = 0;
            }

            mShootTracker++;
            if (mShootTracker > mShootTrackerMax) { mShootTracker = mShootTrackerMax; }
        }

        public float GetAngleToEnemy()
        {
            return (float)Math.Atan2(mEnemyTarget.mYPos - mShip.mYPos, mEnemyTarget.mXPos - mShip.mXPos);
        }

        public float GetDistance(Entity e)
        {
            return Vector2.Distance(new Vector2(mShip.mXPos, mShip.mYPos), new Vector2(e.mXPos, e.mYPos));
        }
    }
}
