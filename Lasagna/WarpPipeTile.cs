using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class WarpPipeTile : ITile
    {
        /// <summary>
        /// State = 0 : Has flower, State = 1 : Does not have flower
        /// </summary>
        private int State = 0;
        private int height = 1;
        public void ChangeState()
        {
            this.State = 1;
        }
        public void ChangeHeight(int height)
        {
            this.height = height;
        }
        public void Update(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos)
        {
            for (int i = 1; i < this.height; i++)
            {
                ISprite pipeBase = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();
                pipeBase.Update(gameTime, spriteXPos, spriteYPos);
                pipeBase.Draw(spriteBatch);
                spriteYPos -= 32;
            }
            ISprite pipeTip = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
            pipeTip.Update(gameTime, spriteXPos, spriteYPos);
            pipeTip.Draw(spriteBatch);
            if (this.State == 0)
            {
                //Spawn flower at the tip
            }
        }
        
    }
}
