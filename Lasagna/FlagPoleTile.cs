using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class FlagPoleTile : ITile
    {
        /// <summary>
        /// State = 0 : Not moving, State = 1 : Moving down
        /// </summary>
        private int State = 0;
        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos)
        {
            ISprite flag = TileSpriteFactory.Instance.CreateSprite_Flag();
            flag.Update(gameTime, spriteXPos, spriteYPos);
            flag.Draw(spriteBatch);
            spriteXPos += 32;

            ISprite flagPole = TileSpriteFactory.Instance.CreateSprite_FlagPole();
            flagPole.Update(gameTime, spriteXPos, spriteYPos);
            flagPole.Draw(spriteBatch);
            if (this.State == 1)
            {
                //Move flag down
            }
        }
        
    }
}
