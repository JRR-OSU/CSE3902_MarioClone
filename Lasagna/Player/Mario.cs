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

        private int[] orignalPos = new int[2];

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 transitionVel;
        public bool isWarping = false;
        private int maxVelX = 150;
        private int maxVelY = 400;
        public bool isFalling = false;
        public int maxHeight;

        readonly Vector2 gravity = new Vector2(0, -500f);
        float time;

        public bool ignoreGravity { get; set; }
        public bool isRunning = false;
        private bool isJumping = false;
        private bool jumpDelay = false;
        private int jumpDelayCount = 0;
        private bool canJump = true;

        public bool isCollideGround { get; set; }


        private bool marioIsDead = false;

        public bool marioMovingLeft;
        public bool marioMovingRight;

        public bool IsDead { get { return marioIsDead; } }
        public bool StarPowered { get { return stateMachine != null && stateMachine.StarPowered; } }

        public Rectangle Bounds { get { return new Rectangle((int)position.X, -(int)position.Y, GetCurrentSprite().Width, GetCurrentSprite().Height); } }
        public bool IsBlinking { get { return stateMachine != null && (stateMachine.IsTransitioning || stateMachine.IsBlinking); } }
        public MarioStateMachine.MarioState CurrentState { get { return (stateMachine != null) ? stateMachine.CurrentState : MarioStateMachine.MarioState.Small; } }

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
            position.X = x;
            position.Y = -y;
            orignalPos[0] = (int)position.X;
            orignalPos[1] = -(int)position.Y;
        }

        public void ForceMove(float x, float y)
        {
            position.X += x;
            position.Y -= y;
        }

        public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = -y;
        }
        private ISprite GetCurrentSprite()
        {
            return stateMachine.CurrentSprite;
        }

        private void Reset(object sender, EventArgs e)
        {
            position.X = orignalPos[0];
            position.Y = -1 * orignalPos[1];
            velocity = Vector2.Zero;
            marioIsDead = false;
            stateMachine.Reset();
        }

        public void SetIdleState()
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;

            velocity.X = velocity.X / 1.2f;
            stateMachine.SetIdleState();
        }

        public void MarioFireProjectile(object sender, EventArgs e)
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;

            if (marioIsDead || stateMachine.CurrentState != MarioStateMachine.MarioState.Fire)
                return;

            bool facingRight = IsMarioMovingRight();

            int spawnX = Bounds.X + (facingRight ? Bounds.Width : 0);

            MarioGame.Instance.RegisterProjectile(new FireProjectile(spawnX, Bounds.Y + Bounds.Height / 2, facingRight));
        }

        private bool IsMarioMovingRight()
        {
            if (stateMachine == null)
                return false;

            MarioStateMachine.MarioMovement m = stateMachine.CurrentMovement;
            return m == MarioStateMachine.MarioMovement.CrouchRight
                || m == MarioStateMachine.MarioMovement.IdleRight
                || m == MarioStateMachine.MarioMovement.JumpRight
                || m == MarioStateMachine.MarioMovement.RunRight;
        }

        public void MoveLeft(object sender, EventArgs e)
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;
            marioMovingLeft = true;
        }

        public void MoveRight(object sender, EventArgs e)
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;
            marioMovingRight = true;
        }


        public void MarioMoveLeft()
        {
            if (marioMovingRight)
                return;

            if (isRunning && !marioIsDead && !(Math.Abs(velocity.X) >= maxVelX + 100))
            {
                velocity.X -= 10;
                stateMachine.MoveLeft();
            }
            else if (!isRunning && !marioIsDead)
            {   // Slow velocity down if moving but not running
                if ((Math.Abs(velocity.X) >= maxVelX) && !marioMovingRight)
                    velocity.X += 2;
                else
                    velocity.X -= 10;

                stateMachine.MoveLeft();
            }

        }

        public void MarioMoveRight()
        {
            if (marioMovingLeft)
                return;

            if (isRunning && !marioIsDead && !(Math.Abs(velocity.X) >= maxVelX + 100))
            {
                velocity.X += 10;
                stateMachine.MoveRight();
            }
            else if (!isRunning && !marioIsDead)
            {   // Slow velocity down if moving but not running
                if ((Math.Abs(velocity.X) >= maxVelX) && !marioMovingLeft)
                    velocity.X -= 2;
                else
                    velocity.X += 10;

                stateMachine.MoveRight();
            }
        }

        public void Crouch(object sender, EventArgs e)
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;

            if (!marioIsDead)
                stateMachine.Crouch();
        }

        public void Jump(object sender, EventArgs e)
        {
            if (stateMachine != null && stateMachine.IsTransitioning || !canJump)
                return;
            isJumping = true;
            if (!marioIsDead && !(Math.Abs(velocity.Y) >= maxVelY))
            {
                stateMachine.Jump();
                if (velocity.Y < 275 && (position.Y * -1 > maxHeight) && !isFalling)
                    velocity.Y += 75;
                else
                    isFalling = true;
            }
        }

        public void HandleJump()
        {
            if (!marioIsDead && !(Math.Abs(velocity.Y) >= maxVelY))
            {
                stateMachine.Jump();
                if (velocity.Y < 350 && !isFalling)
                    velocity.Y += 205;
                else
                    isFalling = true;
            }
        }

        public void CalcMaxHeight(int tileY, int tileHeight)
        {
            if (!isJumping && !isRunning)
                maxHeight = (int)(((tileY - tileHeight) - Bounds.Height * 3.0));
            else if (isRunning)
                maxHeight = (int)(((tileY - tileHeight) - Bounds.Height * 4.0));
        }

        public void StartJumpDelay()
        {
            jumpDelay = true;
        }

        public void HandleJumpDelay()
        {
            if (jumpDelayCount != 2)
                jumpDelayCount++;
            else
            {
                jumpDelayCount = 0;
                jumpDelay = false;
            }
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
            velocity.X = 0;
            stateMachine.KillMario();
        }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;

            if (otherCollider is IPlayer)
                marioCollisionHandler.OnCollisionResponse((IPlayer)otherCollider, side);
            else if (otherCollider is IEnemy)
                marioCollisionHandler.OnCollisionResponse((IEnemy)otherCollider, side);
            else if (otherCollider is ITile)
                marioCollisionHandler.OnCollisionResponse((ITile)otherCollider, side);
            else if (otherCollider is IItem)
                marioCollisionHandler.OnCollisionResponse((IItem)otherCollider, side);
            else if (otherCollider is IProjectile)
                marioCollisionHandler.OnCollisionResponse((IProjectile)otherCollider, side);
        }

        private void SetPhysicsBools()
        {
            if (isCollideGround || velocity.Y == 0)
            {
                isJumping = false;
                if (Keyboard.GetState().GetPressedKeys().Length == 0) // Set idle if no key is pressed
                    SetIdleState();
            }
            else if (isJumping == true)
                ignoreGravity = false;
        }
        private void HandleJumpBools()
        {
            if (isJumping && !(Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)))
                canJump = false;
            else if (!isJumping)
                canJump = true;
        }

        private void HandleRunning()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && !marioMovingRight && !marioMovingLeft)
            {
                isRunning = false;
                SetIdleState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (marioMovingRight || marioMovingLeft))
                isRunning = true;
            else
                isRunning = false;
        }
        private void HandleMovement()
        {
            if (marioMovingLeft)
                MarioMoveLeft();
            else if (marioMovingRight)
                MarioMoveRight();
        }
        public void UpdatePhysics(GameTime gameTime)
        {
            SetPhysicsBools();
            if (marioIsDead && (stateMachine == null || stateMachine.IsTransitioning))
                return;

            time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((Math.Abs(velocity.Y) >= maxVelY))
                stateMachine.EndJump();
            // Slow mario down if he starts moving the opposite direction
            if ((marioMovingLeft && velocity.X > 0) || (marioMovingRight && velocity.X < 0))
                velocity.X = velocity.X / 1.2f;

            if (!ignoreGravity && velocity.Y > -200)
                velocity += gravity * time;
            else if (ignoreGravity)
                velocity.Y = 0;
            position += velocity * time;
        }


        public void Update(GameTime gameTime)
        {
            if (stateMachine.IsTransitioning)
            {
                velocity = Vector2.Zero;
                ignoreGravity = true;
            }
            else if (!stateMachine.IsTransitioning && velocity != Vector2.Zero)
                transitionVel = velocity;


            if (!isWarping && !stateMachine.IsTransitioning)
                ignoreGravity = false;

            HandleJumpBools();

            HandleMovement();

            if (jumpDelay)
                HandleJumpDelay();

            HandleRunning();

            UpdatePhysics(gameTime);
            stateMachine.Update(gameTime, (int)position.X, -(int)position.Y);
            marioMovingLeft = false;
            marioMovingRight = false;
            isCollideGround = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }


        public void BeginWarpAnimation(Direction moveDir, bool startWithMove)
        {
            isWarping = true;
            stateMachine.BeginWarpAnimation(moveDir, startWithMove);
        }
    }
}
