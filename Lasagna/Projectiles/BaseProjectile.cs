using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public abstract class BaseProjectile : IProjectile
    {
        protected ISprite currentSprite;
        protected int posX;
        protected int posY;

        public BaseProjectile(int spawnPosX, int spawnPosY)
        {
            posX = spawnPosX;
            posY = spawnPosY;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (currentSprite != null)
                currentSprite.Update(gameTime, posX, posY);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentSprite != null)
                currentSprite.Draw(spriteBatch);
        }

        public abstract void ChangeState();
    }
}
