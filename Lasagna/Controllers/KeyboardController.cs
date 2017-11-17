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
                { new [] { Keys.X }, P1_ShootFire },
                { new [] { Keys.RightControl }, P2_ShootFire },
                { new [] { Keys.Enter }, Pause }
            };

            onKeyHeldEvents = new Dictionary<Keys[], MarioEventHandler>
            {
                { new [] { Keys.W }, P1_Jump },
                { new [] { Keys.S }, P1_Crouch },
                { new [] { Keys.A }, P1_MoveLeft },
                { new [] { Keys.D }, P1_MoveRight },
                { new [] { Keys.Up }, P2_Jump },
                { new [] { Keys.Down }, P2_Crouch },
                { new [] { Keys.Left }, P2_MoveLeft },
                { new [] { Keys.Right }, P2_MoveRight },
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
            Score.Lives = 3;
        }

        public void Pause()
        {
            MarioEvents.Pause(this, EventArgs.Empty);
        }

        /*public  void ToggleMouseController()
        {
            MarioEvents.ToggleMouseController(this, EventArgs.Empty);
        }*/

        public void P1_MoveLeft()
        {
            MarioEvents.P1_MoveLeft(this, EventArgs.Empty);
        }

        public  void P1_MoveRight()
        {
            MarioEvents.P1_MoveRight(this, EventArgs.Empty);
        }

        public  void P1_Jump()
        {
            MarioEvents.P1_Jump(this, EventArgs.Empty);
        }

        public  void P1_Crouch()
        {
            MarioEvents.P1_Crouch(this, EventArgs.Empty);
        }

        public void P1_ShootFire()
        {
            MarioEvents.P1_ShootFire(this, EventArgs.Empty);
        }

        public void P2_MoveLeft()
        {
            MarioEvents.P2_MoveLeft(this, EventArgs.Empty);
        }

        public void P2_MoveRight()
        {
            MarioEvents.P2_MoveRight(this, EventArgs.Empty);
        }

        public void P2_Jump()
        {
            MarioEvents.P2_Jump(this, EventArgs.Empty);
        }

        public void P2_Crouch()
        {
            MarioEvents.P2_Crouch(this, EventArgs.Empty);
        }

        public void P2_ShootFire()
        {
            MarioEvents.P2_ShootFire(this, EventArgs.Empty);
        }
    }
}
