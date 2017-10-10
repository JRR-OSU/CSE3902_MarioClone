using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    public class FlagPoleTile : ITile
    {
        private enum BlockState
        {
            Idle,
            Moving
        }

        private ISprite flagSprite = TileSpriteFactory.Instance.CreateSprite_Flag();
        private ISprite flagPoleSprite = TileSpriteFactory.Instance.CreateSprite_FlagPole();
        private BlockState currentState;
        private int posX;
        private int posY;
        private int flagOffsetX;
        private int flagOffsetY;
        public Rectangle Bounds { get { return new Rectangle(this.posX, this.posY, this.flagPoleSprite.Width, this.flagPoleSprite.Height); } }

        public FlagPoleTile(int spawnPosX, int spawnPosY)
        {
            posX = spawnPosX;
            posY = spawnPosY;

            //Flag offset is based on half the flag width + half the pole width, and half the flag height
            if (flagSprite != null && flagPoleSprite != null)
            {
                flagOffsetX = -((flagSprite.Width + flagPoleSprite.Width) / 2);
                flagOffsetY = flagSprite.Height / 2;
            }
            //If given null values, set to default.
            else
            {
                flagOffsetX = -24;
                flagOffsetY = 16;
            }
        }

        public void ChangeState()
        {
            //Make the flag start moving down, or reset us.
            if (currentState == BlockState.Idle)
                currentState = BlockState.Moving;
            else
            {
                ///TODO: reset position to top
                currentState = BlockState.Idle;
            }

        }

        public void Update(GameTime gameTime)
        {
            ///TODO: If moving make flag move down

            if (flagPoleSprite != null)
                flagPoleSprite.Update(gameTime, posX, posY);
            if (flagSprite != null)
                flagSprite.Update(gameTime, posX + flagOffsetX, posY + flagOffsetY);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (flagPoleSprite != null)
                flagPoleSprite.Draw(spriteBatch);
            if (flagSprite != null)
                flagSprite.Draw(spriteBatch);
        }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (otherCollider is IPlayer)
                OnCollisionResponse((IPlayer)otherCollider, side);
        }

        private static void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (Mario == null || side == CollisionSide.None)
                return;

            //Reserved for moving flag down. (Need to be discussed.)
            return;
        }
    }
}
