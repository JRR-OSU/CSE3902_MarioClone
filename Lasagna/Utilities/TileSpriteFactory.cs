using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class TileSpriteFactory
    {
        private Texture2D breakableBrickSheet;
        private Texture2D floorSheet;
        private Texture2D itemBlockUsedSheet;
        private Texture2D pipeBaseSheet;
        private Texture2D pipeTipSheet;
        private Texture2D questionBlockSheet;
        private Texture2D unbreakableBlockSheet;
        private Texture2D flagSheet;
        private Texture2D flagPoleSheet;
        private Texture2D brickPieceLeftSheet;
        private Texture2D brickPieceRightSheet;

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
            breakableBrickSheet = content.Load<Texture2D>("BlockSprites/Brick_Default");
            floorSheet = content.Load<Texture2D>("BlockSprites/FloorBlock");
            itemBlockUsedSheet = content.Load<Texture2D>("BlockSprites/ItemBlock_Used");
            pipeBaseSheet = content.Load<Texture2D>("BlockSprites/Pipe_Base");
            pipeTipSheet = content.Load<Texture2D>("BlockSprites/Pipe_Tip");
            questionBlockSheet = content.Load<Texture2D>("BlockSprites/QuestionBlock_Default");
            unbreakableBlockSheet = content.Load<Texture2D>("BlockSprites/UnbreakableBlock");
            flagSheet = content.Load<Texture2D>("MiscSprites/Flag");
            flagPoleSheet = content.Load<Texture2D>("MiscSprites/FlagPole");
            brickPieceLeftSheet = content.Load<Texture2D>("BlockSprites/BrickPieceLeft");
            brickPieceRightSheet = content.Load<Texture2D>("BlockSprites/BrickPieceRight");
        }

        public NonAnimatedSprite CreateSprite_BreakableBrick()
        {
            return new NonAnimatedSprite(breakableBrickSheet, 32, 32);
        }
        public NonAnimatedSprite CreateSprite_BrickPieceLeft()
        {
            return new NonAnimatedSprite(brickPieceLeftSheet, 16, 16);
        }
        public NonAnimatedSprite CreateSprite_BrickPieceRight()
        {
            return new NonAnimatedSprite(brickPieceRightSheet, 16, 16);
        }
        public NonAnimatedSprite CreateSprite_Floor()
        {
            return new NonAnimatedSprite(floorSheet, 32, 32);
        }

        public NonAnimatedSprite CreateSprite_ItemBlockUsed()
        {
            return new NonAnimatedSprite(itemBlockUsedSheet, 32, 32);
        }

        public NonAnimatedSprite CreateSprite_Pipe_Base()
        {
            return new NonAnimatedSprite(pipeBaseSheet, 64, 32);
        }

        public NonAnimatedSprite CreateSprite_Pipe_Tip()
        {
            return new NonAnimatedSprite(pipeTipSheet, 64, 64);
        }

        public AnimatedSprite CreateSprite_QuestionBlock()
        {
            return new AnimatedSprite(questionBlockSheet, 3, 1, 32, 32, 8);
        }

        public NonAnimatedSprite CreateSprite_UnbreakableBlock()
        {
            return new NonAnimatedSprite(unbreakableBlockSheet, 32, 32);
        }

        public NonAnimatedSprite CreateSprite_Flag()
        {
            return new NonAnimatedSprite(flagSheet, 32, 32);
        }

        public NonAnimatedSprite CreateSprite_FlagPole()
        {
            return new NonAnimatedSprite(flagPoleSheet, 16, 304);
        }
    }
}
