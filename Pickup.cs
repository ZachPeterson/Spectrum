using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWars
{
    class Pickup
    {
        private CircleCollision mCollisionCircle;
        private Display mDisplay;
        private GameWorld mGameWorld;

        private const float mPickupRadius = 15.0f;

        private DisplayObject mPickupDisplay;


        public Pickup(Display pDisplay, GameWorld pGameWorld, string pAssetFile, Vector2 pPosition, Vector2 pOrigin, float pDepth, Rectangle pSrcRect, float pRotation = 0)
        {
            mDisplay = pDisplay;
            mGameWorld = pGameWorld;
            mPickupDisplay = new DisplayObject(pDisplay, pAssetFile, pPosition.X, pPosition.Y, (int)pOrigin.X, (int)pOrigin.Y, pDepth, pSrcRect.X, pSrcRect.Y, pSrcRect.Width, pSrcRect.Height, pRotation, false);

            mCollisionCircle = new CircleCollision(pPosition.X, pPosition.Y, mPickupRadius);
            mCollisionCircle.mIsPickup = true;
            mCollisionCircle.mPickupOwner = this;
            mGameWorld.CollisionShapes_Circle.Add(mCollisionCircle);
        }

        public virtual void Destroy()
        {
            if (mPickupDisplay != null)
            {
                mPickupDisplay.Hide();
                mPickupDisplay.Destroy();
                mPickupDisplay = null;
            }
            if(mCollisionCircle != null)
                mGameWorld.CollisionShapes_Circle_ToRemove.Add(mCollisionCircle);
        }

        public virtual void PickUp(Ship pShipToPickup)
        {
            SoundEffectInstance mPickUpInstance = mGameWorld.aaDisplay.aaGame.mPickUpSound.CreateInstance();
            mPickUpInstance.Volume = 0.8f;
            mPickUpInstance.Play();
            mGameWorld.aaDisplay.aaGame.mGameSoundFX.Add(mPickUpInstance);
        }
    }

    class HealthPickup : Pickup
    {
        private const float mHealthAmount = 0.2f;

        public HealthPickup(Display pDisplay, GameWorld pGameWorld, string pAssetFile, Vector2 pPosition, Vector2 pOrigin, float pDepth, Rectangle pSrcRect, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosition, pOrigin, pDepth, pSrcRect, pRotation)
        {
        }

        public override void PickUp(Ship pShipToPickup)
        {
            base.PickUp(pShipToPickup);

            pShipToPickup.aHull += (mHealthAmount * pShipToPickup.mKit.HULL);
            pShipToPickup.aHull = Math.Min(pShipToPickup.aHull, pShipToPickup.mKit.HULL);
        }
    }

    class DualShotPickup : Pickup
    {
        public DualShotPickup(Display pDisplay, GameWorld pGameWorld, string pAssetFile, Vector2 pPosition, Vector2 pOrigin, float pDepth, Rectangle pSrcRect, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosition, pOrigin, pDepth, pSrcRect, pRotation)
        {
        }

        public override void PickUp(Ship pShipToPickup)
        {
            base.PickUp(pShipToPickup);

            pShipToPickup.FireBullet = new Ability_ShootTier2(pShipToPickup);
            if (pShipToPickup.mMySatellite != null)
            {
                pShipToPickup.mMySatellite.FireBullet = new Ability_ShootTier2(pShipToPickup.mMySatellite);
            }
        }
    }

    class TriShotPickup : Pickup
    {
        public TriShotPickup(Display pDisplay, GameWorld pGameWorld, string pAssetFile, Vector2 pPosition, Vector2 pOrigin, float pDepth, Rectangle pSrcRect, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosition, pOrigin, pDepth, pSrcRect, pRotation)
        {
        }

        public override void PickUp(Ship pShipToPickup)
        {
            base.PickUp(pShipToPickup);

            pShipToPickup.FireBullet = new Ability_ShootTier3(pShipToPickup);
            if (pShipToPickup.mMySatellite != null)
            {
                pShipToPickup.mMySatellite.FireBullet = new Ability_ShootTier3(pShipToPickup.mMySatellite);
            }
        }
    }

    class QuadShotPickup : Pickup
    {
        public QuadShotPickup(Display pDisplay, GameWorld pGameWorld, string pAssetFile, Vector2 pPosition, Vector2 pOrigin, float pDepth, Rectangle pSrcRect, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosition, pOrigin, pDepth, pSrcRect, pRotation)
        {
        }

        public override void PickUp(Ship pShipToPickup)
        {
            base.PickUp(pShipToPickup);

            pShipToPickup.FireBullet = new Ability_ShootTier4(pShipToPickup);
            if (pShipToPickup.mMySatellite != null)
            {
                pShipToPickup.mMySatellite.FireBullet = new Ability_ShootTier4(pShipToPickup.mMySatellite);
            }
        }
    }
}
