using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class MouseController : IController
    {
        private int MousePosX;
        private int MousePosY;
        private MouseState State;
        public MouseController()
        {
            State = Mouse.GetState();
            this.MousePosX = State.X;
            this.MousePosY = State.Y;
        }
        public void Update()
        {
            if (this.MousePosX > State.X)
            {
                this.MoveLeft();
                this.MousePosX = State.X;
            }
            else if (this.MousePosX < State.X)
            {
                this.MoveRight();
                this.MousePosX = State.X;
            }
            else if (this.MousePosY > State.Y)
            {
                this.Jump();
                this.MousePosY = State.Y;
            }
            else if (this.MousePosY < State.Y)
            {
                this.Reset();
                this.MousePosY = State.Y;
            }
        }

        public void Quit()
        {
            MarioEvents.Quit(this, EventArgs.Empty);
        }

        public void Reset()
        {
            MarioEvents.Reset(this, EventArgs.Empty);
        }

        public void MoveLeft()
        {
            MarioEvents.MoveLeft(this, EventArgs.Empty);
        }

        public void MoveRight()
        {
            MarioEvents.MoveRight(this, EventArgs.Empty);
        }

        public void Jump()
        {
            MarioEvents.Jump(this, EventArgs.Empty);
        }

        public void Crouch()
        {
            MarioEvents.Crouch(this, EventArgs.Empty);
        }

        public void Fire()
        {
            MarioEvents.Fire(this, EventArgs.Empty);
        }

        public void MarioDamage()
        {
            MarioEvents.MarioDamage(this, EventArgs.Empty);
        }

        public void MarioDie()
        {
            MarioEvents.MarioDie(this, EventArgs.Empty);
        }

        public void GetMushroom()
        {
            MarioEvents.GetMushroom(this, EventArgs.Empty);
        }

        public void GetFireFlower()
        {
            MarioEvents.GetFireFlower(this, EventArgs.Empty);
        }

        public void UseQuestionBlock()
        {
            MarioEvents.UseQuestionBlock(this, EventArgs.Empty);
        }

        public void DestroyBrickBlock()
        {
            MarioEvents.DestroyBrickBlock(this, EventArgs.Empty);
        }

        public void UseHiddenBlock()
        {
            MarioEvents.UseHiddenBlock(this, EventArgs.Empty);
        }
    }
}
