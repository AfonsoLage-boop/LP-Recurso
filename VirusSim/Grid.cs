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

        public void MoveAgent(int oldX, int oldY, Agent agent, Agent[] allA)
        {
            // Cycles through all agents.
            foreach (Agent other in allA)
            {
                // Agent finds himself, ignore.
                if(other.ID == agent.ID) continue;

                // Another agent is in this agent's OLD POSITION.
                else if(other.Pos.X == oldX && other.Pos.Y == oldY)
                {
                    bool infected = false;

                    // The other agent is infected.
                    if(other.State == State.Infected) infected = true;

                    // The other agent just died.
                    else if(other.State == State.Dead) 
                    {
                        // 1st priority-  representing that someone died there.
                        grid[oldX, oldY] = State.Dead;
                        // No need to check the other agents due to priorities.
                        break;
                    }

                    // 2nd priority - someone is infected there.
                    if (infected) grid[oldX, oldY] = State.Infected;
                    
                    // 3rd priority - someone is there.
                    else grid[oldX, oldY] = other.State;
                }
                // 4th priority - no one is there.
                else grid[oldX, oldY] = State.Null;
            }
            // Sets this agent's state in the NEW POSITION.
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

        public State GetState(int x, int y)
        {
            // Returns grid state in (x, y).
            return grid[x, y];
        }
    }
}