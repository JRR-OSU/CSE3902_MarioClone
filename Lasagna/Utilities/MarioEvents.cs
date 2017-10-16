using System;

namespace Lasagna
{
    public delegate void MarioEventHandler();

    //Class which contains all events for the project in one easy to locate place.
    public static class MarioEvents
    {
        public static event EventHandler<EventArgs> OnQuit;
        public static event EventHandler<EventArgs> OnReset;
        public static event EventHandler<EventArgs> OnMoveLeft;
        public static event EventHandler<EventArgs> OnMoveRight;
        public static event EventHandler<EventArgs> OnJump;
        public static event EventHandler<EventArgs> OnCrouch;
        public static event EventHandler<EventArgs> OnShootFire;
        public static event EventHandler<EventArgs> OnToggleMouseController;

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

        public static void MoveLeft(object sender, EventArgs e)
        {
            if (OnMoveLeft != null)
                OnMoveLeft(sender, e);
        }

        public static void MoveRight(object sender, EventArgs e)
        {
            if (OnMoveRight != null)
                OnMoveRight(sender, e);
        }

        public static void Jump(object sender, EventArgs e)
        {
            if (OnJump != null)
                OnJump(sender, e);
        }

        public static void Crouch(object sender, EventArgs e)
        {
            if (OnCrouch != null)
                OnCrouch(sender, e);
        }

        
        public static void ShootFire(object sender, EventArgs e)
        {
            if (OnShootFire != null)
                OnShootFire(sender, e);
        }

        public static void ToggleMouseController(object sender, EventArgs e)
        {
            if (OnToggleMouseController != null)
                OnToggleMouseController(sender, e);
        }
    }
}
