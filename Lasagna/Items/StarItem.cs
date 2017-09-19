using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class StarItem :IItem
    {
        private ISprite powerupStar = ItemSpriteFactory.Instance.CreateSprite_Star();
        private int posX;
        private int posY;
        public StarItem(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
        public void Update(GameTime gameTime)
        {

            powerupStar.Update(gameTime, this.posX, this.posY);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            powerupStar.Draw(spriteBatch);
        }
    }
}
