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

        public Rectangle GetRectangle
        {
            get
            {
                if (itemSprite == null)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
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
        
        public virtual void OnCollisionResponse(IPlayer mario, CollisionSide side)
        {
            //Destroy the item after mario takes it
            currentState = ItemState.Taken;
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

        ///TODO: Temp methods for sprint3
        public void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == ItemState.Taken)
                currentState = ItemState.Idle;
        }
    }
}
