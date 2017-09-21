//Class which contains all enums for the project in one easy to locate place.
namespace Lasagna
{
    public enum SpriteType
    {
        NoMoveAndNoAnimation,
        NoMoveAndAnimation,
        MoveAndNoAnimation,
        MoveAndAnimation
    }

    public enum EnemyState
    {
        Idle,
        WalkLeft,
        WalkRight,
        Dead
    }
}
