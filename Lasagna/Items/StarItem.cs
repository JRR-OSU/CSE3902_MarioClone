using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Lasagna
{
    public class StarItem : BaseItem
    {
        private const int ZERO = 0;
        private const float ZEROF = 0f;
        private ISprite starItemSprite = ItemSpriteFactory.Instance.CreateSprite_Star();
        private float verticalMoveSpeed = ZEROF;
        private const float standardVerticalMoveSpeed = -4.4f;
        private const float acceleration = 0.15f;
        private bool hittedGround = false;
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
                verticalMoveSpeed += acceleration;
                PosY += verticalMoveSpeed;
            }
        }
        public override void Reset(object sender, EventArgs e)
        {
            base.Reset(sender, e);
            verticalMoveSpeed = ZEROF;
            hittedGround = false;
        }

        protected override void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            if (isInBlock || currentState == ItemState.Idle)
                return;

            if (currentState == ItemState.Bounce)
            {
                if (tile is FloorBlockTile || tile is UndergroundFloorBlockTile)
                {
                    hittedGround = true;
                }
                if (side == CollisionSide.Left)
                {
                    movingLeft = false;
                }
                else if (side == CollisionSide.Right)
                {
                    movingLeft = true;
                }
                else
                {
                    verticalMoveSpeed = -verticalMoveSpeed;
                    if (verticalMoveSpeed < standardVerticalMoveSpeed || (side == CollisionSide.Bottom && hittedGround
                        ))
                    {
                        verticalMoveSpeed = standardVerticalMoveSpeed;
                    }
                }
            }
            CorrectPosition(side, tile);
        }
    }
}
