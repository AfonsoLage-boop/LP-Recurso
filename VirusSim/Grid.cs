using System;

namespace VirusSim
{
    public class Grid
    {
        private State[,] grid;

        public Grid(int xMax, int yMax)
        {
            grid = new State[xMax, yMax];

            for (int i = 0; i < xMax; i++)
            {
                for (int j = 0; j < yMax; j++)
                {
                    grid[i, j] = State.Null;
                }
            }

////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine($"\n(D) Grid created: {xMax}, {yMax}");
////////////////////////////////////////////////////////////////////////////////
        }

        public void PlaceAgent(Agent agent)
        {
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

    }
}