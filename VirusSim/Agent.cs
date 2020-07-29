namespace VirusSim
{
    /// <summary>
    /// <c>Agents</c> Class.
    /// Contains all info about each individual agent.
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Read only self implemented property that stores an unique ID for 
        /// each agent created.
        /// </summary>
        /// <value>Agent's ID.</value>
        public int ID {get;}

        /// <summary>
        /// Self implemented property that stores the HP o value of each agent, 
        /// can be modified outside of this class.
        /// </summary>
        /// <value>Agent's HP value.</value>
        public int HP {get; set;}

        /// <summary>
        /// Self implemented property that stores the position of each agent.
        /// </summary>
        /// <value>Coordinates of each agent's position in the grid.</value>
        public Coords Pos {get; private set;}

        /// <summary>
        /// Self implemented property that stores the State of each agent.
        /// </summary>
        /// <value>Agent's State: healthy, infected, dead or null.</value>
        public State State {get; private set;}

        /// <summary>
        /// Reference to the grid where the simulation will be visualized.
        /// </summary>
        private Grid grid;

        /// <summary>
        /// Constructor method, instantiates a new Agent.
        /// </summary>
        /// <param name="id">Agent's ID.</param>
        /// <param name="hp">Agent's HP value.</param>
        /// <param name="pos">Coordinates of each agent's position.</param>
        /// <param name="grid">Grid where simulation will be visualized</param>
        public Agent(int id, int hp, Coords pos, Grid grid)
        {
            // Saves all the constructor info in each new agent.
            ID        = id;
            HP        = hp;
            Pos       = pos;
            State     = State.Healthy;
            this.grid = grid;

            // Places each agent state in the grid.
            grid.PlaceAgent(this);
        }

        /// <summary>
        /// Updates the agent's State to Infected and updates his grid 
        /// position state.
        /// </summary>
        public void Infect()
        {
            // Updates agent to Infected State.
            State = State.Infected;

            // Updates agent state in the grid.
            grid.PlaceAgent(this);
        }

        /// <summary>
        /// Updates the agent's State to Dead and updates his grid 
        /// position state.
        /// </summary>
        public void Die()
        {
            // Updates agent to Dead State.
            State = State.Dead;

            // Updates agent state in the grid.
            grid.PlaceAgent(this);
        }

        /// <summary>
        /// Updates the agent's HP to -1, his state to Null and updates his grid 
        /// position state.
        /// </summary>
        public void Remove()
        {
            // Sets agent's HP to -1.
            HP = -1;

            // Updates agent to Null State.
            State = State.Null;

            // Updates agent state in the grid.
            grid.PlaceAgent(this);
        }

        /// <summary>
        /// Updates all agents' states (to infected) who are in the same 
        /// position as an already infected agent.
        /// </summary>
        /// <param name="allAgents">Array that contains all agents.</param>
        public void Contaminate(Agent[] allAgents)
        {
            // Cycles through all agents.
            foreach (Agent victim in allAgents)
            {
                // Infected agent finds himself, ignore.
                if (ID == victim.ID) continue;
                
                // Infected agent finds victim in the same position.
                else if (Pos.X == victim.Pos.X && Pos.Y == victim.Pos.Y &&
                    victim.State == State.Healthy)
                {
                    // Victim becomes infected.
                    victim.State = State.Infected;

                    // Updates agent info in grid.
                    grid.PlaceAgent(victim);
                }
            }
        }

        /// <summary>
        /// Finds a valid position to move an agent to, based on the random 
        /// number passed and then updates both old and new position states 
        /// in the grid.
        /// </summary>
        /// <param name="random">Random value in-between 0 and 7.</param>
        /// <param name="allAgents">Array with all agents.</param>
        public void Move(int random, Agent[] allAgents)
        {
            // Saves in variables the agent's position before moving.
            int oldX = Pos.X;
            int oldY = Pos.Y;

            // Bool that controls the next while cycle.
            bool hasMoved = false;

            // Cycle that determines in which direction the agent will move.
            // Starts in the if that contains the random value passed and keeps 
            // moving inside the cycle until it finds a valid move.
            while(!hasMoved)
            {
                if (random == 0)
                {
                    // Agent isn't in the top Row of the grid.
                    if (Pos.X != 0)
                    {
                        // Moves North.
                        Pos.X -= 1;

                        // Stop cycle. 
                        break;
                    }

                    // Changes random value to check if the next move is valid.
                    else random = 1;
                }
                else if (random == 1)
                {
                    // Agent isn't in the last Column of the grid.
                    if(Pos.Y != grid.Max - 1)
                    {
                        // Moves East.
                        Pos.Y += 1;
                        break;
                    }
                    else random = 2;
                }
                else if (random == 2)
                { 
                    // Agent isn't in the bottom Row of the grid.
                    if (Pos.X != grid.Max - 1)
                    {
                        // Moves South.
                        Pos.X += 1;
                        break;
                    }
                    else random = 3;;
                }
                else if (random == 3)
                {
                    // Agent isn't in the first Column of the grid.
                    if (Pos.Y != 0)
                    {
                        // Moves West.
                        Pos.Y -= 1;
                        break;
                    }
                    else random = 4;
                }
                else if (random == 4)
                {
                    // Agent isn't in the top Row nor in the last Column.
                    if (Pos.X != 0 && Pos.Y != grid.Max - 1)
                    {
                        // Moves NorthEast.
                        Pos.X -= 1;
                        Pos.Y += 1;
                        break;
                    }
                    else random = 5;
                }
                else if (random == 5)
                {
                    // Agent isn't in the last Row nor in the last Column.
                    if (Pos.X != grid.Max - 1 && Pos.Y != grid.Max - 1)
                    {
                        // Moves SouthEast.
                        Pos.X += 1;
                        Pos.Y += 1;
                        break;
                    }
                    else random = 6;
                }
                else if (random == 6)
                {
                    // Agent isn't in the last Row nor in the first Column.
                    if (Pos.X != grid.Max - 1 && Pos.Y != 0)
                    {
                        // Moves SouthWest.
                        Pos.X += 1;
                        Pos.Y -= 1;
                        break;
                    }
                    else random = 7; 
                }
                else if (random == 7)
                {
                    // Agent isn't in the last Row nor in the first Column.
                    if (Pos.X != 0 && Pos.Y != 0)
                    {
                        // Moves NorthWest.
                        Pos.X -= 1;
                        Pos.Y -= 1;
                        break;
                    }
                    else random = 0;
                }
            }

            // Updates both old and new grid states.
            grid.MoveAgent(oldX, oldY, this, allAgents);
        }

        /// <summary>
        /// Shows all the agent's important information.
        /// </summary>
        /// <remarks>Specially usefull for debugging.</remarks>
        /// <returns>A string with all the compact agent info.</returns>
        public override string ToString()
        {
            // Saves the initial letter of each State.
            string s = "";
            if      (State == State.Healthy)  s = "H";
            else if (State == State.Infected) s = "I";
            else if (State == State.Dead)     s = "D";
            else if (State == State.Null)     s = "N";

            return $"(A) {s}{ID,-3}:{Pos}:{HP,3}";
        }
    }
}