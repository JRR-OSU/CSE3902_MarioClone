using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class FixedCamera : ICamera
    {
        private const int Two = 2;
        private const int Zero = 0;

        private Matrix transform;
        private float screenXPos;
        private float screenYPos;
        private float originXPos;
        private float originYPos;
        private float rightEdge;
        private float bottomEdge;

        public FixedCamera (float startXPos, float startYPos)
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

            //Update our camera view matrix
            UpdateViewMatrix();
        }

        private void UpdateViewMatrix()
        {
            //Update our camera view matrix
            transform = Matrix.CreateTranslation(-screenXPos, -screenYPos, Zero);
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
            UpdateViewMatrix();
        }
    }
}
