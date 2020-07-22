namespace VirusSim
{
    public class Grid
    {
        private State[,] state;

        public Grid(int size)
        {
            state = new State[size,size];

        }
    }
}