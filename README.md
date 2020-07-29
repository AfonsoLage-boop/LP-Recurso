
# Virus Simulation - Social Distance Edition

Resolução do projeto de recurso de LP1 2019/2020

## Sumário

Este [repositório] é composto pelos seguintes elementos:

* O código C# que compõe o nosso programa;
* A divisão do projeto pelos membros do grupo;
* A arquitetura do programa;
* Diagrama UML;
* Referências.
  
### Divisão de trabalho no projeto

|Afonso                 |André                  |
|:---------------------:|:---------------------:|
|States (Base)          |                       |
|Criação da Grid        |                       |
|Sistema de Coordenadas |                       |
|Criação de Agentes     |                       |
|Contagem de Agentes    |                       |
|Movimentação (Base)    |                       |
|Vida de agentes        |                       |
|README                 |                       |

## Arquitetura da solução

### Descrição da Solução

O programa inicia no método ``Main()``, dentro da classe ``[Program]``. Dentro do
 ``Main()``começa-se por criar uma instância de ``[UserInterface]`` e de
 ``[Variables]`` que ficam disponíveis através das propriedades estáticas UI e v
 , respetivamente. De seguida tratam-se dos argumentos através da do método
 ``ValidadeVars()`` que consta na classe ``[Variables]``. Caso os argumentos
 sejam inválidos o método ``InsufArgsMsg()`` corre e aparece uma mensagem de
 erro, caso contrário o programa inicia correndo o método ``Start()`` que se
 encontra na classe ``[Simulation]``.  


### Diagrama UML

![UML](UML.png "UML")

## Referências

* [Zombies vs Humanos - Nuno Fachada]
* [Wait one second in running program - Stackoverflow]
* [Int32.TryParse Method - Microsoft Docs]
* [ANSI colors - Haoyi's Programming Blog]
* [String.Contains Method - Microsoft Docs]
  
## Metadados

* Autores: [Afonso Lage (a21901381)], [André Santos (a21901767)]
  
* Professor: [Nuno Fachada]

* Curso: [Licenciatura em Videojogos]

* Universidade: [Universidade Lusófona de Humanidades e Tecnologias][ULHT]

[Program]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Program.cs
[UserInterface]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/UserInterface.cs
[Variables]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Variables.cs
[Simulation]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Simulation.cs
[Agent]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Agent.cs
[Coords]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Coords.cs
[Grid]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Grid.cs
[State]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/State.cs
[repositório]: https://github.com/AfonsoLage-boop/LP-Recurso
[Zombies vs Humanos - Nuno Fachada]:https://github.com/VideojogosLusofona/lp1_2018_p2_solucao
[Wait one second in running program - Stackoverflow]:https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
[Int32.TryParse Method - Microsoft docs]:https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=netcore-3.1
[ANSI colors - Haoyi's Programming Blog]:https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html#decorations
[String.Contains Method - Microsoft docs]:https://docs.microsoft.com/pt-br/dotnet/api/system.string.contains?view=netcore-3.1
[Afonso Lage (a21901381)]:https://github.com/AfonsoLage-boop
[André Santos (a21901767)]:https://github.com/andrepucas
[Nuno Fachada]:https://github.com/fakenmc
[Licenciatura em Videojogos]:https://www.ulusofona.pt/licenciatura/videojogos
[ULHT]:https://www.ulusofona.pt/