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

        

        
        /// <summary>
        /// These methods will just change state, the state machine will handle sprite changes
        /// </summary>
        public Mario()
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
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public class MarioStateMachine
        {
            private bool facingLeft = true;

            private enum MarioState { Small, Big, Fire, Star, Dead };
            private enum MarioMovement { Crouched, StillLeft, StillRight, WalkingLeft, WalkingRight, RunningLeft, RunningRight, JumpingLeft, JumpingRight };
            private MarioState currentState = MarioState.Small;
            private MarioMovement marioMovement = MarioMovement.StillRight;

            private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();

            


            // Question for tim: Do I subscribe to each specific event as a field, or can I just pass a reference to the Mario events class and somehow subscribe to all events?



            // individual state values listed in class

            // potential values

            // States:
            // jumping, direction, deadMario, fireMario, star
            // 


            private void ChangeCurrentSprite(SpriteType newSpriteType)
            {
                ISprite oldSprite = currentSprite;

                switch (newSpriteType)
                {
                    case SpriteType.NoMoveAndNoAnimation:
                     //   currentSprite = noMoveAndNoAnimSprite;
                        break;

                    case SpriteType.NoMoveAndAnimation:
                      //  currentSprite = noMoveAndAnimSprite;
                        break;

                    case SpriteType.MoveAndNoAnimation:
                       // currentSprite = moveAndNoAnimSprite;
                        break;

                    case SpriteType.MoveAndAnimation:
                       // currentSprite = moveAndAnimSprite;
                        break;

                    default:
                        Debug.WriteLine("Invalid sprite type passed to ChangeCurrentSprite.");
                        break;
                }

                //If sprite was changed, reset current sprite. This resets position and animation
               // if (oldSprite != currentSprite)
                   // currentSprite.ResetSprite(screenWidth, screenHeight);
            }


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
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();

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
                marioMovement = MarioMovement.WalkingLeft;
            }

            public void MoveRight()
            {
                marioMovement = MarioMovement.WalkingRight;
            }


            public void Crouch()
            {
               marioMovement = MarioMovement.Crouched;
            }


            public void Jump()
            {
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
                currentState = MarioState.Dead;
                //currentSprite = MarioSpriteFactory.Instance.Crea
            }

            public void Update(GameTime time)
            {
                // if-else logic based on the current values of facingLeft and health to determine how to move
            }


        }
    }
}
