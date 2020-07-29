using System;

namespace VirusSim
{
    /// <summary>
    /// <c>UserInterface</c> Class.
    /// Renders visual info to console. Its methods include: error messages, 
    /// simulation status, data and live grid visualization.
    /// </summary>
    public class UserInterface
    {
        ////////////////////////////////////////////////////////////////////////
        // ANSI escape codes (an alternative to ConsoleColor using GitBash).  //
        ////////////////////////////////////////////////////////////////////////

        /// <summary>Red text color.</summary>
        private const string red = "\u001b[31m";

        /// <summary>Red background color.</summary>
        private const string redBG = "\u001b[41m";

        /// <summary>Green text color.</summary>
        private const string green = "\u001b[32m";

        /// <summary>Green background color.</summary>
        private const string greenBG = "\u001b[42m";

        /// <summary>Yellow text color.</summary>
        private const string yellow = "\u001b[33m";

        /// <summary>Yellow background color.</summary>
        private const string yellowBG = "\u001b[43m";

        /// <summary>White background color.</summary>
        private const string whiteBG = "\u001b[47m";

        /// <summary>Black text color.</summary>
        private const string black = "\u001b[30m";

        /// <summary>Black backround color.</summary>
        private const string blackBG = "\u001b[40m";

        /// <summary>Bold text.</summary>
        private const string bold = "\u001b[1m";

        /// <summary>Resets ANSI configurations.</summary>
        private const string reset = "\u001b[0m";

        /// <summary>
        /// Renders error message that gives information on how to properly 
        /// run the program and what to input in the command line.
        /// </summary>
        public void InsufArgsMsg()
        {
            Console.WriteLine(
                $"\n{bold}{red}!! ERROR !!");
            Console.WriteLine(
                $"Insufficient arguments passed in command line.{reset}");
            Console.WriteLine(
                "\nTo start the simulation I need to know:");
            Console.WriteLine(
                $"{yellow} -N{reset}    > grid dimensions (N x N);");
            Console.WriteLine(
                $"{yellow} -M{reset}    > number of agents;");
            Console.WriteLine(
                $"{yellow} -L{reset}    > agents' health (in turns)");
            Console.WriteLine(
                $"{yellow} -Tinf{reset} > first infected turn");
            Console.WriteLine(
                $"{yellow} -T{reset}    > number of total turns");
            Console.WriteLine(
                "\nExtras:");
            Console.WriteLine(
                $"{yellow} -v{reset}    > view live simulation");
            Console.WriteLine(
                $"{yellow} -o{reset}    > export data to a .tsv file");
            Console.WriteLine(
                $"\nInput Examples:\n(after // {green}dotnet run --{reset} )\n");
            Console.WriteLine(
                $"{green} -N 25 -M 50 -L 5 -Tinf 5 -T 50");
            Console.WriteLine(
                " -M 50 -N 25 -Tinf 5 -T 50 -L 5");
            Console.WriteLine(
                " -N 50 -M 50 -L 2 -Tinf 5 -T 50 -v -o");
            Console.WriteLine(
                $" -v -N 50 -M 50 -L 2 -Tinf 5 -T 50 -o data.tsv{reset}");
        }

        /// <summary>
        /// Renders first simulation message, confirmating it has begun.
        /// </summary>
        /// <param name="size">Grid Dimensions.</param>
        /// <param name="agents">Number of agents.</param>
        public void StartMsg(int size, int agents)
        {
            Console.WriteLine("\nSIMULATION BEGINS");
            Console.WriteLine($"{size} X {size} Test area");
            Console.WriteLine($"{agents} Healthy agents\n");
        }

        /// <summary>
        /// End Message that is rendered if the turn limit is reached.
        /// </summary>
        /// <param name="turn">Current simulation turn.</param>
        public void MaxTurnsMsg(int turn)
        {
            Console.WriteLine("\n// Simulation Complete.");
            Console.WriteLine($"\n// User turns limit reached. ({turn})");
        }

        /// <summary>
        /// End Message that is rendered if all agents die.
        /// </summary>
        /// <param name="turn">Current simulation turn.</param>
        /// <param name="agents"></param>
        public void AllDeadMsg(int turn, int agents)
        {
            Console.WriteLine("\n// Simulation Complete.");
            Console.WriteLine($"\n// All {agents} agents died by turn {turn}");
        }

        /// <summary>
        /// End Message that is rendered if the virus dies.
        /// </summary>
        /// <param name="turn">Current simulation turn.</param>
        /// <param name="healthy">Number of healthy agents.</param>
        public void InfectEndMsg(int turn, int healthy)
        {
            Console.WriteLine("\n// Simulation Complete.");
            Console.WriteLine($"\n// The virus died in turn {turn} with " +
                $"{healthy} agents remaining.");
        }

        /// <summary>
        /// Renders current turn's count.
        /// </summary>
        /// <param name="turn">Current simulation turn.</param>
        /// <param name="healthy">Number of healthy agents.</param>
        /// <param name="infected">Number of infected agents.</param>
        /// <param name="dead">Number of dead agents.</param>
        public void ShowStats(int turn, int healthy, int infected, int dead)
        {
            string t = $"{bold}{turn,-3}{reset}";
            string h = $"{green}{bold}{healthy,3}{reset}";
            string i = $"{yellow}{bold}{infected,3}{reset}";
            string d = $"{red}{bold}{dead,3}{reset}";

            Console.WriteLine($"Turn {t} >> {h} healthy | {i} infected | " +
                $"{d} dead");
        }

        /// <summary>
        /// Renders message confirming that a file has been exported.
        /// </summary>
        /// <param name="file">File name.</param>
        public void Exported(string file)
        {
            Console.WriteLine($"\n// Data exported to {file}.");
        }

        /// <summary>
        /// Renders a board with the current grid states, reflecting the 
        /// simulation every turn.
        /// </summary>
        /// <param name="grid">States grid.</param>
        public void RenderGrid(Grid grid)
        {
            // Top grid border - white.
            // (Max+2 to give space to the grid inside)
            Console.WriteLine("");
            for (int top = 0; top < grid.Max+2; top++)
            {
                Console.Write($"{whiteBG}{black}//{reset}");
            }
            Console.WriteLine("");

            // Cycles the grid matrix.
            for (int i = 0; i < grid.Max; i++)
            {
                for (int j = 0; j < grid.Max; j++)
                {

                    // Left grid border - white.
                    if (j == 0)
                    {
                        Console.Write($"{whiteBG}{black}//{reset}");
                    }

                    // Renders every grid State.
                    switch (grid.GetState(i, j))
                    {
                        case State.Null:
                            // Renders a black square.
                            Console.Write($"{blackBG}{black}[]{reset}");
                            break;

                        case State.Healthy:
                            // Renders a green square.
                            Console.Write($"{greenBG}{black}[]{reset}");
                            break;

                        case State.Infected:
                            // Renders a yellow square.
                            Console.Write($"{yellowBG}{black}[]{reset}");
                            break;

                        case State.Dead:
                            // Renders a red square.
                            Console.Write($"{redBG}[]{reset}");
                            break;
                    }
                }
                
                // Right grid border - white.
                Console.WriteLine($"{whiteBG}{black}//{reset}");
            }
            
            // Bottom grid border - white.
            for (int bottom = 0; bottom < grid.Max+2; bottom++)
            {
                Console.Write($"{whiteBG}{black}//{reset}");
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Improvised console clear for GitBash.
        /// </summary>
        public void Clear()
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }
    }
}