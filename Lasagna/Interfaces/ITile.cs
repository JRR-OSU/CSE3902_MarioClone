using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface ITile : ICollider
    {
        void Update(GameTime gameTime);
        //Unique update method for the invisible block.
        void Draw(SpriteBatch spriteBatch);
        void ChangeState();
    }
}
