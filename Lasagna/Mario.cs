using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    class Mario
    {
        private MarioStateMachine stateMachine;

        public Mario()
        {
            stateMachine = new MarioStateMachine();
        }

        public void ChangeDirection()
        {
            stateMachine.ChangeDirection();
        }

        public void Grow()
        {
            stateMachine.Grow();
        }

        public void Fire()
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
        public class MarioStateMachine
        {
            private bool facingLeft = true;

            private enum MarioStates { Small, Big, Fire, Star, Dead };
            private enum MarioMovement { StillLeft, StillRight, WalkingLeft, WalkingRight, RunningLeft, RunningRight, JumpingLeft, JumpingRight };
            private MarioStates marioState = MarioStates.Small;
            private MarioMovement marioMovement = MarioMovement.StillRight;
            // individual state values listed in class

            // potential values

            // States:
            // jumping, direction, deadMario, fireMario, star
            // 


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

            public void Grow()
            {
                marioState = MarioStates.Big;

                if (marioState != MarioStates.Big) // Note: the if is needed so we only do the transition once
                {
                   
                    
                }
                // Make mario grow and then do some event that changes his sprite 
            }

            public void Fire()
            {

                marioState = MarioStates.Fire;
                if (marioState != MarioStates.Fire) // Note: the if is needed so we only do the transition once
                {
                   
                     
                }
                // Make mario grow into fire state and then do some event that changes his sprite 
            }

            public void Shrink()
            {
                marioState = MarioStates.Small;

                // Make mario's sprite shink to the small size
            }

            public void Star()
            {
                marioState = MarioStates.Star;
            }


            public void KillMario()
            {
                marioState = MarioStates.Dead;
            }

            public void Update()
            {
                // if-else logic based on the current values of facingLeft and health to determine how to move
            }
        }
    }
}
