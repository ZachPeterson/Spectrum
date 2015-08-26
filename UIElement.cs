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
using System.Text;

namespace SpaceWars
{
    class UIElement
    {
        public Display aaDisplay;
        public GameWorld aaGameWorld;

        public string mAssetFile;

        public int posX;
        public int posY;

        public Vector2 mOrigin;

        public float mDepth;
        public float mRotation;
        public float mOpacity;
        public float mScale;

        public bool mIsShowing;

        public int src_RectX;
        public int src_RectY;
        public int src_RectWidth;
        public int src_RectHeight;


        public UIElement(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = -1, float pRotation = 0, float pScale = 1.0f)
        {
            aaDisplay = pDisplay;
            aaGameWorld = pGameWorld;
            mAssetFile = pAssetFile;
            posX = pPosX;
            posY = pPosY;
            mOrigin = new Vector2(pOriginX, pOriginY);
            src_RectX = pSrc_RectX;
            src_RectY = pSrc_RectY;
            src_RectWidth = pWidth;
            src_RectHeight = pHeight;
            mDepth = pDepth;
            mRotation = pRotation;
            mOpacity = 1.0f;
            mScale = pScale;

            Hide();
        }

        public virtual void Destroy()
        {
            aaDisplay.HideUIElement(this);
        }

        public virtual void MenuDestroy()
        {
            aaDisplay.HideMenuUIElement(this);
        }

        public virtual void Show()
        {
            mIsShowing = true;
            aaDisplay.ShowUIElement(this);
        }


        public virtual void Hide()
        {
            mIsShowing = false;
            aaDisplay.HideUIElement(this);
        }

        public virtual void Update(float pDT)
        {
        }

        public virtual void Draw(SpriteBatch pSBatch)
        {
            pSBatch.Draw(aaDisplay.mAssetList.getAsset(mAssetFile), new Vector2(posX, posY), new Rectangle(src_RectX, src_RectY, src_RectWidth, src_RectHeight), Color.White * mOpacity, mRotation, mOrigin, mScale, SpriteEffects.None, mDepth);
        }
    }

    class UIElement_AbilityIndicator : UIElement
    {
        Ability mTrackedAbility;

        public UIElement_AbilityIndicator(Display pDisplay, GameWorld pGameWorld, Ability pTrackedAbility, AssetKit pAssetKit, int pPosX, int pPosY, float pDepth, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetKit.ASSET_NAME, pPosX, pPosY, (int)pAssetKit.ORIGIN.X, (int)pAssetKit.ORIGIN.Y, pDepth, pAssetKit.SRC_RECTX, pAssetKit.SRC_RECTY, pAssetKit.SRC_RECTWIDTH, pAssetKit.SRC_RECTHEIGHT, pRotation, 2.0f)
        {
            mTrackedAbility = pTrackedAbility;
            Show();
        }

        public override void Update(float pDT)
        {
            if (mTrackedAbility.GetCooldownPercentage() == 1.0f)
            {
                mOpacity = 1.0f;
            }
            else
            {
                mOpacity = mTrackedAbility.GetCooldownPercentage() / 4.0f + 0.1f;
            }
        }
    }

    class UIElement_ShipCrosshair : UIElement
    {
        public UIElement_ShipCrosshair(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
            Show();
        }

        public override void Update(float pDT)
        {
            posX = Mouse.GetState().X;
            posY = Mouse.GetState().Y;
        }
    }

    class UIElement_ShipAimer : UIElement
    {
        Ship mShip;

        public UIElement_ShipAimer(Display pDisplay, GameWorld pGameWorld, Ship pTrackedShip, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
            mShip = pTrackedShip;
            Show();
        }

