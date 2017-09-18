using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class StarItem
    {
        ISprite powerupStar = ItemSpriteFactory.Instance.CreateSprite_Star();
        public void Update(GameTime gameTime, int X, int Y)
        {

            powerupStar.Update(gameTime, X, Y);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            powerupStar.Draw(spriteBatch);
        }
    }
}
