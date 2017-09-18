namespace Lasagna
{
    public interface IController
    {
        void Update();
        void Quit();
        void Reset();
        void MoveLeft();
        void MoveRight();
        void Jump();
        void Crouch();
        void Fire();
        void MarioDamage();
        void MarioDie();
        void GetMushroom();
        void GetFireFlower();
        void UseQuestionBlock();
        void DestroyBrickBlock();
        void UseHiddenBlock();
    }
}
