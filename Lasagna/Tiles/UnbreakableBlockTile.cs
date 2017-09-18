using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class UnbreakableBlockTile : ITile
    {
        private int spriteXPos;
        private int spriteYPos;
        public UnbreakableBlockTile(int spriteXPos, int spriteYPos)
        {
            this.spriteXPos = spriteXPos;
            this.spriteYPos = spriteYPos;
        }
        private ISprite currentState = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed();
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
