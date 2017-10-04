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

        public void Update()
        {
            if (!enabled)
                return;

            MouseState currentState = Mouse.GetState();


        }

        public void ToggleMouseController(object sender, EventArgs e)
        {
            enabled = !enabled;
        }
    }
}
