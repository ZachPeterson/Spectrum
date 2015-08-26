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
using System.IO;

namespace SpaceWars
{
    class GameEffectKit
    {
        public string ASSET_NAME = "ERROR_EFFECTKIT_NOT_INTIALIZED";

        public int SRC_RECTX = 0;
        public int SRC_RECTY = 0;
        public int SRC_RECTWIDTH = 0;
        public int SRC_RECTHEIGHT = 0;

        public AnimationInfo ANIM_INFO = null;

        public Vector2 ORIGIN = Vector2.Zero;
    }

    class GameEffectKit_Explosion_01 : GameEffectKit
    {
        public GameEffectKit_Explosion_01() : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 128;
            SRC_RECTWIDTH = 64;
            SRC_RECTHEIGHT = 64;

            ANIM_INFO = new AnimationInfo(2, 4, 4);

            ORIGIN = new Vector2(32, 32);
        }
    }

    class GameEffectKit_Explosion_02 : GameEffectKit
    {
        public GameEffectKit_Explosion_02()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 512;
            SRC_RECTWIDTH = 128;
            SRC_RECTHEIGHT = 128;

            ANIM_INFO = new AnimationInfo(4, 4, 8);

            ORIGIN = new Vector2(64, 64);
        }
    }

    class GameEffectKit_QuickFix : GameEffectKit
    {
        public GameEffectKit_QuickFix()
            : base()
        {
            ASSET_NAME = "Ship_MASTER";

            SRC_RECTX = 0;
            SRC_RECTY = 448;
            SRC_RECTWIDTH = 64;
            SRC_RECTHEIGHT = 64;

            ANIM_INFO = new AnimationInfo(2, 4, 4);

            ORIGIN = new Vector2(32, 32);
        }
    }
}
