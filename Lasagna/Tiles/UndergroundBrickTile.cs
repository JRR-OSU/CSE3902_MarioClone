namespace Lasagna
{
    public class UndergroundBrickTile: BaseTile
    {
        public UndergroundBrickTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = TileSpriteFactory.Instance.CreateSprite_UndergroundBrick();
        }
        public override bool IsChangingState { get; set; }

        //Not needed currently
        public override void ChangeState() {}
    }
}
