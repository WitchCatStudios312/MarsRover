using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class LandingZone
    {
        private int width;
        private int height;
        public Dictionary<string, Point> roverPositionDictionary;

        public LandingZone(int w, int h)
        {
            width = w;
            height = h;
            roverPositionDictionary = new Dictionary<string, Point>();
        }

        public void setInitialRoverPositions(Dictionary<string, Point> roverPositionDict)
        {
            if(roverPositionDict != null) {roverPositionDictionary = roverPositionDict; }          
        }

        public void updateRoverPosition(string roverID, Point newPosition)
        {
            if (roverPositionDictionary.ContainsKey(roverID))
            {
                roverPositionDictionary[roverID] = newPosition;
            }
            else
            {
                roverPositionDictionary.Add(roverID, newPosition);
            }
        }

        public Point getRoverPosition(string roverID)
        {
            //TODO - maybe some error handling in case ID is not a key in the dictionary?
            return roverPositionDictionary[roverID];
        }


        public bool isPositionValid(string roverID, Point newPosition, ref string errMsg)
        {
            foreach (KeyValuePair<string, Point> kvp in roverPositionDictionary)
            {
                if (kvp.Key != roverID && kvp.Value.x == newPosition.x && kvp.Value.y == newPosition.y) {
                    errMsg = string.Format("Rover {0} is already at Position {1} {2}", kvp.Key, kvp.Value.x, kvp.Value.y);
                    return false; 
                }
            }
            if (newPosition.x < 0 || newPosition.x > width) { errMsg = "it will fall off the Plateau"; return false; }
            if (newPosition.y < 0 || newPosition.y > height) { errMsg = "it will fall off the Plateau"; return false; }
            return true;
        }

        public void IncrementX(string roverID)
        {
            if (roverPositionDictionary.ContainsKey(roverID))
            {
                var currentPosition = roverPositionDictionary[roverID];
                Point newPosition = new Point((currentPosition.x + 1), currentPosition.y);
                string errMsg = "";
                if (isPositionValid(roverID, newPosition, ref errMsg)) { updateRoverPosition(roverID, newPosition); } else { Console.WriteLine(@"Invalid Move! Rover {0} cannot move forward because {1}.", roverID, errMsg); return; }
            } else { Console.WriteLine(string.Format("Rover with ID {0} does not exist! (Source: LandingZone.IncrementX)", roverID)); }
        }

        public void DecrementX(string roverID)
        {
            if (roverPositionDictionary.ContainsKey(roverID))
            {
                var currentPosition = roverPositionDictionary[roverID];
                Point newPosition = new Point((currentPosition.x - 1), currentPosition.y);
                string errMsg = "";
                if (isPositionValid(roverID, newPosition, ref errMsg)) { updateRoverPosition(roverID, newPosition); } else { Console.WriteLine(string.Format("Invalid Move! Rover {0} cannot move forward because {1}.", roverID, errMsg)); return; }
            } else { Console.WriteLine(string.Format("Rover with ID {0} does not exist! (Source: LandingZone.DecrementX)", roverID)); }
        }

        public void IncrementY(string roverID)
        {
            if (roverPositionDictionary.ContainsKey(roverID))
            {
                var currentPosition = roverPositionDictionary[roverID];
                Point newPosition = new Point(currentPosition.x, (currentPosition.y + 1));
                string errMsg = "";
                if (isPositionValid(roverID, newPosition, ref errMsg)) { updateRoverPosition(roverID, newPosition); } else { Console.WriteLine(string.Format("Invalid Move! Rover {0} cannot move forward because {1}.", roverID, errMsg)); return; }
            } else { Console.WriteLine(string.Format("Rover with ID {0} does not exist! (Source: LandingZone.IncrementY)", roverID)); }
        }

        public void DecrementY(string roverID)
        {
            if (roverPositionDictionary.ContainsKey(roverID))
            {
                var currentPosition = roverPositionDictionary[roverID];
                Point newPosition = new Point(currentPosition.x, (currentPosition.y - 1));
                string errMsg = "";
                if (isPositionValid(roverID, newPosition, ref errMsg)) { updateRoverPosition(roverID, newPosition); } else { Console.WriteLine(string.Format("Invalid Move! Rover {0} cannot move forward because {1}.", roverID, errMsg)); return; }
            } else { Console.WriteLine(string.Format("Rover with ID {0} does not exist! (Source: LandingZone.DecrementY)", roverID)); }
        }



    }
}
