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
            ID        = id;
            Pos       = pos;
            State     = State.Healthy;
            this.grid = grid;

            // grid.AddAgent()
        }

        public override string ToString()
        {
            string s = "";
            if (State == State.Healthy) s = "H";
            if (State == State.Infected) s = "I";
            if (State == State.Dead) s = "D";
            return $"(D) {s}{ID,-3}{Pos}";
        }
    }
}