        public override void Update(float pDT)
        {
            posX = (int)(mShip.mXPos - aaDisplay.aaCamera.mXPos);
            posY = (int)(mShip.mYPos - aaDisplay.aaCamera.mYPos);
            mRotation = (float)Math.Atan2((float)((Mouse.GetState().Y + mShip.aaGameWorld.aaDisplay.aaCamera.mYPos) - mShip.mYPos), (float)((Mouse.GetState().X + mShip.aaGameWorld.aaDisplay.aaCamera.mXPos) - mShip.mXPos)) + (float)Math.PI / 2.0f;
        }
    }

    class UIElement_MenuButton : UIElement
    {
        public int baseSourceX;
        public int baseSourceY;

        public ButtonState prevButtonState;
        public ButtonState currentButtonState;

        public bool forceHighlight = false;
        public bool highlightDisabled = false;

        public UIElement_MenuButton(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
            baseSourceX = pSrc_RectX;
            baseSourceY = pSrc_RectY;
            currentButtonState = Mouse.GetState().LeftButton;
        }

        public override void Update(float pDT)
        {
            prevButtonState = currentButtonState;
            currentButtonState = Mouse.GetState().LeftButton;

            base.Update(pDT);

            if (IsPointInRectangle(Mouse.GetState().X, Mouse.GetState().Y, posX, posY, 2 * src_RectWidth, 2 * src_RectHeight) && !highlightDisabled || forceHighlight)
            {
                src_RectX = baseSourceX + src_RectWidth;
            }
            else
            {
                src_RectX = baseSourceX;
            }
        }

        public override void Draw(SpriteBatch pSBatch)
        {
            pSBatch.Draw(aaDisplay.mAssetList.getAsset(mAssetFile), new Vector2(posX, posY), new Rectangle(src_RectX, src_RectY, src_RectWidth, src_RectHeight), Color.White * mOpacity, mRotation, mOrigin, 2.0f, SpriteEffects.None, mDepth);
        }

        public bool IsPointInRectangle(int pX, int pY, int pRectX, int pRectY, int pRectWidth, int pRectHeight)
        {
            if (pX < pRectX)
                return false;
            if (pY < pRectY)
                return false;
            if (pX > pRectX + pRectWidth)
                return false;
            if (pY > pRectY + pRectHeight)
                return false;

            return true;
        }

        public bool CheckButtonClick()
        {
            return (IsPointInRectangle(Mouse.GetState().X, Mouse.GetState().Y, posX, posY, 2 * src_RectWidth, 2 * src_RectHeight) && prevButtonState == ButtonState.Released && currentButtonState == ButtonState.Pressed);
        }

        public bool CheckButtonHover()
        {
            return IsPointInRectangle(Mouse.GetState().X, Mouse.GetState().Y, posX, posY, 2 * src_RectWidth, 2 * src_RectHeight);
        }
    }

    class UIElement_StaticMenuElement : UIElement
    {
        public UIElement_StaticMenuElement(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
        }


        public override void Draw(SpriteBatch pSBatch)
        {
            pSBatch.Draw(aaDisplay.mAssetList.getAsset(mAssetFile), new Vector2(posX, posY), new Rectangle(src_RectX, src_RectY, src_RectWidth, src_RectHeight), Color.White * mOpacity, mRotation, mOrigin, 2.0f, SpriteEffects.None, mDepth);
        }
    }

    class UIElement_PlayerScore : UIElement
    {
        SpriteFont mFont;
        Color mColor;

        public UIElement_PlayerScore(Display pDisplay, GameWorld pGameWorld, SpriteFont pFont, int pPosX, int pPosY, Color pTextColor)
            : base(pDisplay, pGameWorld, "", pPosX, pPosY, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            mFont = pFont;
            mColor = pTextColor;

            Show();
        }

        public override void Draw(SpriteBatch pSBatch)
        {
            if (aaGameWorld.Player.IsDead())
            {
                String scoreString = "Score: " + aaGameWorld.mPoints;
                pSBatch.DrawString(mFont, scoreString, new Vector2(585, 340), mColor);
            }
            else
            {
                String scoreString = "Score: " + aaGameWorld.mPoints;
                pSBatch.DrawString(mFont, scoreString, new Vector2(posX, posY), mColor);
            }
        }
    }

