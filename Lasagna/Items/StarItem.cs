using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class StarItem : BaseItem
    {
        
        protected const float starItemBounceTime = 0.2f;
        protected float movingUpTimeLeft;
        private ItemState currentState = ItemState.Idle;
        private ISprite starItemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        protected const int verticalMoveSpeed = 200;
        public StarItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            currentState = ItemState.Idle;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentState == ItemState.Bounce)
            {
                if (movingUpTimeLeft > 0)
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
