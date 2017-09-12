using Microsoft.Xna.Framework;

namespace Sprint0
{
    public interface IController
    {
        void Update();
        void Quit();
        void SelectOneFrameFixedPosition();
        void SelectAnimatedFixedPosition();
        void SelectOneFrameMoving();
        void SelectAnimatedMoving();
    }
}
