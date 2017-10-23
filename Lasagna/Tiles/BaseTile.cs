using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public abstract class BaseTile : ITile
    {
        private ISprite currentSprite;
        private int posX;
        private int posY;
        protected GameTime gametime;
        public virtual Rectangle Bounds { get { return new Rectangle(posX, posY, CurrentSprite.Width, CurrentSprite.Height); } }
        protected ISprite CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }
        protected int PosX { get { return posX; } }
        protected int PosY { get { return posY; } }

        protected BaseTile(int spawnPosX, int spawnPosY)
        {
            posX = spawnPosX;
            posY = spawnPosY;
        }
        public virtual void Update(GameTime gameTime)
        {
            this.gametime = gameTime;
            if (currentSprite != null)
                currentSprite.Update(gameTime, posX, posY);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (currentSprite != null)
            {
                currentSprite.Draw(spriteBatch);
            }
            if (currentSprite is BreakableBrickTile)
            {
                ((BreakableBrickTile)currentSprite).item.Draw(spriteBatch);
            }
            else if (currentSprite is InvisibleItemBlockTile)
            {
                ((InvisibleItemBlockTile)currentSprite).item.Draw(spriteBatch);
            }
            else if (currentSprite is QuestionBlockTile)
            {
                ((QuestionBlockTile)currentSprite).item.Draw(spriteBatch);
            }
        }

        public abstract void ChangeState();
        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (otherCollider is IPlayer)
                OnCollisionResponse((IPlayer)otherCollider, side);
        }

        protected virtual void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            return;
        }
    }
}
