using System;

namespace VirusSim
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();

            Grid(5);
            ui.CreateGrid(Grid.Grid);


        }
    }
}
