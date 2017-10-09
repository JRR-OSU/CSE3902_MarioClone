using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    public class MarioCollisionHandler
    {
        private MarioStateMachine state;
        private Mario mario;

        public MarioCollisionHandler(Mario player, MarioStateMachine marioState)
        {
            state = marioState;
            mario = player;
        }
        public void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            state.Reset();
        }

        public void OnCollisionResponse(IItem item, CollisionSide side)
        {
            if (item is FireFlowerItem)
            {
                state.Fire();
            }
            else if (item is GrowMushroomItem)
            {
                state.Grow();
            }
            else if (item is StarItem)
            {
                state.Star();
            }

        }

        public void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            switch (side)
            {
                case CollisionSide.Bottom:
                    mario.SetPosition(mario.Bounds.X, (tile.Bounds.Y - mario.Bounds.Height));
                    break;
                case CollisionSide.Top:
                    mario.SetPosition(mario.Bounds.X, (tile.Bounds.Y + tile.Bounds.Height));
                    break;
                case CollisionSide.Left:
                    mario.SetPosition(tile.Bounds.X + tile.Bounds.Width + 3, mario.Bounds.Y);
                    break;
                case CollisionSide.Right:
                    mario.SetPosition(tile.Bounds.X - mario.Bounds.Width - 3, mario.Bounds.Y);
                    break;
            }

        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            if (state.isStar() || (enemy is GoombaEnemy && enemy.Bounds.Height <= 16) || (enemy is KoopaEnemy && enemy.Bounds.Height <= 40))// if star or enemy is dead
            {
                switch (side)
                {
                    case CollisionSide.Bottom:
                        mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y - enemy.Bounds.Height)); 
                        break;
                    case CollisionSide.Top:
                        mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y + mario.Bounds.Height));
                        break;
                    case CollisionSide.Left:
                        mario.SetPosition(mario.Bounds.X + mario.Bounds.Width / 2, mario.Bounds.Y);
                        break;
                    case CollisionSide.Right:
                        mario.SetPosition(mario.Bounds.X - mario.Bounds.Width / 2, mario.Bounds.Y);
                        break;
                }
                return;
            }
            switch (side)
            {
                case CollisionSide.Bottom:
                    mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y - enemy.Bounds.Height)); // Jump effect if landing on top of an enemy
                    break;
                case CollisionSide.Top:
                    state.DamageMario();
                    mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y + mario.Bounds.Height));
                    break;
                case CollisionSide.Left:
                    state.DamageMario();
                    mario.SetPosition(mario.Bounds.X + mario.Bounds.Width/2, mario.Bounds.Y);
                    break;
                case CollisionSide.Right:
                    state.DamageMario();
                    mario.SetPosition(mario.Bounds.X - mario.Bounds.Width/2, mario.Bounds.Y);
                    break;
            }


        }
    }
}
