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
        private ISprite visibleSprite = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();

        public override Rectangle Properties
        {
            get
            {
                Rectangle properties = new Rectangle();
                if (currentState == BlockState.Invisible)
                {
                    properties = new Rectangle(base.PosX, base.PosY + CurrentSprite.Height, CurrentSprite.Width, 0);
                }
                else
                {
                    properties = new Rectangle(base.PosX, base.PosY, CurrentSprite.Width, CurrentSprite.Height);
                }
                return properties;
            }
        }

        public bool IsVisible
        {
            get { return currentState == BlockState.Visible; }
        }

        public InvisibleItemBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = visibleSprite;
            currentState = BlockState.Invisible;
            MarioEvents.OnReset += ChangeToInvisible;
        }

        public override void Update(GameTime gameTime)
        {
            //Only call base function if we're visible. Else draw nothing.
            if (currentState != BlockState.Invisible)
                base.Update(gameTime);
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
                CurrentSprite = visibleSprite;
                currentState = BlockState.Visible;
            }
            else
            {
                currentState = BlockState.Invisible;
            }

        }

        protected override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState.Equals(BlockState.Invisible) && side.Equals(CollisionSide.Bottom))
            {
                this.ChangeState();
            }
        }

        ///TODO: Temp methods for sprint3
        public void ChangeToInvisible(object sender, EventArgs e)
        {
            if (currentState == BlockState.Visible)
                ChangeState();
        }
    }
}
