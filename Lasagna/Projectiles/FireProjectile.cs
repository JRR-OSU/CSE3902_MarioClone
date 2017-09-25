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

        public FireProjectile(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            CurrentSprite = fireballDefault;
        }

        public override void ChangeState()
        {
            ///TODO: Make this work with other states later. Right now just switches to explode.
            currentState = FireballStates.Explode;
            CurrentSprite = fireballExplode;
        }
    }
}
