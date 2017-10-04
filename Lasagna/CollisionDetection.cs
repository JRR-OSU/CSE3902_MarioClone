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
            Rectangle overlap;

            //Tiles are static, so don't need to check against themselves.
            foreach (ITile tile in tiles)
            {
                CollisionSide tileSide, otherColliderSide;
                foreach (IPlayer player in players)
                {
                    if (CheckCollision(tile.Properties, player.GetRect, out overlap, out tileSide, out otherColliderSide))
                    {
                        //Invisible blocks can only be collided if they're hit from the bottom
                        if (tile is InvisibleItemBlockTile && tileSide != CollisionSide.Bottom)
                            continue;

                        tile.OnCollisionResponse(player, tileSide);
                        player.OnCollisionResponse(tile, otherColliderSide);
                    }
                }
                foreach (IEnemy enemy in enemies)
                {
                    if (CheckCollision(tile.Properties, enemy.GetRectangle, out overlap, out tileSide, out otherColliderSide))
                    {
                        tile.OnCollisionResponse(enemy, tileSide);
                        enemy.OnCollisionResponse(tile, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(tile.Properties, item.GetRectangle, out overlap, out tileSide, out otherColliderSide))
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
                    if (CheckCollision(player.GetRect, enemy.GetRectangle, out overlap, out playerSide, out otherColliderSide))
                    {
                        player.OnCollisionResponse(enemy, playerSide);
                        enemy.OnCollisionResponse(player, otherColliderSide);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(player.GetRect, item.GetRectangle, out overlap, out playerSide, out otherColliderSide))
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
                    if (CheckCollision(enemy.GetRectangle, item.GetRectangle, out overlap, out enemySide, out otherColliderSide))
                    {
                        enemy.OnCollisionResponse(item, enemySide);
                        item.OnCollisionResponse(enemy, otherColliderSide);
                    }
                }
            }
        }

        public bool CheckRectForCollisions(Rectangle rect, out CollisionSide side)
        {
            bool collided = false;
            side = CollisionSide.None;

            return collided;
        }

        private bool CheckCollision(Rectangle r1, Rectangle r2, out Rectangle overlapRect, out CollisionSide r1CollisionSide, out CollisionSide r2CollisionSide)
        {
            overlapRect = Rectangle.Intersect(r1, r2);

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
