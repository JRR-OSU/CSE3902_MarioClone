using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Lasagna
{
    public class KeyboardController : IController
    {
        public KeyboardController()
        {
            //Setup all key events we accept
            keyEvents = new Dictionary<Keys, InputEventHandler>
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
        private Dictionary<Keys, InputEventHandler> keyEvents;
        //Used for determining if key was just pressed this frame
        private KeyboardState oldKeyboardState;

        public void Update()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            //Iterate over all keys, if they are pressed down this frame raise event.
            foreach (KeyValuePair<Keys, InputEventHandler> keyPair in keyEvents)
            {
                if (keyPair.Value != null && oldKeyboardState.IsKeyUp(keyPair.Key) && newKeyboardState.IsKeyDown(keyPair.Key))
                    keyPair.Value();
            }

            oldKeyboardState = newKeyboardState;
        }

        public void Quit()
        {
            MarioEvents.Quit();
        }

        public void Reset()
        {
            MarioEvents.Reset();
        }

        public void MoveLeft()
        {
            MarioEvents.MoveLeft();
        }

        public void MoveRight()
        {
            MarioEvents.MoveRight();
        }

        public void Jump()
        {
            MarioEvents.Jump();
        }

        public void Crouch()
        {
            MarioEvents.Crouch();
        }

        public void Fire()
        {
            MarioEvents.Fire();
        }

        public void MarioDamage()
        {
            MarioEvents.MarioDamage();
        }

        public void MarioDie()
        {
            MarioEvents.MarioDie();
        }

        public void GetMushroom()
        {
            MarioEvents.GetMushroom();
        }

        public void GetFireFlower()
        {
            MarioEvents.GetFireFlower();
        }

        public void UseQuestionBlock()
        {
            MarioEvents.UseQuestionBlock();
        }

        public void DestroyBrickBlock()
        {
            MarioEvents.DestroyBrickBlock();
        }

        public void UseHiddenBlock()
        {
            MarioEvents.UseHiddenBlock();
        }
    }
}
