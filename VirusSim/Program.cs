using System;

namespace VirusSim
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();

            int size = 0, agents = 0, health = 0, infection = 0, turns = 0;
            bool view = false, save = false;

            int i = 0;
            while (i < args.Length)
            {
                if (args[i].Equals("-n",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    size = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-m",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    agents = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-l",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    health = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-tinf",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    infection = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-t",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    turns = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-v",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    view = true;
                }
                else if (args[i].Equals("-o",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    save = true;
                }
                i++;
            }

            if (size > 0 && agents > 0 && health > 0 && infection > 0 && 
            turns > 0)
            {
                Simulation sim = new Simulation(size);
                sim.Start(agents, health, infection, turns, view, save);
            }
            else
            {
                ui.InsufArgs();
            }
        }
    }
}
