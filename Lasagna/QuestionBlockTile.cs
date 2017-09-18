using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class QuestionBlockTile : ITile
    {
        /// <summary>
        /// State = 0 : Not used, State = 1 : Used
        /// </summary>
        private int State = 0;
        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos)
        {
            if (this.State == 0)
            {
                ISprite questionBlock = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
                questionBlock.Update(gameTime, spriteXPos, spriteYPos);
                questionBlock.Draw(spriteBatch);
            }
            else if (this.State == 1)
            {
                ISprite unbreakableBrick = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
                unbreakableBrick.Update(gameTime, spriteXPos, spriteYPos);
                unbreakableBrick.Draw(spriteBatch);
            }
        }
        public void SpawnItem(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos) { }
    }
}
