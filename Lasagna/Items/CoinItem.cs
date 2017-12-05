using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class CoinItem : BaseItem
    {
        private const int ZERO = 0;
        private int coinAnimateTime = ZERO;
        public CoinItem(int spawnPosX, int spawnPosY)
            : base(spawnPosX, spawnPosY)
        {
            ItemSprite = ItemSpriteFactory.Instance.CreateSprite_Coin();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentState.Equals(ItemState.CoinAnimaiotn))
            {
                HandleCoinAnimation();
                Fall(gameTime);
            }
            if (this.isInBlock)
            {
                this.isInvisible = false;
                StartCoinAnimation();
                this.isInBlock = false;
            }
        }

        public override void Reset(object sender, EventArgs e)
        {
            base.Reset(sender, e);
            coinAnimateTime = ZERO;
        }

        public override void Spawn()
        {
            this.isInBlock = true;
        }

        public void StartCoinAnimation()
        {
            currentState = ItemState.CoinAnimaiotn;
        }

        private void HandleCoinAnimation()
        {
            position.Y -= increasingYDifference;
            velocity += increasingVelocity;
            coinAnimateTime++;
            if (coinAnimateTime >= coinAnimateedTimeMax)
            {
                ItemSprite = null;
            }
        }
    }
}
