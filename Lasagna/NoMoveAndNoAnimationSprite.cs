using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sprint0
{
    class NoMoveAndNoAnimationSprite : ISprite
    {
        private const int spriteWidth = 32, spriteHeight = 64;
        private Texture2D marioStanding;

        //Nothing here because sprite doesn't have anything to reset
        public void ResetSprite(int screenWidth, int screenHeight) { }

        public void LoadContent(MarioGame master)
        {
            if (master == null)
            {
                Debug.WriteLine("Null reference to master passed for loading content on NoMoveAndNoAnimationSprite.");
                return;
            }

            marioStanding = master.Content.Load<Texture2D>("MarioStanding");
        }

        //Nothing here because sprite doesn't animate or move
        public void Update(GameTime gameTime, int screenWidth, int screenHeight) {}

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            if (spriteBatch == null || screenWidth < 0 || screenHeight < 0)
            {
                Debug.WriteLine("Invalid params passed for drawing on NoMoveAndNoAnimationSprite.");
                return;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(marioStanding, new Rectangle((screenWidth - spriteWidth) / 2, (screenHeight - spriteHeight) / 2, spriteWidth, spriteHeight), Color.White);
            spriteBatch.End();
        }
    }
}
