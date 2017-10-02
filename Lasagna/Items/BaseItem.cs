using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public abstract class BaseItem : IItem
    {
        //Change this later if items support states.
        private ISprite itemSprite;
        private int posX;
        private int posY;
        public Rectangle temp;

        protected ISprite ItemSprite
        {
            get { return itemSprite; }
            set { itemSprite = value; }
        }
        protected int PosX { get { return posX; } }
        protected int PosY { get { return posY; } }

        protected BaseItem(int spawnPosX, int spawnPosY)
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
        public virtual Rectangle GetRectangle()
        {
            if(itemSprite == null)
            {
                return new Rectangle(0,0,0,0);
            }
            temp.X = posX;
            temp.Y = posY;
            temp.Height = itemSprite.Height;
            temp.Width = itemSprite.Width;
            return temp;
        }

        public virtual void OnCollisionResponse(IPlayer mario, CollisionSide side)
        {
            //Destroy the item after mario takes it
            itemSprite = null;
        }

        public virtual void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            return;
        }

        public virtual void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            return;
        }

        public virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            return;
        }
    }
}
