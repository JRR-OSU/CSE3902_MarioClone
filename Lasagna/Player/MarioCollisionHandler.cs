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

        /// <summary>
        /// Test code for collisions... will flesh out when we finish collision detection.
        /// </summary>
        /// <param name="marioState"></param>
        public MarioCollisionHandler(Mario player, MarioStateMachine marioState)
        {
            state = marioState;
            mario = player;
        }

       public void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
           // Console.WriteLine("Collison mario with player");
            state.Reset();
        }

        public void OnCollisionResponse(IItem item, CollisionSide side)
        {
            //Console.WriteLine("Collison mario with item");
            //Console.WriteLine(item + " " + " " + side);

            if(item is FireFlowerItem)
            {
                state.Fire();
            }
            else if(item is GrowMushroomItem)
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

            if (tile is FloorBlockTile)
            {
                if(state.GetState() != MarioStateMachine.MarioState.Small)
                    mario.SetPos(mario.GetRect.X, (tile.Properties.Y - tile.Properties.Height) - (mario.GetRect.Height) / 2);
                else
                    mario.SetPos(mario.GetRect.X, (tile.Properties.Y - tile.Properties.Height));
                return;
            }
            else if (tile is QuestionBlockTile || tile is InvisibleItemBlockTile || tile is BreakableBrickTile || tile is UnbreakableBlockTile || tile is WarpPipeTile)
            {
                switch (side)
                {
                    case CollisionSide.Top:
                        if (state.GetState() != MarioStateMachine.MarioState.Small)
                            mario.SetPos(mario.GetRect.X, (tile.Properties.Y - tile.Properties.Height) - (mario.GetRect.Height) / 2);
                        else
                            mario.SetPos(mario.GetRect.X, (tile.Properties.Y - tile.Properties.Height));
                        break;
                    case CollisionSide.Bottom:
                        if (state.GetState() != MarioStateMachine.MarioState.Small)
                            mario.SetPos(mario.GetRect.X, (tile.Properties.Y + tile.Properties.Height) + (mario.GetRect.Height / 2));
                        else
                            mario.SetPos(mario.GetRect.X, (tile.Properties.Y + tile.Properties.Height));
                        break;
                    case CollisionSide.Left:
                        mario.SetPos(tile.Properties.X + tile.Properties.Width+5, mario.GetRect.Y);
                        break;
                    case CollisionSide.Right:
                        mario.SetPos(tile.Properties.X - mario.GetRect.Width-5, mario.GetRect.Y);
                        break;
                }
            }
            
        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            // Console.WriteLine("Collison mario with enemy");
            // Console.WriteLine(enemy + " " + " " + side);
            state.Reset();
        }
    }
}
