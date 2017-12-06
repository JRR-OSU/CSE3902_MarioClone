using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    public class MarioPhysics
    {

        Mario mario;
        internal MarioStateMachine stateMachine;

        public readonly Vector2 gravity = new Vector2(0, -500f);
        float time;

        public Vector2 velocity;
        public Vector2 transitionVel;


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

        private int maxVelX = 150;
        private int maxVelY = 400;


        public bool ignoreGravity { get; set; }
        public bool isRunning = false;
        public bool isJumping = false;
        private bool jumpDelay = false;
        private int jumpDelayCount = 0;
        public bool canJump = true;
        public bool disableCrouch = false;
        public bool marioMovingLeft;
        public bool marioMovingRight;


        public MarioPhysics(Mario player)
        {
            mario = player;
        }


        internal void GetStateMachineInstance()
        {
            stateMachine = mario.stateMachine;
        }

       // public bool IsJumping => isJumping;


        public void Jump()
        {
            if (!mario.marioIsDead && !(Math.Abs(velocity.Y) >= maxVelY) && canJump)
            {
                stateMachine.Jump();

                if (velocity.Y < TWO_SEVENTY_FIVE && !mario.isFalling)
                    velocity.Y += SEVENTY_FIVE;
                else
                    mario.isFalling = true;
            }
            isJumping = true;
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

        public void SetIdleState()
        {
            if (stateMachine != null && stateMachine.IsTransitioning)
                return;

            velocity.X = velocity.X / 1.2f;
            stateMachine.SetIdleState();
        }

        private void SetPhysicsBools()
        {
            if ((mario.position.X >= 2193 && mario.position.X <= 2256) || (mario.position.X >= 2738 && mario.position.X <= 2835) || (mario.position.X >= 4884 && mario.position.X <= 4952))
                ignoreGravity = false;
            if (mario.isCollideGround || velocity.Y == 0)
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
            else if (mario.isCollideGround)
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

        public void MarioMoveLeft()
        {
            if (marioMovingRight)
                return;

            if (isRunning && !mario.marioIsDead && !(Math.Abs(velocity.X) >= maxVelX + 100))
            {
                velocity.X -= TEN;
                stateMachine.MoveLeft();
            }
            else if (!isRunning && !mario.marioIsDead)
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

            if (isRunning && !mario.marioIsDead && !(Math.Abs(velocity.X) >= maxVelX + ONE_HUNDRED))
            {
                velocity.X += TEN;
                stateMachine.MoveRight();
            }
            else if (!isRunning && !mario.marioIsDead)
            {   // Slow velocity down if moving but not running
                if ((Math.Abs(velocity.X) >= maxVelX) && !marioMovingLeft)
                    velocity.X -= TWO;
                else
                    velocity.X += TEN;

                stateMachine.MoveRight();
            }
        }
        private void HandleMovement()
        {
            if (stateMachine.IsTransitioning)
                return;
            if (marioMovingLeft)
                MarioMoveLeft();
            else if (marioMovingRight)
                MarioMoveRight();
            else
            {
                stateMachine.SetIdleState();
                velocity.X = velocity.X / 1.2f;
            }
        }
        public void UpdatePhysics(GameTime gameTime)
        {
            SetPhysicsBools();
            if (mario.marioIsDead && (stateMachine == null || stateMachine.IsTransitioning))
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
            mario.position += velocity * time;
        }

        public void Update(GameTime gameTime)
        {
            mario.CheckFlagpoleHeight();
            HandleJumpBools();
            HandleMovement();

            if (jumpDelay)
                HandleJumpDelay();
            HandleRunning();

            UpdatePhysics(gameTime);

            marioMovingLeft = false;
            marioMovingRight = false;
        }
    }
}
