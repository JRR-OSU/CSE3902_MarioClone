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


        public class MarioStateMachine
        {
            private bool facingLeft = true;

            private enum MarioStates { Small, Big, Fire };
            private enum MarioMovement { StillLeft, StillRight, WalkingLeft, WalkingRight, JumpingLeft, JumpingRight };
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

            public void BeStomped()
            {
                if (marioState != MarioStates.Big) // Note: the if is needed so we only do the transition once
                {
                    marioState = MarioStates.Big;
                    // Compute and construct goomba sprite - requires if-else logic with value of health
                }
            }

            public void BeFlipped()
            {
                if (marioState != MarioStates.Fire) // Note: the if is needed so we only do the transition once
                {
                    marioState = MarioStates.Fire;
                    // Compute and construct goomba sprite - requires if-else logic with value of health
                }
            }

            public void Update()
            {
                // if-else logic based on the current values of facingLeft and health to determine how to move
            }
        }
    }
}
