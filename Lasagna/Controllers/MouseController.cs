using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public void Update(IPlayer player, ReadOnlyCollection<IEnemy> enemies, ReadOnlyCollection<ITile> tiles)
        {
            if (!enabled || player == null)
                return;
            
            MouseState currentState = Mouse.GetState();
            Rectangle playerRect = player.Bounds;
            playerRect.X = currentState.X;
            playerRect.Y = currentState.Y;

            if (!player.IsDead && !CollisionDetection.CheckRectForCollisions(player, playerRect, enemies, tiles))
                player.SetPosition(playerRect.X, playerRect.Y);
        }

        public void ToggleMouseController(object sender, EventArgs e)
        {
            enabled = !enabled;
        }
    }
}
