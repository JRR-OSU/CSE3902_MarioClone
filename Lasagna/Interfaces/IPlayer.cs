using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public interface IPlayer : ICollider
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void SetPosition(int x, int y);
        void BeginWarpAnimation(Direction moveDir, bool startWithMove);
        int Tag { get; set; }
        bool isCollideGround { get; set; }
        bool IsDead { get; }
        bool RestrictMovement { get; set; }
    }
}
