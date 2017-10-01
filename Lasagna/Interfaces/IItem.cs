using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IItem
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        Rectangle GetRectangle();
    }
}
