using System;
using System.IO;
using System.Collections.Generic;

namespace VirusSim
{
    public class Simulation
    {
        private Grid grid;
        private UserInterface ui;
        private Variables v;
        
        public Simulation(Variables v)
        {
            this.v = v;
            grid = new Grid(v);
            ui   = new UserInterface();
        }

        public void Start()
        {
            // DEBUG
            Console.WriteLine("\nSimulation Started.");
            Console.WriteLine($"Size           = {v.Size}");
            Console.WriteLine($"Agents         = {v.Agents}");
            Console.WriteLine($"AgentsHP       = {v.AgentsHP}");
            Console.WriteLine($"Infection Turn = {v.TInfect}");
            Console.WriteLine($"Turns          = {v.Turns}");
            Console.WriteLine($"View           = {v.View}");
            Console.WriteLine($"Save           = {v.Save}\n");

            // Simulation Data
            int countAlive    = v.Agents;
            int countHealthy  = v.Agents;
            int countInfected = 0;
            int countDead     = 0;
            
            // Current simulation turn
            int currentTurn = 1;

            // Where all the simulation data is queued to be exported in the end
            Queue<string> data = new Queue<string>();

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

                // DEBUG
                countHealthy = countHealthy - 2;
                countInfected = countInfected + 2;
                countDead = countDead + 1;
                countAlive = countAlive - 1;

                if (v.Save)
                {
                    string allCounts = $"{countHealthy}" + "\t" + 
                        $"{countInfected}" + "\t" + $"{countDead}";
                    data.Enqueue(allCounts);
                }


                // Show current turn stats
                // If v.View == True, update display



                // DEBUG LINES //
                Console.WriteLine($"TURN: {currentTurn} \t| ALIVE: {countAlive}"
                + $"\tHEALTHY: {countHealthy}\tINFECTED: {countInfected}" +
                $"\tDEAD: {countDead}");

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
    }
}