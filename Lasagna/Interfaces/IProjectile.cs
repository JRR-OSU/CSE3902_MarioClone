using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    interface IProjectile
    {
        void ChangeState();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
