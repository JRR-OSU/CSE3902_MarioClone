using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class InvisibleItemBlockTile : ITile
    {
        /// <summary>
        /// State = 0 : Invisible, State = 1 : Visible
        /// </summary>
        private int State = 0;
        private int spriteXPos;
        private int spriteYPos;
		private ISprite Visible = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
        private ISprite Invisible; //Maybe it is not needed, needed to be fixed later.
		private ISprite currentState;
        public InvisibleItemBlockTile(int spriteXPos, int spriteYPos)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
            MarioEvents.OnUseHiddenBlock += ChangeToVisible;
            MarioEvents.OnReset += ChangeToInvisible;
        }
        public void ChangeState()
        {
            this.State = 1;
        }
		public void ChangeToVisible()
		{
			this.State = 1;
            this.currentState = this.Visible;
		}
		public void ChangeToInvisible()
		{
			this.State = 0;
            this.currentState = this.Invisible;
		}
        public void Update(GameTime gameTime)
        {
            if (this.State == 1) {
                this.currentState = this.Visible;
                this.currentState.Update(gameTime, this.spriteXPos, this.spriteYPos);
            }
        }
        public void Draw(SpriteBatch spriteBatch) { 
            if (this.State == 1) { 
                this.currentState.Draw(spriteBatch);
            }
        }
    }
}
