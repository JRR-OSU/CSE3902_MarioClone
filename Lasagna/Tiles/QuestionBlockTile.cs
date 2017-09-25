using System;

namespace Lasagna
{
    public class QuestionBlockTile : BaseTile
    {
        private enum BlockState
        {
            Idle,
            Used
        }

        private BlockState currentState;
        private ISprite unused = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
        private ISprite used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();

        public QuestionBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            MarioEvents.OnUseQuestionBlock += ChangeToUsed;
            MarioEvents.OnReset += ChangeToDefault;
        }

        public override void ChangeState()
        {
            //Toggles us between used and unused
            if (currentState == BlockState.Idle)
            {
                CurrentSprite = used;
                currentState = BlockState.Used;
            }
            else
            {
                CurrentSprite = unused;
                currentState = BlockState.Idle;
            }
        }

        ///TODO: Temp methods for sprint2
        private void ChangeToUsed(object sender, EventArgs e)
        {
            if (currentState == BlockState.Idle)
                ChangeState();
        }
        private void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Used)
                ChangeState();
        }
    }
}
