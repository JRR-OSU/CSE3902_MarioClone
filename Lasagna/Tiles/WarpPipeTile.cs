using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    public class WarpPipeTile : ITile
    {
        private ISprite pipeTipSprite = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
        private ISprite[] pipeBaseSprites;
        //Number of pipe base segments high this pipe is. 0 means just the pipe tip.
        private int height = 0;
        private int posX;
        private int posY;
        private int pipeTipHeight;
        private int pipeBaseHeight;
        public Rectangle Properties;

        public WarpPipeTile(int spawnPosX, int spawnPosY, int pipeHeight)
        {
            posX = spawnPosX;
            posY = spawnPosY;
            height = pipeHeight;

            //Set pipe base sprites
            pipeBaseSprites = new ISprite[height];
            for (int i = 0; i < height; i++)
                pipeBaseSprites[i] = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();

            //Store height of our sprites, if null ref default values
            if (pipeTipSprite != null)
                pipeTipHeight = pipeTipSprite.Height;
            else
                pipeTipHeight = 64;
            if (pipeBaseSprites != null && pipeBaseSprites.Length > 0 && pipeBaseSprites[0] != null)
                pipeBaseHeight = pipeBaseSprites[0].Height;
            else
                pipeBaseHeight = 32;
        }

        //Empty for now
        public void ChangeState() { }

        public void Update(GameTime gameTime)
        {
            if (pipeTipSprite != null)
                pipeTipSprite.Update(gameTime, posX, posY);

            //Draw each base after
            int tempPosY = posY + pipeTipHeight;
            for (int i = 0; i < pipeBaseSprites.Length; i++)
            {
                if (pipeBaseSprites[i] != null)
                {
                    pipeBaseSprites[i].Update(gameTime, posX, tempPosY);
                    tempPosY += pipeBaseHeight;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (pipeTipSprite != null)
                pipeTipSprite.Draw(spriteBatch);
            for (int i = 0; i < pipeBaseSprites.Length; i++)
                if (pipeBaseSprites[i] != null)
                    pipeBaseSprites[i].Draw(spriteBatch);
        }

        public Rectangle GetProperties()
        {
            this.Properties.X = this.posX;
            this.Properties.Y = this.posY;
            this.Properties.Width = this.pipeTipSprite.Width;
            this.Properties.Width = this.pipeTipHeight + this.pipeBaseHeight * this.height;
            return this.Properties;
        }

        public void OnCollisionResponse(IPlayer Mario, CollisionSide side)
        {
            return;
        }
        public void OnCollisionResponse(IItem Item, CollisionSide side)
        {
            return;
        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            return;
        }
    }
}
