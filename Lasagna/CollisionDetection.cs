using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lasagna
{
    public static class CollisionDetection
    {
        //By the interfaces, we know IPlayer, IEnemy, ITile, and IItem are all IColliders as well.
        public static void Update(ReadOnlyCollection<IPlayer> players, ReadOnlyCollection<IEnemy> enemies, ReadOnlyCollection<ITile> tiles, ReadOnlyCollection<IItem> items)
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
        }

        public static bool CheckRectForCollisions(ICollider sourceCollider, Rectangle rect, ReadOnlyCollection<IEnemy> enemies, ReadOnlyCollection<ITile> tiles)
        {
            bool collided = CheckAllCollisions<IEnemy>(sourceCollider, rect, enemies);
            collided = CheckAllCollisions<ITile>(sourceCollider, rect, tiles) || collided;

            return collided;
        }

        private static bool CheckAllCollisions<T>(ICollider sourceCollider, Rectangle sourceRect, ReadOnlyCollection<T> targetColliders)
        {
            if (!typeof(ICollider).IsAssignableFrom(typeof(T)) || sourceCollider == null
                || sourceRect == Rectangle.Empty || targetColliders == null)
                return false;

            bool collided = false;
            CollisionSide sourceSide, targetSide;

            foreach (ICollider col in targetColliders)
            {
                if (CheckCollision(sourceRect, col.Bounds, out sourceSide, out targetSide))
                {
                    /*if (!CheckInvalidInvisibleCollision(sourceCollider, col, sourceSide, targetSide))
                        continue;
                    */

                    sourceCollider.OnCollisionResponse(col, sourceSide);
                    col.OnCollisionResponse(sourceCollider, targetSide);
                    collided = true;
                }
            }

            return collided;
        }

        private static bool CheckCollision(Rectangle r1, Rectangle r2, out CollisionSide r1CollisionSide, out CollisionSide r2CollisionSide)
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

        private static CollisionSide GetSideOfCollision(Rectangle sourceRect, Rectangle overlapRect)
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


        //Checks if either part of a collision is an invisible item block, and if so returns if the collision is valid.
        //Invisible blocks can only be collided if they're hit from the bottom
        /*private static bool CheckInvalidInvisibleCollision(ICollider sourceCollider, ICollider targetCollider, CollisionSide sourceSide, CollisionSide targetSide)
        {
            if (sourceCollider is InvisibleItemBlockTile && (!sourceSide.Equals(CollisionSide.Bottom) || !targetSide.Equals(CollisionSide.Top)) && !((InvisibleItemBlockTile)sourceCollider).IsVisible)
                return false;
            else if (targetCollider is InvisibleItemBlockTile && (!targetSide.Equals(CollisionSide.Bottom) || !sourceSide.Equals(CollisionSide.Top)) && !((InvisibleItemBlockTile)targetCollider).IsVisible)
                return false;
            else
                return true;
        }
        */
    }
}
