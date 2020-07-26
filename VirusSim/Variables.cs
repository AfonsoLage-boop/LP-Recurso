using System;

namespace VirusSim
{
    public struct Variables
    {
        public int  Size     {get; private set;}
        public int  Agents   {get; private set;}
        public int  AgentsHP {get; private set;}
        public int  TInfect  {get; private set;}
        public int  Turns    {get; private set;}
        public bool View     {get; private set;}
        public bool Save     {get; private set;}

        private Variables(int size, int agents, int agentsHP, int tInfect,
            int turns, bool view, bool save)
        {
            // Saves all variables.
            Size     = size;
            Agents   = agents;
            AgentsHP = agentsHP;
            TInfect  = tInfect;
            Turns    = turns;
            View     = view;
            Save     = save;
        }

        public bool ValidateVars(string[] args)
        {
            // Cycles through all arguments given.
            for (int i = 0; i < args.Length; i++)
            {
                // Compares all arguments with "-n".
                if (args[i].Equals("-n",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    // If it finds it, it tries to converts whichever argument
                    // comes next to an integer and saves it.
                    int aux;

                    // If it succeeds,
                    if (int.TryParse(args[++i], out aux))
                    {
                        // the integer is saved in its corresponding property,
                        Size = aux;
                        // and we move on to the next.
                        continue;
                    }
                    // If it doesnt, the cycle breaks, instead of throwing an 
                    // exception.
                    else break;
                }
                // The same process is repeated for all the other key arguments.
                else if (args[i].Equals("-m",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    int aux;
                    if (int.TryParse(args[++i], out aux))
                    {
                        Agents = aux;
                        continue;
                    }
                    else break;
                }
                else if (args[i].Equals("-l",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    int aux;
                    if (int.TryParse(args[++i], out aux))
                    {
                        AgentsHP = aux;
                        continue;
                    }
                    else break;
                }
                else if (args[i].Equals("-tinf",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    int aux;
                    if (int.TryParse(args[++i], out aux))
                    {
                        TInfect = aux;
                        continue;
                    }
                    else break;
                }
                else if (args[i].Equals("-t",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    int aux;
                    if (int.TryParse(args[++i], out aux))
                    {
                        Turns = aux;
                        continue;
                    }
                    else break;
                }

                // Because theres no need to convert, here we only check these
                // properties as true.
                else if (args[i].Equals("-v",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    View = true;
                }
                else if (args[i].Equals("-o",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    Save = true;
                }
            }

            // Checks if all the necessary variables have been converted.
            if (Size > 0 && Agents > 0 && AgentsHP > 0 && TInfect > 0 && 
                Turns > 0)
            {
                // If so, these are valid.
                return true;
            }
            // Otherwise, they are not.
            return false;
        }
    }
}