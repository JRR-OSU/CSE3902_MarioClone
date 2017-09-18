using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class BreakableBrickTile : ITile
    {
        /// <summary>
        /// State = 0 : Not breaked, State = 1 : Breaking, State = 2: Broke
        /// </summary>
        private int State = 0;
        private int spriteXPos;
        private int spriteYPos;
		private ISprite Unbreaked = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
        private ISprite Breaking; //Reserved
		private ISprite Broke;
		private ISprite currentState;

        public BreakableBrickTile(int spriteXPos, int spriteYPos)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
        }

        public void ChangeState()
        {
            this.State ++;
        }

        public void Update(GameTime gameTime)
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
			this.currentState.Update(gameTime, this.spriteXPos, this.spriteYPos);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			this.currentState.Draw(spriteBatch);
		}
    }
}
