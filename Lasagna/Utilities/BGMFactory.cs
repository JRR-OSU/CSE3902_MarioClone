using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Lasagna
{
    public class BGMFactory
    {
        private Song menu;
        private Song song;
        private Song castle;
        private Song castleComplete;
        private Song hurryUnderWater;
        private Song intoTheTunnel;
        private Song starMan;
        private Song hurryOverWorld;
        private Song levelComplete;
        private Song gameOver;
        private Song hurryUnderGround;
        private Song youAreDead;
        private Song hurry;
        private Song hurryCastle;
        private Song mainThemeOverWorld;
        private Song underWater;
        private Song underWorld;
        private Song hurryStarMan;
        private Song gameOverTwo;
        private Song ending;
        private static BGMFactory instance;


        public static BGMFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new BGMFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content) {
            castle = content.Load<Song>("BGM/04-castle");
            castleComplete = content.Load<Song>("BGM/07-castle-complete");
            hurryUnderWater = content.Load<Song>("BGM/15-hurry-underwater-");
            intoTheTunnel = content.Load<Song>("BGM/11-into-the-tunnel");
            starMan = content.Load<Song>("BGM/05-starman");
            hurryOverWorld = content.Load<Song>("BGM/18-hurry-overworld-");
            levelComplete = content.Load<Song>("BGM/06-level-complete");
            gameOver = content.Load<Song>("BGM/09-game-over");
            hurryUnderGround = content.Load<Song>("BGM/14-hurry-underground-");
            youAreDead = content.Load<Song>("BGM/08-you-re-dead");
            hurry = content.Load<Song>("BGM/13-hurry");
            hurryCastle = content.Load<Song>("BGM/16-hurry-castle-");
            mainThemeOverWorld = content.Load<Song>("BGM/01-main-theme-overworld");
            underWater = content.Load<Song>("BGM/03-underwater");
            underWorld = content.Load<Song>("BGM/02-underworld");
            hurryStarMan = content.Load<Song>("BGM/17-hurry-starman-");
            gameOverTwo = content.Load<Song>("BGM/10-game-over-2");
            ending = content.Load<Song>("BGM/12-ending");
            menu = content.Load<Song>("BGM/menu_theme");
            Play_Menu();
            MediaPlayer.Volume = 0.9f;
        }
        public void Play_Castle()
        {
            song = castle;
            PlaySound();
        }

        public void Play_Menu()
        {
            song = menu;
            PlaySound();
        }

        public void Play_CastleComplete()
        {
            song = castleComplete;
            PlaySound();
        }

        public void Play_HurryUnderWater()
        {
            song = hurryUnderWater;
            PlaySound();
        }

        public void Play_IntoTheTunnel()
        {
            song = intoTheTunnel;
            PlaySound();
        }

        public void Play_StarMan()
        {
            song = starMan;
            PlaySound();
        }

        public void Play_HurryOverWorld()
        {
            song = hurryOverWorld;
            PlaySound();
        }

        public void Play_LevelComplete()
        {
            song = levelComplete;
            PlaySound();
        }

        public void Play_GameOver()
        {
            song = gameOver;
            PlaySound();
        }

        public void Play_HurryUnderGround()
        {
            song = hurryUnderGround;
            PlaySound();
        }

        public void Play_YouAreDead()
        {
            song = youAreDead;
            PlaySound();
        }

        public void Play_Hurry()
        {
            song = hurry;
            PlaySound();
        }

        public void Play_HurryCastle()
        {
            song = hurryCastle;
            PlaySound();
        }

        public void Play_MainTheme()
        {
            song = mainThemeOverWorld;

            MediaPlayer.Play(song);           
            MediaPlayer.IsRepeating = true;
        }

        public void Play_UnderWater()
        {
            song = underWater;
            PlaySound();
        }

        public void Play_UnderWorld()
        {
            song = underWorld;
            PlaySound();
        }

        public void Play_HurryStarMan()
        {
            song = hurryStarMan;
            PlaySound();
        }

        public void Play_GameOverTwo()
        {
            song = gameOverTwo;
            PlaySound();
        }

        public void Play_Ending()
        {
            song = ending;
            PlaySound();
        }

        public void PlaySound()
        {
            if (song != null)
            {
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(song);

                //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            }
        }

        public void Pause()
        {
            MediaPlayer.Pause();
        }

        public void Resume() => MediaPlayer.Resume();

        public void DisableRepeatMode() => MediaPlayer.IsRepeating = false;


        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(song);
        }
    }
}
