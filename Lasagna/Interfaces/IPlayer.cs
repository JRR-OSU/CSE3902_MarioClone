using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IPlayer : ICollider
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void SetPosition(int x, int y);

        Rectangle GetRect { get; }
        bool IsDead { get; }
    }
}
