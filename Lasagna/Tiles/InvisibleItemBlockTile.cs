using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Lasagna
{
    public class InvisibleItemBlockTile : BaseTile
    {
        private enum BlockState
        {
            Invisible,
            Visible
        }
        private const int ZERO = 0;
        private const int ONE = 1;
        private BlockState currentState;
        public IItem[] items;
        private ISprite visibleSprite = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
        private bool CollidedWithThreeSides = false;

        public bool MarioCollidedWithThreeSides() { return this.CollidedWithThreeSides; }
        public override bool IsChangingState { get; set; }

        public bool IsVisible
        {
            get { return currentState == BlockState.Visible; }
        }

        public InvisibleItemBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = visibleSprite;
            currentState = BlockState.Invisible;
            MarioEvents.OnReset += Reset;
        }
        public InvisibleItemBlockTile(int spawnXPos, int spawnYPos, IItem[] newItems)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = visibleSprite;
            currentState = BlockState.Invisible;
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
        public void Update(ICollider Mario, GameTime gameTime)
        {
            //Only call base function if we're visible. Else draw nothing.
            if (currentState != BlockState.Invisible)
            {
                base.Update(gameTime);
            }
            //Once the Mario is lower than the block, toggle the collision status back.
            if (Mario.Bounds.Y > this.CurrentSprite.Height + base.PosY)
            {
                this.CollidedWithThreeSides = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Only call base function if we're visible. Else draw nothing.
            if (currentState != BlockState.Invisible)
                base.Draw(spriteBatch);
        }

        public override void ChangeState()
        {
            //Toggles us between visible and invisible
            if (currentState == BlockState.Invisible)
            {
                currentState = BlockState.Visible;
            }
            else
            {
                currentState = BlockState.Invisible;
            }

        }
        public void Reset(object sender, EventArgs e)
        {
            currentState = BlockState.Invisible;
            CollidedWithThreeSides = false;
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
        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            //If the Mario hit the invisible block from top, left and right sides, toggle the collision status to true.
            if (this.currentState.Equals(BlockState.Invisible) && (side.Equals(CollisionSide.Top) ||
                side.Equals(CollisionSide.Left) || side.Equals(CollisionSide.Right)))
            {
                this.CollidedWithThreeSides = true;
            }
            //If the Mario hit the invisible block from the bottom, then change the state of the block.
            if (this.currentState.Equals(BlockState.Invisible) && side.Equals(CollisionSide.Bottom) &&
                this.CollidedWithThreeSides == false)
            {
                this.ChangeState();
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
    }
}
