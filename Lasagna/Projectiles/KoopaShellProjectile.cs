using Microsoft.Xna.Framework;

namespace Lasagna
{
    public class KoopaShellProjectile : BaseProjectile
    {
        private enum KoopaShellStates
        {
            Idle,
            Sliding,
            Gone
        }

        private float explodeTimeLeft;
        private float movingUpTimeLeft;
        private KoopaShellStates currentState = KoopaShellStates.Idle;
        private ISprite shellDefault = EnemySpriteFactory.Instance.CreateSprite_Koopa_Shell();

        public KoopaShellProjectile(int spawnPosX, int spawnPosY, bool startMovingRight)
            : base(spawnPosX, spawnPosY, startMovingRight)
        {
            CurrentSprite = shellDefault;
        }

        public override void Update(GameTime gameTime)
        {
            if (currentState == KoopaShellStates.Sliding)
            {
                posX += (float)(gameTime.ElapsedGameTime.TotalSeconds * horizontalMoveSpeed) * (MovingRight ? 1 : -1);
            }

            base.Update(gameTime);
        }

        public override void DestroyShell()
        {
            currentState = KoopaShellStates.Gone;
            CurrentSprite = null;
        }

        protected override void OnCollisionResponse(IEnemy Enemy, CollisionSide side)
        {
            if (currentState == KoopaShellStates.Sliding)
                this.DestroyShell();
        }

        protected override void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            
                this.DestroyShell();
        }

        protected override void OnCollisionResponse(IItem Item, CollisionSide side)
        {
                this.DestroyShell();
        }
        protected override void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            currentState = KoopaShellStates.Sliding;
        }
    }
}
