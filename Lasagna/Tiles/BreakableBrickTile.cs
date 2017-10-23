using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class BreakableBrickTile : BaseTile
    {
        private enum BlockState
        {
            Idle,
            Breaking,
            Broken,
            Used
        }
        private int brickCount = 0;
        private int originalCount = 0;
        private bool hasItem = false;
        private bool beingCollided = false;
        private BlockState currentState;
        public IItem item;
        private ISprite[] brickPieceSprites;
        private ISprite idleSprite = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
        private ISprite used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
        //private ISprite breakingSprite; //Reserved for breaking tile sprite.
        public override Rectangle Bounds
        {
            get
            {
                Rectangle properties = new Rectangle();
                if (CurrentSprite == null || currentState == BlockState.Broken)
                {
                    properties = Rectangle.Empty;
                }
                else
                {
                    properties = new Rectangle(base.PosX, base.PosY, CurrentSprite.Width, CurrentSprite.Height);
                }
                return properties;
            }
        }

        public BreakableBrickTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = idleSprite;
            currentState = BlockState.Idle;
            MarioEvents.OnReset += ChangeToDefault;
        }

        public BreakableBrickTile(int spawnXPos, int spawnYPos, IItem newItem, int newBrickCount)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = idleSprite;
            currentState = BlockState.Idle;
            this.item = newItem;
            brickCount = newBrickCount;
            originalCount = newBrickCount;
            if (this.brickCount >= 1)
            {
                this.hasItem = true;
            }
            MarioEvents.OnReset += ChangeToDefault;
        }

        public void Update(IPlayer Mario, GameTime gameTime)
        {
            //Only call base function if we're in default state. Else draw nothing.
            if (currentState != BlockState.Broken)
            {
                base.Update(gameTime);
            }
            if (Mario.Bounds.Y > this.CurrentSprite.Height + base.PosY)
            {
                this.beingCollided = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Only call base function if we're in default state. Else draw nothing.
            if (currentState != BlockState.Broken)
                base.Draw(spriteBatch);
        }
        public void Breaking()
        {
            brickPieceSprites = new ISprite[4];
            for (int i = 0; i < 4; i += 2)
            {
                brickPieceSprites[i] = TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft();
                brickPieceSprites[i + 1] = TileSpriteFactory.Instance.CreateSprite_BrickPieceRight();
            }
        }
        public override void ChangeState()
        {
            ///TODO: Implement breaking transition here
            //Toggles us between used and unused
            if (!hasItem)
            {
                if (currentState != BlockState.Idle)
                {
                    CurrentSprite = idleSprite;
                    currentState = BlockState.Idle;
                }
                else
                    currentState = BlockState.Broken;
            }
            else
            {
                if (currentState != BlockState.Idle)
                {
                    CurrentSprite = idleSprite;
                    currentState = BlockState.Idle;
                }
                else
                {
                    CurrentSprite = used;
                    currentState = BlockState.Used;
                    this.brickCount = this.originalCount;
                }
            }
        }
        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Idle) && side.Equals(CollisionSide.Bottom))
            {
                if (this.brickCount == 1) { 
                    this.ChangeState();
                }
                else
                {
                    this.brickCount--;
                }
                this.beingCollided = true;
                if (item != null)
                {
                    this.item.Spawn();
                }
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
        public void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Broken || currentState == BlockState.Used)
                ChangeState();
        }
    }
}
