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
        private int brickCount = 1;
        private int originalCount = 1;
        private bool hasCount = false;
        private int timer = 0;
        private int timeLimit = 50;
        private bool beingCollided = false;
        private BlockState currentState;
        public IItem item;
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

        public BreakableBrickTile(int spawnXPos, int spawnYPos, IItem item, int brickcount)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = idleSprite;
            currentState = BlockState.Idle;
            this.item = item;
            brickCount = brickcount;
            originalCount = brickcount;
            if (this.brickCount > 1)
            {
                this.hasCount = true;
            }
            MarioEvents.OnReset += ChangeToDefault;
        }

        public override void Update(GameTime gameTime)
        {
            //Only call base function if we're visible. Else draw nothing.
            if (currentState != BlockState.Broken)
                base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Only call base function if we're visible. Else draw nothing.
            if (currentState != BlockState.Broken)
                base.Draw(spriteBatch);
        }

        public override void ChangeState()
        {
            ///TODO: Implement breaking transition here
            //Toggles us between used and unused
            if (!hasCount)
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
                if (item != null)
                {
                    this.item.Spawn();
                    this.beingCollided = true;
                    while (timer < timeLimit)
                    {
                        timer += base.gametime.ElapsedGameTime.Milliseconds;
                    }
                    this.beingCollided = false;
                    timer = 0;
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
