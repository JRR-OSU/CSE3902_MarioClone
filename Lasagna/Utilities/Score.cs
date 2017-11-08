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
        public static int[] enemyKilledPoints = new int[9] {200, 400, 500, 800, 1000, 2000, 4000, 8000, 10000};
        public static int marioEnemyKilledCount = 0;

        private static int OneCoinScore = 200;
        private static int OneItemScore = 1000;



        public static void marioEnemyKill()
        {
            increaseScoreMario(enemyKilledPoints[marioEnemyKilledCount]);
        }



        public static void AddCoinMario()
        {
            Coins++;
            marioScore += oneCoinScore;
            if(Coins >= 100)
            {
                Lives++;
                Coins = 0;
            }
        }

        public static void AddItemScore()
        {
            marioScore += oneItemScore;
        }


        public static void ExtraLifeMario()
        {
            Lives++;
        }

        public static void LoseLifeMario()
        {
            Lives--;
        }

        public static void increaseScoreMario(int score)
        {
            marioScore += score;
        }

        public static void AddPoleHeightScore()
        {
            marioScore += poleHeightScore;
        }

        public static void ResetConsecutiveEnemiesKilled()
        {
            marioEnemyKilledCount = 0;
        }
    }
}
