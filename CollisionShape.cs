using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class CollisionShape
    {
        public float mXPos;
        public float mYPos;
        public Projectile mProjOwner;
        public Ship mShipOwner;
        public Pickup mPickupOwner;
        public bool mIsShield = false;
        public bool mIsPickup = false;
        public uint mTeam;

        public CollisionShape(float pXPos, float pYPos, Projectile pProjOwner = null, Ship pShipOwner = null, uint pTeam = 0, bool pIsShield = false)
        {
            mXPos = pXPos;
            mYPos = pYPos;
            mProjOwner = pProjOwner;
            mShipOwner = pShipOwner;
            mTeam = pTeam;
            mIsShield = pIsShield;
        }

        public void Update(float pXPos, float pYPos)
        {
            mXPos = pXPos;
            mYPos = pYPos;
        }
    }

    class CircleCollision : CollisionShape
    {
        public float mRadius;
        public float mDistance;
        public float mMatrixD;
        public float mIncidence;

        public CircleCollision(float pXPos, float pYPos, float pRadius, Projectile pProjOwner = null, Ship pShipOwner = null, uint pTeam = 0, bool pIsShield = false)
            : base(pXPos, pYPos, pProjOwner, pShipOwner, pTeam, pIsShield)
        {
            mRadius = pRadius;
        }

        public bool isCollidingCircle(CircleCollision pCircle)
        {
            mDistance = (float)Math.Sqrt(Math.Pow((mXPos - pCircle.mXPos), 2) + Math.Pow((mYPos - pCircle.mYPos), 2));

            if (mDistance <= mRadius + pCircle.mRadius)
                return true;

            return false;
        }

        public bool isCollidingLine(LineCollision pLine)
        {
            mDistance = (float)Math.Sqrt(Math.Pow((pLine.mXEndPos - pLine.mXPos), 2) + Math.Pow((pLine.mYEndPos - pLine.mYPos), 2));
            mMatrixD = pLine.mXPos* pLine.mYEndPos - pLine.mXEndPos * pLine.mYPos;
            mIncidence = (float)(Math.Pow(mRadius, 2) * Math.Pow(mDistance, 2) - Math.Pow(mMatrixD, 2));

            if (mIncidence >= 0)
                return true;

            return false;
        }

        public bool isCollidingPoint(PointCollision pPoint)
        {
            mDistance = (float)Math.Sqrt(Math.Pow((mXPos - pPoint.mXPos), 2) + Math.Pow((mYPos - pPoint.mYPos), 2));

            if (mDistance <= mRadius)
            {
                return true;
            }
            
            return false;
        }
    }

    class LineCollision : CollisionShape
    {
        public float mXEndPos;
        public float mYEndPos;

        public LineCollision(float pXEndPos, float pYEndPos, float pXPos, float pYPos, Projectile pProjOwner = null, Ship pShipOwner = null)
            : base(pXPos, pYPos, pProjOwner, pShipOwner)
        {
            mXEndPos = pXEndPos;
            mYEndPos = pYEndPos;
        }
    }

    class PointCollision : CollisionShape
    {
        public PointCollision(float pXPos, float pYPos, Projectile pProjOwner = null, Ship pShipOwner = null)
            : base(pXPos, pYPos, pProjOwner, pShipOwner)
        {
        }
    }
}
