using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class TileSpriteFactory
    {

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
