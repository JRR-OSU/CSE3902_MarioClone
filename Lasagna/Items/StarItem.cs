namespace Lasagna
{
    public class StarItem : BaseItem
    {
        public StarItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        }
    }
}
