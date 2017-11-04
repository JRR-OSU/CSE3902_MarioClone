using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class ProjectileSpriteFactory
    {
        private Texture2D fireballDefaultSheet;
        private readonly SpriteSheetInfo fireballDefaultInfo = new SpriteSheetInfo("MiscSprites/Fireball_Default", 16, 16, 4, 1, 12);
        private Texture2D fireballExplodeSheet;
        private readonly SpriteSheetInfo fireballExplodeInfo = new SpriteSheetInfo("MiscSprites/Fireball_Explode", 32, 32, 3, 1, 12);

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
            fireballDefaultSheet = content.Load<Texture2D>(fireballDefaultInfo.ContentPath);
            fireballExplodeSheet = content.Load<Texture2D>(fireballExplodeInfo.ContentPath);
        }

        public AnimatedSprite CreateSprite_Fireball_Default()
        {
            return new AnimatedSprite(fireballDefaultSheet, fireballDefaultInfo);
        }

        public AnimatedSprite CreateSprite_Fireball_Explode()
        {
            return new AnimatedSprite(fireballExplodeSheet, fireballExplodeInfo);
        }
    }
}
