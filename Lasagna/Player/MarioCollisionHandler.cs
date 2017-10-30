﻿using Microsoft.Xna.Framework;
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
            }
            else if (item is GrowMushroomItem)
            {
                state.Grow();
            }
            else if (item is StarItem)
            {
                state.Star();
            }

        }

        public void OnCollisionResponse(IProjectile projectile, CollisionSide side)
        {
            if (!(projectile is KoopaShellProjectile))
                return;

            switch (side)
            {
                case CollisionSide.Bottom:
                    mario.velocity.Y = 0;
                    mario.velocity.Y += 150;
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
            if (tile is InvisibleItemBlockTile && ((InvisibleItemBlockTile)tile).MarioCollidedWithThreeSides())
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

                        mario.CalcMaxHeight(tile.Bounds.Y, tile.Bounds.Height);
                        mario.SetPosition(mario.Bounds.X, ((tile.Bounds.Y - mario.Bounds.Height-2)));

                        mario.isFalling = false;
                        break;
                    case CollisionSide.Top:
                        mario.velocity.Y = 0;
                        mario.SetPosition(mario.Bounds.X, (tile.Bounds.Y + tile.Bounds.Height + 5));
                        mario.ignoreGravity = false;
                        break;
                    case CollisionSide.Left:
                        mario.velocity.X = 0;
                        mario.SetPosition((tile.Bounds.X + tile.Bounds.Width), mario.Bounds.Y);
                      
                        break;
                    case CollisionSide.Right:
                       mario.velocity.X = 0;
                        mario.SetPosition((tile.Bounds.X - mario.Bounds.Width), mario.Bounds.Y);

                        break;
                }
            }

        }

        public void OnCollisionResponse(IEnemy enemy, CollisionSide side)
        {
            if (state.isStar() || (enemy is GoombaEnemy && enemy.Bounds.Height <= 16) || (enemy is KoopaEnemy && enemy.Bounds.Height <= 40))// if star or enemy is dead
            {
                //switch (side)
                //{
                //    case CollisionSide.Bottom:
                //        mario.SetPosition(mario.Bounds.X, (enemy.Bounds.Y - mario.Bounds.Height));
                //        state.isCollideFloor = true;
                //        state.EndJump();
                //        break;
                //    case CollisionSide.Top:
                //        mario.SetPosition(mario.Bounds.X, (enemy.Bounds.Y + enemy.Bounds.Height));
                //        break;
                //    case CollisionSide.Left:
                //        mario.SetPosition((enemy.Bounds.X + enemy.Bounds.Width) + 3, mario.Bounds.Y);
                //        break;
                //    case CollisionSide.Right:
                //        mario.SetPosition((enemy.Bounds.X - enemy.Bounds.Width) - 3, mario.Bounds.Y);
                //        break;
                //}
            }

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
                        mario.velocity.Y = 0;
                        mario.velocity.Y += 150;
                        state.HandleJump();
                        // Jump effect if landing on top of an enemy
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
                        mario.SetPosition(mario.Bounds.X + mario.Bounds.Width / 2, mario.Bounds.Y);
                        break;
                    case CollisionSide.Right:
                        if ((enemy is GoombaEnemy))
                        {
                            if (((GoombaEnemy)enemy).currentHealth == MovingEnemy.EnemyHealth.Flipped)
                                return;
                        }
                        state.DamageMario();
                        mario.SetPosition(mario.Bounds.X - mario.Bounds.Width / 2, mario.Bounds.Y);
                        break;
                }
            }

        }
    }
}
