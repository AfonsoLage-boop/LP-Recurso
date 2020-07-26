using System;


namespace VirusSim
{
    public class Agent
    {
        public int ID {get ; }

        public Coords Pos { get ; private set; }

        public State Status {get; private set; }

        private Variables v; 

        //private Grid grid;

        public Agent(int id, Coords pos, State Status)
        {;
            ID = id;
            Pos = pos;
        }

        public void CreateAgents ()
        {

            for (int i = 0; i < v.Agents; i++)
            {
                
                Status = State.Healthy;
                
                switch (Status)
                {
                    case State.Healthy:
                        
                        Console.WriteLine("Healthy Guy");
                        break;
                    case State.Infected:

                        Console.WriteLine("Zombie Guy");
                        break;

                }
                
            } 
            
        }


        private void infected()
        {

            Status = State.Infected;

        }


    }


}