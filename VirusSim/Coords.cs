namespace VirusSim
{
    public class Coords
    {
        public int X {get ; set;}
        public int Y {get ; set;}

        public Coords(int x, int y)
        {
            // Saves the receiving coordinates.
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X,2},{Y,2})";
        }
    }
}