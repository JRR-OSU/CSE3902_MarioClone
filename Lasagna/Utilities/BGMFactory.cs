using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Lasagna
{
    public class BGMFactory
    {
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
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
            castle = content.Load<Song>("prepare");
        }      
    }
}
