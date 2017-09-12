namespace Sprint0
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
            OnQuit();
        }

        public static void Reset()
        {
            OnReset();
        }

        public static void MoveLeft()
        {
            OnMoveLeft();
        }

        public static void MoveRight()
        {
            OnMoveRight();
        }

        public static void Jump()
        {
            OnJump();
        }

        public static void Crouch()
        {
            OnCrouch();
        }

        public static void Fire()
        {
            OnFire();
        }

        public static void MarioDamage()
        {
            OnMarioDamage();
        }

        public static void MarioDie()
        {
            OnMarioDie();
        }

        public static void GetMushroom()
        {
            OnGetMushroom();
        }

        public static void GetFireFlower()
        {
            OnGetFireFlower();
        }

        public static void UseQuestionBlock()
        {
            OnUseQuestionBlock();
        }

        public static void DestroyBrickBlock()
        {
            OnDestroyBrickBlock();
        }

        public static void UseHiddenBlock()
        {
            OnUseHiddenBlock();
        }
    }
}
