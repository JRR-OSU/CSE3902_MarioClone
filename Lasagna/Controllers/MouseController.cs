using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    //This doesn't inherit from IController because it is so vastly different from other controllers.
    public class MouseController
    {
        private bool enabled;

        public MouseController()
        {
            enabled = false;
            MarioEvents.OnToggleMouseController += ToggleMouseController;
        }

        public void Update(IPlayer player)
        {
            if (!enabled || player == null)
                return;

            CollisionSide side;
            MouseState currentState = Mouse.GetState();
            Rectangle playerRect = player.GetRect;
            playerRect.X = currentState.X;
            playerRect.Y = currentState.Y;

            if (CollisionDetection.Instance.CheckRectForCollisions(playerRect, out side))
            {

            }
            else 
                player.SetPosition(playerRect.X, playerRect.Y);
        }

        public void ToggleMouseController(object sender, EventArgs e)
        {
            enabled = !enabled;
        }
    }
}
