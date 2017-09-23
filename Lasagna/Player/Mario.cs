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

        public class MarioStateMachine
        {
            private enum MarioState { Small, Big, Fire, Star, Dead };
            private enum MarioMovement { CrouchedRight, CrouchedLeft, StillLeft, StillRight, WalkingLeft, WalkingRight, RunningLeft, RunningRight, JumpingLeft, JumpingRight };
            private MarioState marioState = MarioState.Small;
            private MarioMovement marioMovement = MarioMovement.StillRight;
            private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();

            private Dictionary<String, ISprite> smallStates = new Dictionary<string, ISprite>();
            private Dictionary<String, ISprite> bigStates = new Dictionary<string, ISprite>();
            private Dictionary<String, ISprite> fireStates = new Dictionary<string, ISprite>();
            private Dictionary<String, ISprite> starStates = new Dictionary<string, ISprite>();



            /// <summary>
            /// TODO: HANDLE SPRITE CHANGES VIA CHANGE SPRITE METHOD
            /// </summary>


            public MarioStateMachine()
            {
                smallStates.Add("IdleLeft",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleLeft());
                smallStates.Add("IdleRight",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight());
                smallStates.Add("RunLeft",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunLeft());
                smallStates.Add("RunRight",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunRight());
                smallStates.Add("JumpLeft",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpLeft());
                smallStates.Add("JumpRight",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpRight());
                smallStates.Add("Die",MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Die());
            
                bigStates.Add("CrouchLeft",MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchLeft()); 
                bigStates.Add("CrouchRight",MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchRight()); 
                bigStates.Add("IdleLeft",MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleLeft()); 
                bigStates.Add("IdleRight",MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight()); 
                bigStates.Add("RunLeft",MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunLeft());
                bigStates.Add("RunRight",MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunRight()); 
                bigStates.Add("JumpLeft",MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpLeft()); 
                bigStates.Add("JumpRight",MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpRight()); 


                fireStates.Add("CrouchLeft",MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchLeft()); 
                fireStates.Add("CrouchRight",MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchRight());
                fireStates.Add("IdleLeft",MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleLeft());
                fireStates.Add("IdleRight",MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight());
                fireStates.Add("RunLeft",MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunLeft());
                fireStates.Add("RunRight",MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunRight());
                fireStates.Add("JumpLeft",MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpLeft());
                fireStates.Add("JumpRight",MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpRight());

            }

            public void Grow()
            {
                marioState = MarioState.Big;
                currentSprite = bigStates["IdleRight"];
            }

            public void Fire()
            {
                marioState = MarioState.Fire;
                currentSprite = fireStates["IdleRight"];
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

                        currentSprite = bigStates["IdleLeft"];
                        marioMovement = MarioMovement.StillLeft;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingLeft;

                        currentSprite = bigStates["RunLeft"];
                    }
                }
                else if (marioState == MarioState.Fire)
                {

                    if (marioMovement == MarioMovement.WalkingLeft)
                    {

                        currentSprite = fireStates["IdleLeft"];
                        marioMovement = MarioMovement.StillLeft;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingLeft;

                        currentSprite = fireStates["RunLeft"];
                    }
                }
                else if (marioState == MarioState.Small)
                {

                    if (marioMovement == MarioMovement.WalkingLeft)
                    {

                        currentSprite = smallStates["IdleLeft"];
                        marioMovement = MarioMovement.StillLeft;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingLeft;

                        currentSprite = smallStates["RunLeft"];
                    }
                }

            }

            public void MoveRight()
            {
                if (marioState == MarioState.Dead)
                    return;
                if (marioState == MarioState.Big)
                {

                    if (marioMovement == MarioMovement.WalkingRight)
                    {

                        currentSprite = bigStates["IdleRight"];
                        marioMovement = MarioMovement.StillRight;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingRight;

                        currentSprite = bigStates["RunRight"];
                    }
                }
                else if (marioState == MarioState.Fire)
                {

                    if (marioMovement == MarioMovement.WalkingRight)
                    {

                        currentSprite = fireStates["IdleRight"];
                        marioMovement = MarioMovement.StillRight;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingRight;

                        currentSprite = fireStates["RunRight"];
                    }
                }
                else if (marioState == MarioState.Small)
                {

                    if (marioMovement == MarioMovement.WalkingRight)
                    {

                        currentSprite = smallStates["IdleRight"];
                        marioMovement = MarioMovement.StillRight;
                    }
                    else
                    {
                        marioMovement = MarioMovement.WalkingRight;

                        currentSprite = smallStates["RunRight"];
                    }
                }

            }

            public void Crouch()
            {
                if (marioState == MarioState.Dead)
                    return;
               
                if (marioMovement == MarioMovement.JumpingRight || marioMovement == MarioMovement.JumpingLeft)
                {
                  
                    if (marioState == MarioState.Big)
                    {
                        if (marioMovement == MarioMovement.JumpingRight)
                        {
                            currentSprite = bigStates["IdleRight"];
                            marioMovement = MarioMovement.StillRight;
                        }
                        else if (marioMovement == MarioMovement.JumpingLeft)
                        {
                            currentSprite = bigStates["IdleLeft"];
                            marioMovement = MarioMovement.StillLeft;
                        }
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        if (marioMovement == MarioMovement.JumpingRight)
                        {
                            currentSprite = fireStates["IdleRight"];
                            marioMovement = MarioMovement.StillRight;
                        }
                        else if (marioMovement == MarioMovement.JumpingLeft)
                        {
                            currentSprite = fireStates["IdleLeft"];
                            marioMovement = MarioMovement.StillLeft;
                        }
                    }
                    else if (marioState == MarioState.Small)
                    {
                        if (marioMovement == MarioMovement.JumpingRight)
                        {
                            currentSprite = smallStates["IdleRight"];
                            marioMovement = MarioMovement.StillRight;
                        }
                        else if (marioMovement == MarioMovement.JumpingLeft)
                        {
                            currentSprite = smallStates["IdleLeft"];
                            marioMovement = MarioMovement.StillLeft;
                        }
                    }
                    return;
                }

                else
                {
                    if (marioMovement == MarioMovement.WalkingLeft || marioMovement == MarioMovement.StillLeft)
                    {
                        marioMovement = MarioMovement.CrouchedLeft;
                        if (marioState == MarioState.Big)
                        {
                            currentSprite = bigStates["CrouchLeft"];
                        }
                        else if (marioState == MarioState.Fire)
                        {
                            currentSprite = fireStates["CrouchLeft"];
                     
                        }
                        else if (marioState == MarioState.Small)
                        {
                            return;
                        }
                        return;
                    }

                    else if (marioMovement == MarioMovement.WalkingRight || marioMovement == MarioMovement.StillRight)
                    {
                        marioMovement = MarioMovement.CrouchedRight;
                        if (marioState == MarioState.Big)
                        {
                            currentSprite = bigStates["CrouchRight"];
                        }
                        else if (marioState == MarioState.Fire)
                        {
                            currentSprite = fireStates["CrouchRight"];
                        }
                        else if (marioState == MarioState.Small)
                        {
                            return;
                        }
                        return;
                    }
                }
                
            }

            public void Jump()
            {
                if(marioState == MarioState.Dead)
                    return;

                if (marioMovement == MarioMovement.JumpingLeft || marioMovement == MarioMovement.JumpingRight)
                    return;

                if (marioMovement == MarioMovement.WalkingLeft || marioMovement == MarioMovement.StillLeft)
                {
                    marioMovement = MarioMovement.JumpingLeft;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = bigStates["JumpLeft"];
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = fireStates["JumpLeft"];
                    }
                    else if (marioState == MarioState.Small)
                    {
                            currentSprite = smallStates["JumpLeft"];
                    }
                    return;
                }

                else if (marioMovement == MarioMovement.WalkingRight || marioMovement == MarioMovement.StillRight)
                {
                    marioMovement = MarioMovement.JumpingRight;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = bigStates["JumpRight"];
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = fireStates["JumpRight"];
                    }
                    else if (marioState == MarioState.Small)
                    {
                        currentSprite = smallStates["JumpRight"];
                    }
                    return;
                }

                else if (marioState != MarioState.Small && marioMovement == MarioMovement.CrouchedRight)
                {
                    marioMovement = MarioMovement.StillRight;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = bigStates["IdleRight"];
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = fireStates["IdleRight"];
                    }
                    return;
                }

                else if(marioState != MarioState.Small && marioMovement == MarioMovement.CrouchedLeft)
                {
                    marioMovement = MarioMovement.StillLeft;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = bigStates["IdleLeft"];
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = fireStates["IdleLeft"];
                    }
                }


            }

            public void Shrink()
            {
                marioState = MarioState.Small;
                currentSprite = smallStates["IdleRight"];
            }

            public void Star()
            {
                marioState = MarioState.Star;
            }

            public void KillMario()
            {
                currentSprite = smallStates["Die"];
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
                currentSprite = smallStates["IdleRight"];
                marioState = MarioState.Small;
                marioMovement = MarioMovement.StillRight;
        }
        }
    }
}
