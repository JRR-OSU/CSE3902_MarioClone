using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    ///DEPRECATED: Currently not supported
    /*public class GamepadController : IController
    {
        //Used for determining if key was just pressed this frame
        private GamePadState oldGamepadState;

        public void Update()
        {
            GamePadState newGamepadState = GamePad.GetState(PlayerIndex.One);

            //Check each key if it was pressed this frame, execute command if it was.
            if (oldGamepadState.Buttons.Start == ButtonState.Released && newGamepadState.Buttons.Start == ButtonState.Pressed)
                Quit();
            if (oldGamepadState.Buttons.A == ButtonState.Released && newGamepadState.Buttons.A == ButtonState.Pressed)
                SelectOneFrameFixedPosition();
            if (oldGamepadState.Buttons.B == ButtonState.Released && newGamepadState.Buttons.B == ButtonState.Pressed)
                SelectAnimatedFixedPosition();
            if (oldGamepadState.Buttons.X == ButtonState.Released && newGamepadState.Buttons.X == ButtonState.Pressed)
                SelectOneFrameMoving();
            if (oldGamepadState.Buttons.Y == ButtonState.Released && newGamepadState.Buttons.Y == ButtonState.Pressed)
                SelectAnimatedMoving();

            oldGamepadState = newGamepadState;
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
    }*/
}
