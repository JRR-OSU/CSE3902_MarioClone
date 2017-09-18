using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class InvisibleItemBlockTile : ITile
    {
        /// <summary>
        /// State = 0 : Invisible, State = 1 : Visible
        /// </summary>
        private int State = 0;

		private ISprite Invisible;
		private ISprite Visible = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
		private ISprite currentState;
        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (this.State == 0) {
                this.currentState = this.Invisible; 
            }
            else if (this.State == 1)
            {
                this.currentState = this.Visible;
            }
            this.currentState.Update(gameTime, spriteXPos, spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.currentState.Draw(spriteBatch);
        }
        public void SpawnItem(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos) { }
    }
}
