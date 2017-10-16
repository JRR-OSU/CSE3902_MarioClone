using System;

namespace Lasagna
{
    public interface IController
    {
        void Update();
        void Quit();
        void Reset();
        void MoveLeft();
        void MoveRight();
        void Jump();
        void Crouch();
        void ShootFire();
    }
}
