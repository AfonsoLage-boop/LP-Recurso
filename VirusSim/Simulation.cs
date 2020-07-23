using System;

namespace VirusSim
{
    public class Simulation
    {
        private Grid grid;
        private UserInterface ui;
        private Variables v;
        
        public Simulation(Variables v)
        {
            this.v = v;
            grid = new Grid(v);
            ui   = new UserInterface();
        }

        public void Start()
        {
            //DEBUG
            Console.WriteLine("\nSimulation Started.");
            Console.WriteLine($"Size           = {v.Size}");
            Console.WriteLine($"Agents         = {v.Agents}");
            Console.WriteLine($"AgentsHP       = {v.AgentsHP}");
            Console.WriteLine($"Infection Turn = {v.TInfect}");
            Console.WriteLine($"Turns          = {v.Turns}");
            Console.WriteLine($"View           = {v.View}");
            Console.WriteLine($"Save           = {v.Save}");
        }
    }
}