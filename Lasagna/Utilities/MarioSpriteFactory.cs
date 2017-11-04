using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class MarioSpriteFactory
    {
        private Texture2D marioBigCrouchLeftSheet;
        private readonly SpriteSheetInfo marioBigCrouchLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_CrouchLeft", 32, 44);
        private Texture2D marioBigCrouchRightSheet;
        private readonly SpriteSheetInfo marioBigCrouchRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_CrouchRight", 32, 44);
        private Texture2D marioBigIdleLeftSheet;
        private readonly SpriteSheetInfo marioBigIdleLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_IdleLeft", 32, 64);
        private Texture2D marioBigIdleRightSheet;
        private readonly SpriteSheetInfo marioBigIdleRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_IdleRight", 32, 64);
        private Texture2D marioBigJumpLeftSheet;
        private readonly SpriteSheetInfo marioBigJumpLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_JumpLeft", 32, 64);
        private Texture2D marioBigJumpRightSheet;
        private readonly SpriteSheetInfo marioBigJumpRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_JumpRight", 32, 64);
        private Texture2D marioBigRunLeftSheet;
        private readonly SpriteSheetInfo marioBigRunLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_RunLeft", 32, 64, 3, 1, 24);
        private Texture2D marioBigRunRightSheet;
        private readonly SpriteSheetInfo marioBigRunRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_RunRight", 32, 64, 3, 1, 24);
        private Texture2D marioBigTurnLeftSheet;
        private readonly SpriteSheetInfo marioBigTurnLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_TurnLeft", 32, 64);
        private Texture2D marioBigTurnRightSheet;
        private readonly SpriteSheetInfo marioBigTurnRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_TurnRight", 32, 64);
        private Texture2D marioBigShrinkLeftSheet;
        private readonly SpriteSheetInfo marioBigShrinkLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_ShrinkLeft", 32, 64, 10, 1, 10);
        private Texture2D marioBigShrinkRightSheet;
        private readonly SpriteSheetInfo marioBigShrinkRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Big_ShrinkRight", 32, 64, 10, 1, 10);
        private Texture2D marioFireCrouchLeftSheet;
        private readonly SpriteSheetInfo marioFireCrouchLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_CrouchLeft", 32, 44);
        private Texture2D marioFireCrouchRightSheet;
        private readonly SpriteSheetInfo marioFireCrouchRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_CrouchRight", 32, 44);
        private Texture2D marioFireIdleLeftSheet;
        private readonly SpriteSheetInfo marioFireIdleLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_IdleLeft", 32, 64);
        private Texture2D marioFireIdleRightSheet;
        private readonly SpriteSheetInfo marioFireIdleRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_IdleRight", 32, 64);
        private Texture2D marioFireJumpLeftSheet;
        private readonly SpriteSheetInfo marioFireJumpLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_JumpLeft", 32, 64);
        private Texture2D marioFireJumpRightSheet;
        private readonly SpriteSheetInfo marioFireJumpRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_JumpRight", 32, 64);
        private Texture2D marioFireRunLeftSheet;
        private readonly SpriteSheetInfo marioFireRunLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_RunLeft", 32, 64, 3, 1, 24);
        private Texture2D marioFireRunRightSheet;
        private readonly SpriteSheetInfo marioFireRunRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_RunRight", 32, 64, 3, 1, 24);
        private Texture2D marioFireTurnLeftSheet;
        private readonly SpriteSheetInfo marioFireTurnLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_TurnLeft", 32, 64);
        private Texture2D marioFireTurnRightSheet;
        private readonly SpriteSheetInfo marioFireTurnRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Fire_TurnRight", 32, 64);
        private Texture2D marioSmallDieSheet;
        private readonly SpriteSheetInfo marioSmallDieInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_Die", 28, 28);
        private Texture2D marioSmallGrowLeftSheet;
        private readonly SpriteSheetInfo marioSmallGrowLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_GrowLeft", 32, 64, 10, 1, 10);
        private Texture2D marioSmallGrowRightSheet;
        private readonly SpriteSheetInfo marioSmallGrowRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_GrowRight", 32, 64, 10, 1, 10);
        private Texture2D marioSmallIdleLeftSheet;
        private readonly SpriteSheetInfo marioSmallIdleLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_IdleLeft", 24, 32);
        private Texture2D marioSmallIdleRightSheet;
        private readonly SpriteSheetInfo marioSmallIdleRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_IdleRight", 24, 32);
        private Texture2D marioSmallJumpLeftSheet;
        private readonly SpriteSheetInfo marioSmallJumpLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_JumpLeft", 24, 32);
        private Texture2D marioSmallJumpRightSheet;
        private readonly SpriteSheetInfo marioSmallJumpRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_JumpRight", 24, 32);
        private Texture2D marioSmallRunLeftSheet;
        private readonly SpriteSheetInfo marioSmallRunLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_RunLeft", 30, 32, 3, 1, 24);
        private Texture2D marioSmallRunRightSheet;
        private readonly SpriteSheetInfo marioSmallRunRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_RunRight", 30, 32, 3, 1, 24);
        private Texture2D marioSmallTurnLeftSheet;
        private readonly SpriteSheetInfo marioSmallTurnLeftInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_TurnLeft", 24, 32);
        private Texture2D marioSmallTurnRightSheet;
        private readonly SpriteSheetInfo marioSmallTurnRightInfo = new SpriteSheetInfo("MarioSprites/Mario_Small_TurnRight", 24, 32);

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
            marioBigCrouchLeftSheet = content.Load<Texture2D>(marioBigCrouchLeftInfo.ContentPath);
            marioBigCrouchRightSheet = content.Load<Texture2D>(marioBigCrouchRightInfo.ContentPath);
            marioBigIdleLeftSheet = content.Load<Texture2D>(marioBigIdleLeftInfo.ContentPath);
            marioBigIdleRightSheet = content.Load<Texture2D>(marioBigIdleRightInfo.ContentPath);
            marioBigJumpLeftSheet = content.Load<Texture2D>(marioBigJumpLeftInfo.ContentPath);
            marioBigJumpRightSheet = content.Load<Texture2D>(marioBigJumpRightInfo.ContentPath);
            marioBigRunLeftSheet = content.Load<Texture2D>(marioBigRunLeftInfo.ContentPath);
            marioBigRunRightSheet = content.Load<Texture2D>(marioBigRunRightInfo.ContentPath);
            marioBigTurnLeftSheet = content.Load<Texture2D>(marioBigTurnLeftInfo.ContentPath);
            marioBigTurnRightSheet = content.Load<Texture2D>(marioBigTurnRightInfo.ContentPath);
            marioBigShrinkLeftSheet = content.Load<Texture2D>(marioBigShrinkLeftInfo.ContentPath);
            marioBigShrinkRightSheet = content.Load<Texture2D>(marioBigShrinkRightInfo.ContentPath);
            marioFireCrouchLeftSheet = content.Load<Texture2D>(marioFireCrouchLeftInfo.ContentPath);
            marioFireCrouchRightSheet = content.Load<Texture2D>(marioFireCrouchRightInfo.ContentPath);
            marioFireIdleLeftSheet = content.Load<Texture2D>(marioFireIdleLeftInfo.ContentPath);
            marioFireIdleRightSheet = content.Load<Texture2D>(marioFireIdleRightInfo.ContentPath);
            marioFireJumpLeftSheet = content.Load<Texture2D>(marioFireJumpLeftInfo.ContentPath);
            marioFireJumpRightSheet = content.Load<Texture2D>(marioFireJumpRightInfo.ContentPath);
            marioFireRunLeftSheet = content.Load<Texture2D>(marioFireRunLeftInfo.ContentPath);
            marioFireRunRightSheet = content.Load<Texture2D>(marioFireRunRightInfo.ContentPath);
            marioFireTurnLeftSheet = content.Load<Texture2D>(marioFireTurnLeftInfo.ContentPath);
            marioFireTurnRightSheet = content.Load<Texture2D>(marioFireTurnRightInfo.ContentPath);
            marioSmallDieSheet = content.Load<Texture2D>(marioSmallDieInfo.ContentPath);
            marioSmallGrowLeftSheet = content.Load<Texture2D>(marioSmallGrowLeftInfo.ContentPath);
            marioSmallGrowRightSheet = content.Load<Texture2D>(marioSmallGrowRightInfo.ContentPath);
            marioSmallIdleLeftSheet = content.Load<Texture2D>(marioSmallIdleLeftInfo.ContentPath);
            marioSmallIdleRightSheet = content.Load<Texture2D>(marioSmallIdleRightInfo.ContentPath);
            marioSmallJumpLeftSheet = content.Load<Texture2D>(marioSmallJumpLeftInfo.ContentPath);
            marioSmallJumpRightSheet = content.Load<Texture2D>(marioSmallJumpRightInfo.ContentPath);
            marioSmallRunLeftSheet = content.Load<Texture2D>(marioSmallRunLeftInfo.ContentPath);
            marioSmallRunRightSheet = content.Load<Texture2D>(marioSmallRunRightInfo.ContentPath);
            marioSmallTurnLeftSheet = content.Load<Texture2D>(marioSmallTurnLeftInfo.ContentPath);
            marioSmallTurnRightSheet = content.Load<Texture2D>(marioSmallTurnRightInfo.ContentPath);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_CrouchLeft()
        {
            return new NonAnimatedSprite(marioBigCrouchLeftSheet, marioBigCrouchLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_CrouchRight()
        {
            return new NonAnimatedSprite(marioBigCrouchRightSheet, marioBigCrouchRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_IdleLeft()
        {
            return new NonAnimatedSprite(marioBigIdleLeftSheet, marioBigIdleLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_IdleRight()
        {
            return new NonAnimatedSprite(marioBigIdleRightSheet, marioBigIdleRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_JumpLeft()
        {
            return new NonAnimatedSprite(marioBigJumpLeftSheet, marioBigJumpLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_JumpRight()
        {
            return new NonAnimatedSprite(marioBigJumpRightSheet, marioBigJumpRightInfo);
        }

        public AnimatedSprite CreateSprite_MarioBig_RunLeft()
        {
            return new AnimatedSprite(marioBigRunLeftSheet, marioBigRunLeftInfo);
        }

        public AnimatedSprite CreateSprite_MarioBig_RunRight()
        {
            return new AnimatedSprite(marioBigRunRightSheet, marioBigRunRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_TurnLeft()
        {
            return new NonAnimatedSprite(marioBigTurnLeftSheet, marioBigTurnLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioBig_TurnRight()
        {
            return new NonAnimatedSprite(marioBigTurnRightSheet, marioBigTurnRightInfo);
        }

        public AnimatedSprite CreateSprite_MarioBig_ShrinkLeft()
        {
            return new AnimatedSprite(marioBigShrinkLeftSheet, marioBigShrinkLeftInfo);
        }

        public AnimatedSprite CreateSprite_MarioBig_ShrinkRight()
        {
            return new AnimatedSprite(marioBigShrinkRightSheet, marioBigShrinkRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_CrouchLeft()
        {
            return new NonAnimatedSprite(marioFireCrouchLeftSheet, marioFireCrouchLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_CrouchRight()
        {
            return new NonAnimatedSprite(marioFireCrouchRightSheet, marioFireCrouchRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_IdleLeft()
        {
            return new NonAnimatedSprite(marioFireIdleLeftSheet, marioFireIdleLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_IdleRight()
        {
            return new NonAnimatedSprite(marioFireIdleRightSheet, marioFireIdleRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_JumpLeft()
        {
            return new NonAnimatedSprite(marioFireJumpLeftSheet, marioFireJumpLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_JumpRight()
        {
            return new NonAnimatedSprite(marioFireJumpRightSheet, marioFireJumpRightInfo);
        }

        public AnimatedSprite CreateSprite_MarioFire_RunLeft()
        {
            return new AnimatedSprite(marioFireRunLeftSheet, marioFireRunLeftInfo);
        }

        public AnimatedSprite CreateSprite_MarioFire_RunRight()
        {
            return new AnimatedSprite(marioFireRunRightSheet, marioFireRunRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_TurnLeft()
        {
            return new NonAnimatedSprite(marioFireTurnLeftSheet, marioFireTurnLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioFire_TurnRight()
        {
            return new NonAnimatedSprite(marioFireTurnRightSheet, marioFireTurnRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_Die()
        {
            return new NonAnimatedSprite(marioSmallDieSheet, marioSmallDieInfo);
        }

        public AnimatedSprite CreateSprite_MarioSmall_GrowLeft()
        {
            return new AnimatedSprite(marioSmallGrowLeftSheet, marioSmallGrowLeftInfo);
        }

        public AnimatedSprite CreateSprite_MarioSmall_GrowRight()
        {
            return new AnimatedSprite(marioSmallGrowRightSheet, marioSmallGrowRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_IdleLeft()
        {
            return new NonAnimatedSprite(marioSmallIdleLeftSheet, marioSmallIdleLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_IdleRight()
        {
            return new NonAnimatedSprite(marioSmallIdleRightSheet, marioSmallIdleRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_JumpLeft()
        {
            return new NonAnimatedSprite(marioSmallJumpLeftSheet, marioSmallJumpLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_JumpRight()
        {
            return new NonAnimatedSprite(marioSmallJumpRightSheet, marioSmallJumpRightInfo);
        }

        public AnimatedSprite CreateSprite_MarioSmall_RunLeft()
        {
            return new AnimatedSprite(marioSmallRunLeftSheet, marioSmallRunLeftInfo);
        }

        public AnimatedSprite CreateSprite_MarioSmall_RunRight()
        {
            return new AnimatedSprite(marioSmallRunRightSheet, marioSmallRunRightInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_TurnLeft()
        {
            return new NonAnimatedSprite(marioSmallTurnLeftSheet, marioSmallTurnLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_MarioSmall_TurnRight()
        {
            return new NonAnimatedSprite(marioSmallTurnRightSheet, marioSmallTurnRightInfo);
        }
    }
}
