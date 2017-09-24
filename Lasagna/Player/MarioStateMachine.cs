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



        /// <summary>
        /// TODO: HANDLE SPRITE CHANGES VIA CHANGE SPRITE METHOD
        /// </summary>


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
            marioState = MarioState.Big;
            marioMovement = MarioMovement.IdleRight;
            currentSprite = bigStates[marioMovement];
        }

        public void Fire()
        {
            marioState = MarioState.Fire;
            marioMovement = MarioMovement.IdleRight;
            currentSprite = fireStates[marioMovement];
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
            if (marioMovement == MarioMovement.Die)
                return;
            if (marioState == MarioState.Big)
            {

                if (marioMovement == MarioMovement.RunLeft)
                {
                    marioMovement = MarioMovement.IdleLeft;
                    currentSprite = bigStates[marioMovement];

                }
                else
                {
                    marioMovement = MarioMovement.RunLeft;

                    currentSprite = bigStates[marioMovement];
                }
            }
            else if (marioState == MarioState.Fire)
            {

                if (marioMovement == MarioMovement.RunLeft)
                {
                    marioMovement = MarioMovement.IdleLeft;
                    currentSprite = fireStates[marioMovement];

                }
                else
                {
                    marioMovement = MarioMovement.RunLeft;

                    currentSprite = fireStates[marioMovement];
                }
            }
            else if (marioState == MarioState.Small)
            {

                if (marioMovement == MarioMovement.RunLeft)
                {
                    marioMovement = MarioMovement.IdleLeft;
                    currentSprite = smallStates[marioMovement];

                }
                else
                {
                    marioMovement = MarioMovement.RunLeft;

                    currentSprite = smallStates[marioMovement];
                }
            }

        }

        public void MoveRight()
        {
            if (marioMovement == MarioMovement.Die)
                return;
            if (marioState == MarioState.Big)
            {

                if (marioMovement == MarioMovement.RunRight)
                {
                    marioMovement = MarioMovement.IdleRight;
                    currentSprite = bigStates[marioMovement];

                }
                else
                {
                    marioMovement = MarioMovement.RunRight;

                    currentSprite = bigStates[marioMovement];
                }
            }
            else if (marioState == MarioState.Fire)
            {

                if (marioMovement == MarioMovement.RunRight)
                {
                    marioMovement = MarioMovement.IdleRight;
                    currentSprite = fireStates[marioMovement];

                }
                else
                {
                    marioMovement = MarioMovement.RunRight;

                    currentSprite = fireStates[marioMovement];
                }
            }
            else if (marioState == MarioState.Small)
            {

                if (marioMovement == MarioMovement.RunRight)
                {
                    marioMovement = MarioMovement.IdleRight;
                    currentSprite = smallStates[marioMovement];

                }
                else
                {
                    marioMovement = MarioMovement.RunRight;
                    currentSprite = smallStates[marioMovement];
                }
            }

        }

        public void Crouch()
        {
            if (marioMovement == MarioMovement.Die)
                return;

            if (marioMovement == MarioMovement.JumpRight || marioMovement == MarioMovement.JumpLeft)
            {

                if (marioState == MarioState.Big)
                {
                    if (marioMovement == MarioMovement.JumpRight)
                    {
                        marioMovement = MarioMovement.IdleRight;
                        currentSprite = bigStates[marioMovement];
                    }
                    else if (marioMovement == MarioMovement.JumpLeft)
                    {
                        marioMovement = MarioMovement.IdleLeft;
                        currentSprite = bigStates[marioMovement];
                    }
                }
                else if (marioState == MarioState.Fire)
                {
                    if (marioMovement == MarioMovement.JumpRight)
                    {
                        marioMovement = MarioMovement.IdleRight;
                        currentSprite = fireStates[marioMovement];

                    }
                    else if (marioMovement == MarioMovement.JumpLeft)
                    {
                        marioMovement = MarioMovement.IdleLeft;
                        currentSprite = fireStates[marioMovement];
                    }
                }
                else if (marioState == MarioState.Small)
                {
                    if (marioMovement == MarioMovement.JumpRight)
                    {
                        marioMovement = MarioMovement.IdleRight;
                        currentSprite = smallStates[marioMovement];

                    }
                    else if (marioMovement == MarioMovement.JumpLeft)
                    {
                        marioMovement = MarioMovement.IdleLeft;
                        currentSprite = smallStates[marioMovement];

                    }
                }
                return;
            }

            else
            {
                if (marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft)
                {
                    marioMovement = MarioMovement.CrouchLeft;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = bigStates[marioMovement];
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = fireStates[marioMovement];

                    }
                    else if (marioState == MarioState.Small)
                    {
                        return;
                    }
                    return;
                }

                else if (marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight)
                {
                    marioMovement = MarioMovement.CrouchRight;
                    if (marioState == MarioState.Big)
                    {
                        currentSprite = bigStates[marioMovement];
                    }
                    else if (marioState == MarioState.Fire)
                    {
                        currentSprite = fireStates[marioMovement];
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
            if (marioMovement == MarioMovement.Die)
                return;

            if (marioMovement == MarioMovement.JumpLeft || marioMovement == MarioMovement.JumpRight)
                return;

            if (marioMovement == MarioMovement.RunLeft || marioMovement == MarioMovement.IdleLeft)
            {
                marioMovement = MarioMovement.JumpLeft;
                if (marioState == MarioState.Big)
                {
                    currentSprite = bigStates[marioMovement];
                }
                else if (marioState == MarioState.Fire)
                {
                    currentSprite = fireStates[marioMovement];
                }
                else if (marioState == MarioState.Small)
                {
                    currentSprite = smallStates[marioMovement];
                }
                return;
            }

            else if (marioMovement == MarioMovement.RunRight || marioMovement == MarioMovement.IdleRight)
            {
                marioMovement = MarioMovement.JumpRight;
                if (marioState == MarioState.Big)
                {
                    currentSprite = bigStates[marioMovement];
                }
                else if (marioState == MarioState.Fire)
                {
                    currentSprite = fireStates[marioMovement];
                }
                else if (marioState == MarioState.Small)
                {
                    currentSprite = smallStates[marioMovement];
                }
                return;
            }

            else if (marioState != MarioState.Small && marioMovement == MarioMovement.CrouchRight)
            {
                marioMovement = MarioMovement.IdleRight;
                if (marioState == MarioState.Big)
                {
                    currentSprite = bigStates[marioMovement];
                }
                else if (marioState == MarioState.Fire)
                {
                    currentSprite = fireStates[marioMovement];
                }
                return;
            }

            else if (marioState != MarioState.Small && marioMovement == MarioMovement.CrouchLeft)
            {
                marioMovement = MarioMovement.IdleLeft;
                if (marioState == MarioState.Big)
                {
                    currentSprite = bigStates[marioMovement];
                }
                else if (marioState == MarioState.Fire)
                {
                    currentSprite = fireStates[marioMovement];
                }
            }


        }

        public void Shrink()
        {
            marioState = MarioState.Small;
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

        public void Reset()
        {
            marioState = MarioState.Small;
            marioMovement = MarioMovement.IdleRight;
            currentSprite = smallStates[marioMovement];
        }
    }
}
