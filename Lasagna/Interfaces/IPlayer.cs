using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IPlayer
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        Rectangle GetRect { get; }
    }
}
