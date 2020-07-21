using System;

namespace VirusSim
{
    public class Simulation
    {
        private Grid grid;
        private UserInterface ui;

        public Simulation(int size)
        {
            grid.Grid(size);

            ui.CreateGrid(grid, size);
        }

        public void Start(int agents, int health, int infection, int turns, 
        bool view, bool save)
        {
            //DEBUG
            Console.WriteLine("Simulation Started.");
            Console.WriteLine($"Agents    = {agents}");
            Console.WriteLine($"Health    = {health}");
            Console.WriteLine($"Infection = {infection}");
            Console.WriteLine($"Turns     = {turns}");
            Console.WriteLine($"View      = {view}");
            Console.WriteLine($"Save      = {save}");
        }
    }
}