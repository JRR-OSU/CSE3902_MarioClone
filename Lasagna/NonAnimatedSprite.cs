using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    public class NonAnimatedSprite : ISprite
    {
        //Constant strings
        private const string DrawError = "Null sprite batch passed for drawing sprite!";
        //References to specific constant values
        private const int Zero = 0;
        private const int Two = 2;

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
                return (destinationRectangle != null) ? destinationRectangle.Height : Zero;
            }
        }
        public int Width
        {
            get
            {
                return (destinationRectangle != null) ? destinationRectangle.Width : Zero;
            }
        }

        public virtual float ClipLength
        {
            get
            {
                return Zero;
            }
        }

        public NonAnimatedSprite(Texture2D spriteSheet, SpriteSheetInfo info)
        {
            sourceSpriteSheet = spriteSheet;

            SetSpriteScreenSize(info.XSize, info.YSize);

            //Set our source rectangle size to be the whole sprite by default
            sourceRectangle = new Rectangle(Zero, Zero, spriteSheet.Width, spriteSheet.Height);
        }

        public virtual void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            SetSpriteScreenPosition(spriteXPos, spriteYPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Color.White);
        }
        
        public void Draw(SpriteBatch spriteBatch, Color spriteTint, float rotation = Zero)
        {
            if (spriteBatch == null)
            {
                Debug.WriteLine(DrawError);
                return;
            }

            if (destinationRectangle == null)
                destinationRectangle = new Rectangle();
            if (sourceRectangle == null)
                sourceRectangle = new Rectangle();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, MarioGame.Instance.CameraTransform);
            if (rotation != Zero)
            {
                //Rotation takes place along the top-left corner, offset sprite to account for this.
                Rectangle offsetRect = destinationRectangle;
                Vector2 origin = new Vector2(sourceSpriteSheet.Width / (float)Two, sourceSpriteSheet.Height / (float)Two);
                offsetRect.X += destinationRectangle.Width / Two;
                offsetRect.Y += destinationRectangle.Height / Two;

                spriteBatch.Draw(sourceSpriteSheet, offsetRect, sourceRectangle, spriteTint, rotation, origin, SpriteEffects.None, Zero);
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
