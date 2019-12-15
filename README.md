# 1º Projeto de Inteligência Artificial

###### Projeto realizado por:
Joana Marques a21701929 <p>
Pedro Santos a21702907

##### Link do repositório GitHub : https://github.com/pedrosantosobral/AI_Projeto1

### Divisão de tarefas:
Pedro: <p>
- Criou a pasta de projeto inicial; <p>
- Alterou o script NavAgentBehavior adicionando variáveis para definir a
stamina e health e velocidade dos agentes e seus métodos para as alterar, variáveis de referência para os diferentes locais no mapa; <p>
- Fez os estados de mudança de palcos e respetivos métodos intermédios; <p>
- Adicionou transições entre estados em movimento; <p>
- Corrigiu bug da escolha de espaços verdes; <p>
- Fez os agentes espalharem-se pelas zonas de descanso <p>
- Fez a parte inicial da introdução; <p>


Joana: <p>
- Fez o layout com o bake da navmesh; <p>
- Fez os limitadores das variáveis; <p>
- Fez os estados para comer e descansar e respetivos métodos intermédios; <p>
- Fez o processo de escolha dos espaços verdes; <p>
- Fez o estado de panico e respetivas transições; <p>
- Adicionou a instaciação de uma explosão num local aleatório; <p>
- Comentou algumas classes; <p> 
- Fez a introdução e contextualização à pesquisa sobre simulação baseada em agentes; <p>
- Fez os agradecimentos; <p>
- Pesquisou sobre o estudo de Wagner e Agrawal; <p>



### Introdução:
Para este projeto foi-nos proposto fazer um simulador de multidões de um festival
de música em larga escala. Cada agente tem de ter um comportamento próprio
tentando assemelhar-se ao máximo a uma situação real de festival e as suas
reações a certos eventos. Para resolver este problema usámos Finite State Machines
de forma a dar diferentes estados aos agentes. Associados a estes estados temos
transições entre eles.
Um dos principais objetivos deste projeto é conseguir implementar as states
machines de forma a ter as reações dos agentes a certos acontecimentos.
Ao chegar ao fim do projeto, o nosso objetivo é conseguir implementar tudo aquilo
que pretendemos e obter os comportamentos mais realistas possiveis.
Para além de fazer este simulador, foi-nos pedido também uma pesquisa sobre este
tipo de simulações. 

Para evitar acontecimentos trágicos é muito importante planear e preparar para
qualquer situação de desastre. Em situações de pânico, as pessoas têm tendência
a pensar apenas em si e nos seus interesses, querem-se salvar sem se preocupar
com os outros e isto pode levar muitas vezes a desfechos graves. Por ser tão
difícil e caro simular e reproduzir todos os cenários de perigo com pessoas reais,
são feitos estudos com agentes virtuais para imitar os eventuais comportamentos
de multidões podendo assim criar estratégias para evitar um desfecho mais grave
em situações de perigo. 
Modelação de agentes é a melhor técnica para simulação de múltiplos sistemas de
objetos porque consegue capturar dinâmicas altamente complexas que são comuns no
mundo real (Borshchev e Filippov, 2004). Esta técnica é uma abordagem em que cada
entidade do sistema que é modelado é exclusivamente representada como um agente
independente no que respeita a tomadas de decisão(Fachada et al, 2015).
O comportamento global do sistema é o resultado de relacionamentos simples e
auto-organizados entre os agentes(Fachada, 2008).
Muita densidade de pessoas e poucas saídas é uma das características comuns dos
locais de concertos e a sua combinação é um problema para a segurança das pessoas
(Wagner & Agrawla, 2014). Há alguns toolkits que são usados para estas implementações
de simulaçoes baseadas em agentes que são: Swarm, Repast (Recursive Porous Agent
Simulation Toolkit), MASON (Multi-Agent Simulator of Neighborhoods) e NetLogo.
Estes toolkits têm ferramentas para fazer o design de agentes e dos ambientes em
que eles interagem.
Já há vários estudos que envolvem modelos de agentes para simulações de multidões
e geralmente recaem em 3 categorias: 1- evacuação de multidões de edifícios,
2- evacuações de ruas, 3- comportamentos de multidões durante uma evacuação. <p>

No modelo de Wagner e Agrawal é usada uma abordagem em que os agentes individuais e autónomos interagem entre si e com o ambiente. Os agentes retratam pessoas que são colocadas ao pé dos palcos, caminhos e outros lugares e que se tentam mover rapidamente para uma saída enquanto evitam um ou mais fogos. <p>
O modelo tem 3 componentes mais importantes: o ambiente, as dinâmicas do fogo e o movimento das pessoas. <p>
O ambiente, como já foi dito anteriormente é bastante customizável, podendo
decidir onde são os palcos, saídas e entradas e ainda os lugares sentados de
um auditório de concertos. <p>
A cada fogo pode ser especificado o seu raio de propagação e produção de fumo.
Os agentes podem-se aleijar ficando queimados com o fogo ou por inalação de fumo. Estes dados são então guardados. <p>
Em relação ao movimento dos agentes, este têm apenas um objetivo, sair do recinto sem passar pelo fogo. Assim, estes movimentos consistem em 3 componentes: seleção da saída, mover-se do seu lugar para um caminho e mover-se pelo caminho até à saída mais próxima. <p>
Para o estudo efetuado por Wagner e Agrawal, foram realizadas duas experiências,
uma para um auditório e outro para um estádio.
O porpósito desde sistema é para permitir a criação de multiplos cenários para
testes e avaliação de medidas de segurança para minimizar os riscos no caso
de um incêndio. <p>



### Metodologia:

### Resultados e discussão:

### Conclusões:

### Agradecimentos:
Queremos agradecer ao professor Nuno Fachada por nos ter disponibilizado a biblioteca com o código das Finite State Machines e pela ajuda dada na resolução de alguns problemas que tivemos durante a implementação. <p>
Queremos agradecer também ao nosso colega Rui Martins que nos ajudou a implementar a dispersão dos agentes nas zonas de descanso e nos disponibilizou um método de extensão da classe Bounds. <p>

### Referências:
* Usámos a biblioteca de FSMs criada pelo professor Nuno Fachada
* Usámos um método de extensão da classe Bounds disponibilizado por Rui Martins
