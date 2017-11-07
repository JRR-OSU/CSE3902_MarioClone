using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Lasagna
{
    public class BGMFactory
    {
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
        }
        //Sample
        public void Play_Castle()
        {
            song = castle;
            MediaPlayer.Play(song);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }


        void MediaPlayer_MediaStateChanged(object sender, System.
                                           EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }
    }
}
