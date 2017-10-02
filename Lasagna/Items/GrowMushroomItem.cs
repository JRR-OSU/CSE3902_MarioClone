﻿namespace Lasagna
{
    class GrowMushroomItem : BaseItem
    {
        public GrowMushroomItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_1UpMushroom();
        }
        public override void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            //TO DO: Mushroom will bounce when it hits the tile
        }
    }
}
