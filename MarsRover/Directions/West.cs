namespace MarsRover
{
    public class West : IDirection
    {
        private readonly LandingZone landingZone;
        private readonly string roverID;
        public West(LandingZone lz, string strID) { landingZone = lz; roverID = strID; }

        public IDirection SpinLeft() { return new South(landingZone, roverID); }

        public IDirection SpinRight() { return new North(landingZone, roverID); }

        public void MoveForward() { landingZone.DecrementX(roverID); }

        public override string ToString() { return "W"; }
    }
}