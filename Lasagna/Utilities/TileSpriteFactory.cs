using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class TileSpriteFactory
    {
        private Texture2D breakableBrickSheet;
        private Texture2D floorSheet;
        private Texture2D itemBlockUsedSheet;
        private Texture2D pipeBaseSheet;
        private Texture2D pipeTipSheet;
        private Texture2D questionBlockSheet;
        private Texture2D unbreakableBlockSheet;
        private Texture2D flagSheet;
        private Texture2D flagPoleSheet;

        private static TileSpriteFactory instance;

        public static TileSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new TileSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
        }
    }
}
