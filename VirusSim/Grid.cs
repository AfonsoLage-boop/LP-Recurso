namespace VirusSim
{
    public class Grid
    {
        private State[,] state;

        public int turn;

        public Grid(int size)
        {
            state = new State[size,size];

            turn = 0;
        }
    }
}