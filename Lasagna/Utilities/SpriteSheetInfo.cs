namespace Lasagna
{
    public struct SpriteSheetInfo
    {
        private const int Zero = 0;

        public SpriteSheetInfo(string spriteContentPath, int spriteXSize, int spriteYSize, int animationColumns = Zero, int animationRows = Zero, int sheetAnimationFPS = Zero)
        {
            contentPath = spriteContentPath;
            xSize = spriteXSize;
            ySize = spriteYSize;
            columns = animationColumns;
            rows = animationRows;
            animationFPS = sheetAnimationFPS;
        }

        private int columns;
        private int rows;
        private int xSize;
        private int ySize;
        private int animationFPS;
        private string contentPath;

        public string ContentPath { get { return contentPath; } }
        public int Columns { get { return columns; } }
        public int Rows { get { return rows; } }
        public int XSize { get { return xSize; } }
        public int YSize { get { return ySize; } }
        public int AnimationFPS { get { return animationFPS; } }
    }
}
