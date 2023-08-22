namespace MarsRover
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object obj)
        {
            return ((Point)obj).x == this.x && ((Point)obj).y == this.y;
        }

    }
}
