using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
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
