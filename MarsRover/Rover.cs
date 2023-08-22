using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarsRover
{
    public class Rover
    {
        private readonly LandingZone LandingZone;
        public string ID { get; private set; }
        public int DeploySequence { get; set; }
        public Point Position;
        private IDirection Direction;

        public LinkedList<ICommand> CMDList = new LinkedList<ICommand>();

        public Rover(LandingZone lz, string id, int deploySeq, Point position, IDirection direction, LinkedList<ICommand> cmdList)
        {
            LandingZone = lz;
            ID = id;
            DeploySequence = deploySeq;
            Position = position;
            Direction = direction;
            CMDList = cmdList;
        }

        public void ExecuteCommands()
        {
            if (CMDList != null && CMDList.Count > 0)
            {
                Console.WriteLine("Executing commands...");
                foreach (ICommand cmd in CMDList)
                {
                    if (cmd != null) { cmd.Execute(this); }
                }
            }else { Console.WriteLine(@"Rover {0} does not have any commands to execute", ID); return; }
        }


        public void MoveForward()
        {
            Direction.MoveForward();
            Position = LandingZone.getRoverPosition(ID);
            Debug.WriteLine("Move Forward - (" + Position?.x + ", " + Position?.y + ") " + Direction?.ToString());
        }

        public void SpinLeft()
        {
            Direction = Direction.SpinLeft();
            Debug.WriteLine("Spin Left - (" + Position?.x + ", " + Position?.y + ") " + Direction?.ToString());
        }


        public void SpinRight()
        {
            Direction = Direction.SpinRight();
            Debug.WriteLine("Spin Right - (" + Position?.x + ", " + Position?.y + ") " + Direction?.ToString());
        }



        #region "Print/String Helper Methods"

        /* Note: I created the nested wrapper method calls because I noticed the same code was being 
         * used multiple times, and also because if in the future we wish to change the way the
         * position is formatted, for example to "(x,y) D", we only have to make the change in a single place
         */

        /// <summary>
        /// Returns the string representation of the rover's current position
        /// </summary>
        private string getRoverPositionString()
        {
            return string.Format("{0} {1} {2}", Position?.x.ToString(), Position?.y.ToString(), Direction?.ToString());
        }

        /// <summary>
        /// Prints the rover's current position to the console
        /// </summary>
        public void printRoverPosition()
        {
            Console.WriteLine(getRoverPositionString());
        }

        /// <summary>
        /// Wrapper method to print a formatted string representing the rover's current position to the console.
        /// Note: This method does not calculate what the rover's starting position will be, it simply 
        /// prints the rover's current postition inside a string that says 'Start Position'.
        /// It is the responsibility of the caller to know that the current position is the starting position
        /// </summary>
        public void printRoverStartPosition()
        {       
            Console.WriteLine(string.Format("Rover {0} - Start Position: {1}", ID, getRoverPositionString()));
            Debug.WriteLine(string.Format("Rover {0} - Start Position: {1}", ID, getRoverPositionString()));
        }

        /// <summary>
        /// Wrapper method to print a formatted string representing the rover's current position to the console.
        /// Note: This method does not calculate what the rover's final position will be, it simply 
        /// prints the rover's current postition inside a string that says 'Final Position'.
        /// It is the responsibility of the caller to know that the current position is the final position
        /// </summary>
        public void printRoverFinalPosition()
        {
            Console.WriteLine(string.Format("Rover {0} - Final Position: {1}", ID, getRoverPositionString()));
            Debug.WriteLine(string.Format("Rover {0} - Final Position: {1}", ID, getRoverPositionString()));
        }

        
       
        /// <summary>
        /// Returns the string representation of the rover's current direction
        /// </summary>
        public string getRoverDirectionString() { return Direction?.ToString(); }        

        #endregion

    }
}
