using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWars
{
    class EntityManager
    {
        private GameWorld aaGameWorld;

        // Asset kits
        private AssetKit pEnemyBulletAssetKit;
        private AssetKit pLaserAssetKit;
        private AssetKit pMineAssetKit;
        private AssetKit pMissileAssetKit;
        private AssetKit pPlayerBulletAssetKit;
        private AssetKit pAntiBulletAssetKit;
        private AssetKit pPlayerMineAssetKit;

        // Projectile kits
        private ProjectileKit pEnemyBulletProjectileKit;
        private ProjectileKit pLaserProjectileKit;
        private ProjectileKit pMineProjectileKit;
        private ProjectileKit pMissileProjectileKit;
        private ProjectileKit pPlayerBulletProjectileKit;
        private ProjectileKit pAntiBulletProjectileKit;

        private UInt64 mNextProjectileID;



        public EntityManager(GameWorld pGameWorld)
        {
            aaGameWorld = pGameWorld;

            pEnemyBulletAssetKit = new AssetKit_Proj_Bullet_01();
            pLaserAssetKit = new AssetKit_Proj_Laser_01();
            pMineAssetKit = new AssetKit_Proj_Mine_01();
            pMissileAssetKit = new AssetKit_Proj_Missile_01();
            pPlayerBulletAssetKit = new AssetKit_Proj_PlayerBullet_01();
            pAntiBulletAssetKit = new AssetKit_Proj_AntiBullet();
            pPlayerMineAssetKit = new AssetKit_PlayerMine();

            pEnemyBulletProjectileKit = new ProjectileKit_Enemy_Bullet();
            pLaserProjectileKit = new ProjectileKit_Laser();
            pMineProjectileKit = new ProjectileKit_Mine();
            pMissileProjectileKit = new ProjectileKit_Missile();
            pPlayerBulletProjectileKit = new ProjectileKit_Player_Bullet();
            pAntiBulletProjectileKit = new ProjectileKit_Antibullet();
            mNextProjectileID = 0;
        }

        public Projectile AddProjectile(uint pTeam, ProjectileType pType, Vector2 pPosition, float pRotation, ProjectileKitType pProjectileKitType, Ship pTarget = null, Ship pOwner = null)
        {
            ProjectileKit projectileKit = null;
            AssetKit assetKit = null;
            switch (pProjectileKitType)
            {
                case ProjectileKitType.EnemyBullet:
                    projectileKit = pEnemyBulletProjectileKit;
                    assetKit = pEnemyBulletAssetKit;
                    break;
                case ProjectileKitType.Laser:
                    projectileKit = pLaserProjectileKit;
                    assetKit = pLaserAssetKit;
                    break;
                case ProjectileKitType.Mine:
                    if (pTeam == 0)
                        assetKit = pPlayerMineAssetKit;
                    else
                        assetKit = pMineAssetKit;
                    projectileKit = pMineProjectileKit;
                    break;
                case ProjectileKitType.Missile:
                    projectileKit = pMissileProjectileKit;
                    assetKit = pMissileAssetKit;
                    break;
                case ProjectileKitType.PlayerBullet:
                    projectileKit = pPlayerBulletProjectileKit;
                    assetKit = pPlayerBulletAssetKit;
                    break;
                case ProjectileKitType.AntiBullet:
                    projectileKit = pAntiBulletProjectileKit;
                    assetKit = pAntiBulletAssetKit;
                    break;
                default:
                    projectileKit = pEnemyBulletProjectileKit;
                    assetKit = pEnemyBulletAssetKit;
                    break;
            }
            if (aaGameWorld.mRemovedProjectileList.Count != 0)
            {
                Projectile tempProj = aaGameWorld.mRemovedProjectileList.ElementAt(0);
                aaGameWorld.mRemovedProjectileList.RemoveAt(0);
                if (aaGameWorld.mProjectileList.Contains(tempProj))
                {
                    Projectile newProj = new Projectile(aaGameWorld, pTeam, pType, pPosition.X, pPosition.Y, pRotation, projectileKit, pTarget, pOwner);
                    aaGameWorld.mProjectileList.Add(newProj);
                    return newProj;
                }
                else
                {
                    tempProj.mXPos = pPosition.X;
                    tempProj.mYPos = pPosition.Y;
                    tempProj.mType = pType;
                    tempProj.mTeam = pTeam;
                    tempProj.SetProjType();
                    tempProj.mRotation = pRotation;
                    tempProj.mProjKit = projectileKit;
                    tempProj.mTarget = pTarget;
                    tempProj.mOwner = pOwner;
                    tempProj.mDispObject.mAssetFile = assetKit.ASSET_NAME;
                    tempProj.mDispObject.src_RectX = assetKit.SRC_RECTX;
                    tempProj.mDispObject.src_RectY = assetKit.SRC_RECTY;
                    tempProj.mDispObject.src_RectWidth = assetKit.SRC_RECTWIDTH;
                    tempProj.mDispObject.src_RectHeight = assetKit.SRC_RECTHEIGHT;
                    tempProj.mDispObject.SetZoom(1.0f);
                    tempProj.mDispObject.Show();
                    tempProj.mProjectileID = mNextProjectileID++;
                    //tempProj.mDispObject = new DisplayObject(aaGameWorld.aaDisplay, assetKit.ASSET_NAME, pPosition.X, pPosition.Y, (int)assetKit.ORIGIN.X, (int)assetKit.ORIGIN.Y, Constants.LAYER_PROJECTILE_DEPTH, assetKit.SRC_RECTX, assetKit.SRC_RECTY, assetKit.SRC_RECTWIDTH, assetKit.SRC_RECTHEIGHT, pRotation, false);
                    aaGameWorld.mProjectileList.Add(tempProj);
                    return tempProj;
                }
            }
            else
            {
                Projectile newProj = new Projectile(aaGameWorld, pTeam, pType, pPosition.X, pPosition.Y, pRotation, projectileKit, pTarget, pOwner);
                newProj.mProjectileID = mNextProjectileID++;
                aaGameWorld.mProjectileList.Add(newProj);
                return newProj;
            }
        }
    }
}
