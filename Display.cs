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

namespace SpaceWars
{
    class Display
    {
        public Game1 aaGame;

        public DisplayCamera aaCamera;

        public AssetList mAssetList;
        public List<DisplayObject> mDisplayObjects;

        public List<UIElement> mUIElements;
        public List<UIElement> mVisibleUIElements;
        public List<UIElement> mMenuUIElements;

        public RenderTarget2D mScene;

        //FX stuffs
        public Effect fx;

        public Display(Game1 pGame)
        {
            aaGame = pGame;
            aaCamera = new DisplayCamera();
            mAssetList = new AssetList(aaGame.Content);
            mDisplayObjects = new List<DisplayObject>();
            mUIElements = new List<UIElement>();
            mVisibleUIElements = new List<UIElement>();
            mMenuUIElements = new List<UIElement>();
        }

        public bool LoadAssets()
        {
            //May need to be moved somewhere else when menu navigation is added
            fx = aaGame.Content.Load<Effect>("fx");

            List<string> tFileList = new List<string>();
            tFileList.Add("Back_Black_01");
            tFileList.Add("Ship_MASTER");
            tFileList.Add("UI_Element_MASTER");
            tFileList.Add("Ability_Icons_MASTER");
            tFileList.Add("Menu_MASTER");
            tFileList.Add("UI_Element_Health");
            tFileList.Add("Zelbyte");
            tFileList.Add("ShipMenu_MASTER");

            mAssetList.LoadAssetsIntoMemory(tFileList);

            mScene = new RenderTarget2D(aaGame.GraphicsDevice, Constants.RESOLUTION_WIDTH, Constants.RESOLUTION_HEIGHT);

            return true;
        }

        public bool UnloadAssets()
        {
            return true;
        }

        public void AddToDisplayList(DisplayObject d)
        {
            if (mDisplayObjects.IndexOf(d) == -1)
                mDisplayObjects.Add(d);
        }

        public void RemoveFromDisplayList(DisplayObject d)
        {
            if (mDisplayObjects.IndexOf(d) != -1)
                mDisplayObjects.Remove(d);
        }

        public void AddUIElement(UIElement e)
        {
            if (mUIElements.IndexOf(e) == -1)
                mUIElements.Add(e);
        }

        public void RemoveUIElement(UIElement e)
        {
            if (mUIElements.IndexOf(e) != -1)
                mUIElements.Remove(e);
        }

        public void ShowUIElement(UIElement e)
        {
            if (mVisibleUIElements.IndexOf(e) == -1)
                mVisibleUIElements.Add(e);
        }

        public void HideUIElement(UIElement e)
        {
            if (mVisibleUIElements.IndexOf(e) != -1)
                mVisibleUIElements.Remove(e);
        }

        public void AddMenuUIElement(UIElement e)
        {
            if (mMenuUIElements.IndexOf(e) == -1)
                mMenuUIElements.Add(e);
        }

        public void HideMenuUIElement(UIElement e)
        {
            if (mMenuUIElements.IndexOf(e) != -1)
                mMenuUIElements.Remove(e);
        }

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            Matrix scaleMatrix = Matrix.CreateScale(1);

            //Prepare Texture for Screen
            //Draw objects

            aaGame.GraphicsDevice.SetRenderTarget(mScene);
            aaGame.GraphicsDevice.Clear(new Color(18,16,18));
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.LinearClamp, null, null, null, scaleMatrix);
            foreach (DisplayObject dispObj in mDisplayObjects)
            {
                if (dispObj.isExemptFromCamera)
                {
                    spriteBatch.Draw(mAssetList.getAsset(dispObj.mAssetFile), new Vector2(dispObj.posX, dispObj.posY), new Rectangle(dispObj.src_RectX, dispObj.src_RectY, dispObj.src_RectWidth, dispObj.src_RectHeight), dispObj.mBlendColor * dispObj.mOpacity, dispObj.mRotation, dispObj.mOrigin, dispObj.mZoom, (dispObj.mHorizontalFlip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, dispObj.mDepth);
                }
                else
                {
                    spriteBatch.Draw(mAssetList.getAsset(dispObj.mAssetFile), new Vector2(dispObj.posX - aaCamera.mXPos, dispObj.posY - aaCamera.mYPos), new Rectangle(dispObj.src_RectX, dispObj.src_RectY, dispObj.src_RectWidth, dispObj.src_RectHeight), dispObj.mBlendColor * dispObj.mOpacity, dispObj.mRotation, dispObj.mOrigin, dispObj.mZoom, (dispObj.mHorizontalFlip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, dispObj.mDepth);
                }
            }

            foreach (UIElement e in mVisibleUIElements)
            {
                e.Draw(spriteBatch);
            }

            spriteBatch.End();

            //Draw menu UI elements seperately to avoid blending
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, scaleMatrix);
            foreach (UIElement e in mMenuUIElements)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.End();

            //Apply fx
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, fx);
            fx.CurrentTechnique.Passes[0].Apply();
            //Then actually Draw to the Screen
            aaGame.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Draw((Texture2D)mScene, Vector2.Zero, Color.White);
        }
    }
}
