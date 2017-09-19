using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class FireFlowerItem :IItem
    {
        private ISprite fireFlower = ItemSpriteFactory.Instance.CreateSprite_FireFlower();
        private int posX;
        private int posY;
        public FireFlowerItem(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
        public void Update(GameTime gameTime)
        {
            
            fireFlower.Update(gameTime, this.posX, this.posY);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            fireFlower.Draw(spriteBatch);
        }
    }
}
