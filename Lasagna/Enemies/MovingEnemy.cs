using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public abstract class MovingEnemy : IEnemy
    {
        private ISprite currentSprite;
        private EnemyState currentState;
        private int posX;
        private int posY;


        protected ISprite CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }
        protected EnemyState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
        protected int PosX { get { return posX; } }
        protected int PosY { get { return posY; } }

        protected MovingEnemy(int spawnPosX, int spawnPosY)
        {
            posX = spawnPosX;
            posY = spawnPosY;
        }
        public Rectangle GetRectangle
        {
            get
            {
                if (CurrentSprite == null)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(posX, posY, CurrentSprite.Width, CurrentSprite.Height);
            }
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

        public abstract void ChangeState(EnemyState newState);

        public abstract void Damage();

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

        protected virtual void OnCollisionResponse(IPlayer mario, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IItem item, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IProjectile fireball, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IEnemy otherEnemy, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Right))
            {
                ChangeState(EnemyState.WalkLeft);
            }
            else if (side.Equals(CollisionSide.Left))
            {
                ChangeState(EnemyState.WalkRight);
            }
        }

        protected virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Right))
            {
                ChangeState(EnemyState.WalkLeft);
            }
            else if (side.Equals(CollisionSide.Left))
            {
                ChangeState(EnemyState.WalkRight);
            }
        }
    }
}
