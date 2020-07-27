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
        private Direction dir;
        private Coords pos;
        private Agent ag;
        
        
        public Simulation(Variables v)
        {
            // Copies all variable values.
            this.v = v;

            // Instantiates the UI.
            ui = new UserInterface();

            // Creates a grid with the user given size (N x N).
            grid = new Grid((int)v.Size, (int)v.Size);

            // Creates an array with the size of the agents (M).
            allAgents = new Agent[v.Agents];

            // Instantiates the random number generator.
            rand = new Random();

            // Instantiates the directions.
            dir = new Direction();

            // Creates the agents.
            for (int i = 1; i <= v.Agents; i++)
            {
                CreateAgent((int)i);
            }
        }

        public void Start()
        {

            foreach (Agent agent in allAgents)//////////////////////////////////
            {///////////////////////////////////////////////////////////////////
                Console.WriteLine($"{agent}");//////////////////////////////////
            }///////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////

            // Current simulation turn.
            int currentTurn = 1;

            //Alive agents counter.
            int countAlive = 1;

            // Randomly decides which agent will be infected first.
            int randomAgentID = rand.Next(1, v.Agents);

            ////////////////////////////////////////////////////////////////////
            Console.WriteLine($"(D) RandomAgentID = {randomAgentID}");//////////
            ////////////////////////////////////////////////////////////////////

            // All the simulation data is queued to be exported in the end.
            Queue<string> data = new Queue<string>();

            // First message, presents number of agents.
            ui.Start((int)v.Size, (int)v.Agents);

            // Game Loop, ends when the user's set number of turns is reached
            // or if all the simulation agents die.
            while (currentTurn <= v.Turns && countAlive > 0)
            {
                // Cycles through all the agents.
                foreach (Agent agent in allAgents)
                {
                    // If the current turn equals to the user's set infection 
                    // turn, one of the healthy agents is randomly infected.
                    if (currentTurn == v.TInfect)
                    {
                        // Checks if the current agent is the one who was 
                        // randomly picked to be infected earlier.
                        if (randomAgentID == agent.ID) 
                        {
                            // Changes the agent state to infected.
                            agent.Infect();
                            
                            ////////////////////////////////////////////////////
                            Console.WriteLine($"\n(D) Someone is infected:");///
                            foreach (Agent ag in allAgents)/////////////////////
                            {///////////////////////////////////////////////////
                                Console.WriteLine($"{ag}");/////////////////////
                            }///////////////////////////////////////////////////
                            Console.WriteLine();////////////////////////////////
                            ////////////////////////////////////////////////////
                        }
                    }
                }


                // Moves every agent that is alive.
                ag.AgentWalk(pos, dir);

                // In each grid position, if one agent is infected, all other
                // agents in this position also become infected.


                // Count Healthy, Infected and Dead agents.
                CountAgents(out int healthyAgents, out int infectedAgents, 
                out int deadAgents);
                //Count alive agents.
                countAlive = healthyAgents + infectedAgents;


                // If v.Save == True, info is saved to be exported.
                if (v.Save)
                {
                    // Queue organized data from current turn in a line
                    data.Enqueue(DataLine(healthyAgents, infectedAgents, 
                        deadAgents));
                }


                // Shows current turn stats
                ui.ShowStats(currentTurn, healthyAgents, infectedAgents,
                    deadAgents);

                // If v.View == True, update display

                // Increase current turn value by one
                currentTurn++;
            }   

            // If v.Save == True, export all data to a TSV file
            if (v.Save)
            {
                // Output file for saved data
                string dataFile = "simulationData.tsv";

                // Info is written in the file.
                File.WriteAllLines(dataFile, data);

                // Confirmation message.
                Console.WriteLine("\n<simulationData.tsv> data file exported");
            }
        }

        private void CreateAgent(int id)
        {
            Coords pos;
            Agent agent;

            // Gives it a random position within the grid limits.
            pos   = new Coords(rand.Next((int)v.Size), rand.Next((int)v.Size));

            // Passes an ID, the random position and grid reference.
            agent = new Agent(id, pos, grid);

            // Add agent to the allAgents array.
            // ([id-1] because allAgents = [0,19] while IDs = [1,20])
            allAgents[id-1] = agent;
        }

        private string DataLine(int countHealthy, int countInfected, 
            int countDead)
        {
            // Separates all data with tabs.
            string data = $"{countHealthy}" + "\t" + $"{countInfected}" + "\t" +
                $"{countDead}";

            return data;
        }

        private void CountAgents( out int healthyAgents,out int infectedAgents,
            out int deadAgents)
        {
            healthyAgents = 0;
            infectedAgents = 0;
            deadAgents = 0;

            foreach (Agent agent in allAgents)
            {
                if (agent.State == State.Healthy)
                {
                    healthyAgents++;
                }
                else if(agent.State == State.Infected)
                {
                    infectedAgents++;
                }
                else
                {
                    deadAgents++;
                }
            }
        }

    }
}