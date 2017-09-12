using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    public class KeyboardController : IController
    {
        //Used for determining if key was just pressed this frame
        private KeyboardState oldKeyboardState;

        public void Update()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            //Check each key if it was pressed this frame, execute command if it was.
            if (oldKeyboardState.IsKeyUp(Keys.Q) && newKeyboardState.IsKeyDown(Keys.Q))
                Quit();
            if (oldKeyboardState.IsKeyUp(Keys.W) && newKeyboardState.IsKeyDown(Keys.W))
                SelectOneFrameFixedPosition();
            if (oldKeyboardState.IsKeyUp(Keys.E) && newKeyboardState.IsKeyDown(Keys.E))
                SelectAnimatedFixedPosition();
            if (oldKeyboardState.IsKeyUp(Keys.R) && newKeyboardState.IsKeyDown(Keys.R))
                SelectOneFrameMoving();
            if (oldKeyboardState.IsKeyUp(Keys.T) && newKeyboardState.IsKeyDown(Keys.T))
                SelectAnimatedMoving();

            oldKeyboardState = newKeyboardState;
        }

        public void Quit()
        {
            MarioEvents.Quit();
        }

        public void SelectOneFrameFixedPosition()
        {
            MarioEvents.SelectNoMoveAndNoAnimation();
        }

        public void SelectAnimatedFixedPosition()
        {
            MarioEvents.SelectNoMoveAndAnimation();
        }

        public void SelectOneFrameMoving()
        {
            MarioEvents.SelectMoveAndNoAnimation();
        }

        public void SelectAnimatedMoving()
        {
            MarioEvents.SelectMoveAndAnimation();
        }
    }
}
