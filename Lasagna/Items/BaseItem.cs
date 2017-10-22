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
            Taken
        }

        //Change this later if items support states.
        private ISprite itemSprite;
        private ItemState currentState = ItemState.Idle;
        public Vector2 position;
        private GameTime gameTime;
        private bool isInBlock = false;
        private float velocity = 1;
        private float moveUpVelocity = 1;
        private float fallingVelocity = (float)1.5;
        private float fallingVelocityDecayRate = (float).9;
        private float yDifference;
        private int originalX;
        private int originalY;
        private int coinAnimateTime = 0;
        private bool isLeft = false;

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
            if (itemSprite != null && currentState != ItemState.Taken)
            {
                if (currentState.Equals(ItemState.Moving))
                {
                    HandleHorizontalMovement();
                    Fall(gameTime);
                }
                itemSprite.Update(gameTime, (int)position.X, (int)position.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (itemSprite != null && currentState != ItemState.Taken)
                itemSprite.Draw(spriteBatch);
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
            if (itemSprite is CoinItem)
            {
                this.isInBlock = true;
                ((CoinItem)itemSprite).StartCoinAnimation();
                this.isInBlock = false;
            }
            else
            {
                this.isInBlock = true;
                yDifference = moveUpVelocity * ((float)gameTime.ElapsedGameTime.Milliseconds / 50);
                while (position.Y + this.itemSprite.Height > originalY)
                {
                    position.Y -= yDifference;
                }
                ((BaseItem)itemSprite).Move();
                this.isInBlock = false;
            }
        }
        public void Move()
        {
            currentState = ItemState.Moving;
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
