using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class MarioStateMachine
    {
        private Mario mario;
        public enum MarioState { Small, Big, Fire };
        public enum MarioMovement { CrouchRight, CrouchLeft, IdleLeft, IdleRight, RunLeft, RunRight, JumpLeft, JumpRight, Die };
        private MarioState marioState = MarioState.Small;
        private MarioMovement marioMovement = MarioMovement.IdleRight;
        private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();

        private const float powerupTransitionLength = 2f;

        private bool canGrow = true;

        public bool isJumping = false;
        public bool isCollideFloor { get; set; }
        public bool isCollideUnder { get; set; }


        public bool isTouchingGround;
        
        //TIM: If this is > 0, Mario can't move or be hurt. This is for when he first gets a powerup.
        private float powerupTransitionTimeRemaining;

        private bool starPower = false;
        private int starDuration = 600;
        private int starCounter = 0;
        private int frameCount = 0;

        public bool StarPowered { get { return starPower; } }

        private Dictionary<MarioMovement, ISprite> smallStates = new Dictionary<MarioMovement, ISprite>();
        private Dictionary<MarioMovement, ISprite> bigStates = new Dictionary<MarioMovement, ISprite>();
        private Dictionary<MarioMovement, ISprite> fireStates = new Dictionary<MarioMovement, ISprite>();

        public MarioStateMachine(Mario player)
        {
            isCollideFloor = false;
            isCollideUnder = false;
            smallStates.Add(MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleLeft());
            smallStates.Add(MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight());
            smallStates.Add(MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunLeft());
            smallStates.Add(MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunRight());
            smallStates.Add(MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpLeft());
            smallStates.Add(MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpRight());
            smallStates.Add(MarioMovement.Die, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Die());

            bigStates.Add(MarioMovement.CrouchLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchLeft());
            bigStates.Add(MarioMovement.CrouchRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchRight());
            bigStates.Add(MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleLeft());
            bigStates.Add(MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight());
            bigStates.Add(MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunLeft());
            bigStates.Add(MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunRight());
            bigStates.Add(MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpLeft());
            bigStates.Add(MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpRight());

            fireStates.Add(MarioMovement.CrouchLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchLeft());
            fireStates.Add(MarioMovement.CrouchRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchRight());
            fireStates.Add(MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleLeft());
            fireStates.Add(MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight());
            fireStates.Add(MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunLeft());
            fireStates.Add(MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunRight());
            fireStates.Add(MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpLeft());
            fireStates.Add(MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpRight());

            mario = player;
        }

        public void Grow()
        {
            if (canGrow)
            {
                //Play growing transition
                //inPowerupTransition = true;

                marioState = MarioState.Big;
                currentSprite = bigStates[marioMovement];
            }
        }

        public void Shrink()
        {
            canGrow = true;
            if (marioState == MarioState.Fire)
                marioState = MarioState.Big;
            else
                marioState = MarioState.Small;
            if (marioMovement == MarioMovement.CrouchLeft)
                marioMovement = MarioMovement.IdleLeft;
            else if (marioMovement == MarioMovement.CrouchRight)
                marioMovement = MarioMovement.IdleRight;
            else if (marioMovement == MarioMovement.Die)
                marioMovement = MarioMovement.IdleRight;
            currentSprite = smallStates[marioMovement];
        }
        public void DamageMario()
        {
            if (starPower)
                return;
            switch (marioState)
            {
                case MarioState.Big:
                    marioState = MarioState.Small;
                    break;
                case MarioState.Fire:
                    marioState = MarioState.Big;
                    break;
                case MarioState.Small:
                    mario.Die();
                    break;
            }
        }
        public void SetFireState()
        {
            canGrow = false;
            marioState = MarioState.Fire;
            currentSprite = fireStates[marioMovement];
        }

        public static void GetFireflower()
        {
            // Perhaps used for a transitional state
        }

        private void SwitchCurrentSprite(MarioMovement newMovement)
        {
            switch (marioState)
            {
                case MarioState.Big:
                    currentSprite = bigStates[newMovement];
                    break;
                case MarioState.Fire:
                    currentSprite = fireStates[newMovement];
                    break;
                case MarioState.Small:
                    currentSprite = smallStates[newMovement];
                    break;
            }
        }

        public void SetIdleState()
        {
            
            
                if (marioMovement == MarioMovement.RunRight || (marioMovement == MarioMovement.JumpRight && mario.isCollideGround))
                {
                    marioMovement = MarioMovement.IdleRight;
                }
                else if (marioMovement == MarioMovement.RunLeft || (marioMovement == MarioMovement.JumpLeft && mario.isCollideGround))
                {
                    marioMovement = MarioMovement.IdleLeft;
                }
                SwitchCurrentSprite(marioMovement);
            
        }
        public void MoveLeft()
        {
            if(!(marioMovement == MarioMovement.JumpLeft || marioMovement == MarioMovement.JumpRight))
                marioMovement = MarioMovement.RunLeft;
            SwitchCurrentSprite(marioMovement);
        }

        public void MoveRight()
        {
            if(!(marioMovement == MarioMovement.JumpLeft || marioMovement == MarioMovement.JumpRight)) 
                marioMovement = MarioMovement.RunRight;
            SwitchCurrentSprite(marioMovement);
        }
        public void HandleCrouch() // Crouching has commented code as we removed this functionality until physics are implemented properly
        {
            if ((marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft) && marioState != MarioState.Small)
            {
                //marioMovement = MarioMovement.CrouchLeft;
                return;
            }
            else if ((marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight) && marioState != MarioState.Small)
            {
                //marioMovement = MarioMovement.CrouchRight;
                return;
            }
            switch (marioMovement)
            {
                case MarioMovement.JumpLeft:
                    marioMovement = MarioMovement.IdleLeft;
                    break;
                case MarioMovement.JumpRight:
                    marioMovement = MarioMovement.IdleRight;
                    break;

            }
        }
        public void Crouch()
        {
            HandleCrouch();
            SwitchCurrentSprite(marioMovement);
        }
        public void HandleJump()
        {
            //switch (mariomovement)
            //{
            //    case mariomovement.crouchleft:
            //        mariomovement = mariomovement.idleleft;
            //        break;
            //    case mariomovement.crouchright:
            //        mariomovement = mariomovement.idleright;
            //        break;
            //}

            if (marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft)
            {
                marioMovement = MarioMovement.JumpLeft;
            }
            else if (marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight)
            {
                marioMovement = MarioMovement.JumpRight;

            }

            SwitchCurrentSprite(marioMovement);

        }

        public void Fall()
        {
            if(!isCollideFloor)
                mario.SetPosition(mario.Bounds.X, mario.Bounds.Y + 7);
            else
            {
                isCollideUnder = false;
                EndJump();
            }
        }

        public void EndJump()
        {

            isJumping = false;
            //SwitchCurrentSprite(marioMovement);
        }
        public void Jump()
        {
            if(!isJumping)
            HandleJump();
            isJumping = true;
            SwitchCurrentSprite(marioMovement);
        }

        public void JumpEnemy()
        {
            //jumpCounter = 0;
            Jump();
        }
        public void Star()
        {
            starPower = true;
        }
        public bool isStar()
        {
            return starPower;
        }
        private void HandleStarPower()
        {
            if (starCounter < starDuration)
            {
                starCounter++;
            }
            else
            {
                starPower = false;
                starCounter = 0;
            }
        }
        private void DrawStarMario(SpriteBatch spriteBatch)
        {
            if (frameCount < 3)
            {
                currentSprite.Draw(spriteBatch, Color.LightGreen);
            }
            else if (frameCount < 6)
            {
                currentSprite.Draw(spriteBatch, Color.MediumVioletRed);
            }
            else if (frameCount < 9)
            {
                currentSprite.Draw(spriteBatch, Color.Black);
            }
            else
            {
                currentSprite.Draw(spriteBatch);
            }
            frameCount++;
            if (frameCount > 12)
                frameCount = 0;

        }

        public void KillMario()
        {
            marioMovement = MarioMovement.Die;
            currentSprite = smallStates[marioMovement];
        }

        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (starPower)
            {
                HandleStarPower();
            }

            if (mario.isCollideGround)
            {
                isJumping = false;
                if (marioMovement == MarioMovement.JumpLeft)
                    marioMovement = MarioMovement.RunLeft;
                else if (marioMovement == MarioMovement.JumpRight)
                    marioMovement = MarioMovement.RunRight;
                SwitchCurrentSprite(marioMovement);
            }

           
            currentSprite.Update(gameTime, spriteXPos, spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (starPower)
            {
                DrawStarMario(spriteBatch);
            }
            else
            {
                currentSprite.Draw(spriteBatch);
            }
        }

        public ISprite CurrentSprite
        {
            get { return currentSprite; }
        }
        public MarioState CurrentState
        {
            get { return marioState; }
        }
        public MarioMovement CurrentMovement
        {
            get { return marioMovement; }
        }

        public void Reset()
        {
            canGrow = true;
            starPower = false;
            isJumping = false;
            //jumpCounter = 0;
            marioState = MarioState.Small;
            marioMovement = MarioMovement.IdleRight;
            currentSprite = smallStates[marioMovement];
        }
    }
}