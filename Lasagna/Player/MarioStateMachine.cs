using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class MarioStateMachine
    {
        private enum MarioState { Small, Big, Fire, Star };
        private enum MarioMovement { CrouchRight, CrouchLeft, IdleLeft, IdleRight, RunLeft, RunRight, JumpLeft, JumpRight, Die };
        private MarioState marioState = MarioState.Small;
        private MarioMovement marioMovement = MarioMovement.IdleRight;
        private ISprite currentSprite = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();

        private Dictionary<MarioMovement, ISprite> smallStates = new Dictionary<MarioMovement, ISprite>();
        private Dictionary<MarioMovement, ISprite> bigStates = new Dictionary<MarioMovement, ISprite>();
        private Dictionary<MarioMovement, ISprite> fireStates = new Dictionary<MarioMovement, ISprite>();
        private Dictionary<MarioMovement, ISprite> starStates = new Dictionary<MarioMovement, ISprite>();

        public MarioStateMachine()
        {
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

        }

        public void Grow()
        {
            if (marioMovement == MarioMovement.Die)
                marioMovement = MarioMovement.IdleRight;
            marioState = MarioState.Big;
            currentSprite = bigStates[marioMovement];
        }

        public void Fire()
        {
            if (marioMovement == MarioMovement.Die)
                marioMovement = MarioMovement.IdleRight;
            marioState = MarioState.Fire;
            currentSprite = fireStates[marioMovement];
        }

        public void MarioFireProjectile()
        {

        }

        public void GetFireflower()
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
        public void MoveLeft()
        {
            if (marioMovement == MarioMovement.Die)
                return;
            //if (marioMovement == MarioMovement.RunLeft)
            //    marioMovement = MarioMovement.IdleLeft;
           // else
                marioMovement = MarioMovement.RunLeft;
            SwitchCurrentSprite(marioMovement);
        }

        public void MoveRight()
        {
           // if (marioMovement == MarioMovement.Die)
           //     return;
          //  if (marioMovement == MarioMovement.RunRight)
          //      marioMovement = MarioMovement.IdleRight;
          //  else
                marioMovement = MarioMovement.RunRight;
            SwitchCurrentSprite(marioMovement);
        }
        public void HandleCrouch()
        {
            if((marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft) && marioState != MarioState.Small)
            {
                marioMovement = MarioMovement.CrouchLeft;
                return;
            }
            else if((marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight) && marioState != MarioState.Small)
            {
                marioMovement = MarioMovement.CrouchRight;
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
            if (marioMovement == MarioMovement.Die)
                return;
            HandleCrouch();
            SwitchCurrentSprite(marioMovement);
        }
        public void HandleJump()
        {
            if (marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft)
            {
                marioMovement = MarioMovement.JumpLeft;
                return;
            }
            else if (marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight)
            {
                marioMovement = MarioMovement.JumpRight;
                return;
            }
            switch (marioMovement)
            {
                case MarioMovement.CrouchLeft:
                    marioMovement = MarioMovement.IdleLeft;
                    break;
                case MarioMovement.CrouchRight:
                    marioMovement = MarioMovement.IdleRight;
                    break;
            }
        }
        public void Jump()
        {
            if (marioMovement == MarioMovement.Die)
                return;
            HandleJump();
            SwitchCurrentSprite(marioMovement);
        }

        public void Shrink()
        {
            marioState = MarioState.Small;
            if (marioMovement == MarioMovement.CrouchLeft)
                marioMovement = MarioMovement.IdleLeft;
            else if (marioMovement == MarioMovement.CrouchRight)
                marioMovement = MarioMovement.IdleRight;
            else if (marioMovement == MarioMovement.Die)
                marioMovement = MarioMovement.IdleRight;
            currentSprite = smallStates[marioMovement];
        }

        public void Star()
        {
            marioState = MarioState.Star;
        }

        public void KillMario()
        {
            marioMovement = MarioMovement.Die;
            currentSprite = smallStates[marioMovement];
        }

        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {

            currentSprite.Update(gameTime, spriteXPos, spriteYPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch);
        }

        public ISprite GetCurrentSprite()
        {
            return currentSprite;
        }

        public void Reset()
        {
            marioState = MarioState.Small;
            marioMovement = MarioMovement.IdleRight;
            currentSprite = smallStates[marioMovement];
        }
    }
}