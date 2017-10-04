namespace Lasagna
{
    public class LifeMushroomItem : BaseItem
    {

        public LifeMushroomItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        }

        protected override void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            //TO DO: Mushroom will bounce when it hits the tile
        }
    }
}
