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
        private Texture2D goombaFlippedSheet;
        private Texture2D koopaShellSheet;
        private Texture2D koopaWalkRightSheet;

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
            goombaFlippedSheet = content.Load<Texture2D>("EnemySprites/Goomba_Flipped");
            goombaDieSheet = content.Load<Texture2D>("EnemySprites/Goomba_Die");
            goombaWalkSheet = content.Load<Texture2D>("EnemySprites/Goomba_Walk");
            koopaShellSheet = content.Load<Texture2D>("EnemySprites/Koopa_Shell");
            koopaDieSheet = content.Load<Texture2D>("EnemySprites/Koopa_Die");
            koopaWalkSheet = content.Load<Texture2D>("EnemySprites/Koopa_Walk");
            koopaWalkRightSheet = content.Load<Texture2D>("EnemySprites/Koopa_Walk_Right");
        }

        public NonAnimatedSprite CreateSprite_Goomba_Die()
        {
            return new NonAnimatedSprite(goombaDieSheet, 32, 16);
        }
        public NonAnimatedSprite CreateSprite_Goomba_Flipped()
        {
            return new NonAnimatedSprite(goombaFlippedSheet, 35, 44);
        }

        public AnimatedSprite CreateSprite_Goomba_Walk()
        {
            return new AnimatedSprite(goombaWalkSheet, 2, 1, 32, 32, 8);
        }
 
        public NonAnimatedSprite CreateSprite_Koopa_Die()
        {
            return new NonAnimatedSprite(koopaDieSheet, 55, 40);
        }
        public NonAnimatedSprite CreateSprite_Koopa_Shell()
        {
            return new NonAnimatedSprite(koopaShellSheet, 45, 40);
        }

        public AnimatedSprite CreateSprite_Koopa_Walk()
        {
            return new AnimatedSprite(koopaWalkSheet, 2, 1, 32, 48, 8);
        }
        public AnimatedSprite CreateSprite_Koopa_Walk_Left()
        {
            return new AnimatedSprite(koopaWalkRightSheet, 2, 1, 32, 48, 8);
        }
    }
}
