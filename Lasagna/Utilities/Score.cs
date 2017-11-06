using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    public static class Score
    {
        public static int marioScore = 0;
        public static int Coins = 0;
        public static int Lives = 3;
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



        public static void AddCoinMario()
        {
            Coins++;
            if(Coins >= 100)
            {
                Lives++;
                Coins = 0;
            }
        }


        public static void ExtraLifeMario()
        {
            Lives++;
        }

        public static void LoseLifeMario()
        {
            Lives--;
        }


        public static void KillMario()
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

        public static void IncreasePoleHeightScore()
        {
            marioScore += 5000;
        }

    }
}
