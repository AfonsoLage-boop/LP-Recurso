using System;
using System.IO;
using System.Collections.Generic;

namespace VirusSim
{
    public class Simulation
    {
        private Variables v;
        private UserInterface ui;
        private Grid grid;
        private Agent[] allAgents;
        private Random rand;
        
        public Simulation(Variables v)
        {
            this.v    = v;
            ui        = new UserInterface();
            grid      = new Grid((int)v.Size, (int)v.Size);
            
            allAgents = new Agent[v.Agents];
            rand      = new Random();

            for (int i = 1; i <= v.Agents; i++)
            {
                CreateAgent((int)i);
            }
        }

        public void Start()
        {
            // DEBUG BLOCK
            int countAlive    = v.Agents;
            int countHealthy  = v.Agents;
            int countInfected = 0;
            int countDead     = 0;
            foreach (Agent agent in allAgents)
            {
                Console.WriteLine($"{agent}");
            }

            // Current simulation turn
            int currentTurn = 1;

            // Where all the simulation data is queued to be exported in the end
            Queue<string> data = new Queue<string>();

            // First message, presents number of agents
            ui.Start((int)v.Size, (int)v.Agents);

            // Game Loop, ends when the user's set number of turns is reached
            // or if all the simulation agents die.
            while (currentTurn <= v.Turns && countAlive > 0)
            {
                // If the current turn equals to the user's set infection turn,
                // one of the healthy agents is randomly infected.
                if (currentTurn == v.TInfect)
                {
                    
                }

                // Move every agent that is alive


                // In each grid position, if one agent is infected, all other
                // agents in this position also become infected


                // Count Healthy, Infected and Dead agents
                // If v.Save == True, info is saved to be exported



                countHealthy  = countHealthy - 2;  ///////////////
                countInfected = countInfected + 2; //  TESTING  //
                countDead     = countDead + 1;     // VARIABLES //
                countAlive    = countAlive - 1;    ///////////////

                if (v.Save)
                {
                    // Queue organized data from current turn in a line
                    data.Enqueue(DataLine(countHealthy, countInfected, 
                                          countDead));
                }


                // Show current turn stats
                // If v.View == True, update display
                ui.ShowStats(currentTurn, countHealthy, countInfected,
                             countDead);

                // Increase current turn value by one
                currentTurn++;
            }

            // If v.Save == True, export all data to a TSV file
            if (v.Save)
            {
                // Output file for saved data
                string dataFile = "simulationData.tsv";
                File.WriteAllLines(dataFile, data);
                Console.WriteLine("\n// File Exported");
            }
        }

        private void CreateAgent(int id)
        {
            Coords pos;
            Agent agent;

            pos   = new Coords(rand.Next((int)v.Size), rand.Next((int)v.Size));

            agent = new Agent(id, pos, grid);

            allAgents[id-1] = agent;
        }

        // Separates and returns each turn's data with tabs
        private string DataLine(int countHealthy, int countInfected, 
                                int countDead)
        {
            string data;

            data = $"{countHealthy}" + "\t" + $"{countInfected}" + "\t" + 
                   $"{countDead}";

            return data;
        }
    }
}