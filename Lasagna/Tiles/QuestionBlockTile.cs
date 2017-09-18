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

        private ISprite Unused = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
        private ISprite Used = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
        private ISprite currentState;

        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (this.State == 0)
            {
                this.currentState = this.Unused;
            }
            else if (this.State == 1)
            {
                this.currentState = this.Used;
            }
            this.currentState.Update(gameTime, spriteXPos, spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.currentState.Draw(spriteBatch);
        }
        public void SpawnItem(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos) { }
    }
}
