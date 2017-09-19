using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    interface IEnemy
    {
        void changeLiveState();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
