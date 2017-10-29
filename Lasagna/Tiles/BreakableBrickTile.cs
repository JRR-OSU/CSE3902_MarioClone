using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class BreakableBrickTile : BaseTile
    {
        private enum BlockState
        {
            Idle,
            Breaking,
            Broken,
            Bumped,
            Used
        }
        private int numItemsLeft = 0;
        private int originalCount = 0;
        private int bumpingTimer = 0;
        private bool isBreaking = false;
        private List<ISprite> breakingBricks = new List<ISprite>();
        private List<Vector2> positions = new List<Vector2>();
        private Vector2 temp;
        // private Dictionary<List<int>, ISprite  > breakingBricks =
        //new Dictionary<List<int>, ISprite>();
        private int preBumpPos;
        private bool hasItem = false;
        private bool beingCollided = false;
        private BlockState currentState;
        public IItem[] items;
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
        public override bool IsChangingState { get { return beingCollided; } }

        public BreakableBrickTile(int spawnXPos, int spawnYPos, IItem[] newItems)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = idleSprite;
            currentState = BlockState.Idle;
            items = newItems;
            numItemsLeft = newItems.Length;
            originalCount = newItems.Length;
            if (this.numItemsLeft >= 1)
                this.hasItem = true;
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
            if (currentState != BlockState.Broken)
            {
                base.Update(gameTime);
            }

            //if (Mario.Bounds.Y > this.CurrentSprite.Height + base.PosY)
            //{
              //  this.beingCollided = false;
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Only call base function if we're in default state. Else draw nothing.
            if (currentState != BlockState.Broken)
                base.Draw(spriteBatch);

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
                    beingCollided = false;
                    currentState = BlockState.Idle;
                    bumpingTimer = 0;
                }
            }
            else if(currentState == BlockState.Breaking)
            {
                beingCollided = true;
                isBreaking = true;
                currentState = BlockState.Broken;
                
            }
            if (isBreaking)
                HandleBreakingBlock(spriteBatch);
        }

        private void HandleBreakingBlock(SpriteBatch spriteBatch)
        {
            if (bumpingTimer < 20)
            {

                for (int i = 0; i < 4; i++)
                {
                    temp.X = positions[i].X;
                    temp.Y = positions[i].Y;
                    switch (i)
                    {
                        case 0:
                            temp.X -= 3;
                            temp.Y -= 2;
                            break;
                        case 1:
                            temp.X += 3;
                            temp.Y -= 2;
                            break;
                        case 2:
                            temp.X -= 3;
                            temp.Y += 2;
                            break;
                        case 3:
                            temp.X += 3;
                            temp.Y += 2;
                            break;
                    }
                    positions[i] = temp;
                    if (breakingBricks[i] != null)
                    {
                        breakingBricks[i].SetSpriteScreenPosition((int)positions[i].X, (int)positions[i].Y);
                        breakingBricks[i].Draw(spriteBatch);

                    }
                }

                bumpingTimer++;
            }
            else if (bumpingTimer >= 20)
            {
                //currentState = BlockState.Broken;
                bumpingTimer = 0;
                beingCollided = false;
                isBreaking = false;
            }
        }
        public ISprite[] Breaking()
        {
            brickPieceSprites = new ISprite[4];
            for (int i = 0; i < 4; i += 2)
            {
                brickPieceSprites[i] = TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft();
                brickPieceSprites[i + 1] = TileSpriteFactory.Instance.CreateSprite_BrickPieceRight();
            }
            return brickPieceSprites;
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
                    this.numItemsLeft = this.originalCount;
                }
            }
        }
        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Idle) && side.Equals(CollisionSide.Bottom))
            {
                if (this.numItemsLeft == 1) { 
                    this.ChangeState();
                }
                else
                {
                    this.numItemsLeft--;
                }
                this.beingCollided = true;
                int curItem = items.Length - numItemsLeft;
                if (items != null && curItem >= 0 && curItem < items.Length)
                {
                    //If the first item is grow mushroom, then the second item must be flower.
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
                        this.numItemsLeft = 0;
                        this.ChangeState();
                    }
                    else
                    {
                        items[curItem].Spawn();
                    }
                }

                if (this.items.Length == 0)
                {
                    if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Big || ((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Fire)
                    {
                        this.currentState = BlockState.Breaking;

                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft());
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceRight());
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft());
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceRight());
                        breakingBricks[0].SetSpriteScreenPosition(this.Bounds.X, this.Bounds.Y);
                        positions.Add(new Vector2 ( this.Bounds.X, this.Bounds.Y ));
                        breakingBricks[1].SetSpriteScreenPosition(this.Bounds.X + this.Bounds.Width, this.Bounds.Y);
                        positions.Add(new Vector2(this.Bounds.X + this.Bounds.Width, this.Bounds.Y ));
                        breakingBricks[2].SetSpriteScreenPosition(this.Bounds.X,  this.Bounds.Y + this.Bounds.Height);
                        positions.Add(new Vector2(this.Bounds.X, this.Bounds.Y + this.Bounds.Height ));
                        breakingBricks[3].SetSpriteScreenPosition(this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height);
                        positions.Add(new Vector2(this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height ));
                    }
                    else if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Small)
                    {
                        this.currentState = BlockState.Bumped;
                        preBumpPos = PosY;
                    }
                }
                
            }
        }
        public void Reset(object sender, EventArgs e)
        {
            currentState = BlockState.Idle;
            CurrentSprite = this.idleSprite;
            numItemsLeft = originalCount;
            if (this.numItemsLeft >= 1)
            {
                this.hasItem = true;
            }
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
        /*public void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Broken || currentState == BlockState.Used)
                ChangeState();
        }
        */
    }
}
