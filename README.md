# 1º Projeto de Inteligência Artificial

###### Projeto realizado por:
Joana Marques a21701929 <p>
Pedro Santos a21702907

##### Link do repositório GitHub : https://github.com/pedrosantosobral/AI_Projeto1

### Divisão de tarefas:
Pedro: <p>
- Criou a pasta de projeto inicial; <p>
- Alterou o script NavAgentBehavior adicionando variáveis para definir a
stamina e health e velocidade dos agentes e seus métodos para as alterar, variáveis 
de referência para os diferentes locais no mapa; <p>
- Fez os estados de mudança de palcos e respetivos métodos intermédios; <p>
- Adicionou transições entre estados em movimento; <p>
- Corrigiu bug da escolha de espaços verdes; <p>
- Fez os agentes espalharem-se pelas zonas de descanso <p>
- Fez a parte inicial da introdução; <p>
- Fez o comportamento das explosões e da fuga e morte dos agentes; <p>
- Adicionou o pânico entre os agentes e suas melhorias de performance; <p>
- Pesquisou sobre o estudo de Sagun et al; <p>


Joana: <p>
- Fez o layout com o bake da navmesh; <p>
- Fez os limitadores das variáveis; <p>
- Fez os estados para comer e descansar e respetivos métodos intermédios; <p>
- Fez o processo de escolha dos espaços verdes; <p>
- Fez o estado de panico e respetivas transições; <p>
- Adicionou a instaciação de uma explosão num local aleatório; <p>
- Comentou algumas classes; <p> 
- Fez a UI para contar o número de agentes mortos;
- Fez a implementação da saída extra;
- Fez a introdução e contextualização à pesquisa sobre simulação baseada em agentes; <p>
- Pesquisou sobre o estudo de Wagner e Agrawal e sobre o estudo de Ren et al; <p>
- Fez os agradecimentos; <p>




### Introdução:
Para este projeto foi-nos proposto fazer um simulador de multidões de um festival
de música em larga escala. <p>
Cada agente tem de ter um comportamento próprio tentando assemelhar-se ao máximo 
a uma situação real de festival e as suas reações a certos eventos. <p>
Para resolver este problema usámos Finite State Machines de forma a dar diferentes 
estados aos agentes. Associados a estes estados temos transições entre eles. <p>
Um dos principais objetivos deste projeto é conseguir implementar as states
machines de forma a ter as reações dos agentes a certos acontecimentos.
Ao chegar ao fim do projeto, o nosso objetivo é conseguir implementar tudo aquilo
que pretendemos e obter os comportamentos mais realistas possiveis. <p>

Para além de fazer este simulador, foi-nos pedido também uma pesquisa sobre este
tipo de simulações. <p>

Para evitar acontecimentos trágicos é muito importante planear e preparar para
qualquer situação de desastre. Em situações de pânico, as pessoas têm tendência
a pensar apenas em si e nos seus interesses, querem-se salvar sem se preocupar
com os outros e isto pode levar muitas vezes a desfechos graves. Por ser tão
difícil e caro simular e reproduzir todos os cenários de perigo com pessoas reais,
são feitos estudos com agentes virtuais para imitar os eventuais comportamentos
de multidões podendo assim criar estratégias para evitar um desfecho mais grave
em situações de perigo. <p>
A inteligência artificial é amplamente utilizado para imitar parte do 
comportamento humano em computador, como redes neurais e
sistemas especializados (Sagun et al (2011)).
Modelação de agentes é a melhor técnica para simulação de múltiplos sistemas de
objetos porque consegue capturar dinâmicas altamente complexas que são comuns no
mundo real (Borshchev e Filippov, 2004) e então observá-las. Esta técnica é uma 
abordagem em que cada entidade do sistema que é modelado é exclusivamente 
representada como um agente independente no que respeita a tomadas de 
decisão (Fachada et al, 2015).
O comportamento global do sistema é o resultado de relacionamentos simples e
auto-organizados entre os agentes(Fachada, 2008). <p>
Muita densidade de pessoas e poucas saídas é uma das características comuns dos
locais de concertos e a sua combinação é um problema para a segurança das pessoas
(Wagner & Agrawal, 2014). <p>
Há alguns toolkits que são usados para estas implementações de simulaçoes 
baseadas em agentes que são: Swarm, Repast (Recursive Porous Agent
Simulation Toolkit), MASON (Multi-Agent Simulator of Neighborhoods) e NetLogo.
Estes toolkits têm ferramentas para fazer o design de agentes e dos ambientes em
que eles interagem. <p>
Já há vários estudos que envolvem modelos de agentes para simulações de multidões
e geralmente recaem em 3 categorias: 1- evacuação de multidões de edifícios,
2- evacuações de ruas, 3- comportamentos de multidões durante uma evacuação. <p>

**Sagun et al** <p>

No estudo realizado por Sagun et al (2011) são identificados problemas de projeto
de edifícios associados a emergências para melhorar a segurança durante eventos
extremos. <p>
O objetivo desta pesquisa é aumentar a segurança através de um design aprimorado
do ambiente construído.<p>
Mas qual foi o papel da simulação em computadores? <p>
As simulações em computador podem ajudar no desenvolvimento de diretrizes de
projeto de construção que considerem os principais fatores (como número
de rotas e portas, distâncias de viagem, dimensão das rotas e portas, etc.).
Nos primeiros estudos de fluxo de multidões, foram consideradas a capacidade de
carga dos elementos do edifício, taxas de fluxo e tipos de rotas de saída,
com foco na largura e capacidade das saídas. No entanto, é necessário focar na 
compreensão das relações entre pessoas e o ambiente construído (Sagun et al 2011).


