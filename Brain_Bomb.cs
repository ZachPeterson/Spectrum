using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWars
{
    class Brain_Bomb : Brain
    {
        public Ship mShip;
        //public Ship mTarget;

        public float mDistance;
        public Vector3 mShipDirection;
        public Vector3 mTargetDirection;
        public Vector3 mCrossValue;

        public Brain_Bomb(Ship pEntity)
            : base(pEntity)
        {
            mShip = pEntity;
            mShipDirection = Vector3.Zero;
            mTargetDirection = Vector3.Zero;
            pEntity.mSlowBeepInstance = mShip.aaGameWorld.aaDisplay.aaGame.mBombBeepSlow.CreateInstance();
            pEntity.mSlowBeepInstance.Volume = 0.5f;
            pEntity.mSlowBeepInstance.IsLooped = true;
            pEntity.mFastBeepInstance = mShip.aaGameWorld.aaDisplay.aaGame.mBombBeepFast.CreateInstance();
            pEntity.mFastBeepInstance.Volume = 0.5f;
            pEntity.mFastBeepInstance.IsLooped = true;
            pEntity.mSlowBeepInstance.Play();
        }

        public override void UpdateBrain(float DT)
        {
            mShipDirection.X = (float)Math.Cos(mShip.mRotation);
            mShipDirection.Y = (float)Math.Sin(mShip.mRotation);
            mTargetDirection.X = (float)Math.Cos(GetAngleToPlayer());
            mTargetDirection.Y = (float)Math.Sin(GetAngleToPlayer());
            mCrossValue = Vector3.Cross(mShipDirection, mTargetDirection);

            if (mCrossValue.Z < -0.2f)
                mShip.ActivateAbility(AbilityType.TurnLeft);
            else if (mCrossValue.Z > 0.2f)
                mShip.ActivateAbility(AbilityType.TurnRight);

            mDistance = (float)Math.Sqrt(Math.Pow((mShip.aaGameWorld.Player.mXPos - mShip.mXPos), 2) + Math.Pow((mShip.aaGameWorld.Player.mYPos - mShip.mYPos), 2));

            if (mCrossValue.Z >= -0.2f && mCrossValue.Z <= 0.2f)
            {
                mShip.ActivateAbility(AbilityType.ThrustShip);
            }

            if (mDistance < 50)
            {
                mShip.ActivateAbility(AbilityType.SuicideBomb);
                if (mShip.mSlowBeepInstance.State == SoundState.Playing)
                {
                    mShip.mSlowBeepInstance.Stop();
                    mShip.mFastBeepInstance.Play();
                }
            }

            base.UpdateBrain(DT);
        }

        public float GetAngleToPlayer()
        {
            return (float)Math.Atan2(mShip.aaGameWorld.Player.mYPos - mShip.mYPos, mShip.aaGameWorld.Player.mXPos - mShip.mXPos);
        }
 
    }
}
