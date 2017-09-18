using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class FloorBlockTile : ITile
    {
        private int spriteXPos;
        private int spriteYPos;
        private ISprite currentState = TileSpriteFactory.Instance.CreateSprite_Floor();

        public FloorBlockTile(int spriteXPos, int spriteYPos)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
        }
        public void ChangeState()
        {
            return;
        }
        public void Update(GameTime gameTime)
        {
            this.currentState.Update(gameTime, this.spriteXPos, this.spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.currentState.Draw(spriteBatch);
        }
    }
}
