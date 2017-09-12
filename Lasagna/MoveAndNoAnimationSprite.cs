using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0
{
    class MoveAndNoAnimationSprite : ISprite
    {
        private const int spriteWidth = 64, spriteHeight = 32;
        private const float moveSpeedPixelsSecond = 70;

        private Texture2D marioDead;
        private float currentYPosition;
        private bool movingUp;

        public void ResetSprite(int screenWidth, int screenHeight)
        {
            if (screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for resetting sprite on MoveAndNoAnimationSprite.");
                return;
            }

            //Reset sprite to center of screen
            currentYPosition = (screenHeight - spriteHeight) / 2f;
            movingUp = true;
        }

        public void LoadContent(MarioGame master)
        {
            if (master == null)
            {
                Debug.WriteLine("Null reference to master passed for loading content on MoveAndNoAnimationSprite.");
                return;
            }

            marioDead = master.Content.Load<Texture2D>("MarioDead");
        }
        
        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            if (gameTime == null || screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for update on MoveAndNoAnimationSprite.");
                return;
            }

            //Move sprite up and down
            currentYPosition += (movingUp ? 1 : -1) * (float)(gameTime.ElapsedGameTime.TotalSeconds * moveSpeedPixelsSecond);

            //Change direction when we pass the middle 1/4th of the screen
            if (movingUp && currentYPosition > (screenHeight / 2) + ((screenHeight / 4) / 2))
                movingUp = false;
            else if (!movingUp && currentYPosition < (screenHeight / 2) - ((screenHeight / 4) / 2))
                movingUp = true;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            if (spriteBatch == null || screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for drawing on MoveAndNoAnimationSprite.");
                return;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(marioDead, new Rectangle((screenWidth - spriteWidth) / 2, (int)currentYPosition, spriteWidth, spriteHeight), Color.White);
            spriteBatch.End();
        }
    }
}
