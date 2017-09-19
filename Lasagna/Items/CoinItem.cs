using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class CoinItem : IItem
    {
        private ISprite rotatedCoin = ItemSpriteFactory.Instance.CreateSprite_Coin();
        private int posX;
        private int posY;
        public CoinItem(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
        public void Update(GameTime gameTime)
        {
            
            this.rotatedCoin.Update(gameTime, this.posX, this.posY);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.rotatedCoin.Draw(spriteBatch);
        }
    }
}
