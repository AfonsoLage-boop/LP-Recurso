using System;

namespace VirusSim
{
    public class UserInterface
    {
        public void InsufArgs()
        {
            Console.WriteLine("\n// !! ERROR !!");
            Console.WriteLine("// Insufficient arguments passed in command line.");
            Console.WriteLine("\n// To start the simulation I need to know:");
            Console.WriteLine("// -N    => grid dimensions (N x N);");
            Console.WriteLine("// -M    => number of agents;");
            Console.WriteLine("// -L    => agents' health (turns)");
            Console.WriteLine("// -Tinf => first infected (turn)");
            Console.WriteLine("// -T    => number of turns");

            Console.WriteLine("\n// Extras:");
            Console.WriteLine("// -v    => view live simulation");
            Console.WriteLine("// -o    => save simulation data in a .tsv file");
            Console.WriteLine("\n// Input Example:");
            Console.WriteLine("// (-N 50 -M 50 -L 2 -Tinf 5 -T 100 -v -o)");
        }

        public void Start(int size, int agents)
        {
            Console.WriteLine("\n// SIMULATION BEGINS");
            Console.WriteLine($"// {size} X {size} Test area");
            Console.WriteLine($"// {agents} Healthy agents\n");
        }

        public void ShowStats(int currentTurn, int countHealthy, 
                              int countInfected, int countDead)
        {
            string t = String.Format("{0,-4}",currentTurn);
            string h = String.Format("{0,4}",countHealthy);
            string i = String.Format("{0,4}",countInfected);
            string d = String.Format("{0,4}",countDead);
            
            Console.WriteLine("// TURN " + t + " >> " + h + " Healthy | " + i +
                              " Infected | " + d + " Dead");
        }
    }
}