using System;

namespace VirusSim
{
    public class UserInterface
    {
        string red      = "\u001b[31m";
        string redBG    = "\u001b[41m";
        string green    = "\u001b[32m";
        string greenBG  = "\u001b[42m";
        string yellow   = "\u001b[33m";
        string yellowBG = "\u001b[43m";
        string whiteBG  = "\u001b[47m";
        string reset    = "\u001b[0m";
        string bold     = "\u001b[1m";

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
                $"{yellow} -Tinf{reset} > first infected (turn)");
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

        public void StartMsg(int size, int agents)
        {
            Console.WriteLine("\nSIMULATION BEGINS");
            Console.WriteLine($"{size} X {size} Test area");
            Console.WriteLine($"{agents} Healthy agents\n");
        }

        public void ShowStats(int turn, int healthy, int infected, int dead)
        {
            string t = $"{bold}{turn,-3}{reset}";
            string h = $"{green}{bold}{healthy,3}{reset}";
            string i = $"{yellow}{bold}{infected,3}{reset}";
            string d = $"{red}{bold}{dead,3}{reset}";

            Console.WriteLine($"Turn {t} >> {h} healthy | {i} infected | " +
                $"{d} dead");
        }

        public void RenderGrid(Grid grid)
        {
            Console.WriteLine("");
            for (int top = 0; top < grid.Max+2; top++)
            {
                Console.Write($"{whiteBG}  {reset}");
            }
            Console.WriteLine("");

            for (int i = 0; i < grid.Max; i++)
            {
                for (int j = 0; j < grid.Max; j++)
                {

                    if (j == 0)
                    {
                        Console.Write($"{whiteBG}  {reset}");
                    }

                    switch (grid.GetState(i, j))
                    {
                        case State.Null:
                            Console.Write($"  ");
                            break;

                        case State.Healthy:
                            Console.Write($"{greenBG}  {reset}");
                            break;

                        case State.Infected:
                            Console.Write($"{yellowBG}  {reset}");
                            break;

                        case State.Dead:
                            Console.Write($"{redBG}  {reset}");
                            break;
                    }
                }
                Console.WriteLine($"{whiteBG}  {reset}");
            }
            for (int bottom = 0; bottom < grid.Max+2; bottom++)
            {
                Console.Write($"{whiteBG}  {reset}");
            }
            Console.WriteLine("");
        }

        public void Clear()
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }
    }
}