namespace Lasagna
{
    public class StarItem : BaseItem
    {
        public StarItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            itemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        }
    }
}
