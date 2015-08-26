using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SpaceWars
{
    enum ProjectileType
    {
        Bullet,
        Missile,
        Mine,
        LaserBall,
        AntiBullet,
    }

    enum CollisionType
    {
        Point,
        Circle,
        Line,
    }

    class Projectile
    {
        public uint mTeam;
        public float mXPos;
        public float mYPos;
        public float mRotation;
        public ProjectileType mType;

        public ProjectileKit mProjKit;
        public AssetKit mAssetKit;
        public DisplayObject mDispObject;
        public GameWorld aaGameWorld;
        public CollisionShape mCollision;
        public CollisionType mCollisionType;
        public Ship mTarget;
        public Ship mOwner;

        public Vector3 mProjDirection;
        public Vector3 mTargetDirection;
        public Vector3 mCrossValue;
        public UInt64 mProjectileID;

        public Projectile(GameWorld pGameWorld, uint pTeam, ProjectileType pType, float pXPos, float pYPos, float pRotation, ProjectileKit pProjKit, Ship pTarget = null, Ship pOwner = null)
        {
            mXPos = pXPos;
            mYPos = pYPos;
            mType = pType;
            aaGameWorld = pGameWorld;
            mTeam = pTeam;
            SetProjType();
            mRotation = pRotation;
            mProjKit = pProjKit;
            mTarget = pTarget;
            mOwner = pOwner;
            mDispObject = new DisplayObject(aaGameWorld.aaDisplay, mAssetKit.ASSET_NAME, mXPos, mYPos, (int)mAssetKit.ORIGIN.X, (int)mAssetKit.ORIGIN.Y, Constants.LAYER_PROJECTILE_DEPTH, mAssetKit.SRC_RECTX, mAssetKit.SRC_RECTY, mAssetKit.SRC_RECTWIDTH, mAssetKit.SRC_RECTHEIGHT, mRotation, false);
        }

        public void SetProjType()
        {
            if (mType == ProjectileType.Bullet)
            {
                if (mTeam == 0)
                    mAssetKit = new AssetKit_Proj_PlayerBullet_01();
                else
                    mAssetKit = new AssetKit_Proj_Bullet_01();

                mCollision = new PointCollision(mXPos, mYPos, this);
                aaGameWorld.CollisionShapes_Points.Add(mCollision as PointCollision);
                mCollisionType = CollisionType.Point;
            }
            else if (mType == ProjectileType.Mine)
            {
                if (mTeam == 0)
                {
                    mAssetKit = new AssetKit_PlayerMine();
                    mCollision = new CircleCollision(mXPos, mYPos, 15, this);
                }
                else
                {
                    mAssetKit = new AssetKit_Proj_Mine_01();
                    mCollision = new CircleCollision(mXPos, mYPos, 8, this);
                }

                aaGameWorld.CollisionShapes_Circle.Add(mCollision as CircleCollision);
                mCollisionType = CollisionType.Circle;
            }
            else if (mType == ProjectileType.Missile)
            {
                mAssetKit = new AssetKit_Proj_Missile_01();
                mCollision = new PointCollision(mXPos, mYPos, this);
                aaGameWorld.CollisionShapes_Points.Add(mCollision as PointCollision);
                mCollisionType = CollisionType.Point;
            }
            else if (mType == ProjectileType.LaserBall)
            {
                mAssetKit = new AssetKit_Proj_Laser_01();
                mCollision = new CircleCollision(mXPos, mYPos, 8, this);
                aaGameWorld.CollisionShapes_Circle.Add(mCollision as CircleCollision);
                mCollisionType = CollisionType.Circle;
            }
            else if (mType == ProjectileType.AntiBullet)
            {
                mAssetKit = new AssetKit_Proj_AntiBullet();
                mCollision = new CircleCollision(mXPos, mYPos, 15, this);
                aaGameWorld.CollisionShapes_Circle.Add(mCollision as CircleCollision);
                mCollisionType = CollisionType.Circle;
            }
            else
            {
                // Console.WriteLine("ERROR: NO OBJECT TYPE!");
            }
        }

        public void Update(float DT)
        {
            mXPos += (float)(mProjKit.SPEED * Math.Cos(mRotation) * DT);
            mYPos += (float)(mProjKit.SPEED * Math.Sin(mRotation) * DT);
            if (mDispObject != null)
            {
                mDispObject.posX = mXPos;
                mDispObject.posY = mYPos;
                mDispObject.RotateToAngle(mRotation + (float)(Math.PI / 2));
            }

            if (mCollision != null)
            {
                mCollision.Update(mXPos, mYPos);
            }

            if (mTarget != null)
            {
                mProjDirection.X = (float)Math.Cos(mRotation);
                mProjDirection.Y = (float)Math.Sin(mRotation);
                mTargetDirection.X = (float)Math.Cos(GetAngleToTarget());
                mTargetDirection.Y = (float)Math.Sin(GetAngleToTarget());
                mCrossValue = Vector3.Cross(mProjDirection, mTargetDirection);

                if (mCrossValue.Z < -0.1f)
                    mRotation -= 0.1f;
                else if (mCrossValue.Z > 0.1f)
                    mRotation += 0.1f;
            }

            if (mType == ProjectileType.LaserBall)
            {
                if (!(aaGameWorld.mEntityList.Contains(mOwner)) && (mOwner.mBrain as Brain_Laser).isCharging == true)
                    Destroy();
            }

            // Kill if we go outside playable region
            if (mXPos < -50 || mXPos > Constants.REGION_PLAYABLE_WIDTH + 50 || mYPos < -50 || mYPos > Constants.REGION_PLAYABLE_HEIGHT + 50)
            {
                this.Destroy();
            }
        }

        public void Destroy()
        {
            aaGameWorld.mProjDeadList.Add(this);
        }

        public void ResolveCollision(Ship mTarget = null, Projectile mProjTarget = null)
        {
            if (mTarget != null && mType == ProjectileType.LaserBall)
            {
                mTarget.TriggerBuff(BuffType.ShipSlow, mProjKit.SLOW_EFFECT);
                Destroy();
            }
            else if (mType == ProjectileType.Mine)
            {
                if (mTeam != Constants.PLAYER_TEAM && mProjTarget != null || mTarget != null && mTeam != mTarget.mTeam)
                {
                    aaGameWorld.aaGFXHandler.PlayEffect("Explosion_01", mXPos, mYPos, mAssetKit.COLOR_BLENDING);
                    SoundEffectInstance mExplosionInstance = aaGameWorld.aaDisplay.aaGame.mExplosion.CreateInstance();
                    mExplosionInstance.Volume = 0.2f;
                    mExplosionInstance.Play();
                    aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mExplosionInstance);
                    Destroy();
                }
            }
            else if (mType == ProjectileType.Missile)
            {
                aaGameWorld.aaGFXHandler.PlayEffect("Explosion_01", mXPos, mYPos, mAssetKit.COLOR_BLENDING);
                SoundEffectInstance mExplosionInstance = aaGameWorld.aaDisplay.aaGame.mExplosion.CreateInstance();
                mExplosionInstance.Volume = 0.2f;
                mExplosionInstance.Play();
                aaGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mExplosionInstance);
                Destroy();
            }
            else if (mType != ProjectileType.LaserBall)
            {
                Destroy();
            }
        }

        public float GetAngleToTarget()
        {
            return (float)Math.Atan2(mTarget.mYPos - mYPos, mTarget.mXPos - mXPos);
        }
    }
}
