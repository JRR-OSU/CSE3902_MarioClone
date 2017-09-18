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
        ISprite upMushroom = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        public void Update(GameTime gameTime, int X, int Y)
        {
            upMushroom.Update(gameTime, X, Y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            upMushroom.Draw(spriteBatch);
        }
    }
}
