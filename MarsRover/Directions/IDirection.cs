namespace MarsRover
{
    public interface IDirection
    {
        IDirection SpinLeft();
        IDirection SpinRight();
        void MoveForward();
    }
}
