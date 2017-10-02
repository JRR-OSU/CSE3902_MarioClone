using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IPlayer
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void OnCollisionResponse(IPlayer player, CollisionSide side);

        void OnCollisionResponse(IEnemy enemy, CollisionSide side);

        void OnCollisionResponse(IItem item, CollisionSide side);


        void OnCollisionResponse(ITile tile, CollisionSide side);

        Rectangle GetRect { get; }
    }
}
