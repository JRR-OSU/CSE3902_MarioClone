using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasagna
{
    public class MarioCollisionHandler
    {
        private MarioStateMachine state;
        private Mario mario;
        private MarioPhysics marioPhysics;

        /// <summary>
        /// Constants
        /// </summary>
        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int FIVE = 5;
        private const int SIXTEEN = 16;
        private const int TWENTY = 20;
        private const int FORTY = 40;
        private const int ONE_HUNDRED_FIFTY = 150;

        public MarioCollisionHandler(Mario player, MarioStateMachine marioState, MarioPhysics physics)
        {
            state = marioState;
            mario = player;
            marioPhysics = physics;
        }
        public void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            if (player.IsDead || mario.IsDead)
                return;
            switch (side)
            {
                case CollisionSide.Bottom:
                    //((Mario)player).stateMachine.DamageMario();
                    marioPhysics.velocity.Y = ZERO;
                    marioPhysics.velocity.Y += ONE_HUNDRED_FIFTY;
                    state.HandleJump();
                    mario.SetPosition(mario.Bounds.X, (player.Bounds.Y - mario.Bounds.Height));
                    if (this.mario.Tag == 0)
                        Score.marioScore += 1000;
                    else
                        Score.luigiScore += 1000;
                    if (((Mario)player).stateMachine.StarPowered)
                        state.DamageMario();
                    break;
                case CollisionSide.Top:
                    if (state.CurrentMovement.Equals(MarioStateMachine.MarioMovement.Die) || ((Mario)player).stateMachine.CurrentMovement.Equals(MarioStateMachine.MarioMovement.Die))
                        return;
                    if(!((Mario)player).stateMachine.CurrentState.Equals(MarioStateMachine.MarioState.Small) || ((Mario)player).stateMachine.StarPowered)
                        state.DamageMario();
                    mario.SetPosition(mario.Bounds.X, (player.Bounds.Y + player.Bounds.Height));
                    break;
                case CollisionSide.Left:
                    if(((Mario)player).stateMachine.StarPowered)
                        state.DamageMario();
                    mario.SetPosition(player.Bounds.X + player.Bounds.Width, mario.Bounds.Y);
                    break;
                case CollisionSide.Right:
                    if (((Mario)player).stateMachine.StarPowered)
                        state.DamageMario();
                    mario.SetPosition(player.Bounds.X - mario.Bounds.Width, mario.Bounds.Y);

                    break;
            }
        }

        public void OnCollisionResponse(IItem item, CollisionSide side)
        {
            if (item == null && side != CollisionSide.None)
                return;

            if (item is FireFlowerItem)
            {
                state.SetFireState();
                if (this.mario.Tag == 0)
                    Score.marioScore += 1000;
                else
                    Score.luigiScore += 1000;
                SoundEffectFactory.Instance.PlayPowerUp();
            }
            else if (item is GrowMushroomItem)
            {
                state.Grow();
                if (this.mario.Tag == 0)
                    Score.marioScore += 1000;
                else
                    Score.luigiScore += 1000;
                SoundEffectFactory.Instance.PlayPowerUp();
            }
            else if (item is StarItem)
            {
                state.Star();
                if (this.mario.Tag == 0)
                    Score.marioScore += 1000;
                else
                    Score.luigiScore += 1000;
                //bgm.Play_StarMan();
            }
            else if (item is CoinItem)
            {
                Score.AddCoinMario();
                SoundEffectFactory.Instance.PlayCoin();
            }
            else if (item is LifeMushroomItem)
            {
                Score.ExtraLifeMario();
                SoundEffectFactory.Instance.GetOneUp();
            }
        }

        public void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            if (projectile is FireProjectile)
            {
                state.DamageMario();
                return;
            }

        switch (side)
            {
                case CollisionSide.Bottom:
                    if (projectile is KoopaShellProjectile)
                    {
                        marioPhysics.velocity.Y = ZERO;
                        marioPhysics.velocity.Y += ONE_HUNDRED_FIFTY;
                        state.HandleJump();
                    }
                    state.DamageMario();
                    // Jump effect if landing on top of an enemy
                    break;
                case CollisionSide.Top:
                    if(projectile is KoopaShellProjectile)
                    if (((KoopaShellProjectile)projectile).IsShellKicked)
                        mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y + mario.Bounds.Height));
                    state.DamageMario();
                    break;
                case CollisionSide.Left:
                    if(projectile is KoopaShellProjectile)
                    if (((KoopaShellProjectile)projectile).IsShellKicked)
                        mario.SetPosition(((KoopaShellProjectile)projectile).Bounds.X + mario.Bounds.Width, mario.Bounds.Y);
                    state.DamageMario();
                    break;
                case CollisionSide.Right:
                    if(projectile is KoopaShellProjectile)
                    if (((KoopaShellProjectile)projectile).IsShellKicked)
                        mario.SetPosition(((KoopaShellProjectile)projectile).Bounds.X - mario.Bounds.Width, mario.Bounds.Y);
                    state.DamageMario();
                    break;
            }

        }
        public void OnCollisionResponse(ITile tile, CollisionSide side)
        {
            //If the Mario hits the invisible block from the top, left and right sides of the block, do nothing.
            if (tile is InvisibleItemBlockTile && ((InvisibleItemBlockTile)tile).MarioCollidedWithThreeSides() || state.flagpoleSequence)
            {
                return;
            }
            else
            {
                switch (side)
                {
                    case CollisionSide.Bottom:
                        state.SetGroundedState();
                        if (!(tile is FloorBlockTile))
                        {
                            marioPhysics.ignoreGravity = false;
                        }
                        else
                            marioPhysics.ignoreGravity = true;
                        mario.isCollideGround = true;
                        if (tile is FlagPoleTile)
                        {
                            state.flagpoleSequence = true;
                            state.flagpoleColPos.X = ((FlagPoleTile)tile).Bounds.X - TWENTY;
                            state.flagpoleColPos.Y = mario.position.Y;
                        }
                        else if (tile is WarpPipeTile)
                            marioPhysics.disableCrouch = true;
                        else
                            marioPhysics.disableCrouch = false;
                        mario.SetPosition(mario.Bounds.X, ((tile.Bounds.Y - mario.Bounds.Height - TWO)));
                        mario.isFalling = false;
                        break;
                    case CollisionSide.Top:
                        marioPhysics.velocity.Y = ZERO;
                        mario.SetPosition(mario.Bounds.X, (tile.Bounds.Y + tile.Bounds.Height + FIVE));
                        marioPhysics.ignoreGravity = false;
                        if (tile is BreakableBrickTile)
                        {
                            if (((BreakableBrickTile)tile).items.Length > ZERO && ((BreakableBrickTile)tile).items[ZERO] is CoinItem
                                && !((BreakableBrickTile)tile).IsUsed)
                            {
                                Score.AddCoinMario();
                            }
                        }
                        if (tile is QuestionBlockTile)
                        {
                            if (((QuestionBlockTile)tile).items[ZERO] is CoinItem && !((QuestionBlockTile)tile).IsUsed)
                            {
                                Score.AddCoinMario();
                            }
                        }                 
                        break;
                    case CollisionSide.Left:
                        marioPhysics.velocity.X = ZERO;
                        mario.SetPosition((tile.Bounds.X + tile.Bounds.Width), mario.Bounds.Y);
                        break;
                    case CollisionSide.Right:
                        marioPhysics.velocity.X = ZERO;
                        if (tile is FlagPoleTile)
                        {
                            state.flagpoleSequence = true;
                            state.flagpoleColPos.X = mario.position.X;
                            state.flagpoleColPos.Y = mario.position.Y;
                        }
                        else
                            mario.SetPosition((tile.Bounds.X - mario.Bounds.Width), mario.Bounds.Y);
                        break;
                }
            }
        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            if (state.isStar() || (enemy is GoombaEnemy && enemy.Bounds.Height <= SIXTEEN) || (enemy is KoopaEnemy && enemy.Bounds.Height <= FORTY)) // if star or enemy is dead
                return;
            else
            {
                switch (side)
                {
                    case CollisionSide.Bottom:
                        if ((enemy is GoombaEnemy))
                        {
                            if (((GoombaEnemy)enemy).currentHealth == MovingEnemy.EnemyHealth.Flipped)
                                return;
                        }

                        // Jump effect if landing on top of an enemy
                        marioPhysics.velocity.Y = ZERO;
                        marioPhysics.velocity.Y += ONE_HUNDRED_FIFTY;
                        state.HandleJump();
                        Score.marioEnemyKilledCount++;
                        Score.marioEnemyKill();
                        break;
                    case CollisionSide.Top:
                        if ((enemy is GoombaEnemy))
                        {

                            if (((GoombaEnemy)enemy).currentHealth == MovingEnemy.EnemyHealth.Flipped)
                                return;
                        }
                        state.DamageMario();
                        mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y + mario.Bounds.Height));
                        break;
                    case CollisionSide.Left:
                        if ((enemy is GoombaEnemy))
                        {
                            if (((GoombaEnemy)enemy).currentHealth == MovingEnemy.EnemyHealth.Flipped)
                                return;
                        }
                        state.DamageMario();
                        mario.SetPosition(mario.Bounds.X + mario.Bounds.Width / TWO, mario.Bounds.Y);
                        break;
                    case CollisionSide.Right:
                        if ((enemy is GoombaEnemy))
                        {
                            if (((GoombaEnemy)enemy).currentHealth == MovingEnemy.EnemyHealth.Flipped)
                                return;
                        }
                        state.DamageMario();
                        mario.SetPosition(mario.Bounds.X - mario.Bounds.Width / TWO, mario.Bounds.Y);
                        break;
                }
            }

        }
    }
}
