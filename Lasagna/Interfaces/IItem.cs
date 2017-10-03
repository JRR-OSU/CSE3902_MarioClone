using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IItem
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        Rectangle GetRectangle { get; }
        void OnCollisionResponse(IProjectile fireball, CollisionSide side);
        void OnCollisionResponse(IPlayer mario, CollisionSide side);
        void OnCollisionResponse(IEnemy otherEnemy, CollisionSide side);
        void OnCollisionResponse(ITile tile, CollisionSide side);
    }
}
