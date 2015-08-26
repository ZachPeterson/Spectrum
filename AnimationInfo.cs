using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class AnimationInfo
    {
        //AnimInfo Struct
        public uint ID;
        public uint Length;
        public uint Speed;

        public AnimationInfo(uint pID, uint pLength, uint pSpeed = 0)
        {
            ID = pID;
            Length = pLength;
            Speed = pSpeed;
        }
    }
}
