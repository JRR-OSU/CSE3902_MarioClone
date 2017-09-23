namespace Lasagna
{
    class GrowMushroomItem : BaseItem
    {
        public GrowMushroomItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            itemSprite = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        }
    }
}
