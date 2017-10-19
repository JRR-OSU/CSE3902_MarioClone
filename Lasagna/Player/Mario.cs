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

        public Vector2 position;
        public Vector2 velocity;
        private int maxVelX = 150;
        private int maxVelY = 200;
        public bool isFalling = false;
        private int maxHeight;
        readonly Vector2 gravity = new Vector2(0, -300f);
        float time;

        public bool ignoreGravity = false;


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
            MarioEvents.OnShootFire += MarioFireProjectile;
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
            return stateMachine.CurrentSprite;
        }

        private void Reset(object sender, EventArgs e)
        {
            position.X = orignalPos[0];
            position.Y = -1 * orignalPos[1];
            marioIsDead = false;
            stateMachine.Reset();
        }

        public void SetIdleState()
        {
            velocity.X = velocity.X / 1.2f;
            stateMachine.SetIdleState();
        }

        public void MarioFireProjectile(object sender, EventArgs e)
        {
            if (marioIsDead || stateMachine.CurrentState != MarioStateMachine.MarioState.Fire)
                return;

            bool facingRight = IsMarioMovingRight();

            int spawnX = Bounds.X + (facingRight ? Bounds.Width : 0);

            MarioGame.Instance.RegisterProjectile(new FireProjectile(spawnX, Bounds.Y + Bounds.Height / 2, facingRight));
        }

        private bool IsMarioMovingRight ()
        {
            if (stateMachine == null)
                return false;

            MarioStateMachine.MarioMovement m = stateMachine.CurrentMovement;
            return m == MarioStateMachine.MarioMovement.CrouchRight 
                || m == MarioStateMachine.MarioMovement.IdleRight 
                || m == MarioStateMachine.MarioMovement.JumpRight 
                || m == MarioStateMachine.MarioMovement.RunRight;
        }

        public static void GetFireflower()
        {
            MarioStateMachine.GetFireflower();
        }

        public void MoveLeft(object sender, EventArgs e)
        {
            if (!marioIsDead && !(Math.Abs(velocity.X) >= maxVelX))
            {
                velocity.X -= 10;
                stateMachine.MoveLeft();
            }
           
        }

        public void MoveRight(object sender, EventArgs e)
        {
            if (!marioIsDead && !(Math.Abs(velocity.X) >= maxVelX))
            {
                velocity.X += 10;
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
            if (!marioIsDead && !(Math.Abs(velocity.X) >= maxVelY))
            {
                stateMachine.Jump();
                ignoreGravity = false;
                if (velocity.Y < 200 && position.Y * -1 > maxHeight && !isFalling)
                    velocity.Y += 75;
                else
                {
                    isFalling = true;
                }

            }


        }

        public void JumpEnemy()
        {
            ignoreGravity = false;
            velocity.Y += 75;
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
            SetPosition((int)position.X, (int)position.Y * -1);
            if (Keyboard.GetState().GetPressedKeys().Length == 0) // Set idle if no key is pressed
            {
                SetIdleState();
            }
            time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (velocity.Y == 0)
            //   ignoreGravity = true;

            if ((Math.Abs(velocity.Y) >= maxVelY))
            {
                stateMachine.EndJump();
            }
         //   Console.WriteLine(maxHeight);
       //     Console.WriteLine(position.Y);
            if (ignoreGravity)
            {
                maxHeight = ((int)position.Y + Bounds.Height * 2) * -1;
            }

            if (!ignoreGravity && velocity.Y > -200)
                velocity += gravity * time;
            position += velocity * time;


            //   Console.WriteLine(position);
           // Console.WriteLine(velocity);

            KeepMarioScreenBounds();

            stateMachine.Update(gameTime, spriteXPos, spriteYPos);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }

    }
}
