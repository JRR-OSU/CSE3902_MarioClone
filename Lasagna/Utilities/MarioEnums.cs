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
        Dead,
        Flipped,
        Shell,
        Idle_Left
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

    public enum CameraType
    {
        EdgeControlled,
        Fixed
    }

    public enum PlayerType
    {
        Mario,
        Luigi
    }

    public enum EnemyType
    {
        Goomba,
        Koopa
    }

    public enum TileType
    {
        Brick,
        Flag,
        Floor,
        InvisibleBlock,
        QuestionBlock,
        UnbreakableBlock,
        Pipe,
        UndergroundBrick,
        UndergroundFloor
    }

    public enum ItemType
    {
        Coin,
        FireFlower,
        GrowMushroom,
        LifeMushroom,
        Star
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
