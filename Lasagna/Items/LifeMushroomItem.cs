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
        ISprite powerUpMushroom = ItemSpriteFactory.Instance.CreateSprite_PowerupMushroom();
        public void Update(GameTime gameTime, int X, int Y)
        {
            
            powerUpMushroom.Update(gameTime, X, Y);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            powerUpMushroom.Draw(spriteBatch);
        }
    }
}
