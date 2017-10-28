namespace Lasagna
{
    public class FloorBlockTile : BaseTile
    {
        public FloorBlockTile(int spawnXPos, int spawnYPos)
            : base(spawnXPos, spawnYPos)
        {
            CurrentSprite = TileSpriteFactory.Instance.CreateSprite_Floor();
        }
        public override bool IsChangingState { get; set; }

        //Not needed currently
        public override void ChangeState() {}
    }
}