    class UIElement_HealthBar_Border : UIElement
    {
        public UIElement_HealthBar_Border(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
            Show();
        }
    }

    class UIElement_HealthBar_Bar : UIElement
    {
        public Ship mPlayer;
        public float healthRatio;
        public float mOrigRectWidth;

        public UIElement_HealthBar_Bar(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, Ship pPlayer, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
            Show();
            mPlayer = pPlayer;
            mOrigRectWidth = pWidth;
        }

        public override void Update(float pDT)
        {
            healthRatio = mPlayer.aHull / mPlayer.mKit.HULL;
            src_RectWidth = (int)(mOrigRectWidth * healthRatio);
        }
    }

    class UIElement_HealthBar_BarWhite : UIElement
    {
        public Ship mPlayer;
        public float healthRatio;
        public float mOrigRectWidth;

        public UIElement_HealthBar_BarWhite(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, Ship pPlayer, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
            Show();
            mPlayer = pPlayer;
            mOrigRectWidth = pWidth;
        }

        public override void Update(float pDT)
        {
            healthRatio = mPlayer.aHull / mPlayer.mKit.HULL;
            src_RectWidth = (int)MathHelper.Lerp(src_RectWidth, (mOrigRectWidth * healthRatio), 0.03f);
        }
    }

    class UIElement_Plaque_GameOver : UIElement
    {
        public UIElement_Plaque_GameOver(Display pDisplay, GameWorld pGameWorld, string pAssetFile, int pPosX, int pPosY, int pOriginX, int pOriginY, float pDepth, int pSrc_RectX = 0, int pSrc_RectY = 0, int pWidth = -1, int pHeight = 01, float pRotation = 0)
            : base(pDisplay, pGameWorld, pAssetFile, pPosX, pPosY, pOriginX, pOriginY, pDepth, pSrc_RectX, pSrc_RectY, pWidth, pHeight, pRotation)
        {
        }
    }

    class UIElement_WaveDisplay : UIElement
    {
        SpriteFont mFont;
        Color mColor;

        float mElapsedTime;
        float mSolidTime = 3.0f;
        float mFadeTime = 0.5f;

        bool mShowingNow = false;

        public UIElement_WaveDisplay(Display pDisplay, GameWorld pGameWorld, SpriteFont pFont, int pPosX, int pPosY, Color pTextColor)
            : base(pDisplay, pGameWorld, "", pPosX, pPosY, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            mFont = pFont;
            mColor = pTextColor;

            mElapsedTime = 0.0f;

            Hide();
        }

        public override void Show()
        {
            mIsShowing = true;
            aaDisplay.ShowUIElement(this);
            mShowingNow = true;
            mElapsedTime = 0.0f;
        }

        public override void Hide()
        {
            mIsShowing = false;
            mShowingNow = false;
            aaDisplay.HideUIElement(this);
        }

        public override void Update(float pDT)
        {
            base.Update(pDT);

            if (mShowingNow)
            {
                mElapsedTime += pDT;

                if (mElapsedTime < mSolidTime)
                {
                    mOpacity = 1.0f;
                }
                else if (mElapsedTime <= mSolidTime + mFadeTime)
                {
                    mOpacity = 1 - ((mElapsedTime - mSolidTime) / mFadeTime);
                }
                else
                {
                    Hide();
                }
            }
        }

        public override void Draw(SpriteBatch pSBatch)
        {
            if (mShowingNow)
            {
                String scoreString = "Wave " + aaGameWorld.mCurrentWave;
                pSBatch.DrawString(mFont, scoreString, new Vector2(posX, posY), mColor * mOpacity);
            }
        }
    }

    class UIElement_Text : UIElement
    {
        SpriteFont mFont;
        Color mColor;
        public string mText;
        public Vector2 mPos;

