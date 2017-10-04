using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class CollisionDetection
    {
        private static CollisionDetection instance;

        public static CollisionDetection Instance
        {
            get
            {
                if (instance == null)
                    instance = new CollisionDetection();

                return instance;
            }
        }

        public void Update(List<IPlayer> players, List<IEnemy> enemies, List<ITile> tiles, List<IItem> items)
        {
            //Tiles are static, so don't need to check against themselves.
            foreach (ITile tile in tiles)
            {
                //If this is not ICollider, skip loop
                if (!(tile is ICollider))
                    continue;

                CollisionSide tileSide, otherColliderSide;
                foreach (IPlayer player in players)
                {
                    //If this is not ICollider, skip loop
                    if (!(player is ICollider))
                        continue;

                    if (CheckCollision(tile.Properties, player.GetRect, out tileSide, out otherColliderSide))
                    {
                        //Invisible blocks can only be collided if they're hit from the bottom
                        if (tile is InvisibleItemBlockTile && tileSide != CollisionSide.Bottom)
                            continue;

                        tile.OnCollisionResponse((ICollider)player, tileSide);
                        player.OnCollisionResponse((ICollider)tile, otherColliderSide);
                    }
                }
                foreach (IEnemy enemy in enemies)
                {
                    //If this is not ICollider, skip loop
                    if (!(enemy is ICollider))
                        continue;

                    if (CheckCollision(tile.Properties, enemy.GetRectangle, out tileSide, out otherColliderSide))
                    {
                        tile.OnCollisionResponse((ICollider)enemy, tileSide);
                        enemy.OnCollisionResponse((ICollider)tile, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    //If this is not ICollider, skip loop
                    if (!(item is ICollider))
                        continue;

                    if (CheckCollision(tile.Properties, item.GetRectangle, out tileSide, out otherColliderSide))
                    {
                        tile.OnCollisionResponse((ICollider)item, tileSide);
                        item.OnCollisionResponse((ICollider)tile, otherColliderSide);
                    }
                }
            }

            //Players vs. Enemies & Items
            foreach (IPlayer player in players)
            {
                //If this is not ICollider, skip loop
                if (!(player is ICollider))
                    continue;

                CollisionSide playerSide, otherColliderSide;
                foreach (IEnemy enemy in enemies)
                {
                    //If this is not ICollider, skip loop
                    if (!(enemy is ICollider))
                        continue;

                    if (CheckCollision(player.GetRect, enemy.GetRectangle, out playerSide, out otherColliderSide))
                    {
                        player.OnCollisionResponse((ICollider)enemy, playerSide);
                        enemy.OnCollisionResponse((ICollider)player, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    //If this is not ICollider, skip loop
                    if (!(item is ICollider))
                        continue;

                    if (CheckCollision(player.GetRect, item.GetRectangle, out playerSide, out otherColliderSide))
                    {
                        player.OnCollisionResponse((ICollider)item, playerSide);
                        item.OnCollisionResponse((ICollider)player, otherColliderSide);
                    }
                }
            }

            //Enemies vs items
            foreach (IEnemy enemy in enemies)
            {
                //If this is not ICollider, skip loop
                if (!(enemy is ICollider))
                    continue;

                CollisionSide enemySide, otherColliderSide;
                foreach (IItem item in items)
                {
                    //If this is not ICollider, skip loop
                    if (!(item is ICollider))
                        continue;

                    if (CheckCollision(enemy.GetRectangle, item.GetRectangle, out enemySide, out otherColliderSide))
                    {
                        enemy.OnCollisionResponse((ICollider)item, enemySide);
                        item.OnCollisionResponse((ICollider)enemy, otherColliderSide);
                    }
                }
            }
        }

        public bool CheckRectForCollisions(ICollider sourceCollider, Rectangle rect, List<IEnemy> enemies, List<ITile> tiles, out CollisionSide sourceSide)
        {
            bool collided = false;
            sourceSide = CollisionSide.None;
            Rectangle overlap;

            foreach (IEnemy enemy in enemies)
            {
                if (collided == true | !(enemy is ICollider))
                    continue;

                CollisionSide enemySide;
                if (CheckCollision(enemy.GetRectangle, rect, out enemySide, out sourceSide))
                {
                    enemy.OnCollisionResponse((ICollider)sourceCollider, enemySide);
                    collided = true;
                }
            }
            foreach (ITile tile in tiles)
            {
                if (collided == true || !(tile is ICollider))
                    continue;

                CollisionSide tileSide;
                if (CheckCollision(tile.Properties, rect, out tileSide, out sourceSide))
                {
                    //Invisible blocks can only be collided if they're hit from the bottom
                    if (tile is InvisibleItemBlockTile && tileSide != CollisionSide.Bottom)
                        continue;

                    tile.OnCollisionResponse((ICollider)sourceCollider, tileSide);
                    collided = true;
                }
            }

            return collided;
        }

        private bool CheckCollision(Rectangle r1, Rectangle r2, out CollisionSide r1CollisionSide, out CollisionSide r2CollisionSide)
        {
            Rectangle overlapRect = Rectangle.Intersect(r1, r2);

            r1CollisionSide = GetSideOfCollision(r1, overlapRect);
            r2CollisionSide = GetSideOfCollision(r2, overlapRect);

            return !overlapRect.IsEmpty;
        }

        private CollisionSide GetSideOfCollision(Rectangle sourceRect, Rectangle overlapRect)
        {
            CollisionSide side;

            if (overlapRect.IsEmpty || sourceRect.IsEmpty || !sourceRect.Contains(overlapRect))
                side = CollisionSide.None;
            //Overlap rect is taller than wide, collision must be on the left or right side.
            else if (overlapRect.Height > overlapRect.Width)
                side = (overlapRect.Center.X < sourceRect.Center.X) ? CollisionSide.Left : CollisionSide.Right;
            //Else overlap rect is wider than tall, collision must be on the top or bottom side
            else
                side = (overlapRect.Center.Y < sourceRect.Center.Y) ? CollisionSide.Top : CollisionSide.Bottom;

            return side;
        }
    }
}
