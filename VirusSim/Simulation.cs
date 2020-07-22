using System;

namespace VirusSim
{
    public class Simulation
    {
        private Grid grid;
        private UserInterface ui;
        
        public Simulation(int size)
        {
            grid = new Grid(size);
            ui = new UserInterface();
        }

        public void Start(int agents, int health, int infection, int turns, 
        bool view, bool save)
        {
            //DEBUG
            Console.WriteLine("\nSimulation Started.");
            Console.WriteLine($"Agents    = {agents}");
            Console.WriteLine($"Health    = {health}");
            Console.WriteLine($"Infection = {infection}");
            Console.WriteLine($"Turns     = {turns}");
            Console.WriteLine($"View      = {view}");
            Console.WriteLine($"Save      = {save}");
        }
    }
}