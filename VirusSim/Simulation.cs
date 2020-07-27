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
            // Copies all variable values.
            this.v = v;

            // Instantiates the UI.
            ui = new UserInterface();

            // Creates a grid with the user given size (N x N).
            grid = new Grid((int)v.Size);

            // Creates an array with the size of the agents (M).
            allAgents = new Agent[v.Agents];

            // Instantiates the random number generator.
            rand = new Random();

            // Creates the agents.
            for (int i = 1; i <= v.Agents; i++)
            {
                CreateAgent((int)i, (int)v.AgentsHP);
            }
        }

        public void Start()
        {
            // Current simulation turn.
            int currentTurn = 1;

            // Randomly decides which agent will be infected first.
            int randomAgentID = rand.Next(1, v.Agents);

            // If all agents are dead.
            bool allDead = false;

            // All the simulation data is queued to be exported in the end.
            Queue<string> data = new Queue<string>();

            // First message, presents number of agents.
            ui.StartMsg((int)v.Size, (int)v.Agents);

            // Game Loop, ends when the user's set number of turns is reached
            // or if all the simulation agents die.
            while (currentTurn <= v.Turns && !allDead)
            {
                // Cycles through all the agents.
                foreach (Agent agent in allAgents)
                {
                    // Removes 1HP from every infected agent.
                    if (agent.State == State.Infected) agent.HP -= 1; 

                    // Kills all agents with 0 HP remaining.
                    if (agent.HP == 0) agent.Die();

                    // If the current turn equals to the user's set infection 
                    // turn, one of the healthy agents is randomly infected.
                    if (currentTurn == v.TInfect)
                    {
                        // Checks if the current agent is the one who was 
                        // randomly picked to be infected earlier.
                        if (randomAgentID == agent.ID) 
                        {
                            // Updates the agent state to infected.
                            agent.Infect();
                        }
                    }

                    // Moves every agent that is alive in a random direction.
                    if (agent.State == State.Healthy || 
                        agent.State == State.Infected)
                    {
                        int random = rand.Next(7);
                        agent.Move(random);
                    }

                    // In each grid position, if one agent is infected, all 
                    // other agents in this position also become infected.


                    
                }

                // Count Healthy, Infected and Dead agents.
                CountAgents(out int countHealthy, out int countInfected, 
                    out int countDead);

                // Check if all agents are dead.
                if (countDead == v.Agents) allDead = true;

                // If v.Save == True, data is queued in a line for later.
                if (v.Save) data.Enqueue(DataLine(countHealthy, countInfected, 
                    countDead));

                // If v.View == True, updates display.
                if (v.View)
                {
                    // Waits a second.
                    System.Threading.Thread.Sleep(1000);

                    // Improvised console clear for Git Bash.
                    ui.Clear();

////////////////////////////////////////////////////////////////////////////////
                    foreach (Agent agent in allAgents)
                    {
                        Console.WriteLine($"{agent}");
                    }
                    Console.WriteLine("");
////////////////////////////////////////////////////////////////////////////////

                    // Shows line stats.
                    ui.ShowStats(currentTurn, countHealthy, countInfected,
                        countDead);
                    
                    // Renders the grid.
                    ui.RenderGrid(grid);
                }

                // Only shows line stats.
                else ui.ShowStats(currentTurn, countHealthy, countInfected,
                    countDead);

                // Increase current turn by one.
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

        private void CreateAgent(int id, int hp)
        {
            Coords pos;
            Agent agent;

            // Gives it a random position within the grid limits.
            pos   = new Coords(rand.Next((int)v.Size), rand.Next((int)v.Size));

            // Passes an ID, the random position and grid reference.
            agent = new Agent(id, hp, pos, grid);

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

        private void CountAgents(out int countHealthy, out int countInfected, 
            out int countDead)
        {
            countHealthy  = 0;
            countInfected = 0;
            countDead     = 0;

            foreach (Agent agent in allAgents)
            {
                if (agent.State == State.Healthy) countHealthy++;
                
                else if(agent.State == State.Infected) countInfected++;

                else countDead++;
            }
        }
    }
}