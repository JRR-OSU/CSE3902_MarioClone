using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna.Items
{
    class CoinItem
    {
        public void CreateOneCoin(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y)
        {
            ISprite rotatedCoin = ItemSpriteFactory.Instance.CreateSprite_Coin();
            rotatedCoin.Update(gameTime, X, Y);
            rotatedCoin.Draw(spriteBatch);
        }
    }
}
