using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Lasagna
{
    public class AnimatedSprite : NonAnimatedSprite
    {
        private int sourceSpriteSheetColumns,
            sourceSpriteSheetRows,
            animationFPS;

        private int currentFrame = 0;
        private int totalFrames;
        private float frameTime;

        public AnimatedSprite(Texture2D spriteSheet, int spriteSheetColumns, int spriteSheetRows, int spriteScreenXSize, int spriteScreenYSize, int spriteAnimationFPS)
            : base(spriteSheet, spriteScreenXSize, spriteScreenYSize)
        {
            sourceSpriteSheetColumns = spriteSheetColumns;
            sourceSpriteSheetRows = spriteSheetRows;
            animationFPS = spriteAnimationFPS;
            
            if (sourceRectangle == null)
                sourceRectangle = new Rectangle();

            //Calculate an individual sprite size on the source sheet based on our number of rows and columns
            sourceRectangle.Width = sourceSpriteSheet.Width / sourceSpriteSheetColumns;
            sourceRectangle.Height = sourceSpriteSheet.Height / sourceSpriteSheetRows;

            totalFrames = spriteSheetColumns * spriteSheetRows;
        }
        
        public override void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (gameTime == null)
            {
                Debug.WriteLine("Invalid params passed for update on AnimatedSprite.");
                return;
            }

            //Perform base updating for non-animated sprite.
            base.Update(gameTime, spriteXPos, spriteYPos);

            //Perform spritesheet animation calculations.
            frameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds * animationFPS);
            if (frameTime >= 1.0f)
            {
                frameTime = 0.0f;
                currentFrame++;
                if (currentFrame >= totalFrames)
                    currentFrame = 0;

                if (sourceRectangle == null)
                    sourceRectangle = new Rectangle();

                //Calculate new frame source position
                int row = (int)((float)currentFrame / (float)sourceSpriteSheetColumns);
                int column = currentFrame % sourceSpriteSheetColumns;
                sourceRectangle.X = sourceRectangle.Width * column;
                sourceRectangle.Y = sourceRectangle.Height * row;
            }
        }
    }
}
