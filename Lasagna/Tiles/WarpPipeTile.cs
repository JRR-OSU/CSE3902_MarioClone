using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class WarpPipeTile : ITile
    {
        /// <summary>
        /// State = 0 : Has flower, State = 1 : Does not have flower
        /// </summary>
        private int State = 0;
        private int height = 1;
        private int spriteXPos;
        private int spriteYPos;
        private ISprite pipeBase = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();
        private ISprite pipeTip = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
        public WarpPipeTile(int spriteXPos, int spriteYPos)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
        }
        public void ChangeState()
        {
            this.State = 1;
        }
        public void ChangeHeight(int height)
        {
            this.height = height;
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 1; i < this.height; i++)
            {
                this.pipeBase.Update(gameTime, this.spriteXPos, this.spriteYPos);
                this.spriteYPos -= 32;
            }
            this.pipeTip.Update(gameTime, this.spriteXPos, this.spriteYPos);
            if (this.State == 0)
            {
                //Spawn flower at the tip
            }
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.pipeTip.Draw(spriteBatch);
            this.pipeBase.Draw(spriteBatch);
        }
        
    }
}
