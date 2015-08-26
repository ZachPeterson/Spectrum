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
    class GraphicsEffectHandler
    {
        //Explosions
        public GameEffect mTmp;
        public GameWorld aaGameWorld;
        public List<GameEffect> mExplosions;


        public GraphicsEffectHandler(GameWorld pGameWorld)
        {
            aaGameWorld = pGameWorld;
            mExplosions = new List<GameEffect>();
        }

        public void Update(float DT)
        {
            foreach (GameEffect gfx in mExplosions)
            {
                gfx.Update(DT);
            }
        }

        public void PlayEffect(string type, float x, float y, Color blend)
        {
            if (type == "Explosion_01")
            {
                foreach (GameEffect gfx in mExplosions)
                {
                    if (!gfx.mIsActive)
                    {
                        gfx.PlayAtPos(x, y, blend);
                        return;
                    }
                }
                //Failed to find one
                mTmp = new GameEffect(aaGameWorld, type, x, y);
                mExplosions.Add(mTmp);
                mTmp.PlayAtPos(x, y, blend);
                return;
            }
            else if (type == "Explosion_02")
            {
                foreach (GameEffect gfx in mExplosions)
                {
                    if (!gfx.mIsActive)
                    {
                        gfx.PlayAtPos(x, y, blend);
                        //return;
                    }
                }
                //Failed to find one
                mTmp = new GameEffect(aaGameWorld, type, x, y);
                mExplosions.Add(mTmp);
                mTmp.PlayAtPos(x, y, blend);
                return;
            }
            else if (type == "QuickFix")
            {
                foreach (GameEffect gfx in mExplosions)
                {
                    if (!gfx.mIsActive)
                    {
                        gfx.PlayAtPos(x, y, blend);
                        //return;
                    }
                }
                //Failed to find one
                mTmp = new GameEffect(aaGameWorld, type, x, y);
                mExplosions.Add(mTmp);
                mTmp.PlayAtPos(x, y, blend);
                return;
            }
            else
            {
                // Console.WriteLine("GameEffect type not recognized, failed to play");
            }
        }

        public void PlayEffect(string type, float x, float y)
        {
            PlayEffect(type, x, y, Color.White);
        }
    }
}
