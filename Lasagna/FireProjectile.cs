using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class FireProjectile : IProjectile
    {
        /// <summary>
        /// State = 0 : Not exploded, State = 1 : Exploded
        /// </summary>
        private int State = 0;
        private ISprite fireballDefault = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Default();
        private ISprite fireballExplode = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Explode();
        private ISprite currentState;
        public void ChangeState()
        {
            this.State = 1;
        }
        public void Update(GameTime gameTime, int spriteXPos, int spriteYPos)
        {
            if (this.State == 0)
            {
                this.currentState = this.fireballDefault;
            }
            else if (this.State == 1)
            {
                this.currentState = this.fireballExplode;
            }
            this.currentState.Update(gameTime, spriteXPos, spriteYPos);

		}
        public void Draw(SpriteBatch spriteBatch) {
            this.currentState.Draw(spriteBatch);
        }
    }
}
