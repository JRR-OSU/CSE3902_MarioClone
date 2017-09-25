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

        public static event InputEventHandler OnMarioDamage;
        public static event InputEventHandler OnMarioDie;
        public static event InputEventHandler OnGetMushroom;
        public static event InputEventHandler OnGetFireFlower;
        public static event InputEventHandler OnUseQuestionBlock;
        public static event InputEventHandler OnDestroyBrickBlock;
        public static event InputEventHandler OnUseHiddenBlock;

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

        public static void MarioDamage(object sender, EventArgs e)
        {
            if (OnMarioDamage != null)
                OnMarioDamage(sender, e);
        }

        public static void MarioDie(object sender, EventArgs e)
        {
            if (OnMarioDie != null)
                OnMarioDie(sender, e);
        }

        public static void GetMushroom(object sender, EventArgs e)
        {
            if (OnGetMushroom != null)
                OnGetMushroom(sender, e);
        }

        public static void GetFireFlower(object sender, EventArgs e)
        {
            if (OnGetFireFlower != null)
                OnGetFireFlower(sender, e);
        }

        public static void UseQuestionBlock(object sender, EventArgs e)
        {
            if (OnUseQuestionBlock != null)
                OnUseQuestionBlock(sender, e);
        }

        public static void DestroyBrickBlock(object sender, EventArgs e)
        {
            if (OnDestroyBrickBlock != null)
                OnDestroyBrickBlock(sender, e);
        }

        public static void UseHiddenBlock(object sender, EventArgs e)
        {
            if (OnUseHiddenBlock != null)
                OnUseHiddenBlock(sender, e);
        }
    }
}
