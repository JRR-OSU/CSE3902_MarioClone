using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class GoombaEnemy : IEnemy
    {
        ISprite currentSprite;
        ISprite goombaWalk = EnemySpriteFactory.Instance.CreateSprite_Goomba_Walk();
        ISprite goombaDead = EnemySpriteFactory.Instance.CreateSprite_Goomba_Die();
        public void changeState(){
            currentSprite = goombaWalk;
        }
        public void Update(GameTime gameTime, int X, int Y)
        {
            currentSprite.Update(gameTime, X, Y);
        }
        public void Draw(SpriteBatch spriteBatch){
            currentSprite.Draw(spriteBatch);
        }
    }
}
