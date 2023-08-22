using System;
using System.Diagnostics;

namespace MarsRover
{
    public class NotFoundCommand : ICommand
    {
        public void Execute(Rover rover)
        {
            Console.WriteLine("Command Not Found!");
            Debug.WriteLine("Command Not Found!");
        }
    }
}