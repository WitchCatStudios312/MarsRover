namespace MarsRover
{
    public class SpinRightCommand : ICommand
    {
        public void Execute(Rover rover)
        {
            rover.SpinRight();
        }
    }
}