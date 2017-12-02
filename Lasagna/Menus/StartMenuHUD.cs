using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public class StartMenuHUD : IHUD
    {
        public void Update()
        {

        }

        public void Draw(SpriteBatch batch, SpriteFont font, bool deathScreen, bool gameComplete)
        {
            batch.Begin();
            batch.DrawString(font, "1 PLAYER (Press F1)", new Vector2((640 / 2) - 60, (480 / 2) - 50), Color.White);
            batch.DrawString(font, "2 PLAYERS (Press F2)", new Vector2((640 / 2) - 60, (480 / 2) + 10), Color.White);
            batch.End();
        }
    }
}
