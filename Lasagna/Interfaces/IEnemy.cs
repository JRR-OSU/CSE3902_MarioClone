using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IEnemy : ICollider
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState(EnemyState newState);
        void Damage();
        Rectangle GetRectangle { get; }
    }
}
