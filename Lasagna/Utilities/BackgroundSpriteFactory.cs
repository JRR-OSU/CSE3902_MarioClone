using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lasagna
{
    public class BackgroundSpriteFactory
    {
        private int screenHeight;
        private Texture2D marioClearSheet;
        private readonly SpriteSheetInfo marioClearInfo = new SpriteSheetInfo("LevelBackgrounds/level_1-1", 0, 0);

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

        public void LoadAllContent(ContentManager content, int viewPortHeight)
        {
            screenHeight = viewPortHeight;
            marioClearSheet = content.Load<Texture2D>(marioClearInfo.ContentPath);
        }

        public NonAnimatedSprite CreateBackground_MarioClear()
        {
            return new NonAnimatedSprite(marioClearSheet, new SpriteSheetInfo(string.Empty, screenHeight / marioClearSheet.Height * marioClearSheet.Width, screenHeight));
        }
    }
}
