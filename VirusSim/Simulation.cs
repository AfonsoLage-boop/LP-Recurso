using System;

namespace VirusSim
{
    public class Simulation
    {
        private Grid grid;
        private UserInterface ui;
        private Variables v;

        private Agent agent;
        
        public Simulation(Variables v)
        {
            this.v = v;
            grid = new Grid(v);
            ui   = new UserInterface();
            agent = new Agent(v);
        }

        public void Start()
        {
            //DEBUG
            Console.WriteLine("\nSimulation Started.");
            Console.WriteLine($"Size           = {v.Size}");
            Console.WriteLine($"Agents         = {v.Agents}");
            Console.WriteLine($"AgentsHP       = {v.AgentsHP}");
            Console.WriteLine($"Infection Turn = {v.TInfect}");
            Console.WriteLine($"Turns          = {v.Turns}");
            Console.WriteLine($"View           = {v.View}");
            Console.WriteLine($"Save           = {v.Save}\n\n");
            agent.Agents();
            // Debug variable. agentsAlive will have to be a list of some sort
            int agentsAlive = v.Agents;
            
            // Saves the current simulation turn
            int currentTurn = 1;

            // Game Loop, ends when the user's set number of turns is reached
            // or if all the simulation agents die.
            while (currentTurn <= v.Turns && agentsAlive > 0)
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


                // Show current turn stats
                // If v.View == True, update display


                // DEBUG LINES
                Console.WriteLine($"Current Turn: {currentTurn}");
                

                // Increase current turn value by one
                currentTurn++;
            }
        }
    }
}