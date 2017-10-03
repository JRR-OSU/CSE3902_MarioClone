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
            Broken
        }

        private BlockState currentState;
        private ISprite idleSprite = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
        private ISprite breakingSprite; //Reserved for breaking tile sprite.

        public BreakableBrickTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = idleSprite;
            currentState = BlockState.Idle;
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
            if (currentState != BlockState.Idle)
            {
                CurrentSprite = idleSprite;
                currentState = BlockState.Idle;
            }
            else
                currentState = BlockState.Broken;
        }

        ///TODO: Temp methods for sprint2
        public void ChangeToInvisible(object sender, EventArgs e)
        {
            if (currentState == BlockState.Idle)
                ChangeState();
        }

        public void ChangeToDefault(object sender, EventArgs e)
        {
            if (currentState == BlockState.Broken)
                ChangeState();
        }
        public override void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            if (this.currentState == BlockState.Idle && side.Equals(CollisionSide.Top))
            {
                this.ChangeState();
            }
        }
    }
}
