using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceWars
{
    class Entity
    {
        public GameWorld aaGameWorld;
        public AssetKit mAssetKit;
        public Brain mBrain;
        public DisplayObject mDispObj;

        public CircleCollision mCircleCollision;

        public float mXPos;
        public float mYPos;

        public float mXVel;
        public float mYVel;

        public float mRotation; //Radians

        public string mType;

        public Entity(GameWorld pGameWorld, string pType, float pXPos, float pYPos, float pRotation = 0, float pXVel = 0, float pYVel = 0)
        {
            //Set Entity Type
            aaGameWorld = pGameWorld;
            mType = pType;
            SetEntityType();
            //Initialize Variables
            mXPos = pXPos;
            mYPos = pYPos;
            mXVel = pXVel;
            mYVel = pYVel;
            mRotation = pRotation;
            mDispObj = new DisplayObject(aaGameWorld.aaDisplay, mAssetKit.ASSET_NAME, mXPos, mYPos, (int)mAssetKit.ORIGIN.X, (int)mAssetKit.ORIGIN.Y, 0.1f, mAssetKit.SRC_RECTX, mAssetKit.SRC_RECTY, mAssetKit.SRC_RECTWIDTH, mAssetKit.SRC_RECTHEIGHT, mRotation, false);
        }

        public virtual void Destroy()
        {
            mDispObj.Destroy();
            mDispObj = null;
        }

        public void SetEntityType()
        {
            if (mType == "Player_Agility")
            {
                mAssetKit = new AssetKit_Ship_AGI_01();
                mBrain = new Brain_Player(this as Ship, "Agility");
            }
            else if (mType == "Player_Commando")
            {
                mAssetKit = new AssetKit_Ship_Commando();
                mBrain = new Brain_Player(this as Ship, "Commando");
            }
            else if (mType == "Player_Tank")
            {
                mAssetKit = new AssetKit_Ship_PTank();
                mBrain = new Brain_Player(this as Ship, "Tank");
            }
            else if (mType == "Grunt_Ship")
            {
                mAssetKit = new AssetKit_Ship_GRUNT_01();
                mBrain = new Brain_Grunt(this as Ship);
            }
            else if (mType == "Tank_Ship")
            {
                mAssetKit = new AssetKit_Ship_TANK_01();
                mBrain = new Brain_Tank(this as Ship);
            }
            else if (mType == "Bomb_Ship")
            {
                mAssetKit = new AssetKit_Ship_BOMB_01();
                mBrain = new Brain_Bomb(this as Ship);
            }
            else if(mType == "Stealth_Ship")
            {
                mAssetKit = new AssetKit_Ship_STEALTH_01();
                mBrain = new Brain_Stealth(this as Ship);
            }
            else if (mType == "Laser_Ship")
            {
                mAssetKit = new AssetKit_Ship_LASER_01();
                mBrain = new Brain_Laser(this as Ship);
            }
            else if (mType == "Grunt_Boss")
            {
                mAssetKit = new AssetKit_Ship_GRUNT_BOSS();
                mBrain = new Brain_Grunt_Boss(this as Ship);
            }
            else if (mType == "Tank_Boss")
            {
                mAssetKit = new AssetKit_Ship_TANK_BOSS();
                mBrain = new Brain_Tank_Boss(this as Ship);
            }
            else if (mType == "Bomb_Boss")
            {
                mAssetKit = new AssetKit_Ship_BOMB_BOSS_01();
                mBrain = new Brain_Bomb_Boss(this as Ship);
            }
            else if (mType == "Satellite")
            {
                mAssetKit = new AssetKit_Satellite();
                mBrain = new Brain_Satellite(this as Ship);
            }
            else
            {
                // Console.WriteLine("ERROR: NO OBJECT TYPE!");
            }
        }

        public void UpdateBrain(float DT)
        {
            mBrain.UpdateBrain(DT);
        }

        public virtual void Update(float DT)
        {
            mDispObj.posX = mXPos;
            mDispObj.posY = mYPos;
            FixRotation();
        }

        public void FixRotation()
        {
            if (mRotation > Math.PI)
                mRotation -= 2 * (float)Math.PI;
            else if (mRotation < -1 * Math.PI)
                mRotation += 2 * (float)Math.PI;
        }

        public virtual void ResolveCollision(int pDamage = 0, Ship pShip = null)
        {
            //Resolve Entity Collisions Here
        }
    }
}
