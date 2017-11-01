using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class NonAnimatedSprite : ISprite
    {
        private Texture2D sourceSpriteSheet;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;

        protected Texture2D SourceSpriteSheet { get { return sourceSpriteSheet; } }
        protected Rectangle SourceRectangle
        {
            get
            {
                if (sourceRectangle == null)
                    sourceRectangle = new Rectangle();

                return sourceRectangle;
            }
            set { sourceRectangle = value; }
        }
        protected Rectangle DestinationRectangle { get { return destinationRectangle; } }

        public int Height
        {
            get
            {
                return (destinationRectangle != null) ? destinationRectangle.Height : 0;
            }
        }
        public int Width
        {
            get
            {
                return (destinationRectangle != null) ? destinationRectangle.Width : 0;
            }
        }

        public virtual float ClipLength
        {
            get
            {
                return 0;
            }
        }

        public NonAnimatedSprite(Texture2D spriteSheet, int spriteScreenXSize, int spriteScreenYSize)
        {
            sourceSpriteSheet = spriteSheet;

            SetSpriteScreenSize(spriteScreenXSize, spriteScreenYSize);

            //Set our source rectangle size to be the whole sprite by default
            sourceRectangle = new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height);
        }

        public virtual void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            SetSpriteScreenPosition(spriteXPos, spriteYPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Color.White);
        }
        
        public void Draw(SpriteBatch spriteBatch, Color spriteTint, float rotation = 0)
        {
            if (spriteBatch == null)
            {
                Debug.WriteLine("Null sprite batch passed for drawing sprite!");
                return;
            }

            if (destinationRectangle == null)
                destinationRectangle = new Rectangle();
            if (sourceRectangle == null)
                sourceRectangle = new Rectangle();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, MarioGame.Instance.CameraTransform);
            if (rotation != 0)
            {
                //Rotation takes place along the top-left corner, offset sprite to account for this.
                Rectangle offsetRect = destinationRectangle;
                Vector2 origin = new Vector2(sourceSpriteSheet.Width / 2f, sourceSpriteSheet.Height / 2f);
                offsetRect.X += destinationRectangle.Width / 2;
                offsetRect.Y += destinationRectangle.Height / 2;

                spriteBatch.Draw(sourceSpriteSheet, offsetRect, sourceRectangle, spriteTint, rotation, origin, SpriteEffects.None, 0);
            }
            else
                spriteBatch.Draw(sourceSpriteSheet, destinationRectangle, sourceRectangle, spriteTint);
            
            spriteBatch.End();
        }

        public void SetSpriteScreenSize(int spriteXSize, int spriteYSize)
        {
            if (destinationRectangle == null)
                destinationRectangle = new Rectangle();

            destinationRectangle.Width = spriteXSize;
            destinationRectangle.Height = spriteYSize;
        }

        public void SetSpriteScreenPosition(int spriteXPos, int spriteYPos)
        {
            if (destinationRectangle == null)
                destinationRectangle = new Rectangle();

            destinationRectangle.X = spriteXPos;
            destinationRectangle.Y = spriteYPos;
        }
    }
}
