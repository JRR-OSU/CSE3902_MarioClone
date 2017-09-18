using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class WarpPipeTile : ITile
    {
        /// <summary>
        /// State = 0 : Has flower, State = 1 : Does not have flower
        /// Heigit indicates the number of pipe bases, when height = 0, there is only a pipe tip.
        /// </summary>
        private int State = 0;
        private int height = 0;
        private int spriteXPos;
        private int spriteYPos;
        private ISprite pipeTip = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
        private ISprite[] pipeBases;
        public WarpPipeTile(int spriteXPos, int spriteYPos, int height)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
            this.height = height;
            pipeBases = new ISprite[height];
            for (int i = 0; i < this.height; i ++)
            {
                pipeBases[i] = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();
            }
        }
        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime)
        {
            int tempYPos = this.spriteYPos;
            for (int i = 0; i < this.height; i++)
            {
                this.pipeBases[i].Update(gameTime, this.spriteXPos, tempYPos);
                tempYPos -= 32;
            }
            this.pipeTip.Update(gameTime, this.spriteXPos, tempYPos);
            if (this.State == 0)
            {
                //Spawn flower at the tip
            }
        }
        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < this.height; i++)
            {
                this.pipeBases[i].Draw(spriteBatch);
            }
            this.pipeTip.Draw(spriteBatch);
        }
    }
}
