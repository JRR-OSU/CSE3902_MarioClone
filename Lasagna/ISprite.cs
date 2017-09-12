using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    interface ISprite
    {
        void ResetSprite(int screenWidth, int screenHeight);
        void LoadContent(MarioGame master);
        void Update(GameTime gameTime, int screenWidth, int screenHeight);
        void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight);
    }
}
