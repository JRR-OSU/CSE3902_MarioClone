using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class BreakableBrickTile : ITile
    {
        /// <summary>
        /// State = 0 : Not breaked, State = 1 : Breaking, State = 2: Broke
        /// </summary>
        private int State = 0;

		private ISprite Unbreaked = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
        private ISprite Breaking; //Reserved
		private ISprite Broke;
		private ISprite currentState;
        public void ChangeState()
        {
            this.State ++;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (this.State == 0) {
                this.currentState = this.Unbreaked;
            }
            else if (this.State == 1) {
                this.currentState = this.Breaking;
            }
            else if (this.State == 2) {
                this.currentState = this.Broke;
            }
			this.currentState.Update(gameTime, spriteXPos, spriteYPos);
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			this.currentState.Draw(spriteBatch);
		}
        public void SpawnItem(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos) { }
    }
}
