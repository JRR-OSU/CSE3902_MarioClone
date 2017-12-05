namespace Lasagna
{
    public class FireFlowerItem : BaseItem
    {
        public FireFlowerItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_FireFlower();
        }

        public override void Move()
        {
            return;
        }
    }
}
