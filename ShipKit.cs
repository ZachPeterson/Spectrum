using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class ShipKit
    {
        public List<AbilityType> ABILITIES;
        public int HULL = 0;
        public float THRUST_POWER = 0.0f;
        public float THRUST_MAX = 0.0f;
        public float TURN_POWER = 0.0f;
        public float COLLISION_SIZE = 0.0f;
        public float MASS = 0.0f;
        public int POINTS = 0;
    }

    class ShipKit_Player : ShipKit
    {
        public ShipKit_Player()
            : base()
        {
            ABILITIES = new List<AbilityType>();
            HULL = 100;
            THRUST_POWER = 200.0f;
            THRUST_MAX = 300.0f;
            TURN_POWER = 5.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
        }
    }

    class ShipKit_Player_Agility : ShipKit
    {
        public ShipKit_Player_Agility()
            : base()
        {
            ABILITIES = new List<AbilityType>();
            HULL = 100;
            THRUST_POWER = 200.0f;
            THRUST_MAX = 310.0f;
            TURN_POWER = 5.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
        }
    }

    class ShipKit_Player_Commando : ShipKit
    {
        public ShipKit_Player_Commando()
            : base()
        {
            ABILITIES = new List<AbilityType>();
            HULL = 120;
            THRUST_POWER = 175.0f;
            THRUST_MAX = 280.0f;
            TURN_POWER = 4.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
        }
    }

    class ShipKit_Player_Tank : ShipKit
    {
        public ShipKit_Player_Tank()
            : base()
        {
            ABILITIES = new List<AbilityType>();
            HULL = 200;
            THRUST_POWER = 50.0f;
            THRUST_MAX = 230.0f;
            TURN_POWER = 3.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
        }
    }

    class ShipKit_Grunt : ShipKit
    {
        public ShipKit_Grunt()
            : base()
        {
            //ABILITIES = new List<AbilityType>(AbilityType.FireBullet);
            HULL = 15;
            THRUST_POWER = 70.0f;
            THRUST_MAX = 200.0f;
            TURN_POWER = 3.0f;
            COLLISION_SIZE = 10.0f;
            MASS = 0.0001f;
            POINTS = 100;
        }
    }

    class ShipKit_Tank : ShipKit
    {
        public ShipKit_Tank()
            : base()
        {
            HULL = 45;
            THRUST_POWER = 50.0f;
            THRUST_MAX = 150.0f;
            TURN_POWER = 2.0f;
            COLLISION_SIZE = 18.0f;
            MASS = 0.0002f;
            POINTS = 500;
        }
    }

    class ShipKit_Bomb : ShipKit
    {
        public ShipKit_Bomb()
            : base()
        {
            HULL = 30;
            THRUST_POWER = 200.0f;
            THRUST_MAX = 400.0f;
            TURN_POWER = 20.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.00001f;
            POINTS = 800;
        }
    }

    class ShipKit_Stealth : ShipKit
    {
        public ShipKit_Stealth()
            : base()
        {
            HULL = 30;
            THRUST_POWER = 50.0f;
            THRUST_MAX = 200.0f;
            TURN_POWER = 5.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
            POINTS = 1000;
        }
    }

    class ShipKit_Laser : ShipKit
    {
        public ShipKit_Laser()
            : base()
        {
            HULL = 30;
            THRUST_POWER = 50.0f;
            THRUST_MAX = 150.0f;
            TURN_POWER = 5.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
            POINTS = 300;
        }
    }

    class ShipKit_Grunt_Boss : ShipKit
    {
        public ShipKit_Grunt_Boss()
            : base()
        {
            HULL = 3000;
            THRUST_POWER = 50.0f;
            THRUST_MAX = 150.0f;
            TURN_POWER = 2.0f;
            COLLISION_SIZE = 70.0f;
            MASS = 0.001f;
            POINTS = 5000;
        }
    }

    class ShipKit_Tank_Boss : ShipKit
    {
        public ShipKit_Tank_Boss()
            : base()
        {
            HULL = 4000;
            THRUST_POWER = 20.0f;
            THRUST_MAX = 100.0f;
            TURN_POWER = 1.0f;
            COLLISION_SIZE = 70.0f;
            MASS = 0.001f;
            POINTS = 5000;
        }
    }

    class ShipKit_Bomb_Boss : ShipKit
    {
        public ShipKit_Bomb_Boss()
            : base()
        {
            HULL = 3000;
            THRUST_POWER = 50.0f;
            THRUST_MAX = 230.0f;
            TURN_POWER = 2.0f;
            COLLISION_SIZE = 70.0f;
            MASS = 0.001f;
            POINTS = 5000;
        }
    }

    class ShipKit_Satellite : ShipKit
    {
        public ShipKit_Satellite()
            : base()
        {
            ABILITIES = new List<AbilityType>();
            HULL = 20;
            THRUST_POWER = 0.0f;
            THRUST_MAX = 0.0f;
            TURN_POWER = 0.0f;
            COLLISION_SIZE = 12.0f;
            MASS = 0.0001f;
        }
    }
}
