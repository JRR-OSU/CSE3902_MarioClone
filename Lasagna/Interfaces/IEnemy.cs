using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    interface IEnemy
    {
        void changeState();
        void Update(GameTime gameTime, int X, int Y);
        void Draw(SpriteBatch spriteBatch);
    }
}
