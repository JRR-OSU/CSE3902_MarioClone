using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class EnemySpriteFactory
    {
        private Texture2D goombaDieSheet;
        private Texture2D goombaWalkSheet;
        private Texture2D koopaDieSheet;
        private Texture2D koopaWalkSheet;

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
            goombaDieSheet = content.Load<Texture2D>("EnemySprites/Goomba_Die");
            goombaWalkSheet = content.Load<Texture2D>("EnemySprites/Goomba_Walk");
            koopaDieSheet = content.Load<Texture2D>("EnemySprites/Koopa_Die");
            koopaWalkSheet = content.Load<Texture2D>("EnemySprites/Koopa_Walk");
        }

        public NonAnimatedSprite CreateSprite_Goomba_Die()
        {
            return new NonAnimatedSprite(goombaDieSheet, 16, 8);
        }

        public AnimatedSprite CreateSprite_Goomba_Walk()
        {
            return new AnimatedSprite(goombaWalkSheet, 2, 1, 16, 16, 24);
        }
        public AnimatedSprite CreateSprite_Koopa_Die()
        {
            return new AnimatedSprite(koopaDieSheet, 2, 1, 16, 15, 24);
        }

        public AnimatedSprite CreateSprite_Koopa_Walk()
        {
            return new AnimatedSprite(koopaWalkSheet, 2, 1, 16, 24, 24);
        }
    }
}
