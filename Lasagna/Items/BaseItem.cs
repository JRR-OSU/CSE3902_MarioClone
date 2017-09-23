using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public abstract class BaseItem : IItem
    {
        //Change this later if items support states.
        protected ISprite itemSprite;
        protected int posX;
        protected int posY;

        public BaseItem(int spawnPosX, int spawnPosY)
        {
            posX = spawnPosX;
            posY = spawnPosY;
        }

        public void Update(GameTime gameTime)
        {
            if (itemSprite != null)
                itemSprite.Update(gameTime, posX, posY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (itemSprite != null)
                itemSprite.Draw(spriteBatch);
        }
    }
}
