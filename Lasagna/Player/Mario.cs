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

        public class MarioStateMachine
        {
            private enum MarioState { Small, Big, Fire, Star, Dead };
            private enum MarioMovement { Crouched, StillLeft, StillRight, WalkingLeft, WalkingRight, RunningLeft, RunningRight, JumpingLeft, JumpingRight };
            private MarioState marioState = MarioState.Small;
            private MarioMovement marioMovement = MarioMovement.StillRight;

            private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();

            /// <summary>
            /// TODO: HANDLE SPRITE CHANGES VIA CHANGE SPRITE METHOD
            /// </summary>
            public void Grow()
            {
                marioState = MarioState.Big;
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
            }

            public void Fire()
            {
                marioState = MarioState.Fire;
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight();
            }

            public void MarioFireProjectile()
            {
              
            }
        
            public void GetFireflower()
            {
                // Perhaps used for a transitional state
            }
                
            public void MoveLeft()
            {
                if (marioState == MarioState.Dead)
                    return;
                if (marioState == MarioState.Big)
                {

                    if (marioMovement == MarioMovement.WalkingLeft)
                    {

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleLeft();
                        marioMovement = MarioMovement.StillLeft;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingLeft;

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunLeft();
                    }
                }
                else if (marioState == MarioState.Fire)
                {

                    if (marioMovement == MarioMovement.WalkingLeft)
                    {

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleLeft();
                        marioMovement = MarioMovement.StillLeft;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingLeft;

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunLeft();
                    }
                }
                else if (marioState == MarioState.Small)
                {

                    if (marioMovement == MarioMovement.WalkingLeft)
                    {

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleLeft();
                        marioMovement = MarioMovement.StillLeft;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingLeft;

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunLeft();
                    }
                }

            }

            public void MoveRight()
            {
                if (marioState == MarioState.Dead)
                    return;
                if (marioState == MarioState.Big)
                {

                    if (marioMovement == MarioMovement.WalkingLeft)
                    {

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
                        marioMovement = MarioMovement.StillRight;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingRight;

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunRight();
                    }
                }
                else if (marioState == MarioState.Fire)
                {

                    if (marioMovement == MarioMovement.WalkingRight)
                    {

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight();
                        marioMovement = MarioMovement.StillRight;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingRight;

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunRight();
                    }
                }
                else if (marioState == MarioState.Small)
                {

                    if (marioMovement == MarioMovement.WalkingRight)
                    {

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
                        marioMovement = MarioMovement.StillRight;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingRight;

                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunRight();
                    }
                }

            }

            public void Crouch()
            {
                if (marioState == MarioState.Dead)
                    return;
                if (marioMovement == MarioMovement.JumpingRight)
                {
                    marioMovement = MarioMovement.StillRight;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight();
                    }
                    else if (marioState == MarioState.Small)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
                    }
                }
                else
                {
                        marioMovement = MarioMovement.Crouched;                     
                        if (marioState == MarioState.Fire)
                        {
                            currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchRight();
                        }
                        else
                        {
                            currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchRight();
                        }
                    return; 
                }

            }

            public void Jump()
            {
                if(marioState == MarioState.Dead)
                    return;

                if (marioMovement == MarioMovement.WalkingLeft)
                {
                    marioMovement = MarioMovement.JumpingLeft;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpLeft();
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpLeft();
                    }
                    else if (marioState == MarioState.Small)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpLeft();
                    }
                    return;
                }

                if (marioMovement != MarioMovement.Crouched)
                {
                    marioMovement = MarioMovement.JumpingRight;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpRight();
                    }
                    else if(marioState == MarioState.Fire)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpRight();
                    }
                    else if(marioState == MarioState.Small)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpRight();
                    }

                }
                else
                {
                    marioMovement = MarioMovement.StillRight;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight();
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight();
                    }
                    else if (marioState == MarioState.Small)
                    {
                        currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
                    }
                }


            }

            public void Shrink()
            {
                marioState = MarioState.Small;
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
            }

            public void Star()
            {
                marioState = MarioState.Star;
            }

            public void KillMario()
            {
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Die();
                marioState = MarioState.Dead;
            }

            public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
            {
                currentSprite.Update(gameTime, spriteXPos, spriteYPos);
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                currentSprite.Draw(spriteBatch);
            }

            public void Reset()
            {
                currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
                marioState = MarioState.Small;
                marioMovement = MarioMovement.StillRight;
        }
        }
    }
}
