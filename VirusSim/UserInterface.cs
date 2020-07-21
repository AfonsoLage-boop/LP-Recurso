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
            Console.WriteLine("\nPara iniciar a simulacao e obrigatorio " +
            "indicar: (seguido de um valor inteiro separado por espaco)");
            Console.WriteLine("-N    => dimensoes da grelha;");
            Console.WriteLine("-M    => agentes iniciais;");
            Console.WriteLine("-L    => vida de cada agente (em turnos)");
            Console.WriteLine("-Tinf => turno da primeira infecao");
            Console.WriteLine("-T    => duracao da simulacao (em turnos)");
            Console.WriteLine("\nOpcionalmente:");
            Console.WriteLine("-v    => ativar visualizacao em consola");
            Console.WriteLine("-o    => gravar estatisticas num ficheiro, " + 
            "indicando o mesmo a frente");
            

        }
    }
}