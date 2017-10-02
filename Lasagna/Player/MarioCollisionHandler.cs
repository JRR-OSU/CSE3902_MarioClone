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
            Console.WriteLine("Collison mario with player");
            state.Reset();
        }

        public void OnCollisionResponse(IItem item, CollisionSide side)
        {
            Console.WriteLine("Collison mario with item");
            Console.WriteLine(item + " " + " " + side);
            state.Reset();
        }

        public void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            Console.WriteLine("Collison mario with tile");
            Console.WriteLine(tile + " " + " " + side);

            if (tile is FloorBlockTile)
            {
                mario.SetPos(mario.GetRect.X, tile.GetProperties().Y - tile.GetProperties().Height);
            }
            else if (tile is QuestionBlockTile || tile is UnbreakableBlockTile || tile is WarpPipeTile)
            {
                switch (side)
                {
                    case CollisionSide.Top:
                        mario.SetPos(mario.GetRect.X, tile.GetProperties().Y - tile.GetProperties().Height);
                        break;
                    case CollisionSide.Bottom:
                        mario.SetPos(mario.GetRect.X, tile.GetProperties().Y + tile.GetProperties().Height);
                        break;
                    case CollisionSide.Left:
                        mario.SetPos(tile.GetProperties().X + tile.GetProperties().Width, mario.GetRect.Y);
                        break;
                    case CollisionSide.Right:
                        mario.SetPos(tile.GetProperties().X - tile.GetProperties().Width, mario.GetRect.Y);
                        break;
                }
            }
            
        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            Console.WriteLine("Collison mario with enemy");
            Console.WriteLine(enemy + " " + " " + side);
            state.Reset();
        }
    }
}
