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

        public virtual void Damage()
        {
            ChangeState(EnemyState.Dead);
        }
        public virtual Rectangle GetRectangle()
        {
            Rectangle temp;
            temp.X = posX;
            temp.Y = posY;
            temp.Height = currentSprite.Height;
            temp.Width = currentSprite.Width;
            return temp;
        }
    }
}
