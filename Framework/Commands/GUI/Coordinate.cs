namespace PrintHub.Framework.Commands.GUI
{
    public class Coordinate
    {
        public int X;
        public int Y;
        public static Coordinate Empty =  new Coordinate(-1, 0);

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}