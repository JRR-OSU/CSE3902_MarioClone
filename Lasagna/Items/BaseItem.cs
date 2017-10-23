using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public abstract class BaseItem : IItem
    {
        private enum ItemState
        {
            Idle,
            CoinAnimaiotn,
            Moving,
            MovingStar,
            Taken
        }

        //Change this later if items support states.
        private ISprite itemSprite;
        private ItemState currentState = ItemState.Idle;
        public Vector2 position;
        private GameTime gameTime;
        private bool isInBlock = false;
        private bool isInvisible = false;
        private float velocity = 1;
        private float moveUpVelocity = 1;
        private float fallingVelocity = (float)1.5;
        private float fallingVelocityDecayRate = (float).9;
        private float yDifference;
        private int originalX;
        private int originalY;
        private int coinAnimateTime = 0;
        private bool isLeft = false;
        private int MovingTime = 0;
        private int moveUpDifference = 150;

        protected ISprite ItemSprite
        {
            get { return itemSprite; }
            set { itemSprite = value; }
        }
        protected int PosX { get { return (int)position.X; } }
        protected int PosY { get { return (int)position.Y; } }

        protected BaseItem(int spawnPosX, int spawnPosY)
        {
            position.X = spawnPosX;
            position.Y = spawnPosY;
            originalX = spawnPosX;
            originalY = spawnPosY;
            currentState = ItemState.Idle;
            MarioEvents.OnReset += ChangeToDefault;
        }
        protected BaseItem(int spawnPosX, int spawnPosY, bool inBlock)
        {
            position.X = spawnPosX;
            position.Y = spawnPosY;
            this.isInBlock = inBlock;
            currentState = ItemState.Idle;
            MarioEvents.OnReset += ChangeToDefault;
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


        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (currentState.Equals(ItemState.CoinAnimaiotn))
            {
                HandleCoinAnimation();
                Fall(gameTime);
            }
            if (currentState.Equals(ItemState.Moving) || currentState.Equals(ItemState.MovingStar))
            {
                MovingTime++;
            }
            if (MovingTime >= 300)
            {
                itemSprite = null;
            }

            if (itemSprite != null && currentState != ItemState.Taken)
            {
                if (currentState.Equals(ItemState.Moving) || currentState.Equals(ItemState.MovingStar))
                {
                    HandleHorizontalMovement();
                    Fall(gameTime);
                }
                if (this.isInBlock)
                {
                    if (this is CoinItem)
                    {
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
            if (itemSprite != null && currentState != ItemState.Taken && !this.isInvisible)
                itemSprite.Draw(spriteBatch);
        }

        public void ReSet(object sender, EventArgs e)
        {
            currentState = ItemState.Idle;
            position.X = originalX;
            position.Y = originalY;
            isLeft = true;
            fallingVelocity = (float)1.5;
            MovingTime = 0;
            coinAnimateTime = 0;
            isInBlock = false;
            moveUpVelocity = 1;
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

        private void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            if (!isInBlock && side.Equals(CollisionSide.Bottom) && currentState.Equals(ItemState.Moving))
            {
                position.Y -= yDifference;
                velocity = 1;
            }
            if (currentState.Equals(ItemState.MovingStar))
            {
                position.X += 2;
                position.Y -= yDifference;
                position.Y -= moveUpDifference * (float).5;
                velocity = 1;
            }

        }

        ///TODO: Temp methods for sprint3
        public void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == ItemState.Taken)
            {
                currentState = ItemState.Idle;
                position.X = originalX;
                position.Y = originalY;
                this.isInBlock = false;
            }
        }
        public virtual void Spawn()
        {
            this.isInBlock = true;
        }
        public void Move()
        {
            if (!(this is FireFlowerItem))
            {
                currentState = ItemState.Moving;
            }
            if(this is StarItem)
            {
                currentState = ItemState.MovingStar;
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
            velocity *= fallingVelocityDecayRate;
            if (velocity > 50)
                velocity = 50;
        }
        private void HandleHorizontalMovement()
        {
            {
                if (isLeft == true)
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
            position.Y -= 10;
            velocity += 4;
            coinAnimateTime++;
            if (coinAnimateTime >= 40)
                itemSprite = null;
        }
    }
}
