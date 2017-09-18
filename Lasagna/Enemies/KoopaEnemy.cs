using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna.Enemies
{
    class KoopaEnemy
    {
        private ISprite currentSprite;
        private ISprite koopaWalk = EnemySpriteFactory.Instance.CreateSprite_Koopa_Walk();
        private ISprite koopaDead = EnemySpriteFactory.Instance.CreateSprite_Koopa_Die();

        public void changeState()
        {
            this.currentSprite = koopaWalk;
        }
        public void Update(GameTime gameTime, int X, int Y)
        {
            this.currentSprite.Update(gameTime, X, Y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.currentSprite.Draw(spriteBatch);
        }
    }
}
