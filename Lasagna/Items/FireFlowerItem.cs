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
