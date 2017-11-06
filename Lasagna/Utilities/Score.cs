﻿using System;
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


        public static void marioEnemyKill()
        {
            increaseScoreMario(enemyKilledPoints[marioEnemyKilledCount]);
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

        public static void increaseScoreMario(int score)
        {
            marioScore += score;
        }

        public static void IncreasePoleHeightScore()
        {
            marioScore += 5000;
        }

        public static void ResetConsecutiveEnemiesKilled()
        {
            marioEnemyKilledCount = 0;
        }
    }
}
