using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover
{
    public static class Utilities
    {
        #region "Input Parser"

        /// <summary>
        /// Reads a file and returns the text via reference parameter
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool readInputStringFromFile(string strFilePath, ref string strInput)
        {
            bool blnRet = true;
            if (File.Exists(strFilePath))
            {
                try
                {
                    using (var sr = new StreamReader(strFilePath)) { strInput = sr.ReadToEnd(); }
                }
                catch (IOException e) { Console.WriteLine("The file with path {0} could not be read: {1}", strFilePath, e.Message); blnRet = false; }
            }
            else { Console.WriteLine(@"File with path {0} does not exist.", strFilePath); blnRet = false; }
            return blnRet;
        }

        /// <summary>
        /// Parses the input string and returns the object containing the data required to start the program. 
        /// Returns null on fail.
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns>InputDataConfiguration</returns>
        public static InputDataConfiguration parseInputString(string strInput)
        {      
            var rxGrid = new Regex(@"\d+ \d+"); //looks for the grid config string
            var rxRover = new Regex(@"\d+ \d+ [NSEW]\s+[LRM]+"); //Looks for the 2 lines of rover config strings (position and command)
            var rxRoverPosition = new Regex(@"\d+ \d+ [NSEW]"); //looks for the rover position string
            var rxRoverCmds = new Regex(@"[LRM]+"); //looks for the rover command string

            //Get the grid string
            Match mGrid = rxGrid.Match(strInput);
            string strGrid = mGrid.Success ? mGrid.Value : "";
            if (string.IsNullOrWhiteSpace(strGrid)) { Console.WriteLine(@"Could not parse position from string {0}", strInput); return null; }

            //get the rover config strings
            List<RoverDataConfig> roverConfigList = new List<RoverDataConfig>();
            MatchCollection matches = rxRover.Matches(strInput);
            if(matches.Count < 1) { Console.WriteLine("Input string is invalid - could not find rover strings"); return null; }
            int seq = 1; 
            foreach (Match m in matches) 
            {
                //Get the position string
                Match mPosition = rxRoverPosition.Match(m.Value);
                string strPosition = mPosition.Success ? mPosition.Value : "";
                if (string.IsNullOrWhiteSpace(strPosition)) { Console.WriteLine(@"Could not parse position from string {0}", m.Value); continue; }
                //Get the command string
                Match mCommands = rxRoverCmds.Match(m.Value);
                string strCmds = mCommands.Success ? mCommands.Value : "";
                if (string.IsNullOrWhiteSpace(strCmds)) { Console.WriteLine("Could not parse commands from string '{0}'", m.Value); continue; }
                RoverDataConfig config = new RoverDataConfig() { roverID = seq.ToString(), roverDeployNumber = seq, roverPositionString = strPosition, roverCmdString = strCmds };
                roverConfigList.Add(config);
                seq++;
            }
            if(roverConfigList.Count() < 1) { Console.WriteLine("Error parsing input string - At least one Rover is required"); return null; }
            InputDataConfiguration dataConfiguration = new InputDataConfiguration(strGrid, roverConfigList);
            return dataConfiguration;
        }

        #endregion

    }
}
