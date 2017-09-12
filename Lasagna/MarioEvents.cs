namespace Sprint0
{
    public delegate void QuitEventHandler();
    public delegate void SelectNoMoveAndNoAnimationEventHandler();
    public delegate void SelectNoMoveAndAnimationEventHandler();
    public delegate void SelectMoveAndNoAnimationEventHandler();
    public delegate void SelectMoveAndAnimationEventHandler();

    //Class which contains all events for the project in one easy to locate place.
    public static class MarioEvents
    {
        public static event QuitEventHandler OnQuit;
        public static event SelectNoMoveAndNoAnimationEventHandler OnSelectNoMoveAndNoAnimation;
        public static event SelectNoMoveAndAnimationEventHandler OnSelectNoMoveAndAnimation;
        public static event SelectMoveAndNoAnimationEventHandler OnSelectMoveAndNoAnimation;
        public static event SelectMoveAndAnimationEventHandler OnSelectMoveAndAnimation;

        public static void Quit()
        {
            OnQuit();
        }

        public static void SelectNoMoveAndNoAnimation()
        {
            OnSelectNoMoveAndNoAnimation();
        }

        public static void SelectNoMoveAndAnimation()
        {
            OnSelectNoMoveAndAnimation();
        }

        public static void SelectMoveAndNoAnimation()
        {
            OnSelectMoveAndNoAnimation();
        }

        public static void SelectMoveAndAnimation()
        {
            OnSelectMoveAndAnimation();
        }
    }
}
