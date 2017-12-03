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
        public static int luigiScore = 0;
        public static int Coins = 0;
        public static int Lives = 3;
        public static int Lives2 = 3;
        public static int[] enemyKilledPoints = new int[9] {200, 400, 500, 800, 1000, 2000, 4000, 8000, 10000};
        public static int marioEnemyKilledCount = 0;
        public static bool flagAtBottom = false;
        private static int oneCoinScore = 200;
        private static int oneItemScore = 1000;
        private static int poleHeightScore = 5000;

        private const int ZERO = 0;
        private const int ONE_HUNDRED = 100;

        

        public static void marioEnemyKill()
        {
            increaseScoreMario(enemyKilledPoints[marioEnemyKilledCount]);
        }



        public static void AddCoinMario()
        {
            Coins++;
            marioScore += oneCoinScore;
            if(Coins >= ONE_HUNDRED)
            {
                Lives++;
                Coins = ZERO;
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

        public static void LoseLifeLuigi()
        {
            Lives2--;
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
            marioEnemyKilledCount = ZERO;
        }
    }
}
