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
        public override int GetState()
        {
            if (currentState == BlockState.Idle)
                return 0;
            else
            {
                return 1;
            }
        }

        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Idle) && side.Equals(CollisionSide.Bottom))
            {
                this.ChangeState();
            }
        }

        ///TODO: Temp methods for sprint3
        private void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Used)
                ChangeState();
        }
    }
}
