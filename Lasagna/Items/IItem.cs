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
        void CreateOneGrowmushroom(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y);
        void CreateOneCoin(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y);
        void CreateOneLifemushroom(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y);
        void CreateOneStar(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y);
        void CreateOneFlower(GameTime gameTime, SpriteBatch spriteBatch, int X, int Y);
    }
}
