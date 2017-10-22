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

        private int hitCount = 0;
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
            return;
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
            if (side.Equals(CollisionSide.Top))
            {
                hitCount++;
            }
            if(hitCount >= 2)
                currentState = KoopaShellStates.Sliding;
        }
        protected override void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            if (projectile is KoopaShellProjectile)
                currentState = KoopaShellStates.Idle;
        }
    }
}
