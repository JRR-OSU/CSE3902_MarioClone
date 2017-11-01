using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface ISprite
    {
        int Height { get; }
        int Width { get; }
        float ClipLength { get; }

        void Update(GameTime gameTime, int spriteXPos, int spriteYPos);
        void Draw(SpriteBatch spriteBatch);
        void Draw(SpriteBatch spriteBatch, Color spriteTint, float rotation = 0);
        void SetSpriteScreenSize(int spriteXSize, int spriteYSize);
        void SetSpriteScreenPosition(int spriteXPos, int spriteYPos);
    }
}
