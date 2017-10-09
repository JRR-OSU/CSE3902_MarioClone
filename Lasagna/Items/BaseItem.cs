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
            Taken
        }

        //Change this later if items support states.
        private ISprite itemSprite;
        private ItemState currentState;
        private int posX;
        private int posY;

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
                    return new Rectangle(posX, posY, itemSprite.Width, itemSprite.Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (itemSprite != null && currentState != ItemState.Taken)
                itemSprite.Update(gameTime, posX, posY);
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

        protected virtual void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            return;
        }

        ///TODO: Temp methods for sprint3
        public void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == ItemState.Taken)
                currentState = ItemState.Idle;
        }
    }
}
