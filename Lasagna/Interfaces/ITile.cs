using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna.Interfaces
{
    interface ITile
    {
        void ChangeState();
        void Update(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos);
    }
}
