using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    class EnemySpriteFactory
    {

        private static EnemySpriteFactory instance;

        public static EnemySpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new EnemySpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
        }
    }
}
