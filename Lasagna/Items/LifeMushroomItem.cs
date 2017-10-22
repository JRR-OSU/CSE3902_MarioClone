namespace Lasagna
{
    public class LifeMushroomItem : BaseItem
    {

        public LifeMushroomItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        }

    }
}
