using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lasagna
{
    public class EdgeControlledCamera : ICamera
    {
        private Matrix transform;
        private float screenXPos;
        private float screenYPos;

        public EdgeControlledCamera (float startXPos, float startYPos)
        {
            screenXPos = startXPos;
            screenYPos = startYPos;
        }

        public Matrix Transform { get { return transform; } }

        public void Update(List<IPlayer> players, float screenWidth, float screenHeight)
        {
            //Calculate screen edge positions
            float rightEdge = screenXPos + screenWidth,
                bottomEdge = screenYPos + screenHeight;

            //Edge controlled camera moves the camera when any player touches the edge of the screen.
            foreach (IPlayer pl in players)
            {
                Rectangle bounds = pl.Bounds;
                //Left edge
                if (bounds.X < screenXPos)
                    screenXPos -= (screenXPos - bounds.X);
                //Right edge
                else if (bounds.X + bounds.Width > rightEdge)
                    screenXPos += (bounds.X + bounds.Width - rightEdge);
                //Top edge
                if (bounds.Y < screenYPos)
                    screenYPos -= (screenYPos - bounds.Y);
                //Bottom edge
                else if (bounds.Y + bounds.Height > bottomEdge)
                    screenYPos += (bounds.Y + bounds.Height - bottomEdge);
            }

            //Update our camera view matrix
            transform = Matrix.CreateTranslation(-screenXPos, -screenYPos, 0);
        }
    }
}
