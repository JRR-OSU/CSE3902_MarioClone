using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface ITile
    {
        Rectangle Properties { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState();
        int GetState();
        void OnCollisionResponse(IPlayer Mario, CollisionSide side);
        void OnCollisionResponse(IEnemy enemy, CollisionSide side);
        void OnCollisionResponse(IItem Item, CollisionSide side);
    }
}
