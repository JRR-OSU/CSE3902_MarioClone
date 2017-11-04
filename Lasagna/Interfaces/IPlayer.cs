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
        
        bool isCollideGround { get; set; }
        bool IsDead { get; }
    }
}
