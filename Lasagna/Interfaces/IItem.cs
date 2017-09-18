using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    interface IItem
    {
        void Update(GameTime gameTime, int X, int Y);
        void Draw(SpriteBatch spriteBatch);
    }
}
