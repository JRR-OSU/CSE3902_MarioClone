using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0
{
    public class NoMoveAndAnimationSprite : ISprite
    {
        private const int spriteScreenDrawWidth = 32, spriteScreenDrawHeight = 64,
            sourceSpriteSheetColumns = 3,
            animationFPS = 10;

        private int spriteSourceWidth = 16, spriteSourceHeight = 32;
        private Texture2D runningAnimSheet;
        private int currentFrame;
        private float frameTime;
        
        public void ResetSprite(int screenWidth, int screenHeight)
        {
            if (screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for resetting sprite on NoMoveAndAnimationSprite.");
                return;
            }

            currentFrame = 0;
            frameTime = 0;
        }

        public void LoadContent(MarioGame master)
        {
            if (master == null)
            {
                Debug.WriteLine("Null reference to master passed for loading content on NoMoveAndAnimationSprite.");
                return;
            }

            runningAnimSheet = master.Content.Load<Texture2D>("MarioRunningRight");

            spriteSourceWidth = runningAnimSheet.Width / sourceSpriteSheetColumns;
            spriteSourceHeight = runningAnimSheet.Height;
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            if (gameTime == null || screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for update on NoMoveAndAnimationSprite.");
                return;
            }

            frameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds * animationFPS);
            if (frameTime >= 1.0f)
            {
                frameTime = 0.0f;
                currentFrame++;
                if (currentFrame >= sourceSpriteSheetColumns)
                    currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            if (spriteBatch == null || screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for drawing on NoMoveAndAnimationSprite.");
                return;
            }
            
            int column = currentFrame % sourceSpriteSheetColumns;

            Rectangle destinationRect = new Rectangle((screenWidth - spriteScreenDrawWidth) / 2, (screenHeight - spriteScreenDrawHeight) / 2, spriteScreenDrawWidth, spriteScreenDrawHeight);
            Rectangle sourceRect = new Rectangle(spriteSourceWidth * column, 0, spriteSourceWidth, spriteSourceHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(runningAnimSheet, destinationRect, sourceRect, Color.White);
            spriteBatch.End();
        }
    }
}
