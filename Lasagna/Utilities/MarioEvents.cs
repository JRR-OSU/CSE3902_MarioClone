namespace Lasagna
{
    public delegate void InputEventHandler();

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

        public static void Quit()
        {
            if (OnQuit != null)
                OnQuit();
        }

        public static void Reset()
        {
            if (OnReset != null)
                OnReset();
        }

        public static void MoveLeft()
        {
            if (OnMoveLeft != null)
                OnMoveLeft();
        }

        public static void MoveRight()
        {
            if (OnMoveRight != null)
                OnMoveRight();
        }

        public static void Jump()
        {
            if (OnJump != null)
                OnJump();
        }

        public static void Crouch()
        {
            if (OnCrouch != null)
                OnCrouch();
        }

        public static void Fire()
        {
            if (OnFire != null)
                OnFire();
        }

        public static void MarioDamage()
        {
            if (OnMarioDamage != null)
                OnMarioDamage();
        }

        public static void MarioDie()
        {
            if (OnMarioDie != null)
                OnMarioDie();
        }

        public static void GetMushroom()
        {
            if (OnGetMushroom != null)
                OnGetMushroom();
        }

        public static void GetFireFlower()
        {
            if (OnGetFireFlower != null)
                OnGetFireFlower();
        }

        public static void UseQuestionBlock()
        {
            if (OnUseQuestionBlock != null)
                OnUseQuestionBlock();
        }

        public static void DestroyBrickBlock()
        {
            if (OnDestroyBrickBlock != null)
                OnDestroyBrickBlock();
        }

        public static void UseHiddenBlock()
        {
            if (OnUseHiddenBlock != null)
                OnUseHiddenBlock();
        }
    }
}
