using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class QuestionBlockTile : ITile
    {
        /// <summary>
        /// State = 0 : Not used, State = 1 : Used
        /// </summary>
        private int State = 0;
        private int spriteXPos;
        private int spriteYPos;
        private ISprite Unused = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
        private ISprite Used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
        private ISprite currentState;
        public QuestionBlockTile(int spriteXPos, int spriteYPos)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
        }
        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime)
        {
            if (this.State == 0)
            {
                this.currentState = this.Unused;
            }
            else if (this.State == 1)
            {
                this.currentState = this.Used;
            }
            this.currentState.Update(gameTime, this.spriteXPos, this.spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.currentState.Draw(spriteBatch);
        }
    }
}
