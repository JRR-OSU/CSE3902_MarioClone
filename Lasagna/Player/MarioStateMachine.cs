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
        public enum MarioMovement { CrouchRight, CrouchLeft, IdleLeft, IdleRight, RunLeft, RunRight, TurnLeft, TurnRight, JumpLeft, JumpRight, GrowLeft, GrowRight, ShrinkLeft, ShrinkRight, Die };
        private MarioState marioState = MarioState.Small;
        private MarioMovement marioMovement = MarioMovement.IdleRight;
        private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();

        //How long our death animation is
        private const float deathAnimLength = 4f;
        //How fast we move during death anim
        private const float deathAnimSpeed = 200f;
        //Used for fire powerup, which just switches colors
        private const float firePowerupTransitionLength = 2f;
        //Animation colors used for fireflower powerup anim
        private readonly Color[] firePowerupTransitionColors = new[]
        {
            Color.White,
            Color.Green,
            Color.PaleVioletRed,
            new Color(0.3f, 0.3f, 0.3f), //Black
            Color.White,
            Color.Green,
            Color.PaleVioletRed,
            new Color(0.3f, 0.3f, 0.3f),
            Color.White,
            Color.Green,
            Color.PaleVioletRed,
            new Color(0.3f, 0.3f, 0.3f),
            Color.White,
            Color.Green,
            Color.PaleVioletRed,
            new Color(0.3f, 0.3f, 0.3f)
        };
        //How long mario blinks for after beign damaged
        private const float blinkLength = 3f;

        private bool canGrow = true;

        public bool isJumping = false;
        public bool isCollideFloor { get; set; }
        public bool isCollideUnder { get; set; }


        public bool isTouchingGround;

        //If this > 0 mario ignores collisions and can't be hurt. 
        private float blinkTimeRemaining;
        private bool blinkShow;
        //If this is > 0, Mario can't move or be hurt and ignores collisions. This is for when he's growing or shrinking.
        private float stateTransitionTimeRemaining;
        //Current state transition color
        private Color stateTransitionColor = Color.White;
        private float deathAnimTimeRemaining;

        private bool starPower = false;
        private int starDuration = 600;
        private int starCounter = 0;
        private int frameCount = 0;

        private int turnFrames = 0;

        public bool StarPowered { get { return starPower; } }
        public bool IsTransitioning { get { return stateTransitionTimeRemaining > 0; } }
        public bool IsBlinking { get { return blinkTimeRemaining > 0; } }

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
            smallStates.Add(MarioMovement.TurnLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_TurnLeft());
            smallStates.Add(MarioMovement.TurnRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_TurnRight());
            smallStates.Add(MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpLeft());
            smallStates.Add(MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpRight());
            smallStates.Add(MarioMovement.Die, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Die());
            smallStates.Add(MarioMovement.ShrinkLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_ShrinkLeft());
            smallStates.Add(MarioMovement.ShrinkRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_ShrinkRight());

            bigStates.Add(MarioMovement.CrouchLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchLeft());
            bigStates.Add(MarioMovement.CrouchRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchRight());
            bigStates.Add(MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleLeft());
            bigStates.Add(MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight());
            bigStates.Add(MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunLeft());
            bigStates.Add(MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunRight());
            bigStates.Add(MarioMovement.TurnLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_TurnLeft());
            bigStates.Add(MarioMovement.TurnRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_TurnRight());
            bigStates.Add(MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpLeft());
            bigStates.Add(MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpRight());
            bigStates.Add(MarioMovement.GrowLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_GrowLeft());
            bigStates.Add(MarioMovement.GrowRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_GrowRight());

            fireStates.Add(MarioMovement.CrouchLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchLeft());
            fireStates.Add(MarioMovement.CrouchRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchRight());
            fireStates.Add(MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleLeft());
            fireStates.Add(MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight());
            fireStates.Add(MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunLeft());
            fireStates.Add(MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunRight());
            fireStates.Add(MarioMovement.TurnLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_TurnLeft());
            fireStates.Add(MarioMovement.TurnRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_TurnRight());
            fireStates.Add(MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpLeft());
            fireStates.Add(MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpRight());

            mario = player;
        }

        private bool MarioFacingLeft()
        {
            return (marioMovement == MarioMovement.CrouchLeft || marioMovement == MarioMovement.IdleLeft
                    || marioMovement == MarioMovement.JumpLeft || marioMovement == MarioMovement.RunLeft);
        }

        public void Grow()
        {
            if (canGrow)
            {
                marioState = MarioState.Big;
                //If facing left, set movement to be transition left.
                marioMovement = (MarioFacingLeft()) ? MarioMovement.GrowLeft : MarioMovement.GrowRight;
                UpdateSprite();

                //Play growing transition
                stateTransitionTimeRemaining = currentSprite.ClipLength;
            }
            canGrow = false;
        }

        public void Shrink()
        {
            canGrow = true;

            if (marioState == MarioState.Small)
                mario.Die();
            else
            {
                if (marioState == MarioState.Fire)
                {
                    marioState = MarioState.Big;
                    UpdateSprite();
                }
                else
                {
                    //If facing left, set movement to be transition left.
                    marioMovement = (MarioFacingLeft()) ? MarioMovement.ShrinkLeft : MarioMovement.ShrinkRight;
                    marioState = MarioState.Small;
                    UpdateSprite();
                }

                blinkTimeRemaining = blinkLength;
                stateTransitionTimeRemaining = (marioState == MarioState.Big) ? firePowerupTransitionLength : currentSprite.ClipLength;
            }
        }

        public void DamageMario()
        {
            if (!starPower && stateTransitionTimeRemaining <= 0 && blinkTimeRemaining <= 0)
                Shrink();
        }

        public void SetFireState()
        {
            canGrow = false;
            marioState = MarioState.Fire;
            currentSprite = fireStates[marioMovement];

            //Play transition
            stateTransitionTimeRemaining = firePowerupTransitionLength;
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

        public void SetGroundedState()
        {
            if (marioMovement == MarioMovement.JumpRight && !(marioMovement == MarioMovement.TurnRight))
            {
                if(mario.isRunning || mario.moveRight)
                    marioMovement = MarioMovement.RunRight;
                else
                    marioMovement = MarioMovement.IdleRight;
            }
            else if (marioMovement == MarioMovement.JumpLeft && !(marioMovement == MarioMovement.TurnLeft))
            {
                if (mario.isRunning || mario.moveLeft)
                    marioMovement = MarioMovement.RunLeft;
                else
                    marioMovement = MarioMovement.IdleLeft;
            }
            SwitchCurrentSprite(marioMovement);

        }
        public void MoveLeft()
        {
            if ((marioMovement == MarioMovement.JumpLeft || marioMovement == MarioMovement.JumpRight))
                return;
            else if (marioMovement == MarioMovement.RunRight && mario.isRunning)
            {
                marioMovement = MarioMovement.TurnLeft;
                turnFrames++;
            }
            else if (turnFrames == 0)
            {
                marioMovement = MarioMovement.RunLeft;
            }
            SwitchCurrentSprite(marioMovement);
        }

        public void MoveRight()
        {
            if ((marioMovement == MarioMovement.JumpLeft || marioMovement == MarioMovement.JumpRight))
                return;
            else if (marioMovement == MarioMovement.RunLeft && mario.isRunning)
            {
                marioMovement = MarioMovement.TurnRight;
                turnFrames++;
            }
            else if (turnFrames == 0)
            {
                marioMovement = MarioMovement.RunRight;
            }
            SwitchCurrentSprite(marioMovement);
        }


        private void HandleTurnFrames()
        {
            if (marioMovement == MarioMovement.TurnLeft || marioMovement == MarioMovement.TurnRight)
            {
                turnFrames++; 
            }
            if (turnFrames > 10)
            {
                turnFrames=0;
                if (marioMovement == MarioMovement.TurnLeft)
                    marioMovement = MarioMovement.RunLeft;
                else if (marioMovement == MarioMovement.TurnRight)
                    marioMovement = MarioMovement.RunRight;
                SwitchCurrentSprite(marioMovement);
            }
        }

        public void HandleCrouch() // Crouching has commented code as we removed this functionality until physics are implemented properly
        {
            if ((marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft) && marioState != MarioState.Small &&!(isJumping))
            {
                marioMovement = MarioMovement.CrouchLeft;
                return;
            }
            else if ((marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight) && marioState != MarioState.Small && !(isJumping))
            {
                marioMovement = MarioMovement.CrouchRight;
                return;
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
            if (!isCollideFloor)
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
            if (!isJumping)
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
            deathAnimTimeRemaining = deathAnimLength;
            currentSprite = smallStates[marioMovement];
        }

        private void FinishStateTransition()
        {
            //Gain mushroom
            if (marioState == MarioState.Big
                && (marioMovement == MarioMovement.GrowLeft || marioMovement == MarioMovement.GrowRight))
            {
                marioMovement = (marioMovement == MarioMovement.GrowRight) ? MarioMovement.IdleRight : MarioMovement.IdleLeft;
                UpdateSprite();
            }
            //Damaged to small
            else if (marioState == MarioState.Small
                && (marioMovement == MarioMovement.ShrinkLeft || marioMovement == MarioMovement.ShrinkRight))
            {
                marioMovement = (marioMovement == MarioMovement.ShrinkRight) ? MarioMovement.IdleRight : MarioMovement.IdleLeft;
                UpdateSprite();
            }
        }

        private void UpdateSprite()
        {
            if (marioState == MarioState.Small && smallStates.ContainsKey(marioMovement))
                currentSprite = smallStates[marioMovement];
            else if (marioState == MarioState.Big && bigStates.ContainsKey(marioMovement))
                currentSprite = bigStates[marioMovement];
            else if (marioState == MarioState.Fire && fireStates.ContainsKey(marioMovement))
                currentSprite = fireStates[marioMovement];
        }

        private void HandleDeathAnimation(GameTime gameTime)
        {
            if (marioMovement != MarioMovement.Die)
                return;

            deathAnimTimeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            float move = deathAnimSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Move up from 5%-15%
            if (deathAnimTimeRemaining < 0.9167f * deathAnimLength && deathAnimTimeRemaining >= 0.74999f * deathAnimLength)
                mario.ForceMove(0, -move);
            //Then move down from 30%-100%
            else if (deathAnimTimeRemaining < 0.74999f * deathAnimLength)
                mario.ForceMove(0, move);

            //When we finish animation, reset level.
            if (deathAnimTimeRemaining <= 0)
                MarioEvents.Reset(this, EventArgs.Empty);
        }

        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (marioMovement != MarioMovement.Die)
            {
                //If we're transitioning, handle transition timer.
                if (stateTransitionTimeRemaining > 0)
                {
                    stateTransitionTimeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //To or from fire mario change colors
                    if (marioState == MarioState.Fire || (marioState == MarioState.Big && (marioMovement != MarioMovement.GrowLeft && marioMovement != MarioMovement.GrowRight)))
                    {
                        int fireFrame = (int)((stateTransitionTimeRemaining / firePowerupTransitionLength) * firePowerupTransitionColors.Length);
                        stateTransitionColor = firePowerupTransitionColors[fireFrame];
                    }
                    //Else just draw white
                    else
                        stateTransitionColor = Color.White;

                    if (stateTransitionTimeRemaining <= 0)
                        FinishStateTransition();
                }

                if (blinkTimeRemaining > 0)
                {
                    blinkTimeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    blinkShow = !blinkShow;

                    if (!blinkShow)
                        stateTransitionColor = Color.Transparent;
                    else if (stateTransitionTimeRemaining <= 0)
                        stateTransitionColor = Color.White;
                }

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
            }
            else
                HandleDeathAnimation(gameTime);

            HandleTurnFrames();



            currentSprite.Update(gameTime, spriteXPos, spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (starPower)
            {
                DrawStarMario(spriteBatch);
            }
            else if (stateTransitionTimeRemaining > 0 || blinkTimeRemaining > 0)
                currentSprite.Draw(spriteBatch, stateTransitionColor);
            else
                currentSprite.Draw(spriteBatch);
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