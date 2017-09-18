using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class CoinItem
    {
        ISprite rotatedCoin = ItemSpriteFactory.Instance.CreateSprite_Coin();
        public void Update(GameTime gameTime, int X, int Y)
        {
            
            rotatedCoin.Update(gameTime, X, Y);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            rotatedCoin.Draw(spriteBatch);
        }
    }
}
