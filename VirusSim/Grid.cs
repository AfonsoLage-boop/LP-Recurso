using System;

namespace VirusSim
{
    public class Grid
    {
        public int Max {get; private set;}
        private State[,] grid;
        

        public Grid(int max)
        {
            Max = max;
            grid = new State[Max, Max];

            // Cycles through the entire matrix.
            for (int i = 0; i < Max; i++)
            {
                for (int j = 0; j < Max; j++)
                {
                    // All positions have initial Null States.
                    grid[i, j] = State.Null;
                }
            }
        }

        public void PlaceAgent(Agent agent)
        {
            // Sets this agent's state in a position.
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

        public void Update(int oldX, int oldY, Agent agent, Agent[] allA)
        {
            bool dead     = false;
            bool infected = false;
            bool healthy  = false;

            // Cycles through all agents.
            foreach (Agent other in allA)
            {
                // Agent finds himself, ignore.
                if (other.ID == agent.ID) continue;

                // Another agent is in this agent's OLD POSITION.
                if (other.Pos.X == oldX && other.Pos.Y == oldY &&
                    other.State != State.Null)
                {
                    // 1st priority - someone dead is there.
                    if (other.State == State.Dead) dead = true;

                    // 2nd priority - someone infected is there.
                    if (other.State == State.Infected) infected = true;
                    
                    // 3rd priority - someone healthy is there.
                    if (other.State == State.Healthy) healthy = true;
                }
            }

            // In the end of the cycle, it sets the old position's state, 
            // according to their priority.
            if      (dead)     grid[oldX, oldY] = State.Dead;
            else if (infected) grid[oldX, oldY] = State.Infected;
            else if (healthy)  grid[oldX, oldY] = State.Healthy;
            else               grid[oldX, oldY] = State.Null;

            // Sets the new position.
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

        public State GetState(int x, int y)
        {
            // Returns grid state in (x, y).
            return grid[x, y];
        }
    }
}