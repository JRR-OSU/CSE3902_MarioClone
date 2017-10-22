namespace Lasagna
{
    class GrowMushroomItem : BaseItem
    {
        public GrowMushroomItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_PowerupMushroom();
        }

    }
}
