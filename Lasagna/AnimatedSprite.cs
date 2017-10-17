using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

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

        public override float ClipLength
        {
            get
            {
                return (sourceSpriteSheetColumns * sourceSpriteSheetRows) / (float)animationFPS;
            }
        }

        public AnimatedSprite(Texture2D spriteSheet, int spriteSheetColumns, int spriteSheetRows, int spriteScreenXSize, int spriteScreenYSize, int spriteAnimationFPS)
            : base(spriteSheet, spriteScreenXSize, spriteScreenYSize)
        {
            sourceSpriteSheetColumns = spriteSheetColumns;
            sourceSpriteSheetRows = spriteSheetRows;
            animationFPS = spriteAnimationFPS;

            //Calculate an individual sprite size on the source sheet based on our number of rows and columns
            SourceRectangle = new Rectangle(SourceRectangle.X, SourceRectangle.Y, SourceSpriteSheet.Width / sourceSpriteSheetColumns, SourceSpriteSheet.Height / sourceSpriteSheetRows);

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

                //Calculate new frame source position
                int row = (int)((float)currentFrame / (float)sourceSpriteSheetColumns);
                int column = currentFrame % sourceSpriteSheetColumns;
                SourceRectangle = new Rectangle(SourceRectangle.Width * column, SourceRectangle.Height * row, SourceRectangle.Width, SourceRectangle.Height);
            }
        }
    }
}
