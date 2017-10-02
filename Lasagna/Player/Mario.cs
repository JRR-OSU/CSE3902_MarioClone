using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Lasagna
{
    public class Mario : IPlayer
    {
        private MarioStateMachine stateMachine;
        private MarioCollisionHandler marioCollisionHandler;

        private int spriteXPos;
        private int spriteYPos;

        /// <summary>
        /// These methods will just change state, the state machine will handle sprite changes
        /// </summary>
        public Mario(int x, int y)
        {
            stateMachine = new MarioStateMachine();
            marioCollisionHandler = new MarioCollisionHandler(this);

            MarioEvents.OnMoveLeft += MoveLeft;
            MarioEvents.OnMoveRight += MoveRight;
            MarioEvents.OnJump += Jump;
            MarioEvents.OnCrouch += Crouch;

            MarioEvents.OnGetMushroom += Grow;
            MarioEvents.OnMarioDamage += Shrink;
            MarioEvents.OnFire += MarioFireProjectile;
            MarioEvents.OnGetFireFlower += FireState;

            MarioEvents.OnMarioDie += Die;

            MarioEvents.OnReset += Reset;

            spriteXPos = x;
            spriteYPos = y;
        }


        private void Reset(object sender, EventArgs e)
        {
            stateMachine.Reset();
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
            stateMachine.MoveLeft();
        }

        public void MoveRight(object sender, EventArgs e)
        {
            stateMachine.MoveRight();

        }

        public void Crouch(object sender, EventArgs e)
        {
            stateMachine.Crouch();
        }

        public void Jump(object sender, EventArgs e)
        {
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

        bool GetKeysPressed(KeyboardState keyState, params Keys[] keys)
        {
            bool result = false;
            for (int i = 0; i < keys.Length; i++)
            {
                result |= keyState.IsKeyDown(keys[i]);
            }
            return result;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.W) && newState.IsKeyDown(Keys.D))
            {
                spriteXPos += 3;
                spriteYPos -= 3;
            }
            else if (newState.IsKeyDown(Keys.W) && newState.IsKeyDown(Keys.A))
            {
                spriteXPos -= 3;
                spriteYPos -= 3;
            }
            else if (newState.IsKeyDown(Keys.S) && newState.IsKeyDown(Keys.D))
            {
                spriteXPos += 3;
                spriteYPos += 3;
            }
            else if (newState.IsKeyDown(Keys.S) && newState.IsKeyDown(Keys.A))
            {
                spriteXPos -= 3;
                spriteYPos += 3;
            }
            else if (newState.IsKeyDown(Keys.D))
            {
                spriteXPos += 3;
            }
            else if (newState.IsKeyDown(Keys.A))
            {
                spriteXPos -= 3;
            }
            else if (newState.IsKeyDown(Keys.W))
            {
                spriteYPos -= 3;
            }
            else if (newState.IsKeyDown(Keys.S))
            {
                spriteYPos += 3;
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
