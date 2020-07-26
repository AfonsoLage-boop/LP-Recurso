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
            

            grid.PlaceAgent(this);
        }

        public void SpreadInfection(int aux,Agent[] allAgents)
        {

            foreach (Agent agent in allAgents)
            {
                if (agent.ID == aux)
                {
                    agent.State = State.Infected;
                }

            }

        }


        private void Infected()
        {

        }
    }
}