**Wagner e Agrawal** <p>

No modelo de Wagner e Agrawal é usada uma abordagem em que os agentes individuais 
e autónomos interagem entre si e com o ambiente. Os agentes retratam pessoas que 
são colocadas ao pé dos palcos, caminhos e outros lugares e que se tentam mover 
rapidamente para uma saída enquanto evitam um ou mais fogos. <p>
O modelo tem 3 componentes mais importantes: o ambiente, as dinâmicas do fogo e 
o movimento das pessoas. <p>
O ambiente, como já foi dito anteriormente é bastante customizável, podendo
decidir onde são os palcos, saídas e entradas e ainda os lugares sentados de
um auditório de concertos. <p>
A cada fogo pode ser especificado o seu raio de propagação e produção de fumo.
Os agentes podem-se aleijar ficando queimados com o fogo ou por inalação de fumo. 
Estes dados são então guardados. <p>
Em relação ao movimento dos agentes, este têm apenas um objetivo, sair do recinto 
sem passar pelo fogo. Assim, estes movimentos consistem em 3 componentes: seleção 
da saída, mover-se do seu lugar para um caminho e mover-se pelo caminho até à saída 
mais próxima. <p>
Para o estudo efetuado por Wagner e Agrawal, foram realizadas duas experiências,
uma para um auditório e outro para um estádio.
O porpósito desde sistema é para permitir a criação de multiplos cenários para
testes e avaliação de medidas de segurança para minimizar os riscos no caso
de um incêndio. <p>

**Ren et al** <p>

O modelo de Ren et al. (2009) usa agentes para simular uma evacuação na ocorrência
de uma explosão. Consideraram 5 hipóteses ou factos que são considerados para um
incêndio:
* Em situações de uma evacuação de emergência, as pessoas estão em pânico e
nervosas por isso tendem a ter comportamentos irracionais.
* As pessoas tentam mover-se o mais rápido que conseguem o que é muito mais
rápido que o normal.
* O fumo provoca uma dificuldade em respirar e dores nos olhos. Quando o fogo fica
mais denso é então muito dificil ou mesmo impossóvel ver por onde se anda.
* Começam-se a empurrar e são empurradas pelos outros.
* As pessoas que vão a correr para as saídas começam a tropeçar nas pessoas que já
cairam e por isso a multidão abranda.
Neste modelo é tido em conta diversos tipos de pessoas (homens, mulheres, crianças,
seguranças e pessoas responsáveis pelas evacuações) e também são tidas em conta
vários atributos dos agentes (idade, velocidade, escala de pânico, etc) o que não 
é visto nos modelos de simulação mais tradicionais.



### Metodologia:
A simulação foi implementada em 2.5D e os agentes têm movimento dinâmico fazendo
uso de navmesh para os seus movimentos no recinto. <p>
Para definir e criar os seus diferentes estados usámos Finite State Machines que 
é das técnicas mais usadas em IA para jogos e que nos permite criar estados para 
os agentes. Após a criação destes estados, a cada agente atribuiu-se um estado 
atual e este vai-se ligar aos restantes estados por transições também criadas 
por nós. Criámos inicialmente criámos os estados: ver palcos, ir para os palcos, 
ter fome, ir comer, estar cansado e ir descansar. <p>
Os agentes começam a simulação com valores aleatórios de fome (health), cansaço
(stamina) e preferência entre palcos. Desta maneira cada agente vai ter
comportamentos variados logo no início da simulação. <p>
Como num concerto normal,
os agentes quando entram no recinto dirigem-se maioritariamente para os palcos.
Ao longo do tempo vão trocando de palcos se a sua health ou stamina não forem 0. <p>
O fluxograma mostra o funcionamento da mudança entre palcos. <p>
![StagesSwitch](Stage.svg)

### Resultados e discussão:

### Conclusões:

### Agradecimentos:
Queremos agradecer ao professor Nuno Fachada por nos ter disponibilizado a biblioteca 
com o código das Finite State Machines e pela ajuda dada na resolução de alguns 
problemas que tivemos durante a implementação. <p>
Queremos agradecer também ao nosso colega Rui Martins que nos ajudou a implementar 
a dispersão dos agentes nas zonas de descanso e nos disponibilizou um método de 
extensão da classe Bounds. <p>

### Referências:
* Usámos a biblioteca de FSMs criada pelo professor Nuno Fachada
* Usámos um método de extensão da classe Bounds disponibilizado por Rui Martins
* Wagner, N. e Agrawal, V. (2014). An agent-based simulation system for concert
venue crowd evacuation modeling in the presence of a fire disaster.
* Ren C., Yang C., Jin S. (2009) Agent-Based Modeling and Simulation on
Emergency Evacuation. In: Zhou J. (eds) Complex Sciences. Complex 2009.
Lecture Notes of the Institute for Computer Sciences, Social Informatics and
Telecommunications Engineering, vol 5. Springer, Berlin, Heidelberg
https://doi.org/10.1007/978-3-642-02469-6_25
* Fachada N, Lopes VV, Martins RC, Rosa AC. 2015. Towards a standard model
for research in agent-based modeling and simulation. PeerJ Computer Science
1:e36 https://doi.org/10.7717/peerj-cs.36
* Sagun A., Bouchlaghem D., Anumba C. (2011) Computer simulations vs. building 
guidance to enhance evacuation performance of buildings during emergency events. 
https://doi.org/10.1016/j.simpat.2010.12.001
