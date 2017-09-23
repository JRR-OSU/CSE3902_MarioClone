namespace Lasagna
{
    public class FireFlowerItem : BaseItem
    {
        public FireFlowerItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            itemSprite = ItemSpriteFactory.Instance.CreateSprite_FireFlower();
        }
    }
}
