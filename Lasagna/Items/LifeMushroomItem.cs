namespace Lasagna
{
    public class LifeMushroomItem : BaseItem
    {

        public LifeMushroomItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            itemSprite = ItemSpriteFactory.Instance.CreateSprite_PowerupMushroom();
        }
    }
}
