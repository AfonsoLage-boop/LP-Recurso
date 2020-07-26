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
            int i = 0;
            while (i < args.Length)
            {
                if (args[i].Equals("-n",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    Size = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-m",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    Agents = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-l",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    AgentsHP = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-tinf",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    TInfect = int.Parse(args[++i]);
                    continue;
                }
                else if (args[i].Equals("-t",
                StringComparison.InvariantCultureIgnoreCase))
                {
                    Turns = int.Parse(args[++i]);
                    continue;
                }
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
                i++;
            }

            if (Size > 0 && Agents > 0 && AgentsHP > 0 && TInfect > 0 && 
            Turns > 0)
            {
                return true;
            }
            return false;
        }
    }
}