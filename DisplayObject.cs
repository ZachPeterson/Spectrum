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
    class DisplayObject
    {
        public Display aaDisplay;

        public string mAssetFile;

        public float posX;
        public float posY;

        public Vector2 mOrigin;

        public float mDepth;

        public bool mIsShowing;

        public float mRotation;

        public float mOpacity;

        public Color mBlendColor;

        public float mZoom;

        //Src Rectangle stuffs
        public int src_RectX;
        public int src_RectY;
        public int src_RectWidth;
        public int src_RectHeight;

        public bool mHorizontalFlip;
        public bool mVerticalFlip;

        //Camera oriented
        public bool isExemptFromCamera;

        public DisplayObject(Display pDisplay, string pAssetFile, float pPosX, float pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = -1, float pRotation=0, bool pIsExemptFromCamera = false)
        {
            aaDisplay = pDisplay;
            mAssetFile = pAssetFile;
            posX = pPosX;
            posY = pPosY;
            mOrigin = new Vector2(pOriginX, pOriginY);
            src_RectX = pSrc_RectX;
            src_RectY = pSrc_RectY;
            src_RectWidth = pWidth;
            src_RectHeight = pHeight;
            mDepth = pDepth;
            mHorizontalFlip = false;
            mVerticalFlip = false;
            mRotation = pRotation;
            mOpacity = 1.0f;
            mZoom = 1.0f;
            isExemptFromCamera = pIsExemptFromCamera;
            mBlendColor = Color.White;
            aaDisplay.AddToDisplayList(this);
            mIsShowing = false;
            Show();
        }

        public void Show()
        {
            aaDisplay.AddToDisplayList(this);
        }

        public void Hide()
        {
            aaDisplay.RemoveFromDisplayList(this);
        }

        public bool IsShowing()
        {
            return (aaDisplay.mDisplayObjects.IndexOf(this) != -1);
        }

        public void FadeToAmount(float amount)
        {
            mOpacity = amount;
        }

        public void SetBlendColor(Color col)
        {
            mBlendColor = col;
        }

        public void SetZoom(float zoom)
        {
            mZoom = zoom;
        }

        public void Destroy()
        {
            //Remove from draw list
            Hide();
            //Done!
        }

        public void RotateToAngle(float rad)
        {
            mRotation = rad;
        }

        public DisplayObject GetCopy()
        {
            return new DisplayObject(aaDisplay, mAssetFile, posX, posY, (int)mOrigin.X, (int)mOrigin.Y, mDepth, src_RectX, src_RectY, src_RectWidth, src_RectHeight, mRotation, isExemptFromCamera);
        }

    }
}
