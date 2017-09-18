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
        public void CreateOneWalkingKoopa(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y)
        {
            ISprite koopa = EnemySpriteFactory.Instance.CreateSprite_Koopa_Walk();
            koopa.Update(gameTime, X, Y);
            koopa.Draw(spriteBatch);
        }
    }
}
