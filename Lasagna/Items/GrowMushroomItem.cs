using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna.Items
{
    class GrowMushroomItem
    {
        public void CreateOneGrowmushroom(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y)
        {
            ISprite upMushroom = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
            upMushroom.Update(gameTime, X, Y);
            upMushroom.Draw(spriteBatch);
        }
    }
}
