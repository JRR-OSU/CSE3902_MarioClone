using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public interface IProjectile : ICollider
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void DestroyShell();
        void Reset(object sender, EventArgs e);
        
    }
}
