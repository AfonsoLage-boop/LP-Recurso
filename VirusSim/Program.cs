namespace VirusSim
{
    /// <summary>
    /// <c>Program</c> Class.
    /// Contains <see cref="Main()"> method, so the program starts here.
    /// /// </summary>
    class Program
    {
        /// <summary>
        /// Receives command line arguments that control the simulation and 
        /// redirects user to the simulation.
        /// </summary>
        /// <param name="args">Command line arguments. All are required for the 
        /// program to run minus the last 2 of this following list:
        /// <list type="bullet">
        /// <item><term>-N</c></term>
        /// <description>Simulation grid dimensions (N x N).</description></item>
        /// </item>
        /// <item><term>-M</c></term>
        /// <description>Number of agents.</description>
        /// </item>
        /// <item><term>-M</c></term>
        /// <description>Number of agents.</description>
        /// </item>
        /// <item><term>-L</c></term>
        /// <description>Agents' health (in turns).</description>
        /// </item>
        /// <item><term>-Tinf</c></term>
        /// <description>First infected (turn).</description>
        /// </item>
        /// <item><term>-T</c></term>
        /// <description>Number of total turns.</description>
        /// </item>
        /// <item><term>-v</c></term>
        /// <description>View live simulation.</description>
        /// </item>
        /// <item><term>-o</c></term>
        /// <description>Export data to a .tsv file.</description>
        /// </item></list>
        /// <example>Input example: 
        /// <c>dotnet run -- -N 20 -M 300 -L 10 -Tinf 5 -T 100 -v -o stats.tsv
        /// </c></example></param>
        static void Main(string[] args)
        {
            // Instantiates UserInterface so thats its methods are accessible.
            UserInterface ui = new UserInterface();

            // Instantiates a new set of Variables, which will save handle
            // and save all arguments passed here.
            Variables v = new Variables();

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
                ui.InsufArgsMsg();
            }
        }
    }
}
