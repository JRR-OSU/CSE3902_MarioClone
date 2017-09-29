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

    public enum CollisionSide
    {
        None,
        Left,
        Right,
        Top,
        Bottom
    }

    public enum LevelType
    {
        MarioClear
    }

    public enum PlayerType
    {
        Mario
    }
}
