using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna.Items
{
    class LifeMushroomItem
    {
        public void CreateOneLifemushroom(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y)
        {
            ISprite powerUpMushroom = ItemSpriteFactory.Instance.CreateSprite_PowerupMushroom();
            powerUpMushroom.Update(gameTime, X, Y);
            powerUpMushroom.Draw(spriteBatch);
        }
    }
}
