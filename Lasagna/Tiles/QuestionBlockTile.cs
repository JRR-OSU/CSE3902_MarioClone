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

        private BlockState currentState;
        public IItem[] items;
        private int preBumpPos;
        private int bumpingTimer = 0;
        private int bumpingTime = 0;
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
        public QuestionBlockTile(int spawnXPos, int spawnYPos, IItem[] newItems)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = unused;
            currentState = BlockState.Idle;
            if (newItems != null && newItems.Length > 0)
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
            if (bumpingTime > 20)
            {
                beingCollided = false;
                bumpingTime = 0;
            }
            //if (Mario.Bounds.Y > this.CurrentSprite.Height + base.PosY)
            //{
              //  this.beingCollided = false;
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (currentState == BlockState.Bumped)
            {
                if (bumpingTimer < 8)
                {
                    PosY -= 2;
                    bumpingTimer++;
                }
                else if (bumpingTimer >= 8 && PosY != preBumpPos)
                {

                    PosY += 2;
                }
                if (PosY == preBumpPos)
                {
                    currentState = BlockState.Used;
                    bumpingTimer = 0;
                }
            }
        }
        /*public override void ChangeState()
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
        }*/

        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Idle) && side.Equals(CollisionSide.Bottom))
            {
                CurrentSprite = used;
                currentState = BlockState.Bumped;
                this.beingCollided = true;
                preBumpPos = PosY;
                //If the first item is grow mushroom, then the second item must be flower.
                if (items != null && items.Length > 0)
                {
                    if (items[0] is GrowMushroomItem)
                    {
                        if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Small)
                        {
                            items[0].Spawn();
                        }
                        else
                        {
                            items[1].Spawn();
                        }
                    }
                    else
                    {
                        items[0].Spawn();
                    }
                }
            }
        }

        public void Reset(object sender, EventArgs e)
        {
            currentState = BlockState.Idle;
            CurrentSprite = this.unused;
            bumpingTimer = 0;
            beingCollided = false;
            if (items != null && items.Length > 0)
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
        ///TODO: Temp methods for sprint3
        /*private void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Used)
                ChangeState();
        }
        */
    }
}
