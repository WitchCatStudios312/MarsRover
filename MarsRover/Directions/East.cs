namespace MarsRover
{
    public class East : IDirection
    {
        private readonly LandingZone landingZone;
        private readonly string roverID;
        public East(LandingZone lz, string strID) { landingZone = lz; roverID = strID; }

        public IDirection SpinLeft() { return new North(landingZone, roverID); }

        public IDirection SpinRight() { return new South(landingZone, roverID); }

        public void MoveForward() { landingZone.IncrementX(roverID); }

        public override string ToString() { return "E"; }
    }
}