        public UIElement_Text(Display pDisplay, GameWorld pGameWorld, SpriteFont pFont, int pPosX, int pPosY, Color pTextColor, string pText)
            : base(pDisplay, pGameWorld, "", pPosX, pPosY, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            mFont = pFont;
            mColor = pTextColor;
            mText = pText;
            mPos = new Vector2(pPosX, pPosY);
        }

        public override void Draw(SpriteBatch pSBatch)
        {
            pSBatch.DrawString(mFont, mText, mPos, mColor, 0.0f, new Vector2(0,0), 1.0f, SpriteEffects.None, Constants.LAYER_UI_FRONT);
        }
    }

    class UIElement_ScoreTextBox : UIElement
    {
        private SpriteFont mFont;
        private Color mColor;
        private UIElement_StaticMenuElement mTextBox;
        private UIElement_StaticMenuElement mNameLabel;
        private UIElement_MenuButton mAcceptButton;
        private UIElement_MenuButton mCancelButton;
        private bool mAcceptPressed;
        private bool mCancelPressed;
        private const int mTextBoxXOffset = 0;
        private const int mTextBoxYOffset = 40;
        private const int mNameLabelXOffset = 150;
        private const int mNameLabelYOffset = -40;
        private const int mAcceptButtonXOffset = 0;
        private const int mAcceptButtonYOffset = 120;
        private const int mCancelButtonXOffset = 400;
        private const int mCancelButtonYOffset = 120;
        private const int mTextBoxTextXOffset = 20;
        private const int mTextBoxTextYOffset = 65;
        private const int mMaximumLength = 14;

        private static Dictionary<Keys, char> characterByKey;
        private KeyboardState mPreviousKeyState;
        private StringBuilder mTextBoxText;

        public UIElement_ScoreTextBox(Display pDisplay, GameWorld pGameWorld, int pPosX, int pPosY)
            : base(pDisplay, pGameWorld, "", pPosX, pPosY, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            mAcceptPressed = false;
            mCancelPressed = false;
            mFont = pDisplay.aaGame.mTextboxFont;
            mColor = Color.White;
            mTextBox = new UIElement_StaticMenuElement(pDisplay, pGameWorld, "Menu_MASTER", pPosX + mTextBoxXOffset, pPosY + mTextBoxYOffset, 0, 0, Constants.LAYER_UI_FRONT, 0, 197, 301, 39, 0);
            mNameLabel = new UIElement_StaticMenuElement(pDisplay, pGameWorld, "Menu_MASTER", pPosX + mNameLabelXOffset, pPosY + mNameLabelYOffset, 0, 0, Constants.LAYER_UI_FRONT, 302, 197, 150, 39, 0);
            mAcceptButton = new UIElement_MenuButton(pDisplay, pGameWorld, "Menu_MASTER", pPosX + mAcceptButtonXOffset, pPosY + mAcceptButtonYOffset, 0, 0, Constants.LAYER_UI_FRONT, 0, 157, 100, 39, 0);
            mCancelButton = new UIElement_MenuButton(pDisplay, pGameWorld, "Menu_MASTER", pPosX + mCancelButtonXOffset, pPosY + mCancelButtonYOffset, 0, 0, Constants.LAYER_UI_FRONT, 201, 157, 100, 39, 0);

            // Initialize the key dictionary
            characterByKey = new Dictionary<Keys, char>()
            {
                {Keys.A, 'a'},
                {Keys.B, 'b'},
                {Keys.C, 'c'},
                {Keys.D, 'd'},
                {Keys.E, 'e'},
                {Keys.F, 'f'},
                {Keys.G, 'g'},
                {Keys.H, 'h'},
                {Keys.I, 'i'},
                {Keys.J, 'j'},
                {Keys.K, 'k'},
                {Keys.L, 'l'},
                {Keys.M, 'm'},
                {Keys.N, 'n'},
                {Keys.O, 'o'},
                {Keys.P, 'p'},
                {Keys.Q, 'q'},
                {Keys.R, 'r'},
                {Keys.S, 's'},
                {Keys.T, 't'},
                {Keys.U, 'u'},
                {Keys.V, 'v'},
                {Keys.W, 'w'},
                {Keys.X, 'x'},
                {Keys.Y, 'y'},
                {Keys.Z, 'z'},
                {Keys.D0, '0'},
                {Keys.D1, '1'},
                {Keys.D2, '2'},
                {Keys.D3, '3'},
                {Keys.D4, '4'},
                {Keys.D5, '5'},
                {Keys.D6, '6'},
                {Keys.D7, '7'},
                {Keys.D8, '8'},
                {Keys.D9, '9'},
                {Keys.NumPad0, '0'},
                {Keys.NumPad1, '1'},
                {Keys.NumPad2, '2'},
                {Keys.NumPad3, '3'},
                {Keys.NumPad4, '4'},
                {Keys.NumPad5, '5'},
                {Keys.NumPad6, '6'},
                {Keys.NumPad7, '7'},
                {Keys.NumPad8, '8'},
                {Keys.NumPad9, '9'},
                {Keys.OemPeriod, '.'},
                {Keys.OemMinus, '-'},
                {Keys.Space, ' '}
            };
            mTextBoxText = new StringBuilder();
            mPreviousKeyState = Keyboard.GetState();
        }

