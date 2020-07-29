using System;
using System.IO;
using System.Collections.Generic;

namespace VirusSim
{
    /// <summary>
    /// <c>Simulation</c> Class.
    /// Contains the simulation loop and other auxiliar simulation methods.
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// Simulation variables.
        /// </summary>
        private Variables v;

        /// <summary>
        /// Simulation interface.
        /// </summary>
        private UserInterface ui;

        /// <summary>
        /// Reference to the grid where the simulation will be visualized.
        /// </summary>
        private Grid grid;

        /// <summary>
        /// Array with all simulation agents.
        /// </summary>
        private Agent[] allAgents;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private Random rand;
        
        /// <summary>
        /// Constructor method, instantiates a new simulation.
        /// </summary>
        /// <param name="v">Simulation variables.</param>
        public Simulation(Variables v)
        {
            // Saves all variable values.
            this.v = v;

            // Instantiates the UI.
            ui = new UserInterface();

            // Instantiates a grid with the user's given grid dimensions.
            grid = new Grid((int)v.Size);

            // Instantiates an array with the size of the agents (M).
            allAgents = new Agent[v.Agents];

            // Instantiates the random number generator.
            rand = new Random();

            // Loops as many times as the number of agents that the user set.
            for (int i = 1; i <= v.Agents; i++)
            {
                // Creates an agent in each iteration.
                CreateAgent((int)i, (int)v.AgentsHP);
            }
        }

