using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class UnbreakableBrickTile : ITile
    {
        public void ChangeState()
        {
            return;
        }
        public void Update(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos)
        {
            ISprite unbreakableBrick = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
            unbreakableBrick.Update(gameTime, spriteXPos, spriteYPos);
            unbreakableBrick.Draw(spriteBatch);
        }
    }
}
