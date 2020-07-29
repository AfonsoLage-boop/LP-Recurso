using System;

namespace VirusSim
{
    /// <summary>
    /// <c>Variables</c> Struct.
    /// Handles all command line arguments, validating them.
    /// If valid, the arguments are then stored in accessible properties.
    /// </summary>
    public struct Variables
    {
        /// <summary>
        /// Self implemented property that stores the grid dimensions.
        /// </summary>
        /// <value>Dimension of simulation grid.</value>
        public int Size {get; private set;}

        /// <summary>
        /// Self implemented property that stores the number of agents.
        /// </summary>
        /// <value>Number of agents.</value>
        public int Agents {get; private set;}

        /// <summary>
        /// Self implemented property that stores the agents' health (in turns).
        /// </summary>
        /// <value>Number of agents' health (in turns).</value>
        public int AgentsHP {get; private set;}

        /// <summary>
        /// Self implemented property that stores the first infected turn.
        /// </summary>
        /// <value>Number of first infected turn.</value>
        public int TInfect {get; private set;}

        /// <summary>
        /// Self implemented property that stores the number of total turns.
        /// </summary>
        /// <value>Number of total turns.</value>
        public int Turns {get; private set;}

        /// <summary>
        /// Self implemented property that stores a boolean that controls if 
        /// the simulation is to be visualized.
        /// </summary>
        /// <value><c>True</c> if is to be visualized.</value>
        public bool View {get; private set;}

        /// <summary>
        /// Self implemented property that stores a boolean that controls if 
        /// the simulation data is to be exported in the end.
        /// </summary>
        /// <value><c>True</c> if is to be exported.</value>
        public bool Export {get; private set;}

        /// <summary>
        /// Self implemented property that stores the file to where the data 
        /// will be exported to.
        /// </summary>
        /// <value>Export file name.</value>
        public string File {get; private set;}

        /// <summary>
        /// Private constructor that instantiates this struct with the 
        /// command line arguments, if all are valid.
        /// </summary>
        /// <param name="size">Grid dimensions.</param>
        /// <param name="agents">Number of agents.</param>
        /// <param name="agentsHP">Number of agents' health (in turns).</param>
        /// <param name="tInfect">Number of first infected turn.</param>
        /// <param name="turns">Number of total turns.</param>
        /// <param name="view">Is to be visualized.</param>
        /// <param name="export">Is to be exported.</param>
        /// <param name="file">Export file name.</param>
        private Variables(int size, int agents, int agentsHP, int tInfect,
            int turns, bool view, bool export, string file)
        {
            // Saves all the variables values in the properties.
            Size     = size;
            Agents   = agents;
            AgentsHP = agentsHP;
            TInfect  = tInfect;
            Turns    = turns;
            View     = view;
            Export   = export;
            File     = file;
        }

        /// <summary>
        /// Validates the arguments passed in <c>Main</c> Class.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns><c>True</c> if all arguments are valid</returns>
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
                    }
                    // If it doesnt, the cycle breaks, instead of throwing an 
                    // exception.
                    else break;
                }

                ////////////////////////////////////////////////////////////////
                // The same process is repeated for all these next arguments. //
                ////////////////////////////////////////////////////////////////

                else if (args[i].Equals("-m",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    int aux;
                    if (int.TryParse(args[++i], out aux))
                    {
                        Agents = aux;
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
                    }
                    else break;
                }

                // Stores View as True.
                else if (args[i].Equals("-v",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    View = true;
                }

                // Stores Export as True and creates a default file name.
                else if (args[i].Equals("-o",
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    Export = true;

                    // Default file name.
                    File = "simulationData.tsv";
                }
                
                // Overrides default file name with the one given by the user.
                else if(args[i].Contains(".tsv"))
                {
                    File = args[i];
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