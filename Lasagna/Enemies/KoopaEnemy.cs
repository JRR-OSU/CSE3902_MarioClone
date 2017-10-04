using System.Collections.Generic;

namespace Lasagna
{
    public class KoopaEnemy : MovingEnemy
    {
        private Dictionary<EnemyState, ISprite> koopaStates = new Dictionary<EnemyState, ISprite>()
        {
            { EnemyState.Idle, EnemySpriteFactory.Instance.CreateSprite_Koopa_Walk() },
            { EnemyState.Dead, EnemySpriteFactory.Instance.CreateSprite_Koopa_Die() },
        };

        public KoopaEnemy(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            //WalkLeft and WalkRight use same sprite as idle, set that here.
            if (koopaStates.ContainsKey(EnemyState.Idle))
            {
                koopaStates.Add(EnemyState.WalkLeft, koopaStates[EnemyState.Idle]);
                koopaStates.Add(EnemyState.WalkRight, koopaStates[EnemyState.Idle]);
                CurrentSprite = koopaStates[EnemyState.Idle];
            }
        }

        public override void ChangeState(EnemyState newState)
        {
            if (koopaStates != null && koopaStates.ContainsKey(CurrentState) && koopaStates[CurrentState] != null)
            {
                CurrentState = newState;
                CurrentSprite = koopaStates[CurrentState];
            }
        }

        public override void Damage()
        {
            //TODO: Turn into shell here instead of calling base method
            //Koopa can be killed with one attack in level 1-1
            ChangeState(EnemyState.Dead);
        }
        public override void OnCollisionResponse(IPlayer mario, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Bottom))
            {
                Damage();
            }
        }
        public override void OnCollisionResponse(IProjectile fireball, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Left) || side.Equals(CollisionSide.Right))
            {
                Damage();
            }
        }

    }
}
