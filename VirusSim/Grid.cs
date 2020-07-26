using System;

namespace VirusSim
{
    public class Grid
    {
        private Agent[,] grid;

        public Grid(int x, int y)
        {
            grid = new Agent[x, y];
            
            Console.WriteLine($"\n(D) Grid created: {x}, {y}"); //DEBUG LINE
        }


        public void PlaceAgent(Agent agent)
        {
            grid[agent.Pos.X, agent.Pos.Y] = agent;

        }

    }
}