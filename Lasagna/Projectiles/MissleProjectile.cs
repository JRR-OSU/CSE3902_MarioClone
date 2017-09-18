using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class MissleProjectile : IProjectile
    {
        private int spriteXPos;
        private int spriteYPos;
        private ISprite Missle; //Reserved for missle sprite.
        public MissleProjectile(int spriteXPos, int spriteYPos)
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
            this.Missle.Update(gameTime, this.spriteXPos, this.spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.Missle.Draw(spriteBatch);
        }
    }
}
