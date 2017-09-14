using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class SpriteFactory
    {
        //Mario sprite sheets
        private Texture2D TEMP_marioStanding;
        private Texture2D TEMP_marioRunning;
        //Enemy sprite sheets
        //Item sprite sheets
        //Block sprite sheets

        private static SpriteFactory instance;

        public static SpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new SpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
            TEMP_marioStanding = content.Load<Texture2D>("MarioStanding");
            TEMP_marioRunning = content.Load<Texture2D>("MarioRunningRight");
        }

        public NonAnimatedSprite CreateSprite_MarioStanding()
        {
            return new NonAnimatedSprite(TEMP_marioStanding, 32, 64);
        }

        public AnimatedSprite CreateSprite_MarioRunning()
        {
            return new AnimatedSprite(TEMP_marioRunning, 3, 1, 32, 64, 10);
        }
    }
}
