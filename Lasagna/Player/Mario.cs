using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Lasagna
{
    public class Mario : IPlayer
    {
        private MarioStateMachine stateMachine;

        private int spriteXPos;
        private int spriteYPos;
        
        /// <summary>
        /// These methods will just change state, the state machine will handle sprite changes
        /// </summary>
        public Mario(int x, int y)
        {
            stateMachine = new MarioStateMachine();
           
            MarioEvents.OnMoveLeft += MoveLeft;
            MarioEvents.OnMoveRight += MoveRight;
            MarioEvents.OnJump += Jump;
            MarioEvents.OnCrouch += Crouch;

            MarioEvents.OnGetMushroom += Grow;
            MarioEvents.OnMarioDamage += Shrink;
            MarioEvents.OnFire += MarioFireProjectile;
            MarioEvents.OnGetFireFlower += FireState;

            MarioEvents.OnMarioDie +=Die;

            MarioEvents.OnReset += Reset;

            spriteXPos = x;
            spriteYPos = y;
 
        }

        private void Reset()
        {
            stateMachine.Reset();
        }

        public void MarioFireProjectile()
        {
            stateMachine.MarioFireProjectile();
        }

        public void GetFireflower()
        {
            stateMachine.GetFireflower();
        }
     
        public void MoveLeft()
        {
            stateMachine.MoveLeft();
        }

        public void MoveRight()
        {
            stateMachine.MoveRight();
        }


        public void Crouch()
        {
           stateMachine.Crouch();
        }


        public void Jump()
        {
            stateMachine.Jump();
        }

        public void Grow()
        {
            stateMachine.Grow();
        }

        public void FireState()
        {
            stateMachine.Fire();
        }

        public void Shrink()
        {
            stateMachine.Shrink();
        }

        public void Star()
        {
            stateMachine.Star();
        }

        public void Die()
        {
            stateMachine.KillMario();
        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Update(gameTime, spriteXPos, spriteYPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }

        
    }
}
