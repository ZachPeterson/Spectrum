using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class Constants
    {
        //Global Constants
        public static int RESOLUTION_WIDTH = 1280;
        public static int RESOLUTION_HEIGHT = 720;

        public static int REGION_PLAYABLE_WIDTH = 4096;
        public static int REGION_PLAYABLE_HEIGHT = 4096;

        //Layers
        public static float LAYER_ENTITY_DEPTH = 0.5f;
        public static float LAYER_PROJECTILE_DEPTH = 0.4f;
        public static float LAYER_UI_FRONT = 0.1f;
        public static float LAYER_UI_BACK = 0.2f;

        // Team numbers
        public static uint PLAYER_TEAM = 0;
        public static uint ENEMY_TEAM = 1;

        //World Properties
        public static float WORLD_FRICTION = 10.0f;
        public static float SHIP_MAX_VELOCITY = 300.0f;
        public static float COLLISION_FORCE = 5;
        public static int COLLISION_DAMAGE = 2;

        //Ability IDs
        public const int EMPTY_ONE = -1;
        public const int EMPTY_TWO = -2;
        public const int EMPTY_THREE = -3;
        public const int EMPTY_FOUR = -4;
        public const int QUICK_TURN = 0;
        public const int WARP_DRIVE = 1;
        public const int GUN_OVERCHARGE = 2;
        public const int BULLET_STAR = 3;
        public const int BULLET_FURRY = 4;
        public const int DUAL_WIELD = 5;
        public const int OVERCHARGE_ENGINES = 6;
        public const int MISSILE_FLURRY = 7;
        public const int SHOTGUN_BLAST = 8;
        public const int TIGHTER_TURNS = 9;
        public const int ENERGY_SHIELD = 10;
        public const int REAR_GUN = 11;
        public const int ANTI_BULLETS = 12;
        public const int SATELLITE = 13;
        public const int MINES = 14;
        public const int FULL_SHIELD = 15;
    }
}
