using System;

namespace VirusSim
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            Variables v      = new Variables();

            if (v.ValidateVars(args))
            {
                Simulation sim = new Simulation(v);
                sim.Start();
            }
            else 
            {
                ui.InsufArgs();
            }
        }
    }
}
