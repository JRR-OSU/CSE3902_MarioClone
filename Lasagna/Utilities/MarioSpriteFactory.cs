using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class MarioSpriteFactory
    {
        private Texture2D TEMP_marioStanding;
        private Texture2D TEMP_marioRunning;

        private static MarioSpriteFactory instance;

        public static MarioSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new MarioSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
            TEMP_marioStanding = content.Load<Texture2D>("MarioSprites/Mario_Big_IdleLeft");
            TEMP_marioRunning = content.Load<Texture2D>("MarioSprites/Mario_Fire_RunRight");
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
