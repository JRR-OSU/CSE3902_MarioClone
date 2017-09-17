using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class ProjectileSpriteFactory
    {
        private Texture2D fireballDefaultSheet;
        private Texture2D fireballExplodeSheet;

        private static ProjectileSpriteFactory instance;

        public static ProjectileSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProjectileSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
            fireballDefaultSheet = content.Load<Texture2D>("MiscSprites/Fireball_Default");
            fireballExplodeSheet = content.Load<Texture2D>("MiscSprites/Fireball_Explode");
        }

        public AnimatedSprite CreateSprite_Fireball_Default()
        {
            return new AnimatedSprite(fireballDefaultSheet, 4, 1, 16, 16, 12);
        }

        public AnimatedSprite CreateSprite_Fireball_Explode()
        {
            return new AnimatedSprite(fireballExplodeSheet, 3, 1, 32, 32, 12);
        }
    }
}
