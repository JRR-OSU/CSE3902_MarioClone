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

        //By the interfaces, we know IPlayer, IEnemy, ITile, and IItem are all IColliders as well.
        public void Update(List<IPlayer> players, List<IEnemy> enemies, List<ITile> tiles, List<IItem> items)
        {
            //Tiles are static, so don't need to check against themselves.
            foreach (ITile tile in tiles)
            {
                CollisionSide tileSide, otherColliderSide;
                foreach (IPlayer player in players)
                {
                    if (CheckCollision(tile.Properties, player.GetRect, out tileSide, out otherColliderSide))
                    {
                        //Invisible blocks can only be collided if they're hit from the bottom
                        if (tile is InvisibleItemBlockTile && tileSide != CollisionSide.Bottom && !((InvisibleItemBlockTile)tile).IsVisible)
                            continue;

                        player.OnCollisionResponse(tile, otherColliderSide);
                        tile.OnCollisionResponse(player, tileSide);
                    }
                }
                foreach (IEnemy enemy in enemies)
                {
                    if (CheckCollision(tile.Properties, enemy.GetRectangle, out tileSide, out otherColliderSide))
                    {
                        tile.OnCollisionResponse(enemy, tileSide);
                        enemy.OnCollisionResponse(tile, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(tile.Properties, item.GetRectangle, out tileSide, out otherColliderSide))
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
                    if (CheckCollision(player.GetRect, enemy.GetRectangle, out playerSide, out otherColliderSide))
                    {
                        player.OnCollisionResponse(enemy, playerSide);
                        enemy.OnCollisionResponse(player, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(player.GetRect, item.GetRectangle, out playerSide, out otherColliderSide))
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
                    if (CheckCollision(enemy.GetRectangle, item.GetRectangle, out enemySide, out otherColliderSide))
                    {
                        enemy.OnCollisionResponse(item, enemySide);
                        item.OnCollisionResponse(enemy, otherColliderSide);
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
                if (collided == true)
                    continue;

                CollisionSide enemySide;
                if (CheckCollision(enemy.GetRectangle, rect, out enemySide, out sourceSide))
                {
                    enemy.OnCollisionResponse(sourceCollider, enemySide);
                    collided = true;
                }
            }
            foreach (ITile tile in tiles)
            {
                if (collided == true)
                    continue;

                CollisionSide tileSide;
                if (CheckCollision(tile.Properties, rect, out tileSide, out sourceSide))
                {
                    //Invisible blocks can only be collided if they're hit from the bottom
                    if (tile is InvisibleItemBlockTile && tileSide != CollisionSide.Bottom && !((InvisibleItemBlockTile)tile).IsVisible)
                        continue;

                    tile.OnCollisionResponse(sourceCollider, tileSide);
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
