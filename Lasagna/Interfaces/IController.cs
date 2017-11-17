using System;

namespace Lasagna
{
    public interface IController
    {
        void Update();
        void Quit();
        void Reset();
        void Pause();
        void P1_MoveLeft();
        void P1_MoveRight();
        void P1_Jump();
        void P1_Crouch();
        void P1_ShootFire();
        void P2_MoveLeft();
        void P2_MoveRight();
        void P2_Jump();
        void P2_Crouch();
        void P2_ShootFire();
    }
}
