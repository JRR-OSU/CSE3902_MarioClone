using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IProjectile
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState();
        void OnCollisionResponse(IEnemy Enemy, CollisionSide side);
        void OnCollisionResponse(IItem Item, CollisionSide side);
    }
}
