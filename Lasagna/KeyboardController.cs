using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class KeyboardController : IController
    {
        public KeyboardController()
        {
            //Setup all key events we accept
            keyEvents = new Dictionary<Keys, MarioEventHandler>
            {
                { Keys.Q, Quit },
                { Keys.R, Reset },
                { Keys.Up, Jump },
                { Keys.W, Jump },
                { Keys.Down, Crouch },
                { Keys.S, Crouch },
                { Keys.Left, MoveLeft },
                { Keys.A, MoveLeft },
                { Keys.Right, MoveRight },
                { Keys.D, MoveRight },
                { Keys.M, EnableMouseController },

                { Keys.Y, MarioDamage },
                { Keys.O, MarioDie },
                { Keys.U, GetMushroom },
                { Keys.I, GetFireFlower },
                { Keys.Z, UseQuestionBlock },
                { Keys.X, DestroyBrickBlock },
                { Keys.C, UseHiddenBlock },
            };
        }

        //Keys and what event they trigger
        private Dictionary<Keys, MarioEventHandler> keyEvents;
        //Used for determining if key was just pressed this frame
        private KeyboardState oldKeyboardState;

        public void Update()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            //Iterate over all keys, if they are pressed down this frame raise event.
            foreach (KeyValuePair<Keys, MarioEventHandler> keyPair in keyEvents)
            {
                if (keyPair.Value != null && oldKeyboardState.IsKeyUp(keyPair.Key) && newKeyboardState.IsKeyDown(keyPair.Key))
                    keyPair.Value();
            }

            oldKeyboardState = newKeyboardState;
        }

        public void Quit()
        {
            MarioEvents.Quit(this, EventArgs.Empty);
        }

        public void Reset()
        {
            MarioEvents.Reset(this, EventArgs.Empty);
        }

        public void MoveLeft()
        {
            MarioEvents.MoveLeft(this, EventArgs.Empty);
        }

        public void MoveRight()
        {
            MarioEvents.MoveRight(this, EventArgs.Empty);
        }

        public void Jump()
        {
            MarioEvents.Jump(this, EventArgs.Empty);
        }

        public void Crouch()
        {
            MarioEvents.Crouch(this, EventArgs.Empty);
        }

        public void Fire()
        {
            MarioEvents.Fire(this, EventArgs.Empty);
        }

        public void MarioDamage()
        {
            MarioEvents.MarioDamage(this, EventArgs.Empty);
        }

        public void MarioDie()
        {
            MarioEvents.MarioDie(this, EventArgs.Empty);
        }

        public void GetMushroom()
        {
            MarioEvents.GetMushroom(this, EventArgs.Empty);
        }

        public void GetFireFlower()
        {
            MarioEvents.GetFireFlower(this, EventArgs.Empty);
        }

        public void UseQuestionBlock()
        {
            MarioEvents.UseQuestionBlock(this, EventArgs.Empty);
        }

        public void DestroyBrickBlock()
        {
            MarioEvents.DestroyBrickBlock(this, EventArgs.Empty);
        }

        public void UseHiddenBlock()
        {
            MarioEvents.UseHiddenBlock(this, EventArgs.Empty);
        }
        public void EnableMouseController()
        {
            //Wait for mouse controller
        }
    }
}
