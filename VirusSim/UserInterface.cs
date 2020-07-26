using System;

namespace VirusSim
{
    public class UserInterface
    {
        public void InsufArgs()
        {
            Console.WriteLine("\n!! ERROR !!");
            Console.WriteLine("Insufficient arguments passed in command line.");
            Console.WriteLine("\nTo start the simulation I need to know:");
            Console.WriteLine(" -N    => grid dimensions (N x N);");
            Console.WriteLine(" -M    => number of agents;");
            Console.WriteLine(" -L    => agents' health (in turns)");
            Console.WriteLine(" -Tinf => first infected (turn)");
            Console.WriteLine(" -T    => number of total turns");

            Console.WriteLine("\nExtras:");
            Console.WriteLine(" -v    => view live simulation");
            Console.WriteLine(" -o    => save simulation data in a .tsv file");
            Console.WriteLine("\nInput Example:");
            Console.WriteLine("(-N 50 -M 50 -L 2 -Tinf 5 -T 100 -v -o)");
        }

        public void Start(int size, int agents)
        {
            Console.WriteLine("\nSIMULATION BEGINS");
            Console.WriteLine($"{size} X {size} Test area");
            Console.WriteLine($"{agents} Healthy agents\n");
        }

        public void ShowStats(int turn, int healthy, int infected, int dead)
        {
            Console.WriteLine($"Turn {turn,-3} || {healthy,3} Healthy | " + 
            $"{infected,3} Infected | {dead,3} Dead ||");
        }
    }
}