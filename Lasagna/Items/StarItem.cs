namespace Lasagna
{
    public class StarItem : BaseItem
    {
        public StarItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        }
        public override void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            //TO DO: Star will bounce when it hits the tile
        }
    }
}
