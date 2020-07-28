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
            // Controls the simulation loop.
            bool endSimulation = false;

            // Current simulation turn.
            int currentTurn = 1;

            // Randomly decides which agent will be infected first.
            int randomAgentID = rand.Next(1, v.Agents);

            // All the simulation data is queued to be exported in the end.
            Queue<string> data = new Queue<string>();

            // First message, presents number of agents.
            ui.StartMsg((int)v.Size, (int)v.Agents);

            // Game Loop, runs while endSimulation returns false, checked in
            // the end of every turn.
            while (!endSimulation)
            {
                // Cycles through all the agents.
                foreach (Agent agent in allAgents)
                {
                    // Checks if the currentTurn is the infection Turn and if
                    // the current agent is who should be infected.
                    if (currentTurn == v.TInfect && randomAgentID == agent.ID)
                    {
                        agent.Infect();
                    }

                    // Removes 1HP if agent is Infected.
                    if (agent.State == State.Infected && 
                        currentTurn !=v.TInfect) 
                    {
                        agent.HP -= 1; 
                    }

                    // Remove Dead agents from previous round from grid.
                    if (agent.State == State.Dead) agent.Remove();

                    // Moves every agent that is alive in a random direction.
                    if (agent.State == State.Healthy ||
                        agent.State == State.Infected)
                    {
                        int random = rand.Next(7);
                        agent.Move(random, allAgents);
                    }
                }

                ////////////////////////////////////////////////////////////////
                // Both of these methods are out of the agents loop above so  //
                // that the states they update (infected and dead) can        //
                // overlap, respectively, other grid states in the same       //
                // position, allowing a more accurate grid view.              //
                                                                              //
                // If one agent is infected, all healthy ones in his          //
                // position also become infected.                             //
                SpreadInfection();                                            //
                                                                              //
                // Kills all agents with 0 HP remaining.                      //
                KillAgents();                                                 //
                                                                              //
                ////////////////////////////////////////////////////////////////

                // Count Healthy, Infected and Dead agents.
                CountAgents(out int cHealthy, out int cInfected, out int cDead);

                // If v.Save == True, data in a line is queued for later.
                if (v.Save) data.Enqueue(DataLine(cHealthy, cInfected, cDead));
                
                // If v.View == True, updates display.
                if (v.View)
                {
                    // Waits a second.
                    System.Threading.Thread.Sleep(800);

                    // Improvised console clear for Git Bash.
                    ui.Clear();

                    // Show line stats.
                    ui.ShowStats(currentTurn, cHealthy, cInfected, cDead);
                    
                    // Renders the grid.
                    ui.RenderGrid(grid);
                }
                // Only show line stats.
                else ui.ShowStats(currentTurn, cHealthy, cInfected, cDead);

                // Check if simulation can end.
                endSimulation = IsOver(currentTurn - 1, cHealthy, cInfected);

                // Increase current turn by one.
                currentTurn++;
            }

            // If v.Save == True, export all data to a TSV file
            if (v.Save)
            {
                // Info is written in the file.
                File.WriteAllLines(v.File, data);

                // Confirmation message.
                Console.WriteLine($"\n// Data exported to {v.File}.");
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

        private void SpreadInfection()
        {
            foreach (Agent agent in allAgents)
            {
                if (agent.State == State.Infected)
                {
                    agent.Contaminate(allAgents);
                }
            }
        }

        private void KillAgents()
        {
            foreach (Agent agent in allAgents)
            {
                if (agent.HP == 0) agent.Die();
            }
        }

        private void CountAgents(out int cHealthy, out int cInfected, 
            out int cDead)
        {
            cHealthy  = 0;
            cInfected = 0;
            cDead     = 0;

            foreach (Agent agent in allAgents)
            {
                if (agent.State == State.Healthy) cHealthy++;
                
                else if(agent.State == State.Infected) cInfected++;

                else cDead++;
            }
        }

        private string DataLine(int cHealthy, int cInfected, int cDead)
        {
            // Separates all data with tabs.
            string data = $"{cHealthy}" + "\t" + $"{cInfected}" + "\t" +
                $"{cDead}";

            return data;
        }

        private bool IsOver(int turn, int healthy, int infected)
        {
            if (turn == v.Turns)
            {
                ui.MaxTurnsMsg(turn);
                return true;
            }

            else if (healthy + infected == 0)
            {
                ui.AllDeadMsg(turn, v.Agents);
                return true;
            }

            else if (turn > 5 && infected == 0)
            {
                ui.InfectEndMsg(turn, healthy);
                return true;
            }
            return false;
        }
    }
}