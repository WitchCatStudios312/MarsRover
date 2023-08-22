namespace MarsRover
{
    public class South : IDirection
    {
        private readonly LandingZone landingZone;
        private readonly string roverID;
        public South(LandingZone lz, string strID) { landingZone = lz; roverID = strID; }

        public IDirection SpinLeft() { return new East(landingZone, roverID); }

        public IDirection SpinRight() { return new West(landingZone, roverID); }

        public void MoveForward() { landingZone.DecrementY(roverID); }

        public override string ToString() { return "S"; }
    }
}