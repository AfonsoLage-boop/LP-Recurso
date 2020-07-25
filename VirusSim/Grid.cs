using System;

namespace VirusSim
{
    public class Grid
    {
        private State[,] state;
        private Variables v;

        public int turn;

        public CreateGrid(int size, int agents)
        {
            state = new State[size,size];

            for (int i = 0; i < agents; i++)
            {
                int aux1 = Random.Next(0, size);
                int aux2 = Random.Next(0, size);

                state[aux1,aux2] = State.H; 
                // Arranjar uma forma de os guardar 
            }

            //Atribuir o state a um dos gajos healthy

        }
        public Grid(Variables v)
        {
            this.v = v;
            state = new State[v.Size, v.Size];
        }
    }
}