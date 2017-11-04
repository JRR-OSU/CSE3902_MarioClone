using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class BackgroundSpriteFactory
    {
        private int screenWidth;
        private int screenHeight;
        private Texture2D marioClearSheet;
        private readonly SpriteSheetInfo marioClearInfo = new SpriteSheetInfo("LevelBackgrounds/level_1-1", 400, 400);

        private static BackgroundSpriteFactory instance;

        public static BackgroundSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new BackgroundSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content, int newScreenWidth, int newScreenHeight)
        {
            screenWidth = newScreenWidth;
            screenHeight = newScreenHeight;
            marioClearSheet = content.Load<Texture2D>(marioClearInfo.ContentPath);
        }

        public NonAnimatedSprite CreateBackground_MarioClear()
        {
            return new NonAnimatedSprite(marioClearSheet, new SpriteSheetInfo(string.Empty, screenWidth, screenHeight));
        }
    }
}
