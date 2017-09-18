using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class FireFlowerItem
    {
        ISprite fireFlower = ItemSpriteFactory.Instance.CreateSprite_FireFlower();
        public void Update(GameTime gameTime, int X, int Y)
        {
            
            fireFlower.Update(gameTime, X, Y);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            fireFlower.Draw(spriteBatch);
        }
    }
}
