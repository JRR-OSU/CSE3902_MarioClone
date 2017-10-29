using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class InvisibleItemBlockTile : BaseTile
    {
        private enum BlockState
        {
            Invisible,
            Visible
        }

        private BlockState currentState;
        public IItem item;
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
        public InvisibleItemBlockTile(int spawnXPos, int spawnYPos, IItem[] items)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = visibleSprite;
            currentState = BlockState.Invisible;
            if (items != null && items.Length > 0)
                this.item = items[0];

            if (item != null)
            {
                ((BaseItem)this.item).ChangeToInvisible();
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
            if (item != null)
            {
                ((BaseItem)item).Reset(sender, e);
                ((BaseItem)item).ChangeToInvisible();
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
                if (item != null)
                {
                    this.item.Spawn();
                }
            }
        }

        ///TODO: Temp methods for sprint3
        /*public void ChangeToInvisible(object sender, EventArgs e)
        {
            if (currentState == BlockState.Visible)
            {
                if (item != null)
                {
                    ((BaseItem)this.item).ChangeToInvisible();
                }
                ChangeState();
            }
        }*/
    }
}
