using System;

namespace VirusSim
{
    public class Grid
    {
        private State[,] state;
        private Variables v;

        public Grid(Variables v)
        {
            this.v = v;
            state = new State[v.Size, v.Size];
        }
    }
}