namespace Lasagna
{
    public interface ICollider
    {
        void OnCollisionResponse(ICollider otherCollider, CollisionSide side);
    }
}
