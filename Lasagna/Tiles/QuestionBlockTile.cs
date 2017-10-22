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
        public IItem item;
        private int timer = 0;
        private int timeLimit = 50;
        private bool beingCollided = false;
        private ISprite unused = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
        private ISprite used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();

        public QuestionBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            MarioEvents.OnReset += ChangeToDefault;
        }
        public QuestionBlockTile(int spawnXPos, int spawnYPos, IItem item)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            this.item = item;
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

        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Idle) && side.Equals(CollisionSide.Bottom))
            {
                this.ChangeState();
                this.item.Spawn();
                this.beingCollided = true;
                while (timer < timeLimit)
                {
                    timer += base.gameTime.ElapsedGameTime.Milliseconds;
                }
                this.beingCollided = false;
                timer = 0;
            }
        }
      
        public bool CheckCollision()
        {
            return beingCollided;
        }
        public void Reset()
        {
            MarioEvents.OnReset += ChangeToDefault;
        }
        ///TODO: Temp methods for sprint3
        private void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Used)
                ChangeState();
        }
    }
}
