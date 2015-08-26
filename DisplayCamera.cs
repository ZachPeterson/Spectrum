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
    class DisplayCamera
    {
        public GameWorld aaGameWorld;
        public float mFocusX;
        public float mFocusY;
        public float mXPos;
        public float mYPos;

        public DisplayCamera(GameWorld pGameWorld = null)
        {
            aaGameWorld = pGameWorld;
        }

        public void ChangeFocus(float pPosX, float pPosY)
        {
            mFocusX = pPosX;
            mFocusY = pPosY;
        }

        public void Update(float DT)
        {
            //TODO: This needs to update based on wheret the focus is
            //Needs easing
            mXPos = mFocusX - Constants.RESOLUTION_WIDTH/2.0f;
            mYPos = mFocusY - Constants.RESOLUTION_HEIGHT/2.0f;

            if (mXPos < 0)
            {
                mXPos = 0;
            }
            if (mYPos < 0)
            {
                mYPos = 0;
            }

            if (mXPos + Constants.RESOLUTION_WIDTH > Constants.REGION_PLAYABLE_WIDTH)
            {
                mXPos = Constants.REGION_PLAYABLE_WIDTH - Constants.RESOLUTION_WIDTH;
            }
            if (mYPos + Constants.RESOLUTION_HEIGHT > Constants.REGION_PLAYABLE_HEIGHT)
            {
                mYPos = Constants.REGION_PLAYABLE_HEIGHT - Constants.RESOLUTION_HEIGHT;
            }
        }
    }
}
