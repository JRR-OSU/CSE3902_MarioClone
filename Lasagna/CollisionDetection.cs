using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        //By the interfaces, we know IPlayer, IEnemy, ITile, and IItem are all IColliders as well.
        public void Update(ReadOnlyCollection<IPlayer> players, ReadOnlyCollection<IEnemy> enemies, ReadOnlyCollection<ITile> tiles, ReadOnlyCollection<IItem> items)
        {
            //Players vs. Tiles, Enemies, Items.
            foreach (IPlayer player in players)
            {
                CheckAllCollisions<ITile>(player, player.Bounds, tiles);
                CheckAllCollisions<IEnemy>(player, player.Bounds, enemies);
                CheckAllCollisions<IItem>(player, player.Bounds, items);
            }

            //Tiles are static, so don't need to check against themselves.
            //Tiles vs. Enemies, Items.
            foreach (ITile tile in tiles)
            {
                CheckAllCollisions<IEnemy>(tile, tile.Bounds, enemies);
                CheckAllCollisions<IItem>(tile, tile.Bounds, items);
            }

            //Enemies vs items
            foreach (IEnemy enemy in enemies)
                CheckAllCollisions<IItem>(enemy, enemy.Bounds, items);

            //Tiles are static, so don't need to check against themselves.
            /*foreach (ITile tile in tiles)
            {
                CollisionSide tileSide, otherColliderSide;
                foreach (IPlayer player in players)
                {
                    if (CheckCollision(tile.Bounds, player.Bounds, out tileSide, out otherColliderSide))
                    {
                        //Invisible blocks can only be collided if they're hit from the bottom
                        if (tile is InvisibleItemBlockTile && !tileSide.Equals(CollisionSide.Bottom) && !otherColliderSide.Equals(CollisionSide.Top) && !((InvisibleItemBlockTile)tile).IsVisible)
                            continue;

                        player.OnCollisionResponse(tile, otherColliderSide);
                        tile.OnCollisionResponse(player, tileSide);
                    }
                }
                foreach (IEnemy enemy in enemies)
                {
                    if (CheckCollision(tile.Bounds, enemy.Bounds, out tileSide, out otherColliderSide))
                    {
                        tile.OnCollisionResponse(enemy, tileSide);
                        enemy.OnCollisionResponse(tile, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(tile.Bounds, item.Bounds, out tileSide, out otherColliderSide))
                    {
                        tile.OnCollisionResponse(item, tileSide);
                        item.OnCollisionResponse(tile, otherColliderSide);
                    }
                }
            }

            //Players vs. Enemies & Items
            foreach (IPlayer player in players)
            {
                CollisionSide playerSide, otherColliderSide;
                foreach (IEnemy enemy in enemies)
                {
                    if (CheckCollision(player.Bounds, enemy.Bounds, out playerSide, out otherColliderSide))
                    {
                        player.OnCollisionResponse(enemy, playerSide);
                        enemy.OnCollisionResponse(player, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(player.Bounds, item.Bounds, out playerSide, out otherColliderSide))
                    {
                        player.OnCollisionResponse(item, playerSide);
                        item.OnCollisionResponse(player, otherColliderSide);
                    }
                }
            }

            //Enemies vs items
            foreach (IEnemy enemy in enemies)
            {
                CollisionSide enemySide, otherColliderSide;
                foreach (IItem item in items)
                {
                    if (CheckCollision(enemy.Bounds, item.Bounds, out enemySide, out otherColliderSide))
                    {
                        enemy.OnCollisionResponse(item, enemySide);
                        item.OnCollisionResponse(enemy, otherColliderSide);
                    }
                }
            }*/
        }

        private void CheckAllCollisions<T>(ICollider sourceCollider, Rectangle sourceRect, ReadOnlyCollection<T> targetColliders)
        {
            if (!typeof(ICollider).IsAssignableFrom(typeof(T)) || sourceCollider == null
                || sourceRect == Rectangle.Empty || targetColliders == null)
                return;

            CollisionSide sourceSide, targetSide;
            
            foreach (ICollider col in targetColliders)
            {
                if (CheckCollision(sourceRect, col.Bounds, out sourceSide, out targetSide))
                {
                    if (!CheckInvalidInvisibleCollision(sourceCollider, col, sourceSide, targetSide))
                        continue;

                    sourceCollider.OnCollisionResponse(col, sourceSide);
                    col.OnCollisionResponse(sourceCollider, targetSide);
                }
            }
        }

        //Checks if either part of a collision is an invisible item block, and if so returns if the collision is valid.
        //Invisible blocks can only be collided if they're hit from the bottom
        private bool CheckInvalidInvisibleCollision(ICollider sourceCollider, ICollider targetCollider, CollisionSide sourceSide, CollisionSide targetSide)
        {
            if (sourceCollider is InvisibleItemBlockTile && (!sourceSide.Equals(CollisionSide.Bottom) || !targetSide.Equals(CollisionSide.Top)) && !((InvisibleItemBlockTile)sourceCollider).IsVisible)
                return false;
            else if (targetCollider is InvisibleItemBlockTile && (!targetSide.Equals(CollisionSide.Bottom) || !sourceSide.Equals(CollisionSide.Top)) && !((InvisibleItemBlockTile)targetCollider).IsVisible)
                return false;
            else
                return true;
        }

        public bool CheckRectForCollisions(ICollider sourceCollider, Rectangle rect, List<IEnemy> enemies, List<ITile> tiles, out CollisionSide sourceSide)
        {
            bool collided = false;
            sourceSide = CollisionSide.None;

            foreach (IEnemy enemy in enemies)
            {
                if (collided == true)
                    continue;

                CollisionSide enemySide;
                if (CheckCollision(enemy.Bounds, rect, out enemySide, out sourceSide))
                {
                    sourceCollider.OnCollisionResponse(enemy, sourceSide);
                    enemy.OnCollisionResponse(sourceCollider, enemySide);
                    collided = true;
                }
            }
            foreach (ITile tile in tiles)
            {
                if (collided == true)
                    continue;

                CollisionSide tileSide;
                if (CheckCollision(tile.Bounds, rect, out tileSide, out sourceSide))
                {
                    //Invisible blocks can only be collided if they're hit from the bottom
                    if (tile is InvisibleItemBlockTile && !tileSide.Equals(CollisionSide.Bottom) && !sourceSide.Equals(CollisionSide.Top) && !((InvisibleItemBlockTile)tile).IsVisible)
                        continue;

                    sourceCollider.OnCollisionResponse(tile, sourceSide);
                    tile.OnCollisionResponse(sourceCollider, tileSide);
                    collided = true;
                }
            }

            return collided;
        }

        private bool CheckCollision(Rectangle r1, Rectangle r2, out CollisionSide r1CollisionSide, out CollisionSide r2CollisionSide)
        {
            r1CollisionSide = CollisionSide.None;
            r2CollisionSide = CollisionSide.None;

            if (r1.IsEmpty || r2.IsEmpty)
                return false;

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
