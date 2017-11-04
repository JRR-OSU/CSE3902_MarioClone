using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Lasagna
{
    public class AnimatedSprite : NonAnimatedSprite
    {
        private const string drawError = "Invalid params passed for update on AnimatedSprite.";
        private const int Zero = 0;
        private const int One = 1;

        private int sourceSpriteSheetColumns,
            sourceSpriteSheetRows,
            animationFPS;

        private int currentFrame = Zero;
        private int totalFrames;
        private float frameTime;

        public override float ClipLength
        {
            get
            {
                return (sourceSpriteSheetColumns * sourceSpriteSheetRows) / (float)animationFPS;
            }
        }

        public AnimatedSprite(Texture2D spriteSheet, SpriteSheetInfo info)
            : base(spriteSheet, info)
        {
            sourceSpriteSheetColumns = info.Columns;
            sourceSpriteSheetRows = info.Rows;
            animationFPS = info.AnimationFPS;

            //Calculate an individual sprite size on the source sheet based on our number of rows and columns
            SourceRectangle = new Rectangle(SourceRectangle.X, SourceRectangle.Y, SourceSpriteSheet.Width / sourceSpriteSheetColumns, SourceSpriteSheet.Height / sourceSpriteSheetRows);

            totalFrames = info.Columns * info.Rows;
        }

        public override void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (gameTime == null)
            {
                Debug.WriteLine(drawError);
                return;
            }

            //Perform base updating for non-animated sprite.
            base.Update(gameTime, spriteXPos, spriteYPos);

            //Perform spritesheet animation calculations.
            frameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds * animationFPS);
            if (frameTime >= One)
            {
                frameTime = Zero;
                currentFrame++;
                if (currentFrame >= totalFrames)
                    currentFrame = Zero;

                //Calculate new frame source position
                int row = (int)((float)currentFrame / (float)sourceSpriteSheetColumns);
                int column = currentFrame % sourceSpriteSheetColumns;
                SourceRectangle = new Rectangle(SourceRectangle.Width * column, SourceRectangle.Height * row, SourceRectangle.Width, SourceRectangle.Height);
            }
        }
    }
}
