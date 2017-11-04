using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class ItemSpriteFactory
    {
        private Texture2D lifeMushroomSheet;
        private readonly SpriteSheetInfo lifeMushroomInfo = new SpriteSheetInfo("ItemSprites/1UpMushroom", 32, 32);
        private Texture2D coinSheet;
        private readonly SpriteSheetInfo coinInfo = new SpriteSheetInfo("ItemSprites/Coin", 25, 25, 4, 1, 8);
        private Texture2D fireFlowerSheet;
        private readonly SpriteSheetInfo fireFlowerInfo = new SpriteSheetInfo("ItemSprites/FireFlower", 32, 32, 4, 1, 8);
        private Texture2D mushroomSheet;
        private readonly SpriteSheetInfo mushroomInfo = new SpriteSheetInfo("ItemSprites/PowerupMushroom", 32, 32);
        private Texture2D starSheet;
        private readonly SpriteSheetInfo starInfo = new SpriteSheetInfo("ItemSprites/PowerupStar", 28, 32, 4, 1, 8);

        private static ItemSpriteFactory instance;

        public static ItemSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new ItemSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
            lifeMushroomSheet = content.Load<Texture2D>(lifeMushroomInfo.ContentPath);
            coinSheet = content.Load<Texture2D>(coinInfo.ContentPath);
            fireFlowerSheet = content.Load<Texture2D>(fireFlowerInfo.ContentPath);
            mushroomSheet = content.Load<Texture2D>(mushroomInfo.ContentPath);
            starSheet = content.Load<Texture2D>(starInfo.ContentPath);
        }

        public NonAnimatedSprite CreateSprite_1UpMushroom()
        {
            return new NonAnimatedSprite(lifeMushroomSheet, lifeMushroomInfo);
        }

        public AnimatedSprite CreateSprite_Coin()
        {
            return new AnimatedSprite(coinSheet, coinInfo);
        }

        public AnimatedSprite CreateSprite_FireFlower()
        {
            return new AnimatedSprite(fireFlowerSheet, fireFlowerInfo);
        }

        public NonAnimatedSprite CreateSprite_PowerupMushroom()
        {
            return new NonAnimatedSprite(mushroomSheet, mushroomInfo);
        }

        public AnimatedSprite CreateSprite_Star()
        {
            return new AnimatedSprite(starSheet, starInfo);
        }
    }
}
