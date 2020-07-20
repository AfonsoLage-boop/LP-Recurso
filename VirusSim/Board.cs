namespace VirusSim
{
    public class Board
    {
        private State[,] state;

        public int turn;

        public Board(int size)
        {
            state = new State[size,size];

            turn = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Black pieces on the top half of the board
                    if (i == 0 && j % 4 == 0 ||
                        i == 2 && j % 2 == 0 && j != 0 && j != 8)
                    {
                        state[i, j] = State.H;
                    }
                    // White pieces on the bottom half of the board
                    else if (i == 8 && j % 4 == 0 ||
                             i == 6 && j % 2 == 0 && j != 0 && j != 8)
                    {
                        state[i, j] = State.H;
                    }
                    // Free middle spot 
                    else if (i == 4 && j == 4)
                    {
                        state[i, j] = State.Empty;
                    }
                    // Path Down 
                    else if (i % 2 != 0 && j == 4)
                    {
                        state[i, j] = State.S;
                    }
                    // Path Side 
                    else if ((i == 0 || i == 8) && j % 4 != 0 ||
                             (i == 2 || i == 6) && (j == 3 || j == 5))
                    {
                        state[i, j] = State.S;
                    }
                    // Path Diagonal Top Left
                    else if (i == j && i % 2 != 0)
                    {
                        state[i, j] = State.W;
                    }
                    // Path Diagonal Top Right
                    else if (i == 1 && j == 7 ||
                             i == 3 && j == 5 ||
                             i == 5 && j == 3 ||
                             i == 7 && j == 1)
                    {
                        state[i, j] = State.E;
                    }
                    // Board Limits
                    else
                    {
                        state[i, j] = State.Blocked;
                    }
                }
            }
        }
    }
}