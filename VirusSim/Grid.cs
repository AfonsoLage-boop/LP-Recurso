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
    }
}