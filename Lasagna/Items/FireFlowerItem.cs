using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna.Items
{
    class FireFlowerItem
    {
        public void CreateOneFlower(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y)
        {
            ISprite fireFlower = ItemSpriteFactory.Instance.CreateSprite_FireFlower();
            fireFlower.Update(gameTime, X, Y);
            fireFlower.Draw(spriteBatch);
        }
    }
}
