using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    class Program
    {
         static void Main(string[] args)
        {
            //Get the required data
            string strInput = "";
            string strFilePath = args[0];
            if(!Utilities.readInputStringFromFile(strFilePath, ref strInput)) { Console.ReadLine(); return; }
            InputDataConfiguration inputData = Utilities.parseInputString(strInput);
            if(inputData == null) { Console.ReadLine(); return; }

            //Generate the Rovers
            Dictionary<int, Rover> roverDictionary = inputData.getRoverDictionary();         

            //Deploy the Rovers
            var roversInDeploySequence = roverDictionary.Keys.ToArray();
            Array.Sort(roversInDeploySequence);
            foreach (int key in roversInDeploySequence)
            {
                Rover r = roverDictionary[key];
                r.printRoverStartPosition();
                r.ExecuteCommands();
                r.printRoverFinalPosition();
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
