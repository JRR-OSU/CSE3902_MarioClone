namespace Lasagna
{
    /// <summary>
    /// Helper class which will set the dictionary of sprites for either Player 1 (Mario) or Player 2 (Luigi)
    /// </summary>
    public class MarioSpriteHelper {

        /// <summary>
        /// Uses Mario or Luigi Sprite Factory to set sprites for state machine depending on player tag value
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="tag"></param>
        public MarioSpriteHelper(MarioStateMachine machine, int tag) {

            if (tag == 0)
            {
                machine.smallStates.Add(MarioStateMachine.MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_RunRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.TurnLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_TurnLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.TurnRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_TurnRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_JumpRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.Die, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Die());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.ShrinkLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_ShrinkLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.ShrinkRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_ShrinkRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.Flagpole, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_Flagpole());

                machine.bigStates.Add(MarioStateMachine.MarioMovement.CrouchLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.CrouchRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_CrouchRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_IdleRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_RunRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.TurnLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_TurnLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.TurnRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_TurnRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioBig_JumpRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.GrowLeft, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_GrowLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.GrowRight, MarioSpriteFactory.Instance.CreateSprite_MarioSmall_GrowRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.Flagpole, MarioSpriteFactory.Instance.CreateSprite_MarioBig_Flagpole());

                machine.fireStates.Add(MarioStateMachine.MarioMovement.CrouchLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.CrouchRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_CrouchRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.IdleLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.IdleRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_IdleRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.RunLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.RunRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_RunRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.TurnLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_TurnLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.TurnRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_TurnRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.JumpLeft, MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.JumpRight, MarioSpriteFactory.Instance.CreateSprite_MarioFire_JumpRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.Flagpole, MarioSpriteFactory.Instance.CreateSprite_MarioFire_Flagpole());
            }
          
            else
            {
                machine.smallStates.Add(MarioStateMachine.MarioMovement.IdleLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_IdleLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.IdleRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_IdleRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.RunLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_RunLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.RunRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_RunRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.TurnLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_TurnLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.TurnRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_TurnRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.JumpLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_JumpLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.JumpRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_JumpRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.Die, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_Die());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.ShrinkLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_ShrinkLeft());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.ShrinkRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_ShrinkRight());
                machine.smallStates.Add(MarioStateMachine.MarioMovement.Flagpole, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_Flagpole());

                machine.bigStates.Add(MarioStateMachine.MarioMovement.CrouchLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_CrouchLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.CrouchRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_CrouchRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.IdleLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_IdleLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.IdleRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_IdleRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.RunLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_RunLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.RunRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_RunRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.TurnLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_TurnLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.TurnRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_TurnRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.JumpLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_JumpLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.JumpRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_JumpRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.GrowLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_GrowLeft());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.GrowRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_GrowRight());
                machine.bigStates.Add(MarioStateMachine.MarioMovement.Flagpole, LuigiSpriteFactory.Instance.CreateSprite_LuigiBig_Flagpole());

                machine.fireStates.Add(MarioStateMachine.MarioMovement.CrouchLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_CrouchLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.CrouchRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_CrouchRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.IdleLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_IdleLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.IdleRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_IdleRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.RunLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_RunLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.RunRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_RunRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.TurnLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_TurnLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.TurnRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_TurnRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.JumpLeft, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_JumpLeft());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.JumpRight, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_JumpRight());
                machine.fireStates.Add(MarioStateMachine.MarioMovement.Flagpole, LuigiSpriteFactory.Instance.CreateSprite_LuigiFire_Flagpole());
            }
           
        }
    }
}
