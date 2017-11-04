namespace Lasagna
{
    public class UndergroundFloorBlockTile : BaseTile
    {
        public UndergroundFloorBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = TileSpriteFactory.Instance.CreateSprite_UndergroundFloor();
        }
        public override bool IsChangingState { get; set; }

        //Not needed currently
        public override void ChangeState() {}
    }
}
