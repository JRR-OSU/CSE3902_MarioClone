using System;

namespace Lasagna
{
    public delegate void MarioEventHandler();
    public delegate void LevelSelectHandler(object sender, EventArgs e, uint levelNumber);

    //Class which contains all events for the project in one easy to locate place.
    public static class MarioEvents
    {
        public static event EventHandler<EventArgs> OnQuit;
        public static event EventHandler<EventArgs> OnReset;
        public static event EventHandler<EventArgs> OnPause;
        public static event LevelSelectHandler OnSelectLevel;
        public static event EventHandler<EventArgs> OnP1MoveLeft;
        public static event EventHandler<EventArgs> OnP1MoveRight;
        public static event EventHandler<EventArgs> OnP1Jump;
        public static event EventHandler<EventArgs> OnP1Crouch;
        public static event EventHandler<EventArgs> OnP1ShootFire;
        public static event EventHandler<EventArgs> OnP2MoveLeft;
        public static event EventHandler<EventArgs> OnP2MoveRight;
        public static event EventHandler<EventArgs> OnP2Jump;
        public static event EventHandler<EventArgs> OnP2Crouch;
        public static event EventHandler<EventArgs> OnP2ShootFire;

        public static void Quit(object sender, EventArgs e)
        {
            if (OnQuit != null)
                OnQuit(sender, e);
        }

        public static void Reset(object sender, EventArgs e)
        {
            if (OnReset != null)
                OnReset(sender, e);
        }

        public static void Pause(object sender, EventArgs e)
        {
            if (OnPause != null)
                OnPause(sender, e);
        }

        public static void SelectLevel(object sender, EventArgs e, uint levelNumber)
        {
            if (OnSelectLevel != null)
                OnSelectLevel(sender, e, levelNumber);
        }

        public static void P1_MoveLeft(object sender, EventArgs e)
        {
            if (OnP1MoveLeft != null)
                OnP1MoveLeft(sender, e);
        }

        public static void P1_MoveRight(object sender, EventArgs e)
        {
            if (OnP1MoveRight != null)
                OnP1MoveRight(sender, e);
        }

        public static void P1_Jump(object sender, EventArgs e)
        {
            if (OnP1Jump != null)
                OnP1Jump(sender, e);
        }

        public static void P1_Crouch(object sender, EventArgs e)
        {
            if (OnP1Crouch != null)
                OnP1Crouch(sender, e);
        }

        
        public static void P1_ShootFire(object sender, EventArgs e)
        {
            if (OnP1ShootFire != null)
                OnP1ShootFire(sender, e);
        }

        public static void P2_MoveLeft(object sender, EventArgs e)
        {
            if (OnP2MoveLeft != null)
                OnP2MoveLeft(sender, e);
        }

        public static void P2_MoveRight(object sender, EventArgs e)
        {
            if (OnP2MoveRight != null)
                OnP2MoveRight(sender, e);
        }

        public static void P2_Jump(object sender, EventArgs e)
        {
            if (OnP2Jump != null)
                OnP2Jump(sender, e);
        }

        public static void P2_Crouch(object sender, EventArgs e)
        {
            if (OnP2Crouch != null)
                OnP2Crouch(sender, e);
        }

        public static void P2_ShootFire(object sender, EventArgs e)
        {
            if (OnP2ShootFire != null)
                OnP2ShootFire(sender, e);
        }
    }
}
