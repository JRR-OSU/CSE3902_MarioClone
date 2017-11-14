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
        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int THREE = 3;
        private const int FOUR = 4;
        private const int EIGHT = 8;
        private const int TWENTY = 20;
        private int numItemsLeft = ZERO;
        private int originalCount = ZERO;
        private int bumpingTimer = ZERO;
        private bool isBreaking = false;
        private List<ISprite> breakingBricks = new List<ISprite>();
        private List<Vector2> positions = new List<Vector2>();
        private Vector2 temp;
        private int preBumpPos;
        private bool hasItem = false;
        private bool beingCollided = false;
        private BlockState currentState;
        public IItem[] items;
        private ISprite[] brickPieceSprites;
        private ISprite idleSprite = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
        private ISprite used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
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
        public override bool IsUsed { get { return currentState.Equals(BlockState.Used); } }

        public BreakableBrickTile(int spawnXPos, int spawnYPos, IItem[] newItems)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = idleSprite;
            currentState = BlockState.Idle;
            items = newItems;
            numItemsLeft = newItems.Length;
            originalCount = newItems.Length;
            if (this.numItemsLeft >= ONE)
                this.hasItem = true;
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
            if (currentState != BlockState.Broken)
            {
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Only call base function if we're in default state. Else draw nothing.
            if (currentState != BlockState.Broken)
                base.Draw(spriteBatch);

            if (currentState == BlockState.Bumped)
            {
                if (bumpingTimer < EIGHT)
                {
                    this.PosY -= TWO;
                    bumpingTimer++;
                }
                else if (bumpingTimer >= EIGHT && PosY != preBumpPos)
                {
                   
                    this.PosY += TWO;
                }              
                if (PosY == preBumpPos)
                {
                    beingCollided = false;
                    currentState = BlockState.Idle;
                    bumpingTimer = ZERO;
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
            if (bumpingTimer < TWENTY)
            {

                for (int i = ZERO; i < FOUR; i++)
                {
                    temp.X = positions[i].X;
                    temp.Y = positions[i].Y;
                    switch (i)
                    {
                        case ZERO:
                            temp.X -= THREE;
                            temp.Y -= TWO;
                            break;
                        case ONE:
                            temp.X += THREE;
                            temp.Y -= TWO;
                            break;
                        case TWO:
                            temp.X -= THREE;
                            temp.Y += TWO;
                            break;
                        case THREE:
                            temp.X += THREE;
                            temp.Y += TWO;
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
            else if (bumpingTimer >= TWENTY)
            {
                //currentState = BlockState.Broken;
                bumpingTimer = ZERO;
                beingCollided = false;
                isBreaking = false;
            }
        }
        public ISprite[] Breaking()
        {
            brickPieceSprites = new ISprite[FOUR];
            for (int i = ZERO; i < FOUR; i += TWO)
            {
                brickPieceSprites[i] = TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft();
                brickPieceSprites[i + ONE] = TileSpriteFactory.Instance.CreateSprite_BrickPieceRight();
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
                if (this.numItemsLeft == ONE) {
                    this.ChangeState();
                }
                else
                {
                    this.numItemsLeft--;
                }
                this.beingCollided = true;
                int curItem = items.Length - numItemsLeft;
                if (items != null && curItem >= ZERO && curItem < items.Length)
                {
                    //If the first item is grow mushroom, then the second item must be flower.
                    if (items[ZERO] is GrowMushroomItem)
                    {
                        SoundEffectFactory.Instance.PlayPowerUpAppearsSound();
                        if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Small)
                        {
                            items[ZERO].Spawn();
                        }
                        else
                        {
                            items[ONE].Spawn();
                        }
                        this.numItemsLeft = ZERO;
                        this.ChangeState();
                    }
                    else
                    {
                        if (currentState != BlockState.Used)
                        {
                            if (items[ZERO] is CoinItem)
                            {
                                SoundEffectFactory.Instance.PlayCoin();
                            }
                            else
                            {
                                SoundEffectFactory.Instance.PlayPowerUpAppearsSound();
                            }
                            this.currentState = BlockState.Bumped;
                            preBumpPos = PosY;
                        }
                        items[curItem].Spawn();
                    }
                }

                if (this.items.Length == ZERO)
                {
                    if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Big || ((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Fire)
                    {
                        this.currentState = BlockState.Breaking;
                        SoundEffectFactory.Instance.PlayBrickBlock();
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft());
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceRight());
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceLeft());
                        breakingBricks.Add(TileSpriteFactory.Instance.CreateSprite_BrickPieceRight());
                        breakingBricks[ZERO].SetSpriteScreenPosition(this.Bounds.X, this.Bounds.Y);
                        positions.Add(new Vector2 ( this.Bounds.X, this.Bounds.Y ));
                        breakingBricks[ONE].SetSpriteScreenPosition(this.Bounds.X + this.Bounds.Width, this.Bounds.Y);
                        positions.Add(new Vector2(this.Bounds.X + this.Bounds.Width, this.Bounds.Y ));
                        breakingBricks[TWO].SetSpriteScreenPosition(this.Bounds.X,  this.Bounds.Y + this.Bounds.Height);
                        positions.Add(new Vector2(this.Bounds.X, this.Bounds.Y + this.Bounds.Height ));
                        breakingBricks[THREE].SetSpriteScreenPosition(this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height);
                        positions.Add(new Vector2(this.Bounds.X + this.Bounds.Width, this.Bounds.Y + this.Bounds.Height ));
                    }
                    else if (((Mario)Mario).CurrentState == MarioStateMachine.MarioState.Small)
                    {
                        SoundEffectFactory.Instance.PlayBump();
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
            if (this.numItemsLeft >= ONE)
            {
                this.hasItem = true;
            }
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
