using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna.Items
{
    interface IItem
    {
        void Update(GameTime gameTime, int X, int Y);
        void Draw(SpriteBatch spriteBatch);
    }
}
