using System;

namespace VirusSim
{
    public class UserInterface
    {
        public void InsufArgs()
        {
            Console.WriteLine("\n!! ERRO !!");
            Console.WriteLine("Argumentos Insuficientes passados na linha de"  +
            " comando.");
            Console.WriteLine("\nPara iniciar a simulacao sao obrigatorios " +
            "indicar:");
            Console.WriteLine("-N    => dimensoes da grelha;");
            Console.WriteLine("-M    => agentes iniciais;");
            Console.WriteLine("-L    => vida de cada agente (em turnos)");
            Console.WriteLine("-Tinf => turno da primeira infecao");
            Console.WriteLine("-T    => duracao da simulacao (em turnos)");
            Console.WriteLine("(Seguidos de um valor inteiro maior do que 0 e" +
            " separado por espaco)");
            Console.WriteLine("\nOpcionalmente:");
            Console.WriteLine("-v    => ativar visualizacao em consola");
            Console.WriteLine("-o    => gravar estatisticas num ficheiro, " + 
            "indicando o mesmo a frente");
            
        }

        public void DrawGrid(Grid grid)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    if (j < 2) Console.Write("|");

                }
                if (i < 2) Console.WriteLine("\n---+---+---");
            }
            Console.WriteLine();
        }


    }
}