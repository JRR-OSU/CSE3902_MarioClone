using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    public class MarioCollisionHandler
    {
        private MarioStateMachine mario;

        /// <summary>
        /// Test code for collisions... will flesh out when we finish collision detection.
        /// </summary>
        /// <param name="player"></param>
        public MarioCollisionHandler(MarioStateMachine player)
        {
            mario = player;
        }

       public void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            mario.Reset();
        }

        public void OnCollisionResponse(IItem item, CollisionSide side)
        {
            mario.Reset();
        }

        public void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            mario.Reset();
        }
    }
}
