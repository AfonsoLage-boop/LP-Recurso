using System;

namespace VirusSim
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            Variables v      = new Variables();

            // Sends all the command line arguments to the variables class, 
            // where they will then be validated and saved.
            // If validated, the program is redirected to start the simulation.
            if (v.ValidateVars(args))
            {
                Simulation sim = new Simulation(v);
                sim.Start();
            }

            // If not, an error message is presented explaining what might have 
            // gone wrong.
            else 
            {
                ui.InsufArgs();
            }
        }
    }
}