        /// <summary>
        /// Simulation starts here.
        /// </summary>
        public void Start()
        {
            // Controls the simulation loop.
            bool endSimulation = false;

            // Current simulation turn.
            int currentTurn = 1;

            // Randomly decides which agent will be infected first.
            int randomAgentID = rand.Next(1, v.Agents);

            // Instantiates a queue to save simulation data and export later.
            Queue<string> data = new Queue<string>();

            // First message, presents number of agents.
            ui.StartMsg((int)v.Size, (int)v.Agents);

            // Waits 2 seconds.
            System.Threading.Thread.Sleep(2000);

            // Game Loop, runs while endSimulation = false, checked every turn.
            while (!endSimulation)
            {
                // Cycles through all the agents.
                foreach (Agent agent in allAgents)
                {
                    // Checks if the currentTurn is the infection Turn and if
                    // the current agent is who should be infected.
                    if (currentTurn == v.TInfect && randomAgentID == agent.ID)
                    {
                        // If so, infects said agent.
                        agent.Infect();
                    }

                    // Checks if agent is Infected, minus in the infection Turn.
                    if (agent.State == State.Infected && 
                        currentTurn != v.TInfect) 
                    {
                        // If so, removes 1HP from the agent.
                        agent.HP -= 1; 
                    }

                    // Removes agents who died the previous round from grid.
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
                // Both of these following methods are outside of the agents  //
                // loop so that the states they update (infected and dead)    //
                // can overlap, respectively, other grid states in the same   //
                // position, allowing for a more accurate grid view.          //
                ////////////////////////////////////////////////////////////////

                // If one agent is infected, all healthy ones in his
                // position also become infected.
                SpreadInfection();

                // Kills all agents with 0 HP remaining.
                KillAgents();

                // Count current Healthy, Infected and Dead agents.
                CountAgents(out int cHealthy, out int cInfected, out int cDead);

                // If v.Export == True, turn data is queued for later.
                if (v.Export) data.Enqueue(DataLine(cHealthy, cInfected, cDead));
                
                // If v.View == True, updates display.
                if (v.View)
                {
                    // Waits .8 seconds.
                    System.Threading.Thread.Sleep(800);

                    // Improvises a console clear for Git Bash.
                    ui.Clear();

                    // Shows line stats.
                    ui.ShowStats(currentTurn, cHealthy, cInfected, cDead);
                    
                    // Renders the grid.
                    ui.RenderGrid(grid);
                }
                // Only shows line stats.
                else ui.ShowStats(currentTurn, cHealthy, cInfected, cDead);

                // Check if simulation can end.
                endSimulation = IsOver(currentTurn , cHealthy, cInfected);

                // Increases current turn by one.
                currentTurn++;
            }

            // If v.Export == True, exports all data to a TSV file
            if (v.Export)
            {
                // Info is written in the file.
                File.WriteAllLines(v.File, data);

                // Confirmation message.
                ui.Exported(v.File);
            }
        }

        /// <summary>
        /// Creates a new agent.
        /// </summary>
        /// <param name="id">Agent's ID.</param>
        /// <param name="hp">Health value of agent.</param>
        private void CreateAgent(int id, int hp)
        {
            // Agent's starting position.
            Coords pos;

            // Agent Class reference so that a new one can be created.
            Agent agent;

            // Within the grid limits, agent gets a random starting position.
            pos   = new Coords(rand.Next((int)v.Size), rand.Next((int)v.Size));

            // Creates an agent with an unique ID, health value, random
            // position and grid reference.
            agent = new Agent(id, hp, pos, grid);

            // Add agent to the allAgents array.
            // ([id-1] because allAgents begins at 0 while IDs begin at 1.
            allAgents[id-1] = agent;
        }

        /// <summary>
        /// Spreads the infection from one infected agent to all others in his 
        /// position.
        /// </summary>
        private void SpreadInfection()
        {
            // Cycles through all agents.
            foreach (Agent agent in allAgents)
            {
                // Agent is infected?
                if (agent.State == State.Infected)
                {
                    // If yes, contaminate others.
                    agent.Contaminate(allAgents);
                }
            }
        }

        /// <summary>
        /// Kills agents if they run out of HP.
        /// </summary>
        private void KillAgents()
        {
            // Cycles through all agents.
            foreach (Agent agent in allAgents)
            {
                // If agent has 0 HP, he dies.
                if (agent.HP == 0) agent.Die();
            }
        }

        /// <summary>
        /// Counts all agents' states.
        /// </summary>
        /// <param name="cHealthy">Out parameter, saves the number of healthy 
        /// agents.</param>
        /// <param name="cInfected">Out parameter, saves the number of infected 
        /// agents.</param>
        /// <param name="cDead">Out parameter, saves the number of dead agents.
        /// </param>
        private void CountAgents(out int cHealthy, out int cInfected, 
            out int cDead)
        {
            // All parameters start at 0.
            cHealthy  = 0;
            cInfected = 0;
            cDead     = 0;

            // Cycles through all agents.
            foreach (Agent agent in allAgents)
            {
                // Agent is Healthy, increases respective count.
                if (agent.State == State.Healthy) cHealthy++;
                
                // Agent is Infected, increases respective count.
                else if(agent.State == State.Infected) cInfected++;

                // Agent is Dead, increases respective count.
                else cDead++;
            }
        }

        /// <summary>
        /// Organizes each turn's data.
        /// </summary>
        /// <param name="cHealthy">Number of healthy agents.</param>
        /// <param name="cInfected">Number of infected agents.</param>
        /// <param name="cDead">Number of dead agents.</param>
        /// <returns>A single line with data separated by tabs.</returns>
        private string DataLine(int cHealthy, int cInfected, int cDead)
        {
            // Separates all data with tabs and saves it in data string.
            string data = $"{cHealthy}" + "\t" + $"{cInfected}" + "\t" +
                $"{cDead}";

            // Returns the data string.
            return data;
        }

        /// <summary>
        /// Checks if simulation can end.
        /// </summary>
        /// <param name="turn">Current simulation turn.</param>
        /// <param name="healthy">Number of healthy agents.</param>
        /// <param name="infected">Number of infected agents.</param>
        /// <returns><c>True</c> if either the number of turns given by the 
        /// user is reached, all agents die or the infection dies.</returns>
        private bool IsOver(int turn, int healthy, int infected)
        {
            // Turn limit reached.
            if (turn == v.Turns)
            {
                ui.MaxTurnsMsg(turn);
                return true;
            }

            // All agents are dead.
            else if (healthy + infected == 0)
            {
                ui.AllDeadMsg(turn, v.Agents);
                return true;
            }

            // After the first infection round, there are no more infected 
            // agents to spread the virus.
            else if (turn > 5 && infected == 0)
            {
                ui.InfectEndMsg(turn, healthy);
                return true;
            }

            // If neither of the above check out.
            return false;
        }
    }
}