        public override void Update(float pDT)
        {
            base.Update(pDT);

            mTextBox.Update(pDT);
            mNameLabel.Update(pDT);
            mAcceptButton.Update(pDT);
            mCancelButton.Update(pDT);

            if (mAcceptButton.CheckButtonClick())
                mAcceptPressed = true;
            if (mCancelButton.CheckButtonClick())
                mCancelPressed = true;

            // Update the TextBox's text
            KeyboardState currentKeyState = Keyboard.GetState();

            foreach (Keys key in currentKeyState.GetPressedKeys())
            {
                if (!mPreviousKeyState.IsKeyUp(key))
                {
                    continue;
                }

                if (key == Keys.Delete || key == Keys.Back)
                {
                    if (mTextBoxText.Length == 0)
                    {
                        continue;
                    }
                    mTextBoxText.Length--;
                    continue;
                }

                if (mTextBoxText.Length >= mMaximumLength)
                {
                    continue;
                }

                char characterPressed;
                if (!characterByKey.TryGetValue(key, out characterPressed))
                {
                    continue;
                }

                if (currentKeyState.IsKeyDown(Keys.LeftShift) || currentKeyState.IsKeyDown(Keys.RightShift))
                {
                    characterPressed = Char.ToUpper(characterPressed);
                }
                mTextBoxText.Append(characterPressed);
            }
            mPreviousKeyState = currentKeyState;
        }

        public override void Show()
        {
            mIsShowing = true;
            aaDisplay.AddMenuUIElement(this);
        }


        public override void Hide()
        {
            mIsShowing = false;
            aaDisplay.HideMenuUIElement(this);
        }

        public override void Destroy()
        {
            base.Destroy();

            this.Hide();
        }

        public bool AcceptClicked()
        {
            return mAcceptPressed;
        }

        public bool CancelClicked()
        {
            return mCancelPressed;
        }

        public string GetText()
        {
            return mTextBoxText.ToString();
        }

        public override void Draw(SpriteBatch pSBatch)
        {
            pSBatch.DrawString(aaDisplay.aaGame.mTextboxFont, mTextBoxText.ToString(), new Vector2(posX + mTextBoxTextXOffset, posY + mTextBoxTextYOffset), Color.White);

            mTextBox.Draw(pSBatch);
            mAcceptButton.Draw(pSBatch);
            mCancelButton.Draw(pSBatch);
            mNameLabel.Draw(pSBatch);
        }
    }
}
