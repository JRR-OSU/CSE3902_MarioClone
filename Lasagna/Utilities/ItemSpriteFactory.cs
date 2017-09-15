using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class ItemSpriteFactory
    {
        private Texture2D lifeMushroomSheet;
        private Texture2D coinSheet;
        private Texture2D fireFlowerSheet;
        private Texture2D mushroomSheet;
        private Texture2D starSheet;

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
            lifeMushroomSheet = content.Load<Texture2D>("ItemSprites/1UpMushroom");
            coinSheet = content.Load<Texture2D>("ItemSprites/Coin");
            fireFlowerSheet = content.Load<Texture2D>("ItemSprites/FireFlower");
            mushroomSheet = content.Load<Texture2D>("ItemSprites/PowerupMushroom");
            starSheet = content.Load<Texture2D>("ItemSprites/PowerupStar");
        }
    }
}
