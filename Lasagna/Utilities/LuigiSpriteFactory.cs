using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class LuigiSpriteFactory
    {
        private Texture2D luigiBigCrouchLeftSheet;
        private readonly SpriteSheetInfo luigiBigCrouchLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_CrouchLeft", 32, 44);
        private Texture2D luigiBigCrouchRightSheet;
        private readonly SpriteSheetInfo luigiBigCrouchRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_CrouchRight", 32, 44);
        private Texture2D luigiBigIdleLeftSheet;
        private readonly SpriteSheetInfo luigiBigIdleLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_IdleLeft", 32, 64);
        private Texture2D luigiBigIdleRightSheet;
        private readonly SpriteSheetInfo luigiBigIdleRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_IdleRight", 32, 64);
        private Texture2D luigiBigJumpLeftSheet;
        private readonly SpriteSheetInfo luigiBigJumpLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_JumpLeft", 32, 64);
        private Texture2D luigiBigJumpRightSheet;
        private readonly SpriteSheetInfo luigiBigJumpRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_JumpRight", 32, 64);
        private Texture2D luigiBigRunLeftSheet;
        private readonly SpriteSheetInfo luigiBigRunLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_RunLeft", 32, 64, 3, 1, 24);
        private Texture2D luigiBigRunRightSheet;
        private readonly SpriteSheetInfo luigiBigRunRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_RunRight", 32, 64, 3, 1, 24);
        private Texture2D luigiBigTurnLeftSheet;
        private readonly SpriteSheetInfo luigiBigTurnLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_TurnLeft", 32, 64);
        private Texture2D luigiBigTurnRightSheet;
        private readonly SpriteSheetInfo luigiBigTurnRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_TurnRight", 32, 64);
        private Texture2D luigiBigShrinkLeftSheet;
        private readonly SpriteSheetInfo luigiBigShrinkLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_ShrinkLeft", 32, 64, 10, 1, 10);
        private Texture2D luigiBigShrinkRightSheet;
        private readonly SpriteSheetInfo luigiBigShrinkRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_ShrinkRight", 32, 64, 10, 1, 10);
        private Texture2D luigiBigFlagpoleSheet;
        private readonly SpriteSheetInfo luigiBigFlagpoleInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Big_Flagpole", 32, 64);
        private Texture2D luigiFireCrouchLeftSheet;
        private readonly SpriteSheetInfo luigiFireCrouchLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_CrouchLeft", 32, 44);
        private Texture2D luigiFireCrouchRightSheet;
        private readonly SpriteSheetInfo luigiFireCrouchRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_CrouchRight", 32, 44);
        private Texture2D luigiFireIdleLeftSheet;
        private readonly SpriteSheetInfo luigiFireIdleLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_IdleLeft", 32, 64);
        private Texture2D luigiFireIdleRightSheet;
        private readonly SpriteSheetInfo luigiFireIdleRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_IdleRight", 32, 64);
        private Texture2D luigiFireJumpLeftSheet;
        private readonly SpriteSheetInfo luigiFireJumpLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_JumpLeft", 32, 64);
        private Texture2D luigiFireJumpRightSheet;
        private readonly SpriteSheetInfo luigiFireJumpRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_JumpRight", 32, 64);
        private Texture2D luigiFireRunLeftSheet;
        private readonly SpriteSheetInfo luigiFireRunLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_RunLeft", 32, 64, 3, 1, 24);
        private Texture2D luigiFireRunRightSheet;
        private readonly SpriteSheetInfo luigiFireRunRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_RunRight", 32, 64, 3, 1, 24);
        private Texture2D luigiFireTurnLeftSheet;
        private readonly SpriteSheetInfo luigiFireTurnLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_TurnLeft", 32, 64);
        private Texture2D luigiFireTurnRightSheet;
        private readonly SpriteSheetInfo luigiFireTurnRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_TurnRight", 32, 64);
        private Texture2D luigiFireFlagpoleSheet;
        private readonly SpriteSheetInfo luigiFireFlagpoleInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Fire_Flagpole", 32, 64);
        private Texture2D luigiSmallDieSheet;
        private readonly SpriteSheetInfo luigiSmallDieInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_Die", 28, 28);
        private Texture2D luigiSmallGrowLeftSheet;
        private readonly SpriteSheetInfo luigiSmallGrowLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_GrowLeft", 32, 64, 10, 1, 10);
        private Texture2D luigiSmallGrowRightSheet;
        private readonly SpriteSheetInfo luigiSmallGrowRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_GrowRight", 32, 64, 10, 1, 10);
        private Texture2D luigiSmallIdleLeftSheet;
        private readonly SpriteSheetInfo luigiSmallIdleLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_IdleLeft", 24, 32);
        private Texture2D luigiSmallIdleRightSheet;
        private readonly SpriteSheetInfo luigiSmallIdleRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_IdleRight", 24, 32);
        private Texture2D luigiSmallJumpLeftSheet;
        private readonly SpriteSheetInfo luigiSmallJumpLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_JumpLeft", 24, 32);
        private Texture2D luigiSmallJumpRightSheet;
        private readonly SpriteSheetInfo luigiSmallJumpRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_JumpRight", 24, 32);
        private Texture2D luigiSmallRunLeftSheet;
        private readonly SpriteSheetInfo luigiSmallRunLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_RunLeft", 30, 32, 3, 1, 24);
        private Texture2D luigiSmallRunRightSheet;
        private readonly SpriteSheetInfo luigiSmallRunRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_RunRight", 30, 32, 3, 1, 24);
        private Texture2D luigiSmallTurnLeftSheet;
        private readonly SpriteSheetInfo luigiSmallTurnLeftInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_TurnLeft", 24, 32);
        private Texture2D luigiSmallTurnRightSheet;
        private readonly SpriteSheetInfo luigiSmallTurnRightInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_TurnRight", 24, 32);
        private Texture2D luigiSmallFlagpoleSheet;
        private readonly SpriteSheetInfo luigiSmallFlagpoleInfo = new SpriteSheetInfo("LuigiSprites/Luigi_Small_Flagpole", 24, 32);

        private static LuigiSpriteFactory instance;

        public static LuigiSpriteFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new LuigiSpriteFactory();

                return instance;
            }
        }

        public void LoadAllContent(ContentManager content)
        {
            luigiBigCrouchLeftSheet = content.Load<Texture2D>(luigiBigCrouchLeftInfo.ContentPath);
            luigiBigCrouchRightSheet = content.Load<Texture2D>(luigiBigCrouchRightInfo.ContentPath);
            luigiBigIdleLeftSheet = content.Load<Texture2D>(luigiBigIdleLeftInfo.ContentPath);
            luigiBigIdleRightSheet = content.Load<Texture2D>(luigiBigIdleRightInfo.ContentPath);
            luigiBigJumpLeftSheet = content.Load<Texture2D>(luigiBigJumpLeftInfo.ContentPath);
            luigiBigJumpRightSheet = content.Load<Texture2D>(luigiBigJumpRightInfo.ContentPath);
            luigiBigRunLeftSheet = content.Load<Texture2D>(luigiBigRunLeftInfo.ContentPath);
            luigiBigRunRightSheet = content.Load<Texture2D>(luigiBigRunRightInfo.ContentPath);
            luigiBigTurnLeftSheet = content.Load<Texture2D>(luigiBigTurnLeftInfo.ContentPath);
            luigiBigTurnRightSheet = content.Load<Texture2D>(luigiBigTurnRightInfo.ContentPath);
            luigiBigShrinkLeftSheet = content.Load<Texture2D>(luigiBigShrinkLeftInfo.ContentPath);
            luigiBigShrinkRightSheet = content.Load<Texture2D>(luigiBigShrinkRightInfo.ContentPath);
            luigiBigFlagpoleSheet = content.Load<Texture2D>(luigiBigFlagpoleInfo.ContentPath);
            luigiFireCrouchLeftSheet = content.Load<Texture2D>(luigiFireCrouchLeftInfo.ContentPath);
            luigiFireCrouchRightSheet = content.Load<Texture2D>(luigiFireCrouchRightInfo.ContentPath);
            luigiFireIdleLeftSheet = content.Load<Texture2D>(luigiFireIdleLeftInfo.ContentPath);
            luigiFireIdleRightSheet = content.Load<Texture2D>(luigiFireIdleRightInfo.ContentPath);
            luigiFireJumpLeftSheet = content.Load<Texture2D>(luigiFireJumpLeftInfo.ContentPath);
            luigiFireJumpRightSheet = content.Load<Texture2D>(luigiFireJumpRightInfo.ContentPath);
            luigiFireRunLeftSheet = content.Load<Texture2D>(luigiFireRunLeftInfo.ContentPath);
            luigiFireRunRightSheet = content.Load<Texture2D>(luigiFireRunRightInfo.ContentPath);
            luigiFireTurnLeftSheet = content.Load<Texture2D>(luigiFireTurnLeftInfo.ContentPath);
            luigiFireTurnRightSheet = content.Load<Texture2D>(luigiFireTurnRightInfo.ContentPath);
            luigiFireFlagpoleSheet = content.Load<Texture2D>(luigiFireFlagpoleInfo.ContentPath);
            luigiSmallDieSheet = content.Load<Texture2D>(luigiSmallDieInfo.ContentPath);
            luigiSmallGrowLeftSheet = content.Load<Texture2D>(luigiSmallGrowLeftInfo.ContentPath);
            luigiSmallGrowRightSheet = content.Load<Texture2D>(luigiSmallGrowRightInfo.ContentPath);
            luigiSmallIdleLeftSheet = content.Load<Texture2D>(luigiSmallIdleLeftInfo.ContentPath);
            luigiSmallIdleRightSheet = content.Load<Texture2D>(luigiSmallIdleRightInfo.ContentPath);
            luigiSmallJumpLeftSheet = content.Load<Texture2D>(luigiSmallJumpLeftInfo.ContentPath);
            luigiSmallJumpRightSheet = content.Load<Texture2D>(luigiSmallJumpRightInfo.ContentPath);
            luigiSmallRunLeftSheet = content.Load<Texture2D>(luigiSmallRunLeftInfo.ContentPath);
            luigiSmallRunRightSheet = content.Load<Texture2D>(luigiSmallRunRightInfo.ContentPath);
            luigiSmallTurnLeftSheet = content.Load<Texture2D>(luigiSmallTurnLeftInfo.ContentPath);
            luigiSmallTurnRightSheet = content.Load<Texture2D>(luigiSmallTurnRightInfo.ContentPath);
            luigiSmallFlagpoleSheet = content.Load<Texture2D>(luigiSmallFlagpoleInfo.ContentPath);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_CrouchLeft()
        {
            return new NonAnimatedSprite(luigiBigCrouchLeftSheet, luigiBigCrouchLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_CrouchRight()
        {
            return new NonAnimatedSprite(luigiBigCrouchRightSheet, luigiBigCrouchRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_IdleLeft()
        {
            return new NonAnimatedSprite(luigiBigIdleLeftSheet, luigiBigIdleLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_IdleRight()
        {
            return new NonAnimatedSprite(luigiBigIdleRightSheet, luigiBigIdleRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_JumpLeft()
        {
            return new NonAnimatedSprite(luigiBigJumpLeftSheet, luigiBigJumpLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_JumpRight()
        {
            return new NonAnimatedSprite(luigiBigJumpRightSheet, luigiBigJumpRightInfo);
        }

        public AnimatedSprite CreateSprite_LuigiBig_RunLeft()
        {
            return new AnimatedSprite(luigiBigRunLeftSheet, luigiBigRunLeftInfo);
        }

        public AnimatedSprite CreateSprite_LuigiBig_RunRight()
        {
            return new AnimatedSprite(luigiBigRunRightSheet, luigiBigRunRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_TurnLeft()
        {
            return new NonAnimatedSprite(luigiBigTurnLeftSheet, luigiBigTurnLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiBig_TurnRight()
        {
            return new NonAnimatedSprite(luigiBigTurnRightSheet, luigiBigTurnRightInfo);
        }

        public AnimatedSprite CreateSprite_LuigiBig_ShrinkLeft()
        {
            return new AnimatedSprite(luigiBigShrinkLeftSheet, luigiBigShrinkLeftInfo);
        }

        public AnimatedSprite CreateSprite_LuigiBig_ShrinkRight()
        {
            return new AnimatedSprite(luigiBigShrinkRightSheet, luigiBigShrinkRightInfo);
        }
        public NonAnimatedSprite CreateSprite_LuigiBig_Flagpole()
        {
            return new NonAnimatedSprite(luigiBigFlagpoleSheet, luigiBigFlagpoleInfo);
        }
        public NonAnimatedSprite CreateSprite_LuigiFire_CrouchLeft()
        {
            return new NonAnimatedSprite(luigiFireCrouchLeftSheet, luigiFireCrouchLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_CrouchRight()
        {
            return new NonAnimatedSprite(luigiFireCrouchRightSheet, luigiFireCrouchRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_IdleLeft()
        {
            return new NonAnimatedSprite(luigiFireIdleLeftSheet, luigiFireIdleLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_IdleRight()
        {
            return new NonAnimatedSprite(luigiFireIdleRightSheet, luigiFireIdleRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_JumpLeft()
        {
            return new NonAnimatedSprite(luigiFireJumpLeftSheet, luigiFireJumpLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_JumpRight()
        {
            return new NonAnimatedSprite(luigiFireJumpRightSheet, luigiFireJumpRightInfo);
        }

        public AnimatedSprite CreateSprite_LuigiFire_RunLeft()
        {
            return new AnimatedSprite(luigiFireRunLeftSheet, luigiFireRunLeftInfo);
        }

        public AnimatedSprite CreateSprite_LuigiFire_RunRight()
        {
            return new AnimatedSprite(luigiFireRunRightSheet, luigiFireRunRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_TurnLeft()
        {
            return new NonAnimatedSprite(luigiFireTurnLeftSheet, luigiFireTurnLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiFire_TurnRight()
        {
            return new NonAnimatedSprite(luigiFireTurnRightSheet, luigiFireTurnRightInfo);
        }
        public NonAnimatedSprite CreateSprite_LuigiFire_Flagpole()
        {
            return new NonAnimatedSprite(luigiFireFlagpoleSheet, luigiFireFlagpoleInfo);
        }
        public NonAnimatedSprite CreateSprite_LuigiSmall_Die()
        {
            return new NonAnimatedSprite(luigiSmallDieSheet, luigiSmallDieInfo);
        }

        public AnimatedSprite CreateSprite_LuigiSmall_GrowLeft()
        {
            return new AnimatedSprite(luigiSmallGrowLeftSheet, luigiSmallGrowLeftInfo);
        }

        public AnimatedSprite CreateSprite_LuigiSmall_GrowRight()
        {
            return new AnimatedSprite(luigiSmallGrowRightSheet, luigiSmallGrowRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiSmall_IdleLeft()
        {
            return new NonAnimatedSprite(luigiSmallIdleLeftSheet, luigiSmallIdleLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiSmall_IdleRight()
        {
            return new NonAnimatedSprite(luigiSmallIdleRightSheet, luigiSmallIdleRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiSmall_JumpLeft()
        {
            return new NonAnimatedSprite(luigiSmallJumpLeftSheet, luigiSmallJumpLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiSmall_JumpRight()
        {
            return new NonAnimatedSprite(luigiSmallJumpRightSheet, luigiSmallJumpRightInfo);
        }

        public AnimatedSprite CreateSprite_LuigiSmall_RunLeft()
        {
            return new AnimatedSprite(luigiSmallRunLeftSheet, luigiSmallRunLeftInfo);
        }

        public AnimatedSprite CreateSprite_LuigiSmall_RunRight()
        {
            return new AnimatedSprite(luigiSmallRunRightSheet, luigiSmallRunRightInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiSmall_TurnLeft()
        {
            return new NonAnimatedSprite(luigiSmallTurnLeftSheet, luigiSmallTurnLeftInfo);
        }

        public NonAnimatedSprite CreateSprite_LuigiSmall_TurnRight()
        {
            return new NonAnimatedSprite(luigiSmallTurnRightSheet, luigiSmallTurnRightInfo);
        }
        public NonAnimatedSprite CreateSprite_LuigiSmall_Flagpole()
        {
            return new NonAnimatedSprite(luigiSmallFlagpoleSheet, luigiSmallFlagpoleInfo);
        }

    }
}
