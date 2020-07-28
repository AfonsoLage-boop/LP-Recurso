namespace VirusSim
{
    public class Agent
    {
        public  int    ID    {get;}
        public  int    HP    {get; set;}
        public  Coords Pos   {get; private set;}
        public  State  State {get; private set;}
        private Grid   grid;

        public Agent(int id, int hp, Coords pos, Grid grid)
        {
            // Saves all the constructor info in each new agent.
            ID        = id;
            HP        = hp;
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
            grid.PlaceAgent(this);
        }

        public void Contaminate(Agent[] allAgents)
        {
            foreach (Agent victim in allAgents)
            {
                if (ID == victim.ID) continue;
                
                else if (Pos.X == victim.Pos.X && Pos.Y == victim.Pos.Y &&
                    victim.State == State.Healthy)
                {
                    victim.State = State.Infected;
                    grid.PlaceAgent(victim);
                }
            }
        }

        public void Die()
        {
            // Updates Agent to Dead State.
            State = State.Dead;
            grid.PlaceAgent(this);
        }

        public void Remove()
        {
            // Updates Agent to Null State.
            State = State.Null;
            HP = -1;
            grid.PlaceAgent(this);
        }

        public void Move(int random)
        {
            int oldX      = Pos.X;
            int oldY      = Pos.Y;
            bool hasMoved = false;

            while(!hasMoved)
            {
                if (random == 0)
                {
                    if (Pos.X != 0)
                    {
                        Pos.X -= 1; // North
                        break;
                    }
                    else random = 1;
                }
                else if (random == 1)
                { 
                    if(Pos.Y != grid.Max - 1)
                    {
                        Pos.Y += 1; // East
                        break;
                    }
                    else random = 2;
                }
                else if (random == 2)
                { 
                    if (Pos.X != grid.Max - 1)
                    {
                        Pos.X += 1; // South
                        break;
                    }
                    else random = 3;;
                }
                else if (random == 3)
                { 
                    if (Pos.Y != 0)
                    {
                        Pos.Y -= 1; // West
                        break;
                    }
                    else random = 4;
                }
                else if (random == 4)
                { 
                    if (Pos.X != 0 && Pos.Y != grid.Max - 1)
                    {
                        Pos.X -= 1; // North
                        Pos.Y += 1; // East
                        break;
                    }
                    else random = 5;
                }
                else if (random == 5)
                { 
                    if (Pos.X != grid.Max - 1 && Pos.Y != grid.Max - 1)
                    {
                        Pos.X += 1; // South
                        Pos.Y += 1; // East
                        break;
                    }
                    else random = 6;
                }
                else if (random == 6)
                { 
                    if (Pos.X != grid.Max - 1 && Pos.Y != 0)
                    {
                        Pos.X += 1; // South
                        Pos.Y -= 1; // West
                        break;
                    }
                    else random = 7; 
                }
                else if (random == 7)
                { 
                    if (Pos.X != 0 && Pos.Y != 0)
                    {
                        Pos.X -= 1; // North
                        Pos.Y -= 1; // West
                        break;
                    }
                    else random = 0;
                }
            }
            grid.MoveAgent(oldX, oldY, this);
        }

        public override string ToString()
        {
            // Saves the initial of each State.
            string s = "";
            if      (State == State.Healthy)  s = "H";
            else if (State == State.Infected) s = "I";
            else if (State == State.Dead)     s = "D";
            else if (State == State.Null)     s = "N";

            return $"(D) {s}{ID,-3}:{HP,3}:{Pos}";
        }
    }
}