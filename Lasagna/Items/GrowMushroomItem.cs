using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class GrowMushroomItem
    {
        ISprite upMushroom = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        public void Update(GameTime gameTime, int X, int Y)
        {
            upMushroom.Update(gameTime, X, Y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            upMushroom.Draw(spriteBatch);
        }
    }
}
