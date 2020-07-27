namespace VirusSim
{
    public class Agent
    {
        public  int    ID    {get;}
        public  Coords Pos   {get ; private set;}
        public  State  State {get; private set;}
        private Grid   grid;

        public Agent(int id, Coords pos, Grid grid)
        {
            // Saves all the constructor info in each new agent.
            ID        = id;
            Pos       = pos;
            State     = State.Healthy;
            this.grid = grid;

            // Places an agent on the grid.
            grid.PlaceAgent(this);
        }

        public void Infect()
        {
            // Updates Agent to Infected State.
            State = State.Infected;
        }
        
        public override string ToString()
        {
            // Saves the initial of each State.
            string s = "";
            if (State == State.Healthy) s = "H";
            else if (State == State.Infected) s = "I";
            else if (State == State.Dead) s = "D";

            return $"(D) {s}{ID,-3}{Pos}";
        }
    }
}