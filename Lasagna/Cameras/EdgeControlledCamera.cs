using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class EdgeControlledCamera : ICamera
    {
        private Matrix transform;
        private float screenXPos;
        private float screenYPos;
        private float originXPos;
        private float originYPos;
        private float rightEdge;
        private float bottomEdge;

        public EdgeControlledCamera (float startXPos, float startYPos)
        {
            screenXPos = startXPos;
            screenYPos = startYPos;
            originXPos = startXPos;
            originYPos = startYPos;
            MarioEvents.OnReset += Reset;
        }

        public Matrix Transform { get { return transform; } }

        public void Update(List<IPlayer> players, float screenWidth, float screenHeight)
        {
            //Calculate screen edge positions
            rightEdge = screenXPos + screenWidth;
            bottomEdge = screenYPos + screenHeight;
            float horMiddle = screenXPos + (screenWidth / 2);

            foreach (IPlayer pl in players)
            {
                Rectangle bounds = pl.Bounds;
                //Left edge
                if (bounds.X < screenXPos)
                    pl.SetPosition((int)screenXPos, bounds.Y);
                //Right center
                else if (bounds.X + bounds.Width > horMiddle)
                    screenXPos += (bounds.X + bounds.Width - horMiddle);
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

        public void Reset(object sender, EventArgs e)
        {
            screenXPos = originXPos;
            screenYPos = originYPos;
        }

        public bool CanSeeCollider(ICollider col)
        {
            return col.Bounds.X + col.Bounds.Width >= screenXPos && col.Bounds.X <= rightEdge
                && col.Bounds.Y + col.Bounds.Height >= screenYPos && col.Bounds.Y <= bottomEdge;
        }

        public void ForcePosition(int xPos, int yPos)
        {
            screenXPos = xPos;
            screenYPos = yPos;
        }
    }
}
