using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public abstract class BaseProjectile : IProjectile
    {
        private ISprite currentSprite;
        private int posX;
        private int posY;

        public Rectangle Bounds
        {
            get
            {
                if (currentSprite != null)
                    return new Rectangle(posX, posY, currentSprite.Width, currentSprite.Height);
                else
                    return Rectangle.Empty;
            }
        }
        protected ISprite CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }
        protected int PosX { get { return posX; } }
        protected int PosY { get { return posY; } }

        protected BaseProjectile(int spawnPosX, int spawnPosY)
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

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (otherCollider is IPlayer)
                OnCollisionResponse((IPlayer)otherCollider, side);
            else if (otherCollider is IEnemy)
                OnCollisionResponse((IEnemy)otherCollider, side);
            else if (otherCollider is IItem)
                OnCollisionResponse((IItem)otherCollider, side);
            else if (otherCollider is ITile)
                OnCollisionResponse((ITile)otherCollider, side);
        }

        protected virtual void OnCollisionResponse(IEnemy Enemy, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IItem Item, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            return;
        }
    }
}
