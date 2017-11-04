using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    public static class Score
    {
        public static int marioScore;
        public static int Coins;
        public static int Lives;
        public static int[] enemyKilledPoints;
        public static int marioEnemyKilledCount;


        public static void marioEnemyKill(Mario mario)
        {
            // Code goes here for chaining enemy kills
            //if ()
            //{
            //    marioEnemyKilledCount++;
            //}
            //else
            //{
            //    marioEnemyKilledCount = 0;
            //}
            marioScore += enemyKilledPoints[marioEnemyKilledCount];
        }



        public static void addCoinMario()
        {
            Coins++;
        }


        public static void extraLifeMario()
        {
            Lives++;
        }


        public static void killMario()
        {
            if (Lives == 0)
            {
                //death screen
            }
            else if (Lives > 0)
            {
                Lives--;
                //restart screen
            }
        }

        public static void increaseScoreMario(int score)
        {
            marioScore += score;
        }


    }
}
