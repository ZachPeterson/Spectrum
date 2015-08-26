using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    enum ProjectileKitType
    {
        EnemyBullet,
        Laser,
        Mine,
        Missile,
        PlayerBullet,
        AntiBullet,
    }

    class ProjectileKit
    {
        public string ASSET_NAME = "ERROR_PROJECTILE_KIT_NOT_INITIALIZED";
        public int DAMAGE = 0;
        public int SPEED = 0;
        public float SLOW_EFFECT = 0;
    }

    class ProjectileKit_Player_Bullet : ProjectileKit
    {
        public ProjectileKit_Player_Bullet()
            : base()
        {
            ASSET_NAME = "Bullet";
            DAMAGE = 15;
            SPEED = 800;
        }
    }

    class ProjectileKit_Enemy_Bullet : ProjectileKit
    {
        public ProjectileKit_Enemy_Bullet()
            : base()
        {
            ASSET_NAME = "Bullet";
            DAMAGE = 10;
            SPEED = 400;
        }
    }

    class ProjectileKit_Missile : ProjectileKit
    {
        public ProjectileKit_Missile()
            : base()
        {
            ASSET_NAME = "Missile";
            DAMAGE = 25;
            SPEED = 600;
        }
    }

    class ProjectileKit_Mine : ProjectileKit
    {
        public ProjectileKit_Mine()
            : base()
        {
            ASSET_NAME = "Mine";
            DAMAGE = 50;
            SPEED = 0;
        }
    }

    class ProjectileKit_Laser : ProjectileKit
    {
        public ProjectileKit_Laser()
            : base()
        {
            ASSET_NAME = "Laser";
            DAMAGE = 10;
            SPEED = 350;
        }
    }

    class ProjectileKit_Antibullet : ProjectileKit
    {
        public ProjectileKit_Antibullet()
            : base()
        {
            ASSET_NAME = "Antibullet";
            DAMAGE = 3;
            SPEED = 400;
        }
    }
}
