using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MarsRover
{
    public class InputDataConfiguration
    {
        //The object that contains all the information required to start the program
        //Collect the data however you want, from a file, the Console, a UI, as long as the program receives a valid configuration object we can start
        public string strGridDataConfig;
        public List<RoverDataConfig> roverDataConfigList = new List<RoverDataConfig>();

        public InputDataConfiguration(string strGridConfig, List<RoverDataConfig> roverConfigList)
        {
            strGridDataConfig = strGridConfig;
            roverDataConfigList = roverConfigList;
        }

        private LandingZone getLandingZone()
        {           
            var characters = strGridDataConfig.Split(' ');
            if (!int.TryParse(characters[0], out int gridWidth)) { Console.WriteLine("Could not parse grid x coordinate"); return null; }
            if (!int.TryParse(characters[1], out int gridHeight)) { Console.WriteLine("Could not parse grid y coordinate"); return null; }
            return new LandingZone(gridWidth, gridHeight);
        }

        public Dictionary<int, Rover> getRoverDictionary()
        {
            var roverDictionary = new Dictionary<int, Rover>();
            LandingZone lz = getLandingZone(); //initilize the landing zone
            var roverPositionDictionary = new Dictionary<string, Point>();      
            foreach (var rc in roverDataConfigList)
            {
                //Create the Rover and add its starting position to the dictionary               
                LinkedList<ICommand> cmdList = rc.getRoverCommandList();
                Point roverLocation = rc.getRoverPosition();
                IDirection roverDirection = rc.getRoverDirection(ref lz);
                if (roverDirection != null)
                {
                    Rover r = new Rover(lz, rc.roverID, rc.roverDeployNumber, roverLocation, roverDirection, cmdList);
                    roverPositionDictionary.Add(r.ID, r.Position);
                    roverDictionary.Add(r.DeploySequence, r);
                } else { Console.WriteLine("There is a problem with the input file. The following is not a valid rover configuration input: " + rc.ToString()); return null; }
            }          
            lz.setInitialRoverPositions(roverPositionDictionary); //we have to add the rovers positions to the landing zone 
            return roverDictionary;
        }
    }

    public class RoverDataConfig
    {
        public string roverCmdString;
        public string roverPositionString;
        public string roverID;
        public int roverDeployNumber;

        public LinkedList<ICommand> getRoverCommandList()
        {
            return getCMDListFromString(roverCmdString);
        }

        public Point getRoverPosition()
        { 
            var characters = roverPositionString.Split(' ');
            if (!int.TryParse(characters[0], out int roverX)) { Console.WriteLine(@"Could not parse Rover x coordinate {0}", characters[0]); return null; }
            if (!int.TryParse(characters[1], out int roverY)) { Console.WriteLine(@"Could not parse Rover y coordinate {0}", characters[1]); return null; }
            return new Point(roverX, roverY);
        }

        public IDirection getRoverDirection(ref LandingZone lz)
        {
            var characters = roverPositionString.Split(' ');
            switch (characters[2])
            {
                case "N":
                    return new North(lz, roverID);
                case "E":
                    return new East(lz, roverID);
                case "S":
                    return new South(lz, roverID);
                case "W":
                    return new West(lz, roverID);
                default:
                    string strMsg = string.Format("The input value {0} does not resolve to a valid Direction", characters[2]);
                    Console.WriteLine(strMsg);
                    Debug.WriteLine(strMsg);
                    return null;
            }
        }

        public override string ToString()
        {
            return string.Format("Position String: {0}, Command String: {1}");
        }


        #region "Command Parser"

        private Dictionary<string, ICommand> possibleCommands = new Dictionary<string, ICommand>
        {
            {"M", new MoveForwardCommand()},
            {"L", new SpinLeftCommand()},
            {"R", new SpinRightCommand()}
        };

        private ICommand parseCommandFromString(string strCmd)
        {
            if (possibleCommands.ContainsKey(strCmd)) { return possibleCommands[strCmd]; } else { return new NotFoundCommand(); }
        }

        private LinkedList<ICommand> getCMDListFromString(string commandString)
        {
            LinkedList<ICommand> cmdList = new LinkedList<ICommand>();
            for (int index = 0; index < commandString.Length; index++)
            {
                var strCmd = commandString[index].ToString();
                ICommand cmd = parseCommandFromString(strCmd);
                cmdList.AddLast(cmd);
            }
            return cmdList;
        }

        #endregion

    }


}
