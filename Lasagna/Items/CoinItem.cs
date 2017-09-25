namespace Lasagna
{
    public class CoinItem : BaseItem
    {
        public CoinItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Coin();
        }
    }
}
