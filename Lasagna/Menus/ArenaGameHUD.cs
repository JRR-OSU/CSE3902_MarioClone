using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lasagna
{
    public class ArenaGameHUD : IHUD
    {
        public const int coinValue = 200;
        public const int brokenBrickValue = 50;
        public const int mushroomValue = 1000;
        public const int fireflowerValue = 1000;
        public const int starValue = 1000;
        public const int oneUpValue = 1000;
        public const int marioLives = 3;
        public const int luigiLives = 3;
        private const int ZERO = 0;
        private const int FOUR_HUNDRED = 400;
        private const int THREE = 3;
        private const int TEN = 10;
        private const int NINETY_NINE = 99;

        private const string MARIO = "MARIO";
        private const string LUIGI = "LUIGI";
        private const string LIVES = "LIVES";
        private const string COINS = "COINS";
        private const string MARIO_WIN = "MARIO IS THE VICTOR!";
        private const string LUIGI_WIN = "LUIGI IS THE VICTOR!";

        private const string LUIGI_ROUND_WIN = "LUIGI WINS THE ROUND";
        private const string MARIO_ROUND_WIN = "MARIO WINS THE ROUND";
        private const string RESTART = "Press R to restart";



        private bool MARIO_DEAD = false;
        private bool LUIGI_DEAD = false;
        public List<IPlayer> players;

        private int counter = 0;

        private int FPS = 60;

        ISprite ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Coin();

        private bool isDeathScreen = true;
        private bool isGameOver = false;

        ISprite mario;
        ISprite luigi;

        public ArenaGameHUD()
        {

            players = MarioGame.Instance.GetArenaPlayerList();
            MarioEvents.OnReset += Reset;

            Score.Lives = marioLives;
            Score.marioScore = ZERO;
            Score.Coins = ZERO;

            Score.enemyKilledPoints = new int[10] { 100, 200, 400, 500, 800, 1000, 2000, 4000, 8000, 10000 };
            Score.marioEnemyKilledCount = ZERO;
            mario = MarioSpriteFactory.Instance.CreateSprite_MarioSmall_IdleRight();
            luigi = LuigiSpriteFactory.Instance.CreateSprite_LuigiSmall_IdleRight();
            mario.SetSpriteScreenPosition((640 / 2) + 75, (480 / 2) - 20);
            luigi.SetSpriteScreenPosition((640 / 2) + 75, (480 / 2) - 20);
            Console.WriteLine(this.GetHashCode().ToString());
        }

        public void Update()
        {
            if (isDeathScreen || MarioGame.Instance.gameComplete)
                return;

            if (counter < FPS)
            {
                counter++;
            }
            else
            {
                counter = ZERO;
            }


            if (players[0].IsDead)
            {
                MARIO_DEAD = true;
            }
            else if (players[1].IsDead)
            {
                LUIGI_DEAD = true;
            }


        }

        public void Draw(SpriteBatch batch, SpriteFont font, bool deathScreen, bool gameComplete)
        {
            isDeathScreen = deathScreen;
            batch.Begin();
            batch.DrawString(font, MARIO + addSpaces(4) + LIVES + addSpaces(20)+ LUIGI + addSpaces(4) + LIVES, new Vector2(10, 10), Color.White);

            batch.DrawString(font, formattedScore(Score.marioScore) + addSpaces(3) + formattedCoins(Score.Lives) + addSpaces(23) + formattedScore(Score.luigiScore) + addSpaces(3) + formattedLives(Score.Lives2), new Vector2(10, 25), Color.White);




            if (gameComplete)
            {
                batch.DrawString(font, "WORLD_1_1", new Vector2((640 / 2) - 30, (480 / 2) - 50), Color.White);
                batch.DrawString(font, "LEVEL_COMPLETE", new Vector2((640 / 2) - 30, (480 / 2)), Color.White);
            }

            else if (deathScreen && (Score.Lives == 0 || Score.Lives2 == 0))
            {
                MarioGame.Instance.gameComplete = true;
                batch.End();
                batch.Begin();
                if (Score.Lives <= 0)
                {
                    batch.DrawString(font, LUIGI_WIN, new Vector2((640 / 2) - 30, (480 / 2) - 50), Color.White);
                    batch.DrawString(font, RESTART, new Vector2((640 / 2) - 30, (480 / 2) - 10), Color.White);

                    Score.marioScore = ZERO;
                    Score.luigiScore = ZERO;


                }
                if (Score.Lives2 <= 0)
                {
                    batch.DrawString(font, MARIO_WIN, new Vector2((640 / 2) - 30, (480 / 2) - 50), Color.White);
                    batch.DrawString(font, RESTART, new Vector2((640 / 2) - 30, (480 / 2) - 10), Color.White);

                    Score.marioScore = ZERO;
                    Score.luigiScore = ZERO;

                }

            }
            else if (deathScreen && !isGameOver)
            {
                batch.End();
                batch.Begin();
                if (MARIO_DEAD)
                {
                    batch.DrawString(font, LUIGI_ROUND_WIN, new Vector2((640 / 2) - 75, (480 / 2) - 50), Color.White);
                    batch.End();
                    luigi.Draw(batch);
                }
                else if (LUIGI_DEAD)
                {
                    batch.DrawString(font, MARIO_ROUND_WIN, new Vector2((640 / 2) - 75, (480 / 2) - 50), Color.White);
                    batch.End();
                    mario.Draw(batch);
                }

            }

            batch.End();

          //  batch.End();

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

            Score.Coins = ZERO;
            MarioGame.Instance.gameComplete = false;
            Score.enemyKilledPoints = new int[10] { 100, 200, 400, 500, 800, 1000, 2000, 4000, 8000, 10000 };
            Score.marioEnemyKilledCount = ZERO;
        }
    }
}
