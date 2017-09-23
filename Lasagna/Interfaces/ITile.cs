using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    interface ITile
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState();
    }
}
