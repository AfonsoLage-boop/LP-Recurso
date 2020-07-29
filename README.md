# Virus Simulation - Social Distancing Edition

Resolução do projeto de recurso de LP1 2019/2020

## Sumário

Este [repositório] é composto pelos seguintes elementos:

* Código C# que compõe o nosso programa.
* Divisão do projeto pelos membros do grupo.
* Arquitetura do programa.
* Diagrama UML.
* Referências.
  
### Divisão de trabalho no projeto

|Afonso                 |André                  |
|:---------------------:|:---------------------:|
|States (Base)          |Variables.cs           |
|Criação da Grid        |Simulation.cs (Base)   |
|Sistema de Coordenadas |Exporting data file    |
|Criação de Agentes     |UserInterface.cs       |
|Contagem de Agentes    |Agent.cs Rework        |
|Movimentação (Base)    |Render live Simulation |
|Vida de agentes        |Agents moving in grid  |
|README                 |XML Documentation      |

## Arquitetura da solução

### Descrição da Solução

O programa inicia no método `Main()`, dentro da classe [`Program`]. Aqui,
 são instanciados o [`UserInterface`] da simulação juntamente com
 um novo conjunto de [`Variables`]. De seguida, os argumentos são enviados
 para o método `ValidadeVars()` da struct [`Variables`], onde são verificados.
 Caso os argumentos não sejam válidos, é renderizada uma mensagem de erro que
 explica ao utilizador o que pode ter corrido mal e o programam termina. Se os
 argumentos forem válidos, então estes são guardados na forma de propriedades
 dentro de [`Variables`]e o método retorna **True**, que consequentemente
 cria uma nova instância da classe [`Simulation`] e invoca o método `Start()`,
 que dá [inicio à simulação](#funcionamento-de-cada-turno).

Quando [`Simulation`], a classe central do programa, é instanciada esta faz uma
 copia do conjunto de [`Variables`] para uma instância local, e instância ainda:

* O [`UserInterface`] - *ui*.
* Uma matriz [`Grid`] com as dimensões dadas pelo utilizador - *grid*.
* Um array [`Agent`] com o numéro de agentes dados pelo utilizador - *allAgents*.
* Um gerador de números `Random` - *rand*.
* Tantas instâncias de [`Agent`] como o utilizador pediu.

#### Método Start()

`Start()` começa por declarar um booleano *endSimulation* que controla a duração
 da simulação, em turnos. Tais também são controlados por uma variável,
 *currentTurn* que começa em 1 e é incrementada no final de todos os turnos.

*randomAgentID* é uma variável usada para determinar qual dos agentes existentes
 será o primeiro infetado quando chegar o turno de infeção, este é determinado
 aleatoriamente através de `Random` que vai gerar um número entre 1 e o número
 de agentes.

Para gravar os dados da simulação é criada uma `Queue` que vai guardando a
 informação de cada turno e exporta tudo para um ficheiro no final. Esta opção
 é opcional e só é executada se o utilizador o escolher, correndo o programa
 com a opção `-o`. O ficheiro para onde a informação é, por predefinição
 *simulationData.tsv*. Contudo este pode ser alterado se o utilizador colocar um
 nome de ficheiro válido à frente de `-o`. Estes detalhes são tratados na
 classe [`Variables`] logo no inicio.

Depois de renderizar uma mensagem a partir do [`UserInterface`] que confirma que
 a simulação começou, o programa espera 2 segundos e entra finalmente no loop
 principal da simulação, um ciclo while que corre enquanto *endSimulation*
 retornar **False**. Cada ciclo corresponde a um turno da simulação.

#### Classe Agent

Todas as instâncias de [`Agent`] são criadas com um *id* (único a cada um), um
 valor de *hp*, uma *pos* random em [`Coords`], um [`State`] que por default
 é *Healthy* e uma referência à [`Grid`]. Estes valores são todos guardados em
 propriedades auto implementadas da classe.

Para além disto, a classe também é responsável por atualizar os [`State`],
 **HP** e [`Coords`] de cada um dos seus [`Agent`] e de seguida, enviar as
 mudanças para a [`Grid`] para serem atualizados la.

#### Classe Grid

Como está mencionado em cima, [`Grid`] recebe apenas as dimensões que o
 utilizador definiu anteriormente e esta (apenas uma dimensão porque N x N) é
 guardada na forma de propriedade.

Para criar a grid em si, optámos por instanciar uma matriz de [`State`] onde
 cada posição na matriz corresponde ao [`State`] do [`Agent`] que se encontrar
 na mesma. Se houver mais do que um agente na mesma posição, então
 o [`State`] nessa posição da grid será o mais importante para a visualização
 da mesma. Este era um problema recorrente sempre que os agents se mexiam.

Como solução, no método `MoveAgents()` criou-se um loop que passa por todos os
 agents que se encontram na posição de partida do [`Agent`] (que se vai mover
 para uma nova posição) e guarda a existência de outros [`State`] presentes
 nessa posição em booleanos. Depois de passar por todos, é então atribuído um
 [`State`] a essa posição na grid com base na prioridade de visualização:

```Dead > Infected > Healthy > Null```

### Funcionamento de cada turno

* O array *allAgents* onde estão todos os [`Agent`] é percorrido.
  * Se for o turno de infeção, infeta o [`Agent`] com **ID** igual a
  *randomAgentID*.

  ```c#
  agent.Infect() // Atualiza o estado deste Agent e da sua posição na Grid
                 // para 'Infected'.
  ```

  * Remove 1 HP a todos os agentes Infetados (exceção no turno de infeção).
  
  ```c#
  agent.HP -= 1  // Remove um valor da propriedade HP do objeto Agent.
  ```

  * Remove quaisquer agentes que tenham morrido no turno passado da [`Grid`]
  
  ```c#
  agent.Remove() // Atualiza o estado deste Agent e da sua posição na Grid
                 // para 'Null'.
  ```

  * Movimenta todos os agentes vivos numa direção aleatória ([tendo em atenção
     todos os outros agentes](#classe-grid)).

  ```c#
  agent.Move(random, allAgents) // Este método vai verificar se o movimento
                                // é válido. Se não for, tenta outros até
                                // encontrar um e atualiza a sua posição
                                // interna e na Grid.
  ```

* Noutro ciclo, percorre todos os agents 'Infected' e infeta todos os agentes
 que se encontrem nas suas posições.

 ```c#
 agent.Contaminate(allAgents) // Ciclo interno que compara as posições dos
                              // outros agentes e, se for igual, os seus
                              // States passam a 'Infected' internamente e na
                              // Grid.
 ```

* Mata todos os agentes com 0 HP, também noutro ciclo, para que primeiro se
 atualize na Grid os 'Healthy', seguidos dos 'Infected' e agora 'Dead'.

 ```c#
 agent.Die() // Ciclo interno que procura quais agentes têm HP = 0 e muda os
             // seus States internos e na Grid para 'Dead'.
 ```

* Conta todos os [`State`] do turno.
* Se o utilizador correu o programa com `-o`, então esta informação é colocada
 na `Queue` na forma de uma linha com toda a informação separada por tabs.
* Se o utilizador correu o programa com `-v`, então o display é atualizado.
 Isto é feito pelo [`UserInterface`] que pega em todos os [`State`] da [`Grid`]
 atualizados e renderiza todas as posições da matriz
  
  * `'Healthy' - verde`
  * `'Infected' - amarelo`
  * `'Dead' - vermelho`

* Caso o utilizador não tenha esta ultima opção de visualização, é apenas
 mostrada uma linha com as contagens do turno atual.

* A simulação verifica se pode acabar retornado um booleano a *endSimulation*
 através do método:

 ```c#
 // Retorna true se:
 // o limite de turnos foi alcançado.
 // todos os agentes morreram.
 // o virus morreu.
 endSimulation = IsOver(currentTurn, cHealthy, cInfected)
 ```

* Se **False**, é incrementado *currentTurn* e o ciclo repete-se
* Se **True**, é renderizada uma mensagem a explicar o porquê de ter acabado
 (uma das 3 razões acima), toda a informação que tem sido guardada na `Queue`
 até esse ponto é exportada para um ficheiro .tsv e o programa termina.

### Diagrama UML

![UML](UML.png "UML")

## Referências

* [Zombies vs Humanos - Nuno Fachada]
* [Wait one second in running program - StackOverflow]
* [Int32.TryParse Method - Microsoft Docs]
* [ANSI colors - Haoyi's Programming Blog]
* [String.Contains Method - Microsoft Docs]

## Metadados

* Autores: [Afonso Lage (a21901381)], [André Santos (a21901767)]

* Professor: [Nuno Fachada]

* Curso: [Licenciatura em Videojogos]

* Universidade: [Universidade Lusófona de Humanidades e Tecnologias][ULHT]

[`Program`]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Program.cs
[`UserInterface`]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/UserInterface.cs
[`Variables`]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Variables.cs
[`Simulation`]: https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Simulation.cs
[`Agent`]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Agent.cs
[`Coords`]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Coords.cs
[`Grid`]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/Grid.cs
[`State`]:https://github.com/AfonsoLage-boop/LP-Recurso/blob/master/VirusSim/State.cs
[repositório]: https://github.com/AfonsoLage-boop/LP-Recurso
[Zombies vs Humanos - Nuno Fachada]:https://github.com/VideojogosLusofona/lp1_2018_p2_solucao
[Wait one second in running program - StackOverflow]:https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
[Int32.TryParse Method - Microsoft docs]:https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=netcore-3.1
[ANSI colors - Haoyi's Programming Blog]:https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html#decorations
[String.Contains Method - Microsoft docs]:https://docs.microsoft.com/pt-br/dotnet/api/system.string.contains?view=netcore-3.1
[Afonso Lage (a21901381)]:https://github.com/AfonsoLage-boop
[André Santos (a21901767)]:https://github.com/andrepucas
[Nuno Fachada]:https://github.com/fakenmc
[Licenciatura em Videojogos]:https://www.ulusofona.pt/licenciatura/videojogos
[ULHT]:https://www.ulusofona.pt/
