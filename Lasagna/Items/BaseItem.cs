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

        //Change this later if items support states.
        private ISprite itemSprite;
        protected ItemState currentState = ItemState.Idle;
        public Vector2 position;
        protected bool isInBlock = false;
        protected bool isInvisible = false;
        private float velocity = 1;
        protected const float moveUpVelocity = 1;
        private float fallingVelocity = (float)1.5;
        private float fallingVelocityDecayRate = (float).9;
        protected float yDifference;
        protected int originalX;
        protected int originalY;
        private int coinAnimateTime = 0;
        protected bool movingLeft = false;
        protected int MovingTime = 0;
        private ISprite originalSprite;
        private int hideTime = 0;
        private bool waitToDraw = false;

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
                if (itemSprite == null || currentState == ItemState.Taken)
                    return Rectangle.Empty;
                else
                    return new Rectangle((int)position.X, (int)position.Y, itemSprite.Width, itemSprite.Height);
            }
        }

        // Method to allow adjustment of item spawn position in LevelCreator.cs
        public void FixInitialPosition(int x)
        {
            position.X = x;
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
            if (MovingTime >= 2000)
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
                        yDifference = moveUpVelocity * ((float)gameTime.ElapsedGameTime.Milliseconds / 50);
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
            if (hideTime >= 16)
            {
                hideTime = 0;
                this.waitToDraw = false;
            }
        }

        public void Reset(object sender, EventArgs e)
        {
            itemSprite = originalSprite;
            currentState = ItemState.Idle;
            position.X = originalX;
            position.Y = originalY;
            movingLeft = false;
            fallingVelocity = (float)1.5;
            velocity = 1;
            MovingTime = 0;
            coinAnimateTime = 0;
            isInBlock = false;
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
                velocity = 1;
            }
            CorrectPosition(side, tile);
        }

        //This should be called whenever there is a collision, it resolves this item's new position.
        protected void CorrectPosition(CollisionSide side, ICollider target)
        {
            if (side == CollisionSide.None || target == null)
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
            if(!(this is CoinItem))
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
            yDifference = velocity * ((float)gameTime.ElapsedGameTime.Milliseconds / 50);
            position.Y += yDifference;
            velocity += fallingVelocity;
            velocity -= fallingVelocityDecayRate;

        }
        private void HandleHorizontalMovement()
        {
            {
                if (movingLeft == true)
                {
                    position.X -= 2;
                }
                else
                {
                    position.X += 2;
                }
            }
        }
        private void HandleCoinAnimation()
        {
            position.Y -= 15;
            velocity += 4;
            coinAnimateTime++;
            if (coinAnimateTime >= 20)
                itemSprite = null;
        }
    }
}
