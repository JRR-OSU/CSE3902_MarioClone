using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class LifeMushroomItem : IItem
    {
        private ISprite powerUpMushroom = ItemSpriteFactory.Instance.CreateSprite_PowerupMushroom();
        private int posX;
        private int posY;
        public LifeMushroomItem(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
        public void Update(GameTime gameTime)
        {
            
            powerUpMushroom.Update(gameTime, this.posX, this.posY);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            powerUpMushroom.Draw(spriteBatch);
        }
    }
}
