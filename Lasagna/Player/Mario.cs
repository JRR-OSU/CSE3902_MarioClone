using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Lasagna
{
    public class Mario : IPlayer, ICollider
    {
        private MarioStateMachine stateMachine;
        private MarioCollisionHandler marioCollisionHandler;
      //  private MarioCollisionHandler marioCollisionHandler;

        private int spriteXPos;
        private int spriteYPos;
        private int[] orignalPos = new int[2];

        public Rectangle GetRect { get { return new Rectangle(spriteXPos, spriteYPos, GetCurrentSprite().Width, GetCurrentSprite().Height); }}
        
        /// <summary>
        /// These methods will just change state, the state machine will handle sprite changes
        /// </summary>
        public Mario(int x, int y)
        {
            stateMachine = new MarioStateMachine();
            marioCollisionHandler = new MarioCollisionHandler(this, stateMachine);
           
            MarioEvents.OnMoveLeft += MoveLeft;
            MarioEvents.OnMoveRight += MoveRight;
            MarioEvents.OnJump += Jump;
            MarioEvents.OnCrouch += Crouch;

            //MarioEvents.OnGetMushroom += Grow;
            //MarioEvents.OnMarioDamage += Shrink;
            MarioEvents.OnFire += MarioFireProjectile;
           // MarioEvents.OnGetFireFlower += FireState;

           // MarioEvents.OnMarioDie += Die;

            MarioEvents.OnReset += Reset;

            spriteXPos = x;
            spriteYPos = y;
            orignalPos[0] = spriteXPos;
            orignalPos[1] = spriteYPos;
        }

        public void SetPosition(int x, int y)
        {
            spriteXPos = x;
            spriteYPos = y;
        }
        private ISprite GetCurrentSprite()
        {
            return stateMachine.GetCurrentSprite();
        }

        private void Reset(object sender, EventArgs e)
        {
            spriteXPos = orignalPos[0];
            spriteYPos = orignalPos[1];
            stateMachine.Reset();
        }
        
        public void SetIdleState()
        {
            stateMachine.SetIdleState();
        }

        public void MarioFireProjectile(object sender, EventArgs e)
        {
            stateMachine.MarioFireProjectile();
        }

        public void GetFireflower()
        {
            stateMachine.GetFireflower();
        }
     
        public void MoveLeft(object sender, EventArgs e)
        {
            spriteXPos -= 3;
            stateMachine.MoveLeft();
        }

        public void MoveRight(object sender, EventArgs e)
        {
            spriteXPos += 3;
            stateMachine.MoveRight();

        }

        public void Crouch(object sender, EventArgs e)
        {
            spriteYPos += 3;
            stateMachine.Crouch();
        }

        public void Jump(object sender, EventArgs e)
        {
            spriteYPos -= 3;
            stateMachine.Jump();
        }

        public void Grow(object sender, EventArgs e)
        {
            stateMachine.Grow();
        }

        public void FireState(object sender, EventArgs e)
        {
            stateMachine.Fire();
        }

        public void Shrink(object sender, EventArgs e)
        {
            stateMachine.Shrink();
        }

        public void Star()
        {
            stateMachine.Star();
        }

        public void Die(object sender, EventArgs e)
        {
            stateMachine.KillMario();
        }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (otherCollider is IPlayer)
                marioCollisionHandler.OnCollisionResponse((IPlayer) otherCollider, side);
            else if (otherCollider is IEnemy)
                marioCollisionHandler.OnCollisionResponse((IEnemy)otherCollider, side);
            else if (otherCollider is ITile)
                marioCollisionHandler.OnCollisionResponse((ITile)otherCollider, side);
            else if (otherCollider is IItem)
                marioCollisionHandler.OnCollisionResponse((IItem)otherCollider, side);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length == 0)
            {
                SetIdleState();
            }
            if (spriteXPos < 0)
            {
                spriteXPos = 0;
            }
            else if (spriteXPos > 760)
            {
                spriteXPos = 760;
            }
            if (spriteYPos < 0)
            {
                spriteYPos = 0;
            }
            else if (spriteYPos > 420)
            {
                spriteYPos = 420;
            }
            stateMachine.Update(gameTime, spriteXPos, spriteYPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }

    }
}
