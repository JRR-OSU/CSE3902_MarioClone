using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class FireProjectile : BaseProjectile
    {
        private enum FireballStates
        {
            Idle,
            Explode
        }

        private FireballStates currentState = FireballStates.Idle;
        private ISprite fireballDefault = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Default();
        private ISprite fireballExplode = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Explode();

        public FireProjectile(int spawnPosX, int spawnPosY, bool startMovingRight)
            : base(spawnPosX, spawnPosY, startMovingRight)
        {
            CurrentSprite = fireballDefault;
        }

        public override void Update(GameTime gameTime)
        {
            posX += (float)(gameTime.ElapsedGameTime.TotalSeconds * moveSpeed) * (MovingRight ? 1 : -1);

            base.Update(gameTime);
        }

        public override void ChangeState()
        {
            ///TODO: Make this work with other states later. Right now just switches to explode.
            currentState = FireballStates.Explode;
            CurrentSprite = fireballExplode;
        }

        protected override void OnCollisionResponse(IEnemy Enemy, CollisionSide side)
        {
            this.ChangeState();
        }

        protected override void OnCollisionResponse(IItem Item, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Right) && this.currentState == FireballStates.Idle)
            {
                this.ChangeState();
            }
        }
    }
}
