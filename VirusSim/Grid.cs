namespace VirusSim
{
    /// <summary>
    /// <c>Grid</c> Class.
    /// Generates the grid of States where the simulation will be visualized 
    /// and contains auxiliar methods that update said States.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Self implemented property that reflects the grid dimensions.
        /// </summary>
        /// <value>Number of rows and columns.</value>
        public int Max {get; private set;}

        /// <summary>
        /// Reference to a matrix of States.
        /// </summary>
        private State[,] grid;

        /// <summary>
        /// Constructor, creates a new grid.
        /// </summary>
        /// <param name="max">Number of rows and columns.</param>
        public Grid(int max)
        {
            // Saves the variable value in the property.
            Max = max;

            // Creates the matrix of States.
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

        /// <summary>
        /// Updates the grid State that corresponds to the agent's position with
        /// this agent's State.
        /// </summary>
        /// <param name="agent">Agent's info.</param>
        public void PlaceAgent(Agent agent)
        {
            // Sets this agent's state in the same grid position.
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

        /// <summary>
        /// Changes the grid states depending on the agent's old and new 
        /// position. 
        /// <remarks>Takes into consideration that there might be another 
        /// agent in the old position and so, it scans the old position for 
        /// other agents as to not conflict with them.
        /// </remarks></summary>
        /// <param name="oldX">X value of position before moving.</param>
        /// <param name="oldY">Y value of position before moving.</param>
        /// <param name="agent">Agent that is moving.</param>
        /// <param name="allA">All agents.</param>
        public void MoveAgent(int oldX, int oldY, Agent agent, Agent[] allA)
        {
            // Booleans that save info about the states of any agents that 
            // might be in the old position.
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
                    // 1st priority - there is someone dead there.
                    if (other.State == State.Dead) dead = true;

                    // 2nd priority - there is someone infected there.
                    if (other.State == State.Infected) infected = true;
                    
                    // 3rd priority - there is someone healthy there.
                    if (other.State == State.Healthy) healthy = true;
                }
            }

            // In the end of the cycle, it updates the old position's state, 
            // according to their visual priority.
            if (dead) grid[oldX, oldY] = State.Dead;

            else if (infected) grid[oldX, oldY] = State.Infected;

            else if (healthy) grid[oldX, oldY] = State.Healthy;

            else grid[oldX, oldY] = State.Null;

            // Sets the new position.
            grid[agent.Pos.X, agent.Pos.Y] = agent.State;
        }

        /// <summary>
        /// Public method that returns which State is in a grid position.
        /// </summary>
        /// <param name="x">Row.</param>
        /// <param name="y">Column.</param>
        /// <returns>Grid State: Healthy, Infected, Dead or Null.</returns>
        public State GetState(int x, int y)
        {
            // Returns grid state in (x, y).
            return grid[x, y];
        }
    }
}