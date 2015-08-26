using System;
using System.Collections.Generic;
using System.Linq;
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
    class DisplayObject_Animated : DisplayObject
    {
        //Extension of DisplayObject, meant for spritesheet oriented animations
        public uint mCurrentFrame;
        public uint mCurrentAnimation;
        public uint mCurrentAnimation_Length;
        public uint mCurrentAnimation_Speed;
        public uint mCurrentAnimation_Speed_Counter;

        public bool mIsComplete;
        public bool mIsRepeating;

        public DisplayObject_Animated(Display pDisplay, string pAssetFile, float pPosX, float pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = -1, float pRotation=0, bool pIsExemptFromCamera = false, bool pIsRepeating=false)
            : base(pDisplay, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation, pIsExemptFromCamera) 
        {
            mIsComplete = false;
            mCurrentFrame = 0;
            mCurrentAnimation = 0;
            mCurrentFrame = 0;
            mCurrentAnimation_Length = 0;
            mCurrentAnimation_Speed = 0;
            mCurrentAnimation_Speed_Counter = 0;
            mIsRepeating = pIsRepeating;
        }

        public void UpdateSrcRect()
        {
            src_RectY = (int)mCurrentAnimation * src_RectHeight;
            src_RectX = (int)mCurrentFrame * src_RectWidth;
        }

        public bool StartAnimation(AnimationInfo pAnimInfo)
        {
            //Set Animation to Beginning of Track
            mCurrentFrame = 0;
            //Set Current Animation to Parameter
            mCurrentAnimation = pAnimInfo.ID;
            mCurrentAnimation_Length = pAnimInfo.Length + 1;
            mCurrentAnimation_Speed = pAnimInfo.Speed;
            //Update src_Rectangle with new coordinates
            UpdateSrcRect();
            mIsComplete = false;
            return true;
        }

        public void Update(float DT)
        {
            //Change later to add gameTime as effect
            if (!mIsComplete)
            {
                mCurrentAnimation_Speed_Counter++;
                if (mCurrentAnimation_Speed_Counter >= mCurrentAnimation_Speed)
                {
                    //Advance the Frame
                    mCurrentFrame++;
                    mCurrentAnimation_Speed_Counter = 0;
                }
                if (mCurrentFrame >= mCurrentAnimation_Length - 1)
                {
                    if (mIsRepeating)
                    {
                        mCurrentFrame = 0;
                    }
                    else
                    {
                        mIsComplete = true;
                    }
                }
                UpdateSrcRect();
            }
        }
    }
}
