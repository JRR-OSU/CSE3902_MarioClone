using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class GrowMushroomItem : IItem
    {
        private ISprite upMushroom = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        private int posX;
        private int posY;
        public GrowMushroomItem(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
        public void Update(GameTime gameTime)
        {
            upMushroom.Update(gameTime, this.posX, this.posY);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            upMushroom.Draw(spriteBatch);
        }
    }
}
