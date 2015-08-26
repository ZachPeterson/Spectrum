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
    class GameEffect
    {
        public GameWorld aaGameWorld;
        public GameEffectKit mKit;
        public DisplayObject_Animated mDispObj;
        public bool mIsActive;
        public string mType;

        public GameEffect(GameWorld pGameWorld, string type, float pXPos, float pYPos)
        {
            aaGameWorld = pGameWorld;
            mType = type;
            AssignDisplayObject();
            mDispObj = new DisplayObject_Animated(aaGameWorld.aaDisplay, mKit.ASSET_NAME, pXPos, pYPos, (int)mKit.ORIGIN.X, (int)mKit.ORIGIN.Y, 0.1f, mKit.SRC_RECTX, mKit.SRC_RECTY, mKit.SRC_RECTWIDTH, mKit.SRC_RECTHEIGHT, Constants.LAYER_PROJECTILE_DEPTH, false);
            //mDispObj.Hide();
        }

        public void AssignDisplayObject()
        {
            if (mType == "Explosion_01")
            {
                mKit = new GameEffectKit_Explosion_01();
            }
            else if (mType == "Explosion_02")
            {
                mKit = new GameEffectKit_Explosion_02();
            }
            else if (mType == "QuickFix")
            {
                mKit = new GameEffectKit_QuickFix();
            }
            else
            {
                // Console.WriteLine("ERROR: GAMEEFFECT TYPE IS NOT VALID");
            }
        }

        public void Update(float DT)
        {
            if (mIsActive)
            {
                mDispObj.Update(DT);
                if (mDispObj.mIsComplete)
                {
                    mIsActive = false;
                    mDispObj.Hide();
                }
            }
        }

        public void PlayAtPos(float x, float y, Color blend)
        {
            mDispObj.posX = x;
            mDispObj.posY = y;
            mIsActive = true;
            mDispObj.SetBlendColor(blend);
            mDispObj.Show();
            mDispObj.StartAnimation(mKit.ANIM_INFO);
        }

        public void PlayAtPos(float x, float y)
        {
            PlayAtPos(x, y, Color.White);
        }
    }
}
