﻿using System.Collections.Generic;

namespace Lasagna
{
    public class GoombaEnemy : MovingEnemy
    {
        private Dictionary<EnemyState, ISprite> goombaStates = new Dictionary<EnemyState, ISprite>()
        {
            { EnemyState.Idle, EnemySpriteFactory.Instance.CreateSprite_Goomba_Walk() },
            { EnemyState.Dead, EnemySpriteFactory.Instance.CreateSprite_Goomba_Die() },
        };

        public GoombaEnemy(int spawnPosX, int spawnPosY) 
            : base(spawnPosX, spawnPosY)
        {
            //WalkLeft and WalkRight use same sprite as idle, set that here.
            if (goombaStates.ContainsKey(EnemyState.Idle))
            {
                goombaStates.Add(EnemyState.WalkLeft, goombaStates[EnemyState.Idle]);
                goombaStates.Add(EnemyState.WalkRight, goombaStates[EnemyState.Idle]);
                CurrentSprite = goombaStates[EnemyState.Idle];
            }
        }

        public override void ChangeState(EnemyState newState)
        {
            if (goombaStates != null && goombaStates.ContainsKey(CurrentState) && goombaStates[CurrentState] != null)
            {
                CurrentState = newState;
                CurrentSprite = goombaStates[CurrentState];
            }
        }
        public override void OnCollisionResponse(IPlayer mario, CollisionSide side)
        {
            if(side.Equals(CollisionSide.Top))
            {
                ChangeState(EnemyState.Dead);
            }
        }
        public override void OnCollisionResponse(IProjectile fireball, CollisionSide side)
        {
            if (side.Equals(CollisionSide.Left) || side.Equals(CollisionSide.Right))
            {
                ChangeState(EnemyState.Dead);
            }
        }


    }
}
