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

        /// <summary>
        /// Constants
        /// </summary>
        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int FIVE = 5;
        private const int ONE_HUNDRED_FIFTY = 150;

        public MarioCollisionHandler(Mario player, MarioStateMachine marioState)
        {
            state = marioState;
            mario = player;
        }
        public void OnCollisionResponse(IPlayer player, CollisionSide side)
        {
            if (player != null && side != CollisionSide.None)
                state.Reset();
        }

        public void OnCollisionResponse(IItem item, CollisionSide side)
        {
            if (item == null && side != CollisionSide.None)
                return;

            if (item is FireFlowerItem)
            {
                state.SetFireState();
                Score.AddItemScore();
                SoundEffectFactory.Instance.PlayPowerUp();
            }
            else if (item is GrowMushroomItem)
            {
                state.Grow();
                Score.AddItemScore();
                SoundEffectFactory.Instance.PlayPowerUp();
            }
            else if (item is StarItem)
            {
                state.Star();
                Score.AddItemScore();
                //bgm.Play_StarMan();
            }
            else if (item is CoinItem)
            {
                Score.AddCoinMario();
                SoundEffectFactory.Instance.PlayCoin();
            }
            else if(item is LifeMushroomItem)
            {
                Score.ExtraLifeMario();
                SoundEffectFactory.Instance.GetOneUp();
            }
        }

        public void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            if (!(projectile is KoopaShellProjectile))
                return;

            switch (side)
            {
                case CollisionSide.Bottom:
                    mario.velocity.Y = ZERO;
                    mario.velocity.Y += ONE_HUNDRED_FIFTY;
                    state.HandleJump();
                    // Jump effect if landing on top of an enemy
                    break;
                case CollisionSide.Top:
                    if(((KoopaShellProjectile)projectile).IsShellKicked)
                        state.DamageMario();
                    mario.SetPosition(mario.Bounds.X, (mario.Bounds.Y + mario.Bounds.Height));
                    break;
                case CollisionSide.Left:
                    if (((KoopaShellProjectile)projectile).IsShellKicked)
                        state.DamageMario();
                    mario.SetPosition(((KoopaShellProjectile)projectile).Bounds.X + mario.Bounds.Width, mario.Bounds.Y);
                    break;
                case CollisionSide.Right:
                    if (((KoopaShellProjectile)projectile).IsShellKicked)
                        state.DamageMario();
                    mario.SetPosition(((KoopaShellProjectile)projectile).Bounds.X - mario.Bounds.Width, mario.Bounds.Y);
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
                            mario.ignoreGravity = false;
                        }
                        else
                            mario.ignoreGravity = true;
                        mario.isCollideGround = true;
                        if (tile is FlagPoleTile)
                        {
                            Console.WriteLine(mario.position);
                            state.flagpoleSequence = true;
                            state.flagpoleColPos.X = mario.position.X;
                            state.flagpoleColPos.Y = mario.position.Y;
                        }
                        else if (tile is WarpPipeTile)
                            mario.disableCrouch = true;
                        else
                            mario.disableCrouch = false;
                        
                        mario.CalcMaxHeight(tile.Bounds.Y, tile.Bounds.Height);
                        mario.SetPosition(mario.Bounds.X, ((tile.Bounds.Y - mario.Bounds.Height-TWO)));

                        mario.isFalling = false;
                        break;
                    case CollisionSide.Top:
                        mario.velocity.Y = ZERO;
                        mario.SetPosition(mario.Bounds.X, (tile.Bounds.Y + tile.Bounds.Height + FIVE));
                        mario.ignoreGravity = false;
                        if (tile is BreakableBrickTile)
                        {
                            if (((BreakableBrickTile)tile).items.Length > 0 && ((BreakableBrickTile)tile).items[0] is CoinItem
                                && !((BreakableBrickTile)tile).IsUsed)
                            {
                                Score.AddCoinMario();
                            }
                        }
                        if (tile is QuestionBlockTile)
                        {
                               if (((QuestionBlockTile)tile).items[0] is CoinItem && !((QuestionBlockTile)tile).IsUsed)
                               {
                                   Score.AddCoinMario();
                               }
                        }
                            
                        break;
                    case CollisionSide.Left:
                        mario.velocity.X = ZERO;
                        mario.SetPosition((tile.Bounds.X + tile.Bounds.Width), mario.Bounds.Y);
                      
                        break;
                    case CollisionSide.Right:
                        mario.velocity.X = ZERO;

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
            if (state.isStar() || (enemy is GoombaEnemy && enemy.Bounds.Height <= 16) || (enemy is KoopaEnemy && enemy.Bounds.Height <= 40))// if star or enemy is dead
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
                        mario.velocity.Y = ZERO;
                        mario.velocity.Y += ONE_HUNDRED_FIFTY;
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
