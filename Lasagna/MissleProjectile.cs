using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class MissleProjectile : IProjectile
    {
        private ISprite Missle; //Reserved for missle sprite.
        public void ChangeState()
        {
            return;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            this.Missle.Update(gameTime, spriteXPos, spriteYPos);
        }
        public void Draw(SpriteBatch spriteBatch) {
            this.Missle.Draw(spriteBatch);
        }
    }
}
