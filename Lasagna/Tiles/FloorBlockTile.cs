using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class FloorBlockTile : ITile
    {
        private ISprite currentState = TileSpriteFactory.Instance.CreateSprite_Floor();
        public void ChangeState()
        {
            return;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            this.currentState.Update(gameTime, spriteXPos, spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.currentState.Draw(spriteBatch);
        }
    }
}
