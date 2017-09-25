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

        public InvisibleItemBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = visibleSprite;
            currentState = BlockState.Invisible;
            MarioEvents.OnUseHiddenBlock += ChangeToVisible;
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
                currentState = BlockState.Invisible;
        }

        ///TODO: Temp methods for sprint2
        public void ChangeToVisible(object sender, EventArgs e)
        {
            if (currentState == BlockState.Invisible)
                ChangeState();
        }

        public void ChangeToInvisible(object sender, EventArgs e)
        {
            if (currentState == BlockState.Visible)
                ChangeState();
        }
    }
}
