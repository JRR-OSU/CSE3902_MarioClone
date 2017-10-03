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
        Flipped
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
        Pipe
    }

    public enum ItemType
    {
        Coin,
        FireFlower,
        GrowMushroom,
        LifeMushroom,
        Star
    }
}
