using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
    namespace Lasagna
    {
        public class HUD : Game
        {
            public const int startTime = 400;
            public const int coinValue = 200;
            public const int brokenBrickValue = 50;
            public const int mushroomValue = 1000;
            public const int fireflowerValue = 1000;
            public const int starValue = 1000;
            public const int oneUpValue = 1000;
            public const int initialLives = 3;
            private const int ZERO = 0;
            private const int FOUR_HUNDRED = 400;
            private const int THREE = 3;
            private const int TEN = 10;
            private const int NINETY_NINE = 99;

            private const string MARIO = "MARIO";
            private const string WORLD = "WORLD";
            private const string LIVES = "LIVES";
            private const string COINS = "COINS";
            private const string TIME = "TIME";
            private const string WORLD_1_1 = "WORLD 1 - 1";
            private const string LEVEL_COMPLETE = "Level Complete!\nPress R to reset";
            private const string GAME_OVER = "Game Over";




            private int counter = 0;

            private int FPS = 60;

            ISprite ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Coin();


            private int Time;

            private bool isDeathScreen = true;
            private bool isGameOver = false;


            public HUD()
            {
                 MarioEvents.OnReset += Reset;

                Score.Lives = initialLives;
                Score.marioScore = ZERO;
                Score.Coins = ZERO;

                Time = startTime;
                Score.enemyKilledPoints = new int[10] { 100, 200, 400, 500, 800, 1000, 2000, 4000, 8000, 10000 };
               Score.marioEnemyKilledCount = ZERO;
               //mario = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
            //mario.SetSpriteScreenPosition(640 / 2, 480 / 2);
            }

            public void Update()
            {
            Time--;
            if (Time < 100)
                BGMFactory.Instance.Play_HurryOverWorld();
                    if (isDeathScreen || MarioGame.Instance.gameComplete)
                        return;
                    
                    if (counter < FPS)
                    {
                        counter++;
                    }
                    else
                    {
                        Time--;
                        counter = ZERO;
                    }
                    if (Time <= ZERO || Score.Lives <= ZERO)
                    {
                        MarioGame.Instance.TriggerDeathSequence();
                        isGameOver = true;
                    }
                    else
                        isGameOver = false;
                
            }

        public void Draw(SpriteBatch batch, SpriteFont font, bool deathScreen, bool gameComplete)
            {

                isDeathScreen = deathScreen;
                batch.Begin();
                batch.DrawString(font, MARIO + addSpaces(4) + COINS + addSpaces(4) + LIVES + addSpaces(5) + TIME + addSpaces(4) + WORLD, new Vector2(10, 10), Color.White);

                batch.DrawString(font, formattedScore(Score.marioScore) + addSpaces(3) + formattedCoins(Score.Coins) + addSpaces(7) + formattedLives(Score.Lives) + addSpaces(8) + Time.ToString() + addSpaces(5) + "1-1", new Vector2(10, 25), Color.White);
            


                if (gameComplete)
                {
                    batch.DrawString(font, WORLD_1_1, new Vector2((640 / 2)-30, (480 / 2)-50), Color.White);
                    batch.DrawString(font, LEVEL_COMPLETE, new Vector2((640 / 2) - 30, (480 / 2)), Color.White);
                }

                else if (deathScreen && !isGameOver)
                {
                    batch.DrawString(font, "WORLD 1 - 1\n\n" + addSpaces(4) +  "x  " + Score.Lives, new Vector2((640 / 2)-30, (480 / 2)-50), Color.White);
                    Time = FOUR_HUNDRED;
                
                    //mario.Draw(batch);
                }

                else if (isGameOver && deathScreen)
                {
                    batch.DrawString(font, GAME_OVER, new Vector2((640 / 2) - 30, (480 / 2) - 50), Color.White);
                    Time = 400;
                    Score.Lives = THREE;
                    Score.marioScore = ZERO;
                    Score.Coins = ZERO;
                }
               
                batch.End();

            }

            private String addSpaces(int spaces)
            {
                String text = "";
                for (int i = 0; i < spaces; i++) text = text + " ";
                return text;
            }

            private String formattedScore(int score)
            {
                int gameScore = score;
                String text = "";
                for (int i = 0; i < 6; i++)
                {
                    text = (gameScore % TEN) + text;
                    gameScore /= TEN;
                }
                return text;
            }

            private String formattedCoins(int coins)
            {
                String text = "";
                if (coins < TEN)
                {
                    text = "0" + coins.ToString();
                }
                else
                {
                    text = coins.ToString();
                }
                return text;
            }

            private String formattedLives(int lives)
            {
                String text = "";
                if (lives < TEN)
                {
                    text = "0" + lives.ToString();
                }
                else if (lives > NINETY_NINE)
                {
                    text = "99";
                    lives = NINETY_NINE;
                }
                else
                {
                    text = lives.ToString();
                }
                return text;
            }

            public void Reset(object sender, EventArgs e)
            {

                //Score.Lives = initialLives;
                Score.marioScore = ZERO;
                Score.Coins = ZERO;
                MarioGame.Instance.gameComplete = false;
                Time = startTime;
                Score.enemyKilledPoints = new int[10] { 100, 200, 400, 500, 800, 1000, 2000, 4000, 8000, 10000 };
                Score.marioEnemyKilledCount = ZERO;
            }
        }
    }