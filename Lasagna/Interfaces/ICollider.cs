using Microsoft.Xna.Framework;

namespace Lasagna
{
    public interface ICollider
    {
        Rectangle Bounds { get; }
        void OnCollisionResponse(ICollider otherCollider, CollisionSide side);
    }
}
