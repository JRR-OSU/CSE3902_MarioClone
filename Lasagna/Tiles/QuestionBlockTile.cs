using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public class QuestionBlockTile : BaseTile
    {
        private enum BlockState
        {
            Idle,
            Bumped,
            Used
        }

        private BlockState currentState;
        public IItem item;
        private int preBumpPos;
        private int bumpingTimer = 0;
        private bool beingCollided = false;
        private ISprite unused = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
        private ISprite used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();

        public override bool IsChangingState { get { return beingCollided; } }
        public QuestionBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            MarioEvents.OnReset += Reset;
        }
        public QuestionBlockTile(int spawnXPos, int spawnYPos, IItem[] items)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            if (items != null && items.Length > 0)
                this.item = items[0];
            MarioEvents.OnReset += Reset;
        }
        public void Update(IPlayer Mario, GameTime gameTime)
        {
            //Only call base function if we're in default state. Else draw nothing.
            if (currentState != BlockState.Used)
            {
                base.Update(gameTime);
            }
            if (currentState == BlockState.Bumped)
            {
                if (bumpingTimer < 8)
                {
                    this.PosY -= 2;
                    bumpingTimer++;
                }
                else if (bumpingTimer >= 8 && PosY != preBumpPos)
                {

                    this.PosY += 2;
                }
                if (PosY == preBumpPos)
                {
                    currentState = BlockState.Used;
                    CurrentSprite = used;
                    bumpingTimer = 0;
                }
            }
            if (Mario.Bounds.Y > this.CurrentSprite.Height + base.PosY)
            {
                this.beingCollided = false;
            }
        }

        public override void ChangeState()
        {
            //Toggles us between used and unused
            if (currentState == BlockState.Idle)
            {
                CurrentSprite = used;
                currentState = BlockState.Bumped;
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
                this.beingCollided = true;
                preBumpPos = PosY;
                if (item != null)
                {
                    this.item.Spawn();
                }
            }
        }

        public void Reset(object sender, EventArgs e)
        {
            currentState = BlockState.Idle;
            CurrentSprite = this.unused;
            bumpingTimer = 0;
            beingCollided = false;
            if (item != null)
            {
                ((BaseItem)item).Reset(sender, e);
            }
        }
        ///TODO: Temp methods for sprint3
        /*private void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Used)
                ChangeState();
        }
        */
    }
}
