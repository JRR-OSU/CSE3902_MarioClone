using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Lasagna
{
    public class FlagPoleTile : ITile
    {
        private enum BlockState
        {
            Idle,
            Moving
        }
        private const int TWO = 2;
        private const int THREE = 3;
        private const int NTWENTYFOUR = -24;
        private const int SIXTEEN = 16;
        private ISprite flagSprite = TileSpriteFactory.Instance.CreateSprite_Flag();
        private ISprite flagPoleSprite = TileSpriteFactory.Instance.CreateSprite_FlagPole();
        private BlockState currentState;
        private int posX;
        private int posY;
        private int originalFlagX;
        private int originalFlagY;
        private int flagOffsetX;
        private int flagOffsetY;
        private int moveDownVelocity = THREE;
        private bool atBottom = false;
        public bool IsChangingState { get; set; }
        public Rectangle Bounds { get { return new Rectangle(this.posX, this.posY, this.flagPoleSprite.Width, this.flagPoleSprite.Height); } }

        public FlagPoleTile(int spawnPosX, int spawnPosY)
        {
            posX = spawnPosX;
            posY = spawnPosY;

            //Flag offset is based on half the flag width + half the pole width, and half the flag height
            if (flagSprite != null && flagPoleSprite != null)
            {
                flagOffsetX = -((flagSprite.Width + flagPoleSprite.Width) / TWO);
                flagOffsetY = flagSprite.Height / TWO;
            }
            //If given null values, set to default.
            else
            {
                flagOffsetX = NTWENTYFOUR;
                flagOffsetY = SIXTEEN;
            }
            originalFlagX = flagOffsetX;
            originalFlagY = flagOffsetY;
            MarioEvents.OnReset += Reset;
        }

        public void Reset(object sender, EventArgs e)
        {
            currentState = BlockState.Idle;
            flagOffsetX = originalFlagX;
            flagOffsetY = originalFlagY;
            atBottom = false;
        }

        public virtual void ChangeState() {
            return;
        }

        public void Update(GameTime gameTime)
        {
            if (flagPoleSprite != null)
                flagPoleSprite.Update(gameTime, posX, posY);
            if (flagSprite != null)
            {
                flagSprite.Update(gameTime, posX + flagOffsetX, posY + flagOffsetY);
                if (currentState == BlockState.Moving && !atBottom)
                {
                    flagOffsetY += moveDownVelocity;
                    if (flagOffsetY + flagSprite.Height + moveDownVelocity >= flagPoleSprite.Height)
                    {
                        flagOffsetY = flagPoleSprite.Height - flagSprite.Height;
                        atBottom = true;
                        Score.flagAtBottom = true;
                        currentState = BlockState.Idle;
                    }
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (flagPoleSprite != null)
                flagPoleSprite.Draw(spriteBatch);
            if (flagSprite != null)
                flagSprite.Draw(spriteBatch);
        }

        public bool IsAtBottom { get { return atBottom; } }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (otherCollider is IPlayer)
                OnCollisionResponse((IPlayer)otherCollider, side);
        }

        private void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if ((side == CollisionSide.Left || side == CollisionSide.Top) && 
                currentState == BlockState.Idle && !atBottom)
            {
                currentState = BlockState.Moving;
                Score.AddPoleHeightScore();
            }
        }
    }
}
