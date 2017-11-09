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

        private int marioWarpCount = 0;

        /// <summary>
        /// Constants
        /// </summary>
        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int NEGATIVE_ONE = -1;
        private const int TEN = 10;
        private const int ONE_HUNDRED = 100;
        private const int SEVENTY_FIVE = 75;
        private const int NEGATIVE_TWO_HUNDRED = -200;
        private const int TWO_SEVENTY_FIVE = 275;
        private const int NEGATIVE_FOUR_FORTY = -440;


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
            position.X = orignalPos[ZERO];
            position.Y = NEGATIVE_ONE * orignalPos[ONE];
            velocity = Vector2.Zero;
            marioIsDead = false;
            BGMFactory.Instance.Play_MainTheme();
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
            SoundEffectFactory.Instance.PlayFireball();
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
                velocity.X -= TEN;
                stateMachine.MoveLeft();
            }
            else if (!isRunning && !marioIsDead)
            {   // Slow velocity down if moving but not running
                if ((Math.Abs(velocity.X) >= maxVelX) && !marioMovingRight)
                    velocity.X += TWO;
                else
                    velocity.X -= TEN;

                stateMachine.MoveLeft();
            }

        }

        public void MarioMoveRight()
        {
            if (marioMovingLeft)
                return;

            if (isRunning && !marioIsDead && !(Math.Abs(velocity.X) >= maxVelX + ONE_HUNDRED))
            {
                velocity.X += TEN;
                stateMachine.MoveRight();
            }
            else if (!isRunning && !marioIsDead)
            {   // Slow velocity down if moving but not running
                if ((Math.Abs(velocity.X) >= maxVelX) && !marioMovingLeft)
                    velocity.X -= TWO;
                else
                    velocity.X += TEN;

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
            if (!isJumping)
            {
                SoundEffectFactory.Instance.PlayJumpMarioBig();
            }
                isJumping = true;
            if (!marioIsDead && !(Math.Abs(velocity.Y) >= maxVelY))
            {
                stateMachine.Jump();
               
                if (velocity.Y < TWO_SEVENTY_FIVE && !isFalling)
                    velocity.Y += SEVENTY_FIVE;
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
            if (jumpDelayCount != TWO)
                jumpDelayCount++;
            else
            {
                jumpDelayCount = ZERO;
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
            velocity.X = ZERO;
            stateMachine.KillMario();
            Score.LoseLifeMario();
            BGMFactory.Instance.Play_YouAreDead();

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
            if ((position.X >= 2193 && position.X <= 2256) || (position.X >= 2738 && position.X <= 2835) || (position.X >= 4884 && position.X <= 4952))
                ignoreGravity = false;
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
            if ((marioMovingLeft && velocity.X > ZERO) || (marioMovingRight && velocity.X < ZERO))
                velocity.X = velocity.X / 1.2f;

            if (!ignoreGravity && velocity.Y > NEGATIVE_TWO_HUNDRED)
                velocity += gravity * time;
            else if (ignoreGravity)
                velocity.Y = ZERO;
            position += velocity * time;
        }


        public void Update(GameTime gameTime)
        {
            if (marioIsDead || stateMachine.flagpoleSequence)
            {
                Console.WriteLine(position);
                stateMachine.Update(gameTime, (int)position.X, -(int)position.Y);
                return;
            }
            if(!MarioIsInWarpZone() && !stateMachine.IsTransitioning && position.Y < NEGATIVE_FOUR_FORTY)
            {
               Die();
            }

            if (stateMachine.IsTransitioning)
            {
                velocity = Vector2.Zero;
                ignoreGravity = true;
            }
            else if (!stateMachine.IsTransitioning && velocity != Vector2.Zero)
                transitionVel = velocity;



            HandleJumpBools();

            HandleMovement();

            if (jumpDelay)
                HandleJumpDelay();

            HandleRunning();

            UpdatePhysics(gameTime);
            if (isCollideGround)
                Score.ResetConsecutiveEnemiesKilled();
            stateMachine.Update(gameTime, (int)position.X, -(int)position.Y);
            marioMovingLeft = false;
            marioMovingRight = false;
            isCollideGround = false;

            // Console.WriteLine(position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }


        public void BeginWarpAnimation(Direction moveDir, bool startWithMove)
        {
            
            stateMachine.BeginWarpAnimation(moveDir, startWithMove);
        }

        public void MarioEnterWarpZone()
        {
            marioWarpCount++;
        }
        public bool MarioIsInWarpZone()
        {
            return marioWarpCount % TWO == ONE;
        }
    }
}
