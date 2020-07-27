using System;

namespace VirusSim
{
    public class Grid
    {
        public int Max {get; private set;}
        private State[,] grid;
        

        public Grid(int max)
        {
            Max = max + 1;
            grid = new State[Max, Max];

            for (int i = 0; i < Max; i++)
            {
                for (int j = 0; j < Max; j++)
                {
                    grid[i, j] = State.Null;
                }
            }

////////////////////////////////////////////////////////////////////////////////
            // Console.WriteLine($"\n(D) Grid created: {max}, {max}");
////////////////////////////////////////////////////////////////////////////////
        }

        public void PlaceAgent(Agent agent)
        {
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

        public State GetState(int x, int y)
        {
            return grid[x, y];
        }
    }
}