using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class GoombaEnemy : IEnemy
    {

        public void CreateOneWalkingGoomba(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y)
        {
            ISprite goomba = EnemySpriteFactory.Instance.CreateSprite_Goomba_Walk();
            goomba.Update(gameTime, X, Y);
            goomba.Draw(spriteBatch);
        }
    }
}
