namespace Lasagna
{
    public class CoinItem : BaseItem
    {
        public CoinItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            itemSprite = ItemSpriteFactory.Instance.CreateSprite_Coin();
        }
    }
}
