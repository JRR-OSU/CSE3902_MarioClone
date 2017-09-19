using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class KoopaEnemy
    {
        private ISprite currentSprite;
        private ISprite koopaWalk = EnemySpriteFactory.Instance.CreateSprite_Koopa_Walk();
        private ISprite koopaDead = EnemySpriteFactory.Instance.CreateSprite_Koopa_Die();
        private int posX;
        private int posY;
        private bool liveState = true;
        public void KoopaEnemy(int posX, int posY){
            this.posX = posX;
            this.posY = posY;
        }
        public void changeLiveState(){
            this.liveState = !this.liveState;
        }
        public void Update(GameTime gameTime)
        {
            if(this.liveState == true){
                this.currentSprite = this.koopaWalk;
            }
            else{
                this.currentSprite = this.koopaDead;
            }
            this.currentSprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.currentSprite.Draw(spriteBatch);
        }
    }
}
