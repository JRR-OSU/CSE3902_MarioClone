using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface ITile
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ChangeState();
        Rectangle GetProperties();
        void OnCollisionResponse(IPlayer Mario, CollisionSide side);
        void OnCollisionResponse(IItem Item, CollisionSide side);
    }
}
