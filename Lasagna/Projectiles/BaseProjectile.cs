using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public abstract class BaseProjectile : IProjectile
    {
        //Movement speed for projectiles in pixels/second
        protected const int horizontalMoveSpeed = 350;
        protected const int verticalMoveSpeed = 200;

        private ISprite currentSprite;
        protected float posX;
        protected float posY;
        private bool movingRight;

        public Rectangle Bounds
        {
            get
            {
                if (currentSprite != null)
                    return new Rectangle((int)posX, (int)posY, currentSprite.Width, currentSprite.Height);
                else
                    return Rectangle.Empty;
            }
        }
        protected ISprite CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }
        protected bool MovingRight { get { return movingRight; } }

        protected BaseProjectile(int spawnPosX, int spawnPosY, bool startMovingRight)
        {
            posX = spawnPosX;
            posY = spawnPosY;
            movingRight = startMovingRight;

            MarioEvents.OnReset += Reset;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (currentSprite != null)
                currentSprite.Update(gameTime, (int)posX, (int)posY);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentSprite != null)
                currentSprite.Draw(spriteBatch);
        }

        public abstract void DestroyShell();

        public void Reset(object sender, EventArgs e)
        {
            MarioGame.Instance.DeRegisterProjectile(this);
        }

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
            else if (otherCollider is IProjectile)
                OnCollisionResponse((IProjectile)otherCollider, side);
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
        protected virtual void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            return;
        }


        protected virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            if (side == CollisionSide.Left)
                movingRight = true;
            else if (side == CollisionSide.Right)
                movingRight = false;
        }

        protected void CorrectPosition(CollisionSide side, ICollider target)
        {
            if (side == CollisionSide.None || target == null)
                return;

            if (side == CollisionSide.Left)
                posX = target.Bounds.X + target.Bounds.Width;
            else if (side == CollisionSide.Right)
                posX = target.Bounds.X - this.Bounds.Width;
            else if (side == CollisionSide.Top)
                posY = target.Bounds.Y + target.Bounds.Height;
            else if (side == CollisionSide.Bottom)
                posY = target.Bounds.Y - this.Bounds.Height;
        }
    }
}
