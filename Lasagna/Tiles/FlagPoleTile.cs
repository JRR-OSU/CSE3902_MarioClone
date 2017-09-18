using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class FlagPoleTile : ITile
    {
        /// <summary>
        /// State = 0 : Not moving, State = 1 : Moving down
        /// </summary>
        private int State = 0;
		ISprite flag = TileSpriteFactory.Instance.CreateSprite_Flag();
		ISprite flagPole = TileSpriteFactory.Instance.CreateSprite_FlagPole();


		public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            this.flag.Update(gameTime, spriteXPos, spriteYPos);
            spriteXPos += 32;

            this.flagPole.Update(gameTime, spriteXPos, spriteYPos);
            if (this.State == 1)
            {
                //Move flag down
            }
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.flag.Draw(spriteBatch);
            this.flagPole.Draw(spriteBatch);
        }
    }
}
