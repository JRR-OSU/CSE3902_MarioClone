using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

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
        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int EIGHT = 8;
        private const int TWENTY = 20;
        private BlockState currentState;
        public IItem[] items;
        private int preBumpPos;
        private int bumpingTimer = ZERO;
        private int bumpingTime = ZERO;
        private bool beingCollided = false;
        private bool isUsed = false;
        private ISprite unused = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
        private ISprite used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();

        public override bool IsChangingState { get { return beingCollided; } }
        public override bool IsUsed { get { return currentState.Equals(BlockState.Used); } }
        public QuestionBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            MarioEvents.OnReset += Reset;
        }
        public QuestionBlockTile(int spawnXPos, int spawnYPos, IItem[] newItems)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            if (newItems != null && newItems.Length > ZERO)
            {     
                this.items = newItems;
                foreach (IItem item in items)
                {
                    if (item != null)
                    {
                        ((BaseItem)item).ChangeToInvisible();
                    }
                }
            }
            MarioEvents.OnReset += Reset;
        }
        public void Update(IPlayer Mario, GameTime gameTime)
        {
            //Only call base function if we're in default state. Else draw nothing.
            if (currentState != BlockState.Used)
            {
                base.Update(gameTime);
            }
            
            if (beingCollided == true)
            {
                bumpingTime++;
            }
            if (bumpingTime > TWENTY)
            {
                beingCollided = false;
                bumpingTime = ZERO;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (currentState == BlockState.Bumped)
            {
                if (bumpingTimer < EIGHT)
                {
                    PosY -= TWO;
                    bumpingTimer++;
                }
                else if (bumpingTimer >= EIGHT && PosY != preBumpPos)
                {

                    PosY += TWO;
                }
                if (PosY == preBumpPos)
                {
                    currentState = BlockState.Used;
                    bumpingTimer = ZERO;
                }
            }
        }
        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Idle) && side.Equals(CollisionSide.Bottom))
            {
                CurrentSprite = used;
                currentState = BlockState.Bumped;
                this.beingCollided = true;
                preBumpPos = PosY;
                //If the first item is grow mushroom, then the second item must be flower.
                if (items != null && items.Length > ZERO)
                {
                    if (items[ZERO] is GrowMushroomItem)
                    {
                        if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Small)
                        {
                            items[ZERO].Spawn();
                        }
                        else
                        {
                            items[ONE].Spawn();
                        }
                    }
                    else
                    {
                        items[ZERO].Spawn();
                    }
                }
            }
        }

        public void Reset(object sender, EventArgs e)
        {
            currentState = BlockState.Idle;
            CurrentSprite = this.unused;
            bumpingTimer = ZERO;
            beingCollided = false;
            if (items != null && items.Length > ZERO)
            {
                foreach (IItem item in items)
                {
                    if (item != null)
                    {
                        ((BaseItem)item).ChangeToInvisible();
                    }
                }
            }
        }
    }
}
