using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IHUD
    {
        void Update();
        void Draw(SpriteBatch batch, SpriteFont font, bool deathScreen, bool gameComplete);
    }
}
