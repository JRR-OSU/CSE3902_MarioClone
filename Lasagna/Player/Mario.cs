using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


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

            spriteXPos = x;
            spriteYPos = y;
 
        }

        // Later
        public void MarioFireProjectile()
        {
            stateMachine.MarioFireProjectile();
        }

        // Later
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

        public class MarioStateMachine
        {
            private bool facingLeft = true;

            private enum MarioState { Small, Big, Fire, Star, Dead };
            private enum MarioMovement { Crouched, StillLeft, StillRight, WalkingLeft, WalkingRight, RunningLeft, RunningRight, JumpingLeft, JumpingRight };
            private MarioState currentState = MarioState.Small;
            private MarioMovement marioMovement = MarioMovement.StillRight;

            private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();



            // individual state values listed in class

            // potential values

            // States:
            // jumping, direction, deadMario, fireMario, star
            // 




            // Logic to be implemented later
            public void ChangeDirection()
            {
                if (marioMovement == MarioMovement.WalkingLeft)
                {
                    marioMovement = MarioMovement.WalkingRight;
                }
                else if (marioMovement == MarioMovement.WalkingRight)
                {
                    marioMovement = MarioMovement.WalkingLeft;
                }


                if (marioMovement == MarioMovement.JumpingLeft)
                {
                    marioMovement = MarioMovement.JumpingRight;
                }
                else if (marioMovement == MarioMovement.JumpingRight)
                {
                    marioMovement = MarioMovement.JumpingLeft;
                }

                if (marioMovement == MarioMovement.StillLeft)
                {
                    marioMovement = MarioMovement.StillRight;
                }
                else if (marioMovement == MarioMovement.StillRight)
                {
                    marioMovement = MarioMovement.StillLeft;
                }
                
            }




            /// <summary>
            /// TODO: HANDLE SPRITE CHANGES VIA CHANGE SPRITE METHOD
            /// </summary>
            public void Grow()
            {
                currentState = MarioState.Big;
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
            }


            // Perhaps make this a toggle method?
            public void Fire()
            {

                currentState = MarioState.Fire;
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight();

            }


            public void MarioFireProjectile()
            {

            }

        // TRANSITIONAL STATE TO FIRE ?
            public void GetFireflower()
            {
                
            }
                
            public void MoveLeft()
            {
                if (currentState == MarioState.Dead)
                    return;
                marioMovement = MarioMovement.WalkingLeft;
           
                    currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunLeft();
                
               
            }

            public void MoveRight()
            {
                if (currentState == MarioState.Dead)
                    return;

                marioMovement = MarioMovement.WalkingRight;
             
                    currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunRight();
               
            }


            public void Crouch()
            {
                if (currentState == MarioState.Dead)
                    return;

                    if (marioMovement == MarioMovement.JumpingLeft || marioMovement == MarioMovement.JumpingRight){
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
                        marioMovement = MarioMovement.StillRight;
                        return;
                    }
                marioMovement = MarioMovement.Crouched;
                if (marioMovement == MarioMovement.JumpingLeft || marioMovement == MarioMovement.JumpingRight)
                    currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchRight();

             
            }


            public void Jump()
            {
                if(currentState == MarioState.Dead)
                    return;

                if(marioMovement == MarioMovement.Crouched)
                {
                    currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
                    marioMovement = MarioMovement.StillRight;
                    return;
                }



                if (marioMovement != MarioMovement.JumpingLeft || marioMovement != MarioMovement.JumpingRight)
                    currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpRight();

                marioMovement = MarioMovement.JumpingRight;
            }



            public void Shrink()
            {
                currentState = MarioState.Small;
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
                // Make mario's sprite shink to the small size
            }

            public void Star()
            {
                currentState = MarioState.Star;
                // Create star sprite instance
            }


            public void KillMario()
            {
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Die();
                currentState = MarioState.Dead;
                
                
            }

            public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
            {
                currentSprite.Update(gameTime, spriteXPos, spriteYPos);
                // if-else logic based on the current values of facingLeft and health to determine how to move

                // if mario is dead, can't run left or right
                
               
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                currentSprite.Draw(spriteBatch);
            }

        }
    }
}
