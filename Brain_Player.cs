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

namespace SpaceWars
{
    class Brain_Player : Brain
    {
        public MouseState mInput_Mouse;

        public KeyboardState mInput;
        public KeyboardState mInputPrev;

        public Ship mShip;
        public string mShipType;

        public int mShootTracker;
        public int mShootTrackerMax = 8;

        public Brain_Player(Ship pEntity, string pType)
            : base(pEntity)
        {
            mShip = pEntity;
            mShipType = pType;
            UpdateInput();
        }

        public override void UpdateBrain(float DT)
        {
            UpdateInput();

            HandleMovement();

            mInputPrev = mInput;
        }

        public void HandleMovement()
        {
            if (mInput.IsKeyDown(Keys.W))
            {
                (mShip).ActivateAbility(AbilityType.ThrustShip);
            }
            if (mInput.IsKeyDown(Keys.S))
            {
                (mShip).ActivateAbility(AbilityType.SlowShip);
            }
            if (mInput.IsKeyDown(Keys.A))
            {
                (mShip).ActivateAbility(AbilityType.TurnLeft);
            }
            if (mInput.IsKeyDown(Keys.D))
            {
                (mShip).ActivateAbility(AbilityType.TurnRight);
            }
            if (mInput_Mouse.RightButton.Equals(ButtonState.Pressed))
            {
                mShip.ActivateAbility(AbilityType.PlayerAbility1);
            }
            if (mInput.IsKeyDown(Keys.E))
            {
                mShip.ActivateAbility(AbilityType.PlayerAbility3);
            }
            if (mInput.IsKeyDown(Keys.Q))
            {
                mShip.ActivateAbility(AbilityType.PlayerAbility2);
            }
            if (mInput_Mouse.LeftButton.Equals(ButtonState.Pressed) && mShootTracker == mShootTrackerMax)
            {
                mShip.ActivateAbility(AbilityType.FireBullet, GetAngleToMouse());
                mShootTracker = 0;
            }
            if (mInput.IsKeyDown(Keys.Space))
            {
                mShip.ActivateAbility(AbilityType.PlayerAbility4);
            }

            mShootTracker++;
            if (mShootTracker > mShootTrackerMax) { mShootTracker = mShootTrackerMax; }
        }

        public float GetAngleToMouse()
        {
            return (float)Math.Atan2((mInput_Mouse.Y + mShip.aaGameWorld.aaDisplay.aaCamera.mYPos) - mShip.mYPos, (mInput_Mouse.X + mShip.aaGameWorld.aaDisplay.aaCamera.mXPos) - mShip.mXPos);
        }

        private void UpdateInput()
        {
            mInput = Keyboard.GetState();
            mInput_Mouse = Mouse.GetState();
        }
    }
}
