namespace MarsRover
{
    public class North : IDirection
    {
        private readonly LandingZone landingZone;
        private readonly string roverID;
        public North(LandingZone lz, string strID) { landingZone = lz; roverID = strID; }

        public IDirection SpinLeft() { return new West(landingZone, roverID); }

        public IDirection SpinRight() { return new East(landingZone, roverID); }

        public void MoveForward() { landingZone.IncrementY(roverID); }

        public override string ToString() { return "N"; }

    }
}