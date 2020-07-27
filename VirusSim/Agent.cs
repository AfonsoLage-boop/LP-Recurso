namespace VirusSim
{
    public class Agent
    {
        public  int    ID    {get;}
        public  Coords Pos   {get ; private set;}
        public  State  State {get; private set;}
        private Grid   grid;
        private Agent[] allAgents;
        private Variables v;

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
            if (State == State.Infected) s = "I";
            if (State == State.Dead) s = "D";

            return $"(D) {s}{ID,-3}{Pos}";
        }

        public void AgentWalk(Coords pos, Direction dir)
        {
            // Reading the x and y coordinates.
            int x = pos.X;
            int y = pos.Y;


            foreach (Agent agent in allAgents)
            {
                switch(dir)
                {
                    case Direction.N:
                        if (y-1 >= 0)
                        {
                            y -=1;
                            break;
                        }
                        else 
                        {
                            y +=1;
                            break;
                        }
                    case Direction.NW:
                        if (x-1 >= 0 && y-1 >=0)
                        {
                            x -=1;
                            y -=1;
                            break;
                        }
                        else
                        {
                            x +=1;
                            y +=1;
                            break;

                        }
                    case Direction.W:
                        if (x-1 >= 0)
                        {
                            x -=1;
                            break;
                        }
                        else
                        {
                            x +=1;
                            break;
                        }
                    case Direction.SW:
                        if (x-1 >= 0 && y+1 <= v.Size)
                        {
                            x -=1;
                            y +=1;
                            break;
                        }
                        else
                        {
                            x +=1;
                            y -=1;
                            break;
                        }
                    case Direction.S:
                        if (y+1 <= v.Size)
                        {
                            y +=1;
                            break;
                        }
                        else
                        {
                            y -=1;
                            break;
                        }
                    case Direction.SE:
                        if (x+1 <= v.Size && y+1 <= v.Size)
                        {
                            x +=1;
                            y +=1;
                            break;
                        }
                        else
                        {
                            x -=1;
                            y -=1;
                            break;
                        }
                    case Direction.E:
                        if(x+1 <= v.Size)
                        {
                            x +=1;
                            break;
                        }
                        else
                        {
                            x -=1;
                            break;
                        }
                    case Direction.NE:
                        if (x+1 <= v.Size && y-1 >= 0)
                        {
                            x +=1;
                            y -=1;
                            break;
                        }
                        else
                        {
                            x -=1;
                            y +=1;
                            break;
                        }
                }
            }
        }









    }
}