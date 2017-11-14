using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Lasagna
{
    public class StarItem : BaseItem
    {
        private const int ZERO = 0;
        protected const float starItemBounceTime = 0.8f;
        protected float movingUpTimeLeft;
        private ISprite starItemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        protected const int verticalMoveSpeed = 200;
        public StarItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (base.currentState == ItemState.Bounce)
            {
                if (movingUpTimeLeft > ZERO)
                {
                    PosY -= (float)(gameTime.ElapsedGameTime.TotalSeconds * verticalMoveSpeed);
                    movingUpTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                    PosY += (float)(gameTime.ElapsedGameTime.TotalSeconds * verticalMoveSpeed);
            }
        }

       protected override void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            if (isInBlock || currentState == ItemState.Idle)
                return;

            if (currentState == ItemState.Bounce)
            {
                if (side == CollisionSide.Left)
                    movingLeft = false;
                else if (side == CollisionSide.Right)
                    movingLeft = true;
            }

            if (currentState.Equals(ItemState.Bounce) && side.Equals(CollisionSide.Bottom))
            {
                movingUpTimeLeft = starItemBounceTime;
            }
            CorrectPosition(side, tile);
        }
    }
}
