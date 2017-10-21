using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class MarioSpriteFactory
    {
        private Texture2D marioBigCrouchLeftSheet;
        private Texture2D marioBigCrouchRightSheet;
        private Texture2D marioBigIdleLeftSheet;
        private Texture2D marioBigIdleRightSheet;
        private Texture2D marioBigJumpLeftSheet;
        private Texture2D marioBigJumpRightSheet;
        private Texture2D marioBigRunLeftSheet;
        private Texture2D marioBigRunRightSheet;
        private Texture2D marioBigTurnLeftSheet;
        private Texture2D marioBigTurnRightSheet;
        private Texture2D marioBigShrinkLeftSheet;
        private Texture2D marioBigShrinkRightSheet;
        private Texture2D marioFireCrouchLeftSheet;
        private Texture2D marioFireCrouchRightSheet;
        private Texture2D marioFireIdleLeftSheet;
        private Texture2D marioFireIdleRightSheet;
        private Texture2D marioFireJumpLeftSheet;
        private Texture2D marioFireJumpRightSheet;
        private Texture2D marioFireRunLeftSheet;
        private Texture2D marioFireRunRightSheet;
        private Texture2D marioFireTurnLeftSheet;
        private Texture2D marioFireTurnRightSheet;
        private Texture2D marioSmallDieSheet;
        private Texture2D marioSmallGrowLeftSheet;
        private Texture2D marioSmallGrowRightSheet;
        private Texture2D marioSmallIdleLeftSheet;
        private Texture2D marioSmallIdleRightSheet;
        private Texture2D marioSmallJumpLeftSheet;
        private Texture2D marioSmallJumpRightSheet;
        private Texture2D marioSmallRunLeftSheet;
        private Texture2D marioSmallRunRightSheet;
        private Texture2D marioSmallTurnLeftSheet;
        private Texture2D marioSmallTurnRightSheet;

        private static MarioSpriteFactory instance;

        public static MarioSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new MarioSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {            
            marioBigCrouchLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_CrouchLeft");
            marioBigCrouchRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_CrouchRight");
            marioBigIdleLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_IdleLeft");
            marioBigIdleRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_IdleRight");
            marioBigJumpLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_JumpLeft");
            marioBigJumpRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_JumpRight");
            marioBigRunLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_RunLeft");
            marioBigRunRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_RunRight");
            marioBigTurnLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_TurnLeft");
            marioBigTurnRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_TurnRight");
            marioBigShrinkLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_ShrinkLeft");
            marioBigShrinkRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Big_ShrinkRight");
            marioFireCrouchLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_CrouchLeft");
            marioFireCrouchRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_CrouchRight");
            marioFireIdleLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_IdleLeft");
            marioFireIdleRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_IdleRight");
            marioFireJumpLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_JumpLeft");
            marioFireJumpRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_JumpRight");
            marioFireRunLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_RunLeft");
            marioFireRunRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_RunRight");
            marioFireTurnLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_TurnLeft");
            marioFireTurnRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Fire_TurnRight");
            marioSmallDieSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_Die");
            marioSmallGrowLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_GrowLeft");
            marioSmallGrowRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_GrowRight");
            marioSmallIdleLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_IdleLeft");
            marioSmallIdleRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_IdleRight");
            marioSmallJumpLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_JumpLeft");
            marioSmallJumpRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_JumpRight");
            marioSmallRunLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_RunLeft");
            marioSmallRunRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_RunRight");
            marioSmallTurnLeftSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_TurnLeft");
            marioSmallTurnRightSheet = content.Load<Texture2D>("MarioSprites/Mario_Small_TurnRight");
        }
                
        public NonAnimatedSprite CreateSprite_MarioBig_CrouchLeft()
        {
            return new NonAnimatedSprite(marioBigCrouchLeftSheet, 32, 44);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_CrouchRight()
        {
            return new NonAnimatedSprite(marioBigCrouchRightSheet, 32, 44);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_IdleLeft()
        {
            return new NonAnimatedSprite(marioBigIdleLeftSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_IdleRight()
        {
            return new NonAnimatedSprite(marioBigIdleRightSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_JumpLeft()
        {
            return new NonAnimatedSprite(marioBigJumpLeftSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_JumpRight()
        {
            return new NonAnimatedSprite(marioBigJumpRightSheet, 32, 64);
        }

        public AnimatedSprite CreateSprite_MarioBig_RunLeft()
        {
            return new AnimatedSprite(marioBigRunLeftSheet, 3, 1, 32, 64, 24);
        }

        public AnimatedSprite CreateSprite_MarioBig_RunRight()
        {
            return new AnimatedSprite(marioBigRunRightSheet, 3, 1, 32, 64, 24);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_TurnLeft()
        {
            return new NonAnimatedSprite(marioBigTurnLeftSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_TurnRight()
        {
            return new NonAnimatedSprite(marioBigTurnRightSheet, 32, 64);
        }

        public AnimatedSprite CreateSprite_MarioBig_ShrinkLeft()
        {
            return new AnimatedSprite(marioBigShrinkLeftSheet, 10, 1, 32, 64, 10);
        }

        public AnimatedSprite CreateSprite_MarioBig_ShrinkRight()
        {
            return new AnimatedSprite(marioBigShrinkRightSheet, 10, 1, 32, 64, 10);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_CrouchLeft()
        {
            return new NonAnimatedSprite(marioFireCrouchLeftSheet, 32, 44);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_CrouchRight()
        {
            return new NonAnimatedSprite(marioFireCrouchRightSheet, 32, 44);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_IdleLeft()
        {
            return new NonAnimatedSprite(marioFireIdleLeftSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_IdleRight()
        {
            return new NonAnimatedSprite(marioFireIdleRightSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_JumpLeft()
        {
            return new NonAnimatedSprite(marioFireJumpLeftSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_JumpRight()
        {
            return new NonAnimatedSprite(marioFireJumpRightSheet, 32, 64);
        }

        public AnimatedSprite CreateSprite_MarioFire_RunLeft()
        {
            return new AnimatedSprite(marioFireRunLeftSheet, 3, 1, 32, 64, 24);
        }

        public AnimatedSprite CreateSprite_MarioFire_RunRight()
        {
            return new AnimatedSprite(marioFireRunRightSheet, 3, 1, 32, 64, 24);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_TurnLeft()
        {
            return new NonAnimatedSprite(marioFireTurnLeftSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_TurnRight()
        {
            return new NonAnimatedSprite(marioFireTurnRightSheet, 32, 64);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_Die()
        {
            return new NonAnimatedSprite(marioSmallDieSheet, 28, 28);
        }

        public AnimatedSprite CreateSprite_MarioSmall_GrowLeft()
        {
            return new AnimatedSprite(marioSmallGrowLeftSheet, 10, 1, 32, 64, 10);
        }

        public AnimatedSprite CreateSprite_MarioSmall_GrowRight()
        {
            return new AnimatedSprite(marioSmallGrowRightSheet, 10, 1, 32, 64, 10);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_IdleLeft()
        {
            return new NonAnimatedSprite(marioSmallIdleLeftSheet, 24, 32);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_IdleRight()
        {
            return new NonAnimatedSprite(marioSmallIdleRightSheet, 24, 32);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_JumpLeft()
        {
            return new NonAnimatedSprite(marioSmallJumpLeftSheet, 24, 32);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_JumpRight()
        {
            return new NonAnimatedSprite(marioSmallJumpRightSheet, 24, 32);
        }

        public AnimatedSprite CreateSprite_MarioSmall_RunLeft()
        {
            return new AnimatedSprite(marioSmallRunLeftSheet, 3, 1, 30, 32, 24);
        }

        public AnimatedSprite CreateSprite_MarioSmall_RunRight()
        {
            return new AnimatedSprite(marioSmallRunRightSheet, 3, 1, 30, 32, 24);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_TurnLeft()
        {
            return new NonAnimatedSprite(marioSmallTurnLeftSheet, 24, 32);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_TurnRight()
        {
            return new NonAnimatedSprite(marioSmallTurnRightSheet, 24, 32);
        }
    }
}
