using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    interface IEnemy
    {
        void CreateOneWalkingGoomba(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y);
    }
}
