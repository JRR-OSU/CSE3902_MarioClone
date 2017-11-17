using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Lasagna
{
    public class WarpPipeTile : ITile
    {
        private const int Two = 2;
        private const int One = 1;
        private const int Zero = 0;
        private const int defaultPipeTipHeight = 64;
        private const int defaultPipeBaseHeight = 32;

        private ISprite pipeTipSprite = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
        private ISprite[] pipeBaseSprites;
        //Number of pipe base segments high this pipe is. Zero means just the pipe tip.
        private int height = 0;
        private int posX;
        private int posY;
        private int pipeTipHeight;
        private int pipeBaseHeight;
        private string warpDest;
        private string warpSource;
        private bool crouching;
        private bool jumping;
        private bool movingLeft;
        private bool movingRight;
        private int warpCamXPos;
        private int warpCamYPos;
        //Direction this pipe faces, changes where mario can enter pipe from.
        private Direction pipeDir;
        public bool IsChangingState { get; set; }
        public string WarpSource { get { return warpSource; } }
        public Direction PipeDirection { get { return pipeDir; } }
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

        public WarpPipeTile(int spawnPosX, int spawnPosY, Direction facing, int pipeHeight,
            string pipeWarpSource, string pipeWarpDest, Vector2 warpForcesCamPos)
        {
            posX = spawnPosX;
            posY = spawnPosY;
            pipeDir = facing;
            height = pipeHeight;
            warpSource = pipeWarpSource;
            if (warpSource != pipeWarpDest)
                warpDest = pipeWarpDest;
            warpCamXPos = (int)warpForcesCamPos.X;
            warpCamYPos = (int)warpForcesCamPos.Y;

            //Set pipe base sprites
            pipeBaseSprites = new ISprite[height];
            for (int i = Zero; i < height; i++)
                pipeBaseSprites[i] = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();

            //Store height of our sprites, if null ref default values
            if (pipeTipSprite != null)
                pipeTipHeight = pipeTipSprite.Height;
            else
                pipeTipHeight = defaultPipeTipHeight;
            if (pipeBaseSprites != null && pipeBaseSprites.Length > Zero && pipeBaseSprites[Zero] != null)
                pipeBaseHeight = pipeBaseSprites[Zero].Height;
            else
                pipeBaseHeight = defaultPipeBaseHeight;
            
            MarioEvents.OnP1Crouch += OnMarioCrouch;
            MarioEvents.OnP1MoveLeft += OnMarioMoveLeft;
            MarioEvents.OnP1MoveRight += OnMarioMoveRight;
            MarioEvents.OnP1Jump += OnMarioJump;
        }

        //Empty for now
        public void ChangeState() { }

        private bool FacingHorizontal()
        {
            return pipeDir == Direction.Left || pipeDir == Direction.Right;
        }

        private float PipeRotation()
        {
            double rot = Zero;

            if (pipeDir == Direction.Down)
                rot = Math.PI;
            else if (pipeDir == Direction.Left)
                rot = -Math.PI / Two;
            else if (pipeDir == Direction.Right)
                rot = Math.PI / Two;

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
            int yOffsetMod = Zero,
                xOffsetMod = Zero;
            if (FacingHorizontal())
            {
                int pipeBaseXOffset = (pipeBaseHeight / Two);
                xOffsetMod = (pipeDir == Direction.Left) ? One : -One;
                tempPosX += (((pipeDir == Direction.Left) ? pipeTipHeight : pipeBaseHeight) * xOffsetMod) - pipeBaseXOffset;
                tempPosY += pipeBaseXOffset;
            }
            else
            {
                yOffsetMod = (pipeDir == Direction.Up) ? One : -One;
                tempPosY += ((pipeDir == Direction.Up) ? pipeTipHeight : pipeBaseHeight) * yOffsetMod;
            }

            //Draw each base after. 
            for (int i = Zero; i < pipeBaseSprites.Length; i++)
            {
                if (pipeBaseSprites[i] != null)
                {
                    pipeBaseSprites[i].Update(gameTime, tempPosX, tempPosY);
                    tempPosX += pipeBaseHeight * xOffsetMod;
                    tempPosY += pipeBaseHeight * yOffsetMod;
                }
            }

            //Clear crouching flag
            crouching = false;
            movingLeft = false;
            movingRight = false;
            jumping = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (pipeTipSprite != null)
                pipeTipSprite.Draw(spriteBatch, Color.White, PipeRotation());
            for (int i = Zero; i < pipeBaseSprites.Length; i++)
                if (pipeBaseSprites[i] != null)
                    pipeBaseSprites[i].Draw(spriteBatch, Color.White, PipeRotation());
        }

        private void OnMarioCrouch(object sender, EventArgs e)
        {
            crouching = true;
        }
        private void OnMarioJump(object sender, EventArgs e)
        {
            jumping = true;
        }
        private void OnMarioMoveLeft(object sender, EventArgs e)
        {
            movingLeft = true;
        }
        private void OnMarioMoveRight(object sender, EventArgs e)
        {
            movingRight = true;
        }

        public void OnCollisionResponse(ICollider otherCollider, CollisionSide side)
        {
            //If player is crouching, try and warp to destination.
            if (otherCollider != null && ShouldWarp(side))
                MarioGame.Instance.TryWarp(warpDest, warpCamXPos, warpCamYPos, pipeDir);
        }

        private bool ShouldWarp(CollisionSide side)
        {
            return (crouching && pipeDir == Direction.Up && side == CollisionSide.Top)
                || (jumping && pipeDir == Direction.Down && side == CollisionSide.Bottom)
                || (movingRight && pipeDir == Direction.Left && side == CollisionSide.Left)
                || (movingLeft && pipeDir == Direction.Right && side == CollisionSide.Right);
        }
    }
}
