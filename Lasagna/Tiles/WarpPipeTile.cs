using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class WarpPipeTile : ITile
    {
        private ISprite pipeTipSprite = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
        private ISprite[] pipeBaseSprites;
        //Number of pipe base segments high this pipe is. 0 means just the pipe tip.
        private int height = 0;
        private int posX;
        private int posY;
        private int pipeTipHeight;
        private int pipeBaseHeight;
        //Direction this pipe faces, changes where mario can enter pipe from.
        private Direction pipeDir;
        public bool IsChangingState { get; set; }
        public Rectangle Bounds
        {
            get
            {
                int tempWidth = pipeTipSprite.Width,
                    tempHeight = pipeTipHeight;

                if (FacingHorizontal())
                {
                    tempWidth = pipeTipHeight + pipeBaseHeight * height;
                    tempHeight = pipeTipSprite.Width;
                }
                else
                {
                    tempWidth = pipeTipSprite.Width;
                    tempHeight = pipeTipHeight + pipeBaseHeight * height;
                }

                return new Rectangle(posX, posY, tempWidth, tempHeight);
            }
        }

        public WarpPipeTile(int spawnPosX, int spawnPosY, Direction facing, int pipeHeight)
        {
            posX = spawnPosX;
            posY = spawnPosY;
            pipeDir = facing;
            height = pipeHeight;

            //Set pipe base sprites
            pipeBaseSprites = new ISprite[height];
            for (int i = 0; i < height; i++)
                pipeBaseSprites[i] = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();

            //Store height of our sprites, if null ref default values
            if (pipeTipSprite != null)
                pipeTipHeight = pipeTipSprite.Height;
            else
                pipeTipHeight = 64;
            if (pipeBaseSprites != null && pipeBaseSprites.Length > 0 && pipeBaseSprites[0] != null)
                pipeBaseHeight = pipeBaseSprites[0].Height;
            else
                pipeBaseHeight = 32;
        }

        //Empty for now
        public void ChangeState() { }

        private bool FacingHorizontal()
        {
            return pipeDir == Direction.Left || pipeDir == Direction.Right;
        }

        private float PipeRotation()
        {
            double rot = 0;

            if (pipeDir == Direction.Down)
                rot = Math.PI;
            else if (pipeDir == Direction.Left)
                rot = -Math.PI / 2;
            else if (pipeDir == Direction.Right)
                rot = Math.PI / 2;

            return (float)rot;
        }

        public void Update(GameTime gameTime)
        {
            int tempPosX = posX;
            int tempPosY = posY;

            //Adjust where tip and bases are based on facing direction
            if (pipeDir == Direction.Right)
                tempPosX += pipeBaseHeight * pipeBaseSprites.Length;
            if (pipeDir == Direction.Down)
                tempPosY += pipeBaseHeight * pipeBaseSprites.Length;

            if (pipeTipSprite != null)
                pipeTipSprite.Update(gameTime, tempPosX, tempPosY);

            //Modifier for pipe base spawning for X and Y based off spawn direction.
            int yOffsetMod = 0,
                xOffsetMod = 0;
            if (FacingHorizontal())
            {
                int pipeBaseXOffset = (pipeBaseHeight / 2);
                xOffsetMod = (pipeDir == Direction.Left) ? 1 : -1;
                tempPosX += (((pipeDir == Direction.Left) ? pipeTipHeight : pipeBaseHeight) * xOffsetMod) - pipeBaseXOffset;
                tempPosY += pipeBaseXOffset;
            }
            else
            {
                yOffsetMod = (pipeDir == Direction.Up) ? 1 : -1;
                tempPosY += ((pipeDir == Direction.Up) ? pipeTipHeight : pipeBaseHeight) * yOffsetMod;
            }

            //Draw each base after. 
            for (int i = 0; i < pipeBaseSprites.Length; i++)
            {
                if (pipeBaseSprites[i] != null)
                {
                    pipeBaseSprites[i].Update(gameTime, tempPosX, tempPosY);
                    tempPosX += pipeBaseHeight * xOffsetMod;
                    tempPosY += pipeBaseHeight * yOffsetMod;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (pipeTipSprite != null)
                pipeTipSprite.Draw(spriteBatch, Color.White, PipeRotation());
            for (int i = 0; i < pipeBaseSprites.Length; i++)
                if (pipeBaseSprites[i] != null)
                    pipeBaseSprites[i].Draw(spriteBatch, Color.White, PipeRotation());
        }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            //TODO: Warp mario on down if thisis warp pipe.
        }
    }
}
