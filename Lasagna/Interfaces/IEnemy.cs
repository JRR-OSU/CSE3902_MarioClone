using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IEnemy
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState(EnemyState newState);
        void Damage();
        Rectangle GetRectangle();
        void OnCollisionResponse(IProjectile fireball, CollisionSide side);
        void OnCollisionResponse(IItem item, CollisionSide side);
        void OnCollisionResponse(IPlayer mario, CollisionSide side);
        void OnCollisionResponse(IEnemy otherEnemy, CollisionSide side);
        void OnCollisionResponse(ITile tile, CollisionSide side);
    }
}
