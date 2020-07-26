using System;

namespace VirusSim
{
    public class Agent
    {
        public int ID {get ; }

        // public Coords Pos { get ; private set; }

        public State Status {get; private set; }

        private Variables v; 

        //private Grid grid;

         public Agent(Variables v)
        {
            this.v = v;
        }



        public void Agents ()
        {
            
            for (int i = 0; i < v.Agents; i++)
            {
                
                Console.WriteLine("Banana");
                
                
                /*switch (Status)
                {
                    case State.Healthy:
                        
                        Console.WriteLine("Yep está curado");
                        break;
                    case State.Infected:

                        Console.WriteLine("This isn't suppost to appear");
                        break;
                    default:
                        break;
                }*/
            } 
            

        }
    }


}