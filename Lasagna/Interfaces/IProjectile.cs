using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    interface IProjectile
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState();
    }
}
