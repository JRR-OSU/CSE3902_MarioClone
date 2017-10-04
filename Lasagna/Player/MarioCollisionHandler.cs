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
                    mario.SetPosition(mario.GetRect.X, (tile.Properties.Y - mario.GetRect.Height));
                    break;
                case CollisionSide.Top:
                    mario.SetPosition(mario.GetRect.X, (tile.Properties.Y + tile.Properties.Height));
                    break;
                case CollisionSide.Left:
                    mario.SetPosition(tile.Properties.X + tile.Properties.Width + 3, mario.GetRect.Y);
                    break;
                case CollisionSide.Right:
                    mario.SetPosition(tile.Properties.X - mario.GetRect.Width - 3, mario.GetRect.Y);
                    break;
            }

        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            if (state.isStar() || (enemy is GoombaEnemy && enemy.GetRectangle.Height <= 16) || (enemy is KoopaEnemy && enemy.GetRectangle.Height <= 40)) // if star or enemy is dead
                return;
            switch (side)
            {
                case CollisionSide.Bottom:
                    mario.SetPosition(mario.GetRect.X, (mario.GetRect.Y - enemy.GetRectangle.Height)); // Jump effect if landing on top of an enemy
                    break;
                case CollisionSide.Top:
                    state.DamageMario();
                    mario.SetPosition(mario.GetRect.X, (mario.GetRect.Y + mario.GetRect.Height));
                    break;
                case CollisionSide.Left:
                    state.DamageMario();
                    mario.SetPosition(mario.GetRect.X + mario.GetRect.Width/2, mario.GetRect.Y);
                    break;
                case CollisionSide.Right:
                    state.DamageMario();
                    mario.SetPosition(mario.GetRect.X - mario.GetRect.Width/2, mario.GetRect.Y);
                    break;
            }


        }
    }
}
