using System.Collections.Generic;

namespace Lasagna
{
    public class KoopaEnemy : MovingEnemy
    {
        private Dictionary<EnemyState, ISprite> koopaStates = new Dictionary<EnemyState, ISprite>()
        {
            { EnemyState.Idle, EnemySpriteFactory.Instance.CreateSprite_Koopa_Walk() },
            { EnemyState.Dead, EnemySpriteFactory.Instance.CreateSprite_Koopa_Die() },
            { EnemyState.Shell, EnemySpriteFactory.Instance.CreateSprite_Koopa_Shell() },
            { EnemyState.Idle_Left, EnemySpriteFactory.Instance.CreateSprite_Koopa_Walk_Left() },
        };

        public KoopaEnemy(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            //WalkLeft and WalkRight use same sprite as idle, set that here.
            if (koopaStates.ContainsKey(EnemyState.Idle))
            {
                koopaStates.Add(EnemyState.WalkLeft, koopaStates[EnemyState.Idle]);
                koopaStates.Add(EnemyState.WalkRight, koopaStates[EnemyState.Idle_Left]);
                CurrentSprite = koopaStates[EnemyState.Idle_Left];
            }
        }

        public override void ChangeState(EnemyState newState)
        {
            if (koopaStates != null && koopaStates.ContainsKey(CurrentState) && koopaStates[CurrentState] != null)
            {
                FixSpritePosition(koopaStates[CurrentState], koopaStates[newState]);
                CurrentState = newState;
                CurrentSprite = koopaStates[CurrentState];
            }
        }

        public override void Damage()
        {
            CurrentSprite = null;
            MarioGame.Instance.RegisterProjectile(new KoopaShellProjectile(PosX, PosY + 15, true));
            enemyHealth = EnemyHealth.Stomped;
        }

        protected override void OnCollisionResponse(IPlayer mario, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Top) && enemyHealth == EnemyHealth.Normal)
            {
                Damage();
            }
            else if (mario is Mario && ((Mario)mario).StarPowered && enemyHealth == EnemyHealth.Normal)
            {
                ChangeState(EnemyState.Dead);
                enemyHealth = EnemyHealth.Flipped;
            }
        }

        protected override void OnCollisionResponse(IProjectile fireball, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Left) || side.Equals(CollisionSide.Right))
            {
                ChangeState(EnemyState.Dead);
                enemyHealth = EnemyHealth.Flipped;
            }
        }
    }
}
