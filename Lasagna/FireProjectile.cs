using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Lasagna.Interfaces;

namespace Lasagna
{
    class FireProjectile : IProjectile
    {
        /// <summary>
        /// State = 0 : Not exploded, State = 1 : Exploded
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
                ISprite fireballDefault = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Default();
                fireballDefault.Update(gameTime, spriteXPos, spriteYPos);
                fireballDefault.Draw(spriteBatch);
            }
            else if (this.State == 1)
            {
                ISprite fireballExplode = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Explode();
                fireballExplode.Update(gameTime, spriteXPos, spriteYPos);
                fireballExplode.Draw(spriteBatch);
            }
        }
    }
}
