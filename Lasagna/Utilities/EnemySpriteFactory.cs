using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class EnemySpriteFactory
    {
        private Texture2D goombaDieSheet;
        private readonly SpriteSheetInfo goombaDieInfo = new SpriteSheetInfo("EnemySprites/Goomba_Die", 32, 16);
        private Texture2D goombaWalkSheet;
        private readonly SpriteSheetInfo goombaWalkInfo = new SpriteSheetInfo("EnemySprites/Goomba_Walk", 32, 32, 2, 1, 8);
        private Texture2D koopaDieSheet;
        private readonly SpriteSheetInfo koopaDieInfo = new SpriteSheetInfo("EnemySprites/Koopa_Die", 55, 40);
        private Texture2D koopaWalkSheet;
        private readonly SpriteSheetInfo koopaWalkInfo = new SpriteSheetInfo("EnemySprites/Koopa_Walk", 32, 48, 2, 1, 8);
        private Texture2D goombaFlippedSheet;
        private readonly SpriteSheetInfo goombaFlippedInfo = new SpriteSheetInfo("EnemySprites/Goomba_Flipped", 35, 44);
        private Texture2D koopaShellSheet;
        private readonly SpriteSheetInfo koopaShellInfo = new SpriteSheetInfo("EnemySprites/Koopa_Shell", 40, 30);
        private Texture2D koopaWalkRightSheet;
        private readonly SpriteSheetInfo koopaWalkRightInfo = new SpriteSheetInfo("EnemySprites/Koopa_Walk_Right", 32, 48, 2, 1, 8);

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
            goombaFlippedSheet = content.Load<Texture2D>(goombaFlippedInfo.ContentPath);
            goombaDieSheet = content.Load<Texture2D>(goombaDieInfo.ContentPath);
            goombaWalkSheet = content.Load<Texture2D>(goombaWalkInfo.ContentPath);
            koopaShellSheet = content.Load<Texture2D>(koopaShellInfo.ContentPath);
            koopaDieSheet = content.Load<Texture2D>(koopaDieInfo.ContentPath);
            koopaWalkSheet = content.Load<Texture2D>(koopaWalkInfo.ContentPath);
            koopaWalkRightSheet = content.Load<Texture2D>(koopaWalkRightInfo.ContentPath);
        }

        public NonAnimatedSprite CreateSprite_Goomba_Die()
        {
            return new NonAnimatedSprite(goombaDieSheet, goombaDieInfo);
        }
        public NonAnimatedSprite CreateSprite_Goomba_Flipped()
        {
            return new NonAnimatedSprite(goombaFlippedSheet, goombaFlippedInfo);
        }

        public AnimatedSprite CreateSprite_Goomba_Walk()
        {
            return new AnimatedSprite(goombaWalkSheet, goombaWalkInfo);
        }
 
        public NonAnimatedSprite CreateSprite_Koopa_Die()
        {
            return new NonAnimatedSprite(koopaDieSheet, koopaDieInfo);
        }
        public NonAnimatedSprite CreateSprite_Koopa_Shell()
        {
            return new NonAnimatedSprite(koopaShellSheet, koopaShellInfo);
        }

        public AnimatedSprite CreateSprite_Koopa_Walk()
        {
            return new AnimatedSprite(koopaWalkSheet, koopaWalkInfo);
        }
        public AnimatedSprite CreateSprite_Koopa_Walk_Left()
        {
            return new AnimatedSprite(koopaWalkRightSheet, koopaWalkRightInfo);
        }
    }
}
