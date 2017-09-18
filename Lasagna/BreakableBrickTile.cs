using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class BreakableBrickTile : ITile
    {
        /// <summary>
        /// State = 0 : Not breaked, State = 1 : Breaked
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
                ISprite breakableBrick = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
                breakableBrick.Update(gameTime, spriteXPos, spriteYPos);
                breakableBrick.Draw(spriteBatch);
            }
        }
        public void SpawnItem(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos) { }
    }
}
