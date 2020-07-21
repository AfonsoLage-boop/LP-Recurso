namespace VirusSim
{
    public class UserInterface
    {
        public void CreateGrid(Grid grid)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    switch (board.GetState(new Position(i, j)))
                    {
                        case State.X:
                            Console.Write(" X ");
                            break;
                        case State.O:
                            Console.Write(" O ");
                            break;
                        case State.Undecided:
                            Console.Write("   ");
                            break;
                    }
                    if (j < 2) Console.Write("|");
                }
                if (i < 2) Console.WriteLine("\n---+---+---");
            }
            Console.WriteLine();
        }
    }
}