using System;

namespace Lasagna
{
    public delegate void InputEventHandler(object sender, EventArgs e);
    public delegate void MarioEventHandler();

    //Class which contains all events for the project in one easy to locate place.
    public static class MarioEvents
    {
        public static event InputEventHandler OnQuit;
        public static event InputEventHandler OnReset;
        public static event InputEventHandler OnMoveLeft;
        public static event InputEventHandler OnMoveRight;
        public static event InputEventHandler OnJump;
        public static event InputEventHandler OnCrouch;
        public static event InputEventHandler OnFire;

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

        public static void Fire(object sender, EventArgs e)
        {
            if (OnFire != null)
                OnFire(sender, e);
        }
    }
}
