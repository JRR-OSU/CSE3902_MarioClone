using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lasagna
{
    class GoombaEnemy : IEnemy
    {
        private ISprite currentSprite;
        private ISprite goombaWalk = EnemySpriteFactory.Instance.CreateSprite_Goomba_Walk();
        private ISprite goombaDead = EnemySpriteFactory.Instance.CreateSprite_Goomba_Die();
        private int posX;
        private int posY;
        private bool liveState = true;
        public GoombaEnemy(int posX, int posY){
            this.posX = posX;
            this.posY = posY;
        }
        public void changeLiveState(){
            this.liveState = !this.liveState;
        }
        public void Update(GameTime gameTime)
        {
            if(this.liveState == true){
                this.currentSprite = this.goombaWalk;
            }
            else{
                this.currentSprite = this.goombaDead;
            }
            this.currentSprite.Update(gameTime, this.posX, this.posY);
        }
        public void Draw(SpriteBatch spriteBatch){
            this.currentSprite.Draw(spriteBatch);
        }
    }
}
