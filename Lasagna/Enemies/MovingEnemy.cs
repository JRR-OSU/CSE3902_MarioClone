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
        public enum EnemyMovement { IdleLeft, IdleRight, Flipped, Stomped };
        public EnemyMovement enemyMovement = EnemyMovement.IdleLeft;
        public bool isLeft = true;
        public bool isDead = false;
        public bool isFall = false;
        public bool isMoving = true;
        private int[] orignalPos = new int[2];

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
            orignalPos[0] = posX;
            orignalPos[1] = posY;
            MarioEvents.OnReset += ChangeToDefault;
        }
        public Rectangle Bounds
        {
            get
            {
                Rectangle returnValue = new Rectangle();
                if (CurrentSprite == null){
                    returnValue = Rectangle.Empty;
                }
                else{
                    returnValue = new Rectangle(posX, posY, CurrentSprite.Width, CurrentSprite.Height);
                }
                return returnValue;
            }
        }
        public bool IsSeen()
        {
            bool temp = true;
            if (posX > 760 || posX < 0)
            {
                temp = false;
                isMoving = false;
            }
            else
                isMoving = true;
            return temp;
        }
        public void ReSet()
        {
            posX = orignalPos[0];
            posY = orignalPos[1];
            isLeft = true;
            isDead = false;
            ChangeState(EnemyState.WalkRight);
        }
        public virtual void Update(GameTime gameTime)
        {
            if (currentSprite != null) {
                HandleHorizontalMovement();
                HandleVerticalMovement();
                currentSprite.Update(gameTime, posX, posY);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentSprite != null)
                currentSprite.Draw(spriteBatch);
        }

        public abstract void ChangeState(EnemyState newState);

        public abstract void Damage();

        private void ChangeToDefault (object sender, EventArgs e)
        {
            //If we're not idle, change to idle.
            if (currentState != EnemyState.Idle)
                ChangeState(EnemyState.Idle);
        }

        //Many sprites have different heights or widths, and Monogame has sprite pos in top left,
        //fix the positioning here so it doesn't flicker.
        protected void FixSpritePosition(ISprite oldSprite, ISprite newSprite)
        {
            int heightDifference = oldSprite.Height - newSprite.Height;
            if (heightDifference != 0)
                posY = posY + heightDifference;
            int widthDifference = oldSprite.Width - newSprite.Width;
            if (widthDifference != 0)
                posX = posX + widthDifference;
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
                ChangeState(EnemyState.WalkRight);
                isLeft = true;
                enemyMovement = EnemyMovement.IdleRight;
            }
            else if (side.Equals(CollisionSide.Left))
            {
                ChangeState(EnemyState.WalkLeft);
                isLeft = false;
                enemyMovement = EnemyMovement.IdleLeft;
            }
        }

        protected virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            
            if (side.Equals(CollisionSide.Right))
            {
                ChangeState(EnemyState.WalkRight);
                isLeft = true;
                enemyMovement = EnemyMovement.IdleRight;
            }
            else if (side.Equals(CollisionSide.Left))
            {
                ChangeState(EnemyState.WalkLeft);
                isLeft = false;
                enemyMovement = EnemyMovement.IdleLeft;
            }
        }
        private void HandleHorizontalMovement()
        {
            if (isDead == true) {

            }    
            else if(isMoving == false)
            {

            }
            else if (isLeft == true)
            {
                posX--;
            }
            else
            {
                posX++;
            }
        }
        private void HandleVerticalMovement()
        {
            if(isFall == true)
            {
                
            }
        }
    }
}
