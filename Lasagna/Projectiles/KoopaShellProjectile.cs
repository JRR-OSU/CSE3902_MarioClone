using Microsoft.Xna.Framework;

namespace Lasagna
{
    public class KoopaShellProjectile : BaseProjectile
    {
        private enum KoopaShellStates
        {
            Idle,
            SlidingRight,
            SlidingLeft,
            Gone
        }

        private int hitCount = 0;
        private int slidingTime = 0;
        private KoopaShellStates currentState = KoopaShellStates.Idle;
        private ISprite shellDefault = EnemySpriteFactory.Instance.CreateSprite_Koopa_Shell();

        public KoopaShellProjectile(int spawnPosX, int spawnPosY, bool startMovingRight)
            : base(spawnPosX, spawnPosY, startMovingRight)
        {
            CurrentSprite = shellDefault;
        }

        public override void Update(GameTime gameTime)
        {
            if (slidingTime >= 1000)
            {
                DestroyShell();
            }
            if (currentState == KoopaShellStates.SlidingRight)
            {
                posX += (float)(gameTime.ElapsedGameTime.TotalSeconds * horizontalMoveSpeed) * (MovingRight ? 1 : -1);
                slidingTime++;
            }
            else if (currentState == KoopaShellStates.SlidingLeft)
            {
                posX -= (float)(gameTime.ElapsedGameTime.TotalSeconds * horizontalMoveSpeed) * (MovingRight ? 1 : -1);
                slidingTime++;
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
            if (side.Equals(CollisionSide.Right))
            {
                this.currentState = KoopaShellStates.SlidingLeft;
            }
            else if (side.Equals(CollisionSide.Left))
            {
                this.currentState = KoopaShellStates.SlidingRight;
            }
            else
            {
                this.DestroyShell();
            }
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
            if (hitCount >= 2)
            {
                currentState = KoopaShellStates.SlidingRight;
            }
            if (hitCount >= 2 && (side.Equals(CollisionSide.Left) || side.Equals(CollisionSide.Right)) && 
                (currentState.Equals(KoopaShellStates.SlidingLeft) || currentState.Equals(KoopaShellStates.SlidingRight)))
            {
                ((Mario)player).Die();
            }
        }
        protected override void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            if (projectile is KoopaShellProjectile)
                currentState = KoopaShellStates.Idle;
        }
    }
}
