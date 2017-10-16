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

        private int spriteXPos;
        private int spriteYPos;
        private int[] orignalPos = new int[2];

        private bool marioIsDead = false;

        public bool IsDead { get { return marioIsDead; } }
        public bool StarPowered { get { return stateMachine != null && stateMachine.StarPowered; } }

        public Rectangle Bounds { get { return new Rectangle(spriteXPos, spriteYPos, GetCurrentSprite().Width, GetCurrentSprite().Height); } }


        public Mario(int x, int y)
        {
            stateMachine = new MarioStateMachine(this);
            marioCollisionHandler = new MarioCollisionHandler(this, stateMachine);

            MarioEvents.OnMoveLeft += MoveLeft;
            MarioEvents.OnMoveRight += MoveRight;
            MarioEvents.OnJump += Jump;
            MarioEvents.OnCrouch += Crouch;
            MarioEvents.OnReset += Reset;
            spriteXPos = x;
            spriteYPos = 20;
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
            return stateMachine.CurrentSprite;
        }

        private void Reset(object sender, EventArgs e)
        {
            spriteXPos = orignalPos[0];
            spriteYPos = orignalPos[1];
            marioIsDead = false;
            stateMachine.Reset();
        }

        public void SetIdleState()
        {
            stateMachine.SetIdleState();
        }

        public static void MarioFireProjectile(object sender, EventArgs e)
        {
            MarioStateMachine.MarioFireProjectile();
        }

        public static void GetFireflower()
        {
            MarioStateMachine.GetFireflower();
        }

        public void MoveLeft(object sender, EventArgs e)
        {
            if (!marioIsDead)
            {
                spriteXPos -= 3;
                stateMachine.MoveLeft();
            }
        }

        public void MoveRight(object sender, EventArgs e)
        {
            if (!marioIsDead)
            {
                spriteXPos += 3;
                stateMachine.MoveRight();
            }

        }

        public void Crouch(object sender, EventArgs e)
        {
            if (!marioIsDead)
            {
                spriteYPos += 3;
                stateMachine.Crouch();
            }
        }

        public void Jump(object sender, EventArgs e)
        {
            if (!marioIsDead)
            {
             //   spriteYPos -= 3;
                stateMachine.Jump();
            }

        }

        public void Grow(object sender, EventArgs e)
        {
            stateMachine.Grow();
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
            marioIsDead = true;
            stateMachine.KillMario();
        }

        // Overload of die which is used for Mario Collision
        public void Die()
        {
            marioIsDead = true;
            stateMachine.KillMario();
        }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (otherCollider is IPlayer)
                marioCollisionHandler.OnCollisionResponse((IPlayer)otherCollider, side);
            else if (otherCollider is IEnemy)
                marioCollisionHandler.OnCollisionResponse((IEnemy)otherCollider, side);
            else if (otherCollider is ITile)
                marioCollisionHandler.OnCollisionResponse((ITile)otherCollider, side);
            else if (otherCollider is IItem)
                marioCollisionHandler.OnCollisionResponse((IItem)otherCollider, side);
        }

        public void KeepMarioScreenBounds()
        {
            ///TODO: Temp code added by tim to test camera
            return;

            if (spriteXPos < 0) // Restrict mario to screen bounds
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

           // Console.WriteLine(" " + spriteXPos + " " + spriteYPos);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length == 0) // Set idle if no key is pressed
            {
                SetIdleState();
            }

            KeepMarioScreenBounds();

            stateMachine.Update(gameTime, spriteXPos, spriteYPos);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }

    }
}
