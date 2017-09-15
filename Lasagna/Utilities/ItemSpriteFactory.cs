using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class ItemSpriteFactory
    {

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
        }
    }
}
