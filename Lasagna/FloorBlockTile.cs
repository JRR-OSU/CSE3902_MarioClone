using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class FloorBlockTile : ITile
    {
        public void ChangeState()
        {
            return;
        }
        public void Update(GameTime gameTime, SpriteBatch spriteBatch, int spriteXPos, int spriteYPos)
        {
            ISprite floor = TileSpriteFactory.Instance.CreateSprite_Floor();
            floor.Update(gameTime, spriteXPos, spriteYPos);
            floor.Draw(spriteBatch);
        }
    }
}
