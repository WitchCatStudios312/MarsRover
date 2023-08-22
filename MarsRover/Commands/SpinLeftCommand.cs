namespace MarsRover
{
    public class SpinLeftCommand : ICommand
    {
        public void Execute(Rover rover)
        {
            rover.SpinLeft();
        }
    }
}