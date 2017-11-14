using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Lasagna
{
    public abstract class BaseItem : IItem
    {
        protected enum ItemState
        {
            Idle,
            CoinAnimaiotn,
            Bounce,
            Moving,
            Taken
        }
        private const int ZERO = 0;
        private const int ONE = 1;
        private const double ONEPFIVE = 1.5;
        private const int SIXTEEN = 16;
        private const int FIFTY = 50;
        //Change this later if items support states.
        private ISprite itemSprite;
        protected ItemState currentState = ItemState.Idle;
        public Vector2 position;
        protected bool isInBlock = false;
        protected bool isInvisible = false;
        private float velocity = ONE;
        protected const float moveUpVelocity = ONE;
        private float fallingVelocity = (float)ONEPFIVE;
        private float fallingVelocityDecayRate = (float).9;
        protected float yDifference;
        protected int originalX;
        protected int originalY;
        private int coinAnimateTime = ZERO;
        protected bool movingLeft = false;
        protected int MovingTime = ZERO;
        private ISprite originalSprite;
        private int hideTime = 0;
        private bool waitToDraw = false;

        private int maxMovingTime = 2000;
        private int increasingYDifference = 15;
        private int increasingXDifference = 2;
        private int increasingVelocity = 4;
        private int coinAnimateedTimeMax = 20;

        protected ISprite ItemSprite
        {
            get { return itemSprite; }
            set
            {
                if (originalSprite == null)
                    originalSprite = value;

                itemSprite = value;
            }
        }
        protected float PosX { get { return position.X; }}
        protected float PosY { get { return position.Y; } set { position.Y = value; } }

        protected BaseItem(int spawnPosX, int spawnPosY)
        {
            position.X = spawnPosX;
            position.Y = spawnPosY;
            originalX = spawnPosX;
            originalY = spawnPosY;
            currentState = ItemState.Idle;
            MarioEvents.OnReset += Reset;
        }

        public Rectangle Bounds
        {
            get
            {
                if (itemSprite == null || currentState == ItemState.Taken || isInvisible)
                    return Rectangle.Empty;
                else
                    return new Rectangle((int)position.X, (int)position.Y, itemSprite.Width, itemSprite.Height);
            }
        }
              

        public virtual void Update(GameTime gameTime)
        {
            if (currentState.Equals(ItemState.CoinAnimaiotn))
            {
                HandleCoinAnimation();
                Fall(gameTime);
            }
            
            if (currentState.Equals(ItemState.Moving) || currentState.Equals(ItemState.Bounce))
            {
                MovingTime++;
            }
            if (MovingTime >= maxMovingTime)
            {
                itemSprite = null;
            }
            if (itemSprite != null && currentState != ItemState.Taken)
            {
                if (currentState.Equals(ItemState.Moving))
                {
                    HandleHorizontalMovement();
                    Fall(gameTime);
                }
                else if (this is StarItem && currentState.Equals(ItemState.Bounce))
                {
                    HandleHorizontalMovement();
                }
                if (this.isInBlock)
                {
                    if (this is StarItem)
                    {
                    }
                    if (this is CoinItem)
                    {
                        this.isInvisible = false;
                        ((CoinItem)this).StartCoinAnimation();
                        this.isInBlock = false;
                    }
                    else
                    {
                        this.isInvisible = false;
                        yDifference = moveUpVelocity * ((float)gameTime.ElapsedGameTime.Milliseconds / FIFTY);
                        if (position.Y + this.itemSprite.Height > originalY)
                        {
                            position.Y -= yDifference;
                        }
                        else
                        {
                            this.Move();
                            this.isInBlock = false;
                        }
                    }
                }
                
                itemSprite.Update(gameTime, (int)position.X, (int)position.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Don't draw if the block is bumping
            if (this.waitToDraw == true)
                hideTime++;

            if (itemSprite != null && currentState != ItemState.Taken && !this.isInvisible && !this.waitToDraw)
                itemSprite.Draw(spriteBatch);
            if (hideTime >= SIXTEEN)
            {
                hideTime = ZERO;
                this.waitToDraw = false;
            }
        }

        public virtual void Reset(object sender, EventArgs e)
        {
            itemSprite = originalSprite;
            currentState = ItemState.Idle;
            position.X = originalX;
            position.Y = originalY;
            movingLeft = false;
            fallingVelocity = (float)ONEPFIVE;
            velocity = ONE;
            MovingTime = ZERO;
            coinAnimateTime = ZERO;
            isInBlock = false;
            /*if (this is StarItem)
            {
                ((StarItem)this).verticalMoveSpeed = 0f;
                ((StarItem)this).hittedGround = false;
            }*/
        }

        public void ChangeToInvisible()
        {
            this.isInvisible = true;
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
            //Destroy the item after mario takes it
            currentState = ItemState.Taken;
        }

        protected virtual void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IItem item, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            return;
        }

        protected virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            if (isInBlock || currentState == ItemState.Idle)
                return;

            if (currentState == ItemState.Moving)
            {
                if (side == CollisionSide.Left)
                    movingLeft = false;
                else if (side == CollisionSide.Right)
                    movingLeft = true;
            }

            if (currentState.Equals(ItemState.Moving) && side.Equals(CollisionSide.Bottom))
            {
                position.Y -= yDifference;
                velocity = ONE;
            }
            CorrectPosition(side, tile);
        }

        //This should be called whenever there is a collision, it resolves this item's new position.
        protected void CorrectPosition(CollisionSide side, ICollider target)
        {
            if (side == CollisionSide.None || target == null || (this is CoinItem))
                return;

            if (side == CollisionSide.Left)
                position.X = target.Bounds.X + target.Bounds.Width;
            else if (side == CollisionSide.Right)
                position.X = target.Bounds.X - this.Bounds.Width;
            else if (side == CollisionSide.Top)
                position.Y = target.Bounds.Y + target.Bounds.Height;
            else if (side == CollisionSide.Bottom)
                position.Y = target.Bounds.Y - this.Bounds.Height;
        }


        public virtual void Spawn()
        {
            if (!(this is CoinItem))
                this.waitToDraw = true;

            this.isInBlock = true;
        }
        public void Move()
        {
            if (this is StarItem)
            {
                currentState = ItemState.Bounce;
            }
            else if (!(this is FireFlowerItem))
            {
                currentState = ItemState.Moving;
            }
        }
        public void StartCoinAnimation()
        {
            currentState = ItemState.CoinAnimaiotn;
        }
        private void Fall(GameTime gameTime)
        {
            yDifference = velocity * ((float)gameTime.ElapsedGameTime.Milliseconds / FIFTY);
            position.Y += yDifference;
            velocity += fallingVelocity;
            velocity -= fallingVelocityDecayRate;

        }
        private void HandleHorizontalMovement()
        {
            {
                if (movingLeft == true)
                {
                    position.X -= increasingXDifference;
                }
                else
                {
                    position.X += increasingXDifference;
                }
            }
        }
        private void HandleCoinAnimation()
        {
            position.Y -= increasingYDifference;
            velocity += increasingVelocity;
            coinAnimateTime++;
            if (coinAnimateTime >= coinAnimateedTimeMax)
            {
                itemSprite = null;
            }
               
        }
    }
}
