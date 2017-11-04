using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class TileSpriteFactory
    {
        private Texture2D breakableBrickSheet;
        private readonly SpriteSheetInfo breakableBrickInfo = new SpriteSheetInfo("BlockSprites/Brick_Default", 32, 32);
        private Texture2D floorSheet;
        private readonly SpriteSheetInfo floorInfo = new SpriteSheetInfo("BlockSprites/FloorBlock", 32, 32);
        private Texture2D itemBlockUsedSheet;
        private readonly SpriteSheetInfo itemBlockUsedInfo = new SpriteSheetInfo("BlockSprites/ItemBlock_Used", 32, 32);
        private Texture2D pipeBaseSheet;
        private readonly SpriteSheetInfo pipeBaseInfo = new SpriteSheetInfo("BlockSprites/Pipe_Base", 64, 32);
        private Texture2D pipeTipSheet;
        private readonly SpriteSheetInfo pipeTipInfo = new SpriteSheetInfo("BlockSprites/Pipe_Tip", 64, 64);
        private Texture2D questionBlockSheet;
        private readonly SpriteSheetInfo questionBlockInfo = new SpriteSheetInfo("BlockSprites/QuestionBlock_Default", 32, 32, 3, 1, 8);
        private Texture2D unbreakableBlockSheet;
        private readonly SpriteSheetInfo unbreakableBlockInfo = new SpriteSheetInfo("BlockSprites/UnbreakableBlock", 32, 32);
        private Texture2D flagSheet;
        private readonly SpriteSheetInfo flagInfo = new SpriteSheetInfo("MiscSprites/Flag", 32, 32);
        private Texture2D flagPoleSheet;
        private readonly SpriteSheetInfo flagPoleInfo = new SpriteSheetInfo("MiscSprites/FlagPole", 16, 304);
        private Texture2D undergroundBrickSheet;
        private readonly SpriteSheetInfo undergroundBrickInfo = new SpriteSheetInfo("BlockSprites/UndergroundBrick", 32, 32);
        private Texture2D undergroundFloorSheet;
        private readonly SpriteSheetInfo undergroundFloorInfo = new SpriteSheetInfo("BlockSprites/UndergroundFloor", 32, 32);
        private Texture2D brickPieceLeftSheet;
        private readonly SpriteSheetInfo brickPieceLeftInfo = new SpriteSheetInfo("BlockSprites/BrickPieceLeft", 16, 16);
        private Texture2D brickPieceRightSheet;
        private readonly SpriteSheetInfo brickPieceRightInfo = new SpriteSheetInfo("BlockSprites/BrickPieceRight", 16, 16);
   
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
            breakableBrickSheet = content.Load<Texture2D>(breakableBrickInfo.ContentPath);
            floorSheet = content.Load<Texture2D>(floorInfo.ContentPath);
            itemBlockUsedSheet = content.Load<Texture2D>(itemBlockUsedInfo.ContentPath);
            pipeBaseSheet = content.Load<Texture2D>(pipeBaseInfo.ContentPath);
            pipeTipSheet = content.Load<Texture2D>(pipeTipInfo.ContentPath);
            questionBlockSheet = content.Load<Texture2D>(questionBlockInfo.ContentPath);
            unbreakableBlockSheet = content.Load<Texture2D>(unbreakableBlockInfo.ContentPath);
            flagSheet = content.Load<Texture2D>(flagInfo.ContentPath);
            flagPoleSheet = content.Load<Texture2D>(flagPoleInfo.ContentPath);
            brickPieceLeftSheet = content.Load<Texture2D>(brickPieceLeftInfo.ContentPath);
            brickPieceRightSheet = content.Load<Texture2D>(brickPieceRightInfo.ContentPath);
            undergroundBrickSheet = content.Load<Texture2D>(undergroundBrickInfo.ContentPath);
            undergroundFloorSheet = content.Load<Texture2D>(undergroundFloorInfo.ContentPath);
        }

        public NonAnimatedSprite CreateSprite_BreakableBrick()
        {
            return new NonAnimatedSprite(breakableBrickSheet, breakableBrickInfo);
        }
        public NonAnimatedSprite CreateSprite_BrickPieceLeft()
        {
            return new NonAnimatedSprite(brickPieceLeftSheet, brickPieceLeftInfo);
        }
        public NonAnimatedSprite CreateSprite_BrickPieceRight()
        {
            return new NonAnimatedSprite(brickPieceRightSheet, brickPieceRightInfo);
        }
        public NonAnimatedSprite CreateSprite_Floor()
        {
            return new NonAnimatedSprite(floorSheet, floorInfo);
        }

        public NonAnimatedSprite CreateSprite_ItemBlockUsed()
        {
            return new NonAnimatedSprite(itemBlockUsedSheet, itemBlockUsedInfo);
        }

        public NonAnimatedSprite CreateSprite_Pipe_Base()
        {
            return new NonAnimatedSprite(pipeBaseSheet, pipeBaseInfo);
        }

        public NonAnimatedSprite CreateSprite_Pipe_Tip()
        {
            return new NonAnimatedSprite(pipeTipSheet, pipeTipInfo);
        }

        public AnimatedSprite CreateSprite_QuestionBlock()
        {
            return new AnimatedSprite(questionBlockSheet, questionBlockInfo);
        }

        public NonAnimatedSprite CreateSprite_UnbreakableBlock()
        {
            return new NonAnimatedSprite(unbreakableBlockSheet, unbreakableBlockInfo);
        }

        public NonAnimatedSprite CreateSprite_Flag()
        {
            return new NonAnimatedSprite(flagSheet, flagInfo);
        }

        public NonAnimatedSprite CreateSprite_FlagPole()
        {
            return new NonAnimatedSprite(flagPoleSheet, flagPoleInfo);
        }

        public NonAnimatedSprite CreateSprite_UndergroundBrick()
        {
            return new NonAnimatedSprite(undergroundBrickSheet, undergroundBrickInfo);
        }

        public NonAnimatedSprite CreateSprite_UndergroundFloor()
        {
            return new NonAnimatedSprite(undergroundFloorSheet, undergroundFloorInfo);
        }
    }
}
