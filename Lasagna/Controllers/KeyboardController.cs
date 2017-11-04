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
            onKeyDownEvents = new Dictionary<Keys[], MarioEventHandler>
            {
                { new [] { Keys.Q }, Quit },
                { new [] { Keys.R }, Reset },
                //{ new [] { Keys.M }, ToggleMouseController },
                { new [] { Keys.X }, ShootFire },
                { new [] { Keys.Enter }, Pause }
            };

            onKeyHeldEvents = new Dictionary<Keys[], MarioEventHandler>
            {
                { new [] { Keys.Up, Keys.W }, Jump },
                { new [] { Keys.Down, Keys.S }, Crouch },
                { new [] { Keys.Left, Keys.A }, MoveLeft },
                { new [] { Keys.Right, Keys.D }, MoveRight },
            };
        }
        
        //Keys and what event they trigger
        private readonly Dictionary<Keys[], MarioEventHandler> onKeyDownEvents;
        private readonly Dictionary<Keys[], MarioEventHandler> onKeyHeldEvents;
        //Used for determining if key was just pressed this frame for keys that only get on down
        private KeyboardState oldKeyboardState;

        public void Update()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            //Iterate over all down keys, if they are pressed down this frame and not last frame raise event.
            foreach (KeyValuePair<Keys[], MarioEventHandler> keyPair in onKeyDownEvents)
            {
                foreach (Keys k in keyPair.Key)
                {
                    if (keyPair.Value != null && oldKeyboardState.IsKeyUp(k) && newKeyboardState.IsKeyDown(k))
                        keyPair.Value();
                }
            }
            //Iterate over all held keys, if they are pressed down this frame raise event.
            foreach (KeyValuePair<Keys[], MarioEventHandler> keyPair in onKeyHeldEvents)
            {
                foreach (Keys k in keyPair.Key)
                {
                    if (keyPair.Value != null && newKeyboardState.IsKeyDown(k))
                        keyPair.Value();
                }
            }

            oldKeyboardState = newKeyboardState;
        }

        public  void Quit()
        {
            MarioEvents.Quit(this, EventArgs.Empty);
        }

        public  void Reset()
        {
            MarioEvents.Reset(this, EventArgs.Empty);
        }

        public  void MoveLeft()
        {
            MarioEvents.MoveLeft(this, EventArgs.Empty);
        }

        public  void MoveRight()
        {
            MarioEvents.MoveRight(this, EventArgs.Empty);
        }

        public  void Jump()
        {
            MarioEvents.Jump(this, EventArgs.Empty);
        }

        public  void Crouch()
        {
            MarioEvents.Crouch(this, EventArgs.Empty);
        }

        /*public  void ToggleMouseController()
        {
            MarioEvents.ToggleMouseController(this, EventArgs.Empty);
        }*/

        public void ShootFire()
        {
            MarioEvents.ShootFire(this, EventArgs.Empty);
        }

        public void Pause()
        {
            MarioEvents.Pause(this, EventArgs.Empty);
        }
    }
}
