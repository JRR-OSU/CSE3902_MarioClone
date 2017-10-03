using Microsoft.Xna.Framework;
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
            CollisionSide side1, side2;

            //Tiles are static, so don't need to check against themselves.
            foreach (ITile tile in tiles)
            {
                foreach (IPlayer player in players)
                {
                    if (CheckCollision(tile.GetProperties(), player.GetRect, out overlap, out side1, out side2))
                    {
                        tile.OnCollisionResponse(player, side1);
                        player.OnCollisionResponse(tile, side2);
                    }
                }
                foreach (IEnemy enemy in enemies)
                {
                    if (CheckCollision(tile.GetProperties(), enemy.GetRectangle, out overlap, out side1, out side2))
                    {
                        tile.OnCollisionResponse(enemy, side1);
                        enemy.OnCollisionResponse(tile, side2);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(tile.GetProperties(), item.GetRectangle, out overlap, out side1, out side2))
                    {
                        tile.OnCollisionResponse(item, side1);
                        item.OnCollisionResponse(tile, side2);
                    }
                }
            }

            //Players vs. Enemies & Items
            foreach (IPlayer player in players)
            {
                foreach (IEnemy enemy in enemies)
                {
                    if (CheckCollision(player.GetRect, enemy.GetRectangle, out overlap, out side1, out side2))
                    {
                        player.OnCollisionResponse(enemy, side1);
                        enemy.OnCollisionResponse(player, side2);
                    }
                }
                foreach (IItem item in items)
                {
                    if (CheckCollision(player.GetRect, item.GetRectangle, out overlap, out side1, out side2))
                    {
                        player.OnCollisionResponse(item, side1);
                        item.OnCollisionResponse(player, side2);
                    }
                }
            }

            //Enemies vs items
            foreach (IEnemy enemy in enemies)
            {
                foreach (IItem item in items)
                {
                    if (CheckCollision(enemy.GetRectangle, item.GetRectangle, out overlap, out side1, out side2))
                    {
                        enemy.OnCollisionResponse(item, side1);
                        item.OnCollisionResponse(enemy, side2);
                    }
                }
            }
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
                side = (overlapRect.Center.Y < sourceRect.Center.Y) ? CollisionSide.Bottom : CollisionSide.Top;

            return side;
        }
    }
}
