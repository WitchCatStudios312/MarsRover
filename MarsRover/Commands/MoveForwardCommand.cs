namespace MarsRover
{
    public class MoveForwardCommand : ICommand
    {
        public void Execute(Rover rover)
        {
            rover.MoveForward();
        }
    }
}