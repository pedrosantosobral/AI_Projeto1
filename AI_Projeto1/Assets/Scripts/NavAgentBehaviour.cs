using System;
using System.Collections;
using System.Collections.Generic;
using URandom = UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;
/// <summary>
/// Class with every agent behavior and stats. Agent unic stats are created and the
/// state machine is updated
/// </summary>
public class NavAgentBehaviour : MonoBehaviour
{
    /// <summary>
    /// Collision layer mask
    /// </summary>
    public LayerMask collisionLayer;
    /// <summary>
    /// Multiplier of agent panic
    /// </summary>
    public float agentPanicMultiplier;

    /// <summary>
    /// Agent panic max radius
    /// </summary>
    public float agentPanicMaxRadius;

    /// <summary>
    /// Initial agent panic size
    /// </summary>
    private float _panicSize = 1;
    
    /// <summary>
    /// Bool to check if agent is stunned
    /// </summary>
    private bool _isStunned;

    /// <summary>
    /// Agent stun total time
    /// </summary>
    public float stunnedTime;

    /// <summary>
    /// Reference to the table manager
    /// </summary>
    private TableManager _tableManagerReference;

    /// <summary>
    /// Reference to the green spaces manager
    /// </summary>
    private GreenSpaceManager _greenManagerReference;
    
    /// <summary>
    /// Reference to the extra exit manager 
    /// </summary>
    private ExitManager _extraExitManagerReference;
    
    /// <summary>
    /// Requested table gameobject copy
    /// </summary>
    private GameObject   _requestedTableReference;
    
    /// <summary>
    /// Requested green space gameobject copy
    /// </summary>
    private GameObject   _requestedGreenReference;

    /// <summary>
    /// Bool to block agent from requesting more than one table at a time
    /// </summary>
    private bool         _requestedTableOnce;

    /// <summary>
    /// Bool to block agent from requesting more than one green space at a time
    /// </summary>
    private bool         _requestedGreenOnce;
    

    /// <summary>
    /// Agent speed variable
    /// </summary>
    private float        _agentSpeed;

    /// <summary>
    /// Initial state variable to be set at the beginning
    /// </summary>
    State initialState;

    /// <summary>
    /// Bool to check if agent arrived to food location
    /// </summary>
    private bool _arriveFood;

    /// <summary>
    /// Bool to check if agent arrived to green space location
    /// </summary>
    private bool _arriveChill;

    /// <summary>
    /// Bool to check if agent arrived to stage 1 location
    /// </summary>
    private bool _arriveStage1;

    /// <summary>
    /// Bool to check if agent arrived to stage 2 location
    /// </summary>
    private bool _arriveStage2;

    /// <summary>
    /// bool to set the agent on panic
    /// </summary>
    public bool isPanicking;

    /// <summary>
    /// Stage 1 total watch time
    /// </summary>
    private float _stage1TotalTime;

    /// <summary>
    /// Stage 1 timer decrementer
    /// </summary>
    private float _stage1LoseTime;

    /// <summary>
    /// Stage 1 actual watch time
    /// </summary>
    private float _stage1Time;

    /// <summary>
    /// Stage 2 total watch time
    /// </summary>
    private float _stage2TotalTime;

    /// <summary>
    /// Stage 2 timer decrementer
    /// </summary>
    private float _stage2LoseTime;

    /// <summary>
    /// Stage 2 actual watch time
    /// </summary>
    private float _stage2Time;

    /// <summary>
    /// Actual agent health
    /// </summary>
    private float _health;

    /// <summary>
    /// Agent max health
    /// </summary>
    private float _maxHealth;
    
    /// <summary>
    /// Agent starting health 
    /// </summary>
    private float _startingHealth;

    /// <summary>
    /// Agent health lose speed
    /// </summary>
    private float _healthLoseSpeed;

    /// <summary>
    /// Agent health gain speed
    /// </summary>
    private float _healthGainSpeed;

    /// <summary>
    /// Actual agent stamina
    /// </summary>
    private float _stamina;

    /// <summary>
    /// Agent max stamina
    /// </summary
    private float _maxStamina;

    /// <summary>
    /// Agent starting stamina 
    /// </summary>
    private float _startingStamina;

    /// <summary>
    /// Agent stamina lose speed
    /// </summary>
    private float _staminaLoseSpeed;

    /// <summary>
    /// Agent stamina gain speed
    /// </summary>
    private float _staminaGainSpeed;

    //References to locations start at null
    private GameObject _stage1     = null;
    private GameObject _stage2     = null;
    private GameObject _exit1      = null;
    private GameObject _chillZone1 = null;
    private GameObject _chillZone2 = null;
    private GameObject _chillZone3 = null;

    //references to green spaces bounds, to get random positions
    private Bounds _bounds;
    private Bounds _bounds1;
    private Bounds _bounds2;

    // Reference to the NavMeshAgent component
    private NavMeshAgent _agent;

    //Reference to state machine
    private StateMachine stateMachine;
    /// <summary>
    /// Method to initialize important variables, get the diferent locations references,
    /// create agent unic stats and state machine.
    /// </summary>
    private void Start()
    {
        //start with no panic
        isPanicking = false;

        //start with no requested table and green space
        _requestedTableOnce = false;
        _requestedGreenOnce = false;

        //get reference of table manager
        _tableManagerReference = GameObject.Find("TableManager").GetComponent<TableManager>();

        //get reference of green space
        _greenManagerReference = GameObject.Find("GreenSpaceManager").GetComponent<GreenSpaceManager>();

        //get reference to the extra exit manager
        _extraExitManagerReference = GameObject.Find("ExitManager").GetComponent<ExitManager>();

        //Generate unique agent stats
        GenerateAIAgentStats();
        //Create the state machine
        CreateFSM();

        // Get reference to the NavMeshAgent component
        _agent = GetComponent<NavMeshAgent>();
        
        //get reference to locations
        _chillZone1 = GameObject.Find("ChillZone1");
        _chillZone2 = GameObject.Find("ChillZone2");
        _chillZone3 = GameObject.Find("ChillZone3");
        _exit1 = GameObject.Find("Exit");
        _bounds = _chillZone3.GetComponent<Collider>().bounds;
        _bounds1 = _chillZone1.GetComponent<Collider>().bounds;
        _bounds2 = _chillZone2.GetComponent<Collider>().bounds;
    }
    /// <summary>
    /// Method to update the state machine
    /// </summary>
    private void Update()
    {
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    /// <summary>
    /// Check if agent reached the diferent destinations and update the variables to make it change states
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //check if agent reached diferent destinations, change states while not in panic

        if(isPanicking == false)
        {
            //if agent reached food
            if (other.CompareTag("Food"))
            {
                _arriveFood = true;
            }
            //if agent reached green space 1
            if (other.CompareTag("Rest1"))
            {
                //get random position on green zone
                _agent.destination = _bounds1.RandomPositionInBounds(useY: false);
                _arriveChill = true;
            }
            //if agent reached green space 2
            if (other.CompareTag("Rest2"))
            {
                //get random position on green zone
                _agent.destination = _bounds2.RandomPositionInBounds(useY: false);
                _arriveChill = true;
            }
            //if agent reached green space 3
            if (other.CompareTag("RestSquare"))
            {
                //get random position on green zone
                _agent.destination = _bounds.RandomPositionInBounds(useY: false);
                _arriveChill = true;
            }
            //if agent reached stage 1
            if (other.CompareTag("Stage1"))
            {
                _arriveStage1 = true;
            }
            //if agent reached stage 2
            if (other.CompareTag("Stage2"))
            {
                _arriveStage2 = true;
            }

            //if agent enters in panic radius
            if (other.CompareTag("Panic"))
            {
                //enter in panic
                isPanicking = true;
            }

            //if agent enters in stun radius
            if (other.CompareTag("Stun"))
            {
                //stun agent for some time
                _isStunned = true;
            }
        }
    }

   /// <summary>
   /// Check if agent left the rest locations and reset variables to allow the next interation with that place 
   /// the next time the agent arrives there
   /// </summary>
   /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //if agent left any of the green spaces
        if (other.CompareTag("Rest1") || other.CompareTag("RestSquare") || other.CompareTag("Rest2"))
        {
            //reset allow green space once, allow agent to request green space again
            _requestedGreenOnce = false;
        }
    }



    /// <summary>
    /// Method to decrease agent Stamina
    /// </summary>
    private void DecreaseStamina()
    {
        //decrement stamina and limit it to 0 in the end
        _stamina -= _staminaLoseSpeed * Time.fixedDeltaTime;
        if (_stamina <= 0)
        {
            _stamina = 0;
        }
    }

    /// <summary>
    /// Method to decrease agent Health
    /// </summary>
    private void DecreaseHealth()
    {
        //decrement health and limit it to 0 in the end
        _health -= _healthLoseSpeed * Time.fixedDeltaTime;
        if (_health <= 0)
        {
            _health = 0;
        }
    }

    /// <summary>
    /// Method to increase agent stamina
    /// </summary>
    private void IncreaseStamina()
    {
        //increase stamina and limit it if reached the max
        _stamina += _staminaGainSpeed * Time.fixedDeltaTime;
        if (_stamina >= _maxStamina)
        {
            _stamina = _maxStamina;
        }
        //reset arrive to green zone checker variable
        _arriveChill = false;
    }

   /// <summary>
   /// Method to increase agent health
   /// </summary>
    private void IncreaseHealth()
    {
        //increase health and limit it if reached the max
        _health += _healthGainSpeed * Time.fixedDeltaTime;
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        //reset arrive to food table checker variable
        _arriveFood = false;
    }

    /// <summary>
    /// Method to do all resting actions
    /// </summary>
    private void RestingActions()
    {
        //Increase stamina
        IncreaseStamina();
        //Decrease health
        DecreaseHealth();
    }

    /// <summary>
    /// Method to do all eating actions
    /// </summary>
    private void EatingActions()
    {
        //Increase health
        IncreaseHealth();
        //Decrease stamina
        DecreaseStamina();
    }

    /// <summary>
    /// Method to do all the go eat actions
    /// </summary>
    private void GoEatActions()
    {
        //reset agent default speed
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        //if agent is allowed to request table
        if(_requestedTableOnce == false)
        {
            //request table
            RequestTable();
            //block agent from requesting more tables
            _requestedTableOnce = true;
        }
        //Move to food location 
        MoveToFood();

    }
    /// <summary>
    /// Method to do all go rest actions 
    /// </summary>
    private void GoRestActions()
    {
        //reset agent speed
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        //if agent is allowed to request a green space
        if (_requestedGreenOnce == false)
        {
            //request green space
            RequestGreenSpace();
            //block agent from requesting more green spaces
            _requestedGreenOnce = true;
        }
        //Move to green space location
        MoveToRest();

    }

    /// <summary>
    /// Method to do all the going stage 1 actions
    /// </summary>
    private void GoStage1Actions()
    {
        //reset agent speed to default speed
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        //Move to stage 1 location
        MoveToStage1();

    }
    /// <summary>
    /// Method to do all going stage 2 actions
    /// </summary>
    private void GoStage2Actions()
    {
        //reset aagent speed to default speed
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        //Move to stage 2 location
        MoveToStage2();

    }

    /// <summary>
    /// Method to do all watching stage 1 actions
    /// </summary>
    private void WatchingStage1Actions()
    {
        //decrement agent speed until it is 0
        _agent.speed -= Time.deltaTime;
        //reset arrive stage 1 variable for the next time
        _arriveStage1 = false;
        DecreaseHealth();
        DecreaseStamina();
        //Reset Stage 2 Watching time
        _stage2Time = _stage2TotalTime;
        //Decrement stage 1 actual watching time and limit it if its 0
        _stage1Time -= _stage1LoseTime * Time.deltaTime;
        if (_stage1Time <= 0)
        {
            _stage1Time = 0;
        }
    }
    /// <summary>
    /// Method to do all watching stage 2 actions
    /// </summary>
    private void WatchingStage2Actions()
    {   
        //decrement agent speed until it is 0
        _agent.speed -= Time.deltaTime;
        //reset arrive stage 2 variable for the next time
        _arriveStage2 = false;
        DecreaseHealth();
        DecreaseStamina();
        //Reset Stage 1 Watching time
        _stage1Time = _stage1TotalTime;
        //Decrement stage 2 actual watching time and limit it if its 0
        _stage2Time -= _stage2LoseTime * Time.deltaTime;
        if (_stage2Time <= 0)
        {
            _stage2Time = 0;
        }
    }
    /// <summary>
    /// Method to do all the panic actions
    /// </summary>
    private void PanicActions()
    {
        //change agent layer
        gameObject.layer = 0;
        //Increase agent panic propagation radius if it is not at ther max
        if(_panicSize <= agentPanicMaxRadius)
        {
            _panicSize += _panicSize * agentPanicMultiplier;
        }

        //Put the other agents in panic
        //Cast a sphere to spread panic to other agents
        Collider[] array = Physics.OverlapSphere(gameObject.transform.position, _panicSize, collisionLayer);
       //Foreach agent that collided with this agent, set them in panic
        foreach (Collider i in array)
        {
            //Set agent in panic
            i.gameObject.GetComponent<NavAgentBehaviour>().isPanicking = true;
        }

        //If agent is on explosion sun radius stun it
        if (_isStunned == true)
        {
            //Stun agent for some time
            StunAgentForSomeTime();
        }
        //else 
        else
        {

            //double the agent speed
            _agent.speed = _agentSpeed * 2;
        }

        //If there is an extra exit
        if (_extraExitManagerReference.extraExit == true)
        {
            //check for the closest exit and move there
            if (Vector3.Distance(gameObject.transform.position,_exit1.transform.position) <
                Vector3.Distance(gameObject.transform.position, _extraExitManagerReference.exit.transform.position))
            {
                //set agent destination
                _agent.destination = _exit1.transform.position;
            }
            //else move to the the other exit
            else
            {
                //set agent destination
                _agent.destination = _extraExitManagerReference.exit.transform.position;
            }
        }
        //if there is only one exit move there
        else
        {
            //set agent destination
            _agent.destination = _exit1.transform.position;
        }  
   
    }
    /// <summary>
    /// Method to generate all unique stats for the agent
    /// </summary>
    private void GenerateAIAgentStats()
    {
        //Generate a random speed for the agent
        _agentSpeed = URandom.Range(3f, 7f);

        //generate stage preferences values
        _stage1TotalTime = URandom.Range(30f, 70f);
        _stage2TotalTime = 100f - _stage1TotalTime;
        _stage1Time = _stage1TotalTime;
        _stage2Time = _stage2TotalTime;
        _stage1LoseTime = URandom.Range(0.1f, 1f);
        _stage2LoseTime = URandom.Range(0.1f, 1f);

        //generate random health values
        _maxHealth = URandom.Range(80f, 100f);
        _startingHealth = URandom.Range(0, _maxHealth);
        _healthLoseSpeed = URandom.Range(0.05f, 0.2f);
        _healthGainSpeed = URandom.Range(3f, 5f);
        _health = _startingHealth;

        //generate random stamina values
        _maxStamina = URandom.Range(70f, 100f);
        _startingStamina = URandom.Range(0, _maxStamina);
        _staminaLoseSpeed = URandom.Range(0.1f, 0.5f);
        _staminaGainSpeed = URandom.Range(3f, 5f);
        _stamina = _startingStamina;

        //Get references to stages locations
        _stage1 = GameObject.Find("Stage1");
        _stage2 = GameObject.Find("Stage2");
    }

    /// <summary>
    /// Method to create the state machine states and transitions
    /// </summary>
    private void CreateFSM()
    {
        //Hungry state
        State eatingState = new State("Hungry",
            BlockAgent,
            EatingActions,
            UnblockAgent);
        
        //Go eat state
        State goEat = new State("Go Eat",
            null,
            GoEatActions,
            null);

        //Resting state
        State restingState = new State("Tired",
            null,
            RestingActions,
            null);

        //Go rest state
        State goRest = new State("Go Rest",
            null,
            GoRestActions,
            null);

        //Go to stage 1 state
        State goStage1 = new State("Go Stage1",
            null,
            GoStage1Actions,
            null);

        //Go to stage 2 state
        State goStage2 = new State("Go Stage2",
            null,
            GoStage2Actions,
            null);

        //Watching stage 1 state
        State watchingStage1 = new State("Watching Stage1",
            null,
            WatchingStage1Actions,
            null);
        
        //Watching stage 2 state
        State watchingStage2 = new State("Watching Stage2",
            null,
            WatchingStage2Actions,
            null);

        //Panic state
        State panic = new State("Panic",
            null,
            PanicActions,
            null);

        //Go eat to eating transition
        goEat.AddTransition(
            new Transition(
                () => _arriveFood == true,
                null,
                eatingState));

        //Go eat to panic transition
        goEat.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Go rest to resting transition
        goRest.AddTransition(
           new Transition(
               () => _arriveChill == true,
               null,
               restingState));


        //Go rest to panic transition
        goRest.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));
        
        //Eating to go rest transition
        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina <= 0),
               null,
               goRest));

        //Eating to panic transition
        eatingState.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Resting to go eat transition
        restingState.AddTransition(
           new Transition(
               () => (_stamina >= _maxStamina && _health <= 0),
               null,
               goEat));

        //Resting to panic transition
        restingState.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Eating to go watch stage 1 transition
        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina > 0 && _stage1Time > _stage2Time),
               null,
               goStage1));

        //Eating to go watch stage 2 transition
        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina > 0 && _stage2Time > _stage1Time),
               null,
               goStage2));

        //Resting to go watch stage 1 transition
        restingState.AddTransition(
          new Transition(
              () => (_stamina >= _maxStamina && _health > 0 && _stage1Time > _stage2Time),
              null,
              goStage1));

        //Resting to go watch stage 2 transition
        restingState.AddTransition(
           new Transition(
               () => (_stamina >= _maxStamina && _health > 0 && _stage2Time > _stage1Time),
               null,
               goStage2));
    
        //Go stage 1 to watching stage 1 transition
        goStage1.AddTransition(
           new Transition(
               () => _arriveStage1 == true,
               null,
               watchingStage1));

        //Go stage 1 to watching stage 1 transition
        goStage1.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Go stage 1 to go eat transition
        goStage1.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        //Go stage 1 to go rest transition
        goStage1.AddTransition(
           new Transition(
               () => _stamina <= 0,
               null,
               goRest));

        //Go stage 2 to watching stage 2 transition
        goStage2.AddTransition(
           new Transition(
               () => _arriveStage2 == true,
               null,
               watchingStage2));

        //Go stage 2 to panic transition
        goStage2.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Go stage 2 to go eat transition
        goStage2.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        //Go stage 2 to go rest transition
        goStage2.AddTransition(
           new Transition(
               () => _stamina <= 0,
               null,
               goRest));

        //Watching stage 1 to go stage 2 transition
        watchingStage1.AddTransition(
           new Transition(
               () => _stage1Time <= 0,
               null,
               goStage2));

        //Watching stage 1 to panic transition
        watchingStage1.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Watching stage 1 to go eat transition
        watchingStage1.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        //Watching stage 1 to go rest transitions
        watchingStage1.AddTransition(
           new Transition(
               () => _stamina <= 0,
               null,
               goRest));

        //Watching stage 2 to go watch stage 1 transition
        watchingStage2.AddTransition(
          new Transition(
              () => _stage2Time <= 0,
              null,
              goStage1));

        //Watching stage 2 to panic transition
        watchingStage2.AddTransition(
            new Transition(
                () => isPanicking == true,
                null,
                panic));

        //Watching stage 2 to go eat transition
        watchingStage2.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        //Watching stage 2 to go rest transition
        watchingStage2.AddTransition(
           new Transition(
               () => _stamina <= 0 && _health >= 0,
               null,
               goRest));


        //Check what is the stage to start watching at the beginning of the simulation
        if (_stage1Time > _stage2Time)
        {
            initialState = goStage1;
        }
        else
        {
            initialState = goStage2;
        }

        //Set the state machine initial state
        stateMachine = new StateMachine(initialState);
    }

    /// <summary>
    /// Method to set the food destination
    /// </summary>
    private void MoveToFood()
    {
         _agent.destination = _requestedTableReference.transform.position;
     
    }
    /// <summary>
    /// Method to set the green space destination
    /// </summary>
    private void MoveToRest()
    {
        if(_requestedGreenReference != null)
        _agent.destination = _requestedGreenReference.transform.position;
    }

    /// <summary>
    /// Method to set Stage 1 as destination
    /// </summary>
    private void MoveToStage1()
    {
        _agent.destination = _stage1.transform.position;
    }
    /// <summary>
    /// Method to set Stage 2 as destination
    /// </summary>
    private void MoveToStage2()
    {
        _agent.destination = _stage2.transform.position;

    }
    /// <summary>
    /// Method to block agent in place when he is eating
    /// </summary>
    private void BlockAgent()
    {
        //Disbale player movement
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<NavMeshObstacle>().enabled = true;
    }
    /// <summary>
    /// Method to unblock agent when he stops eeating, let him move
    /// </summary>
    private void UnblockAgent()
    {
        //Reste request table permition
        _requestedTableOnce = false;
        //Enable player movement
        gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }


    /// <summary>
    /// Method to request a table gameobject to the table manager
    /// </summary>
    private void RequestTable()
    {
        _requestedTableReference = _tableManagerReference.GiveTableToAgent();
    }
    /// <summary>
    /// Method to request a green space gameobject to the green space manager
    /// </summary>
    private void RequestGreenSpace()
    {
        _requestedGreenReference = _greenManagerReference.GiveGreenSpaceToAgent();
    }
    /// <summary>
    /// Method to stun the agent for some time when he is in stun radius of explosion
    /// </summary>
    private void StunAgentForSomeTime()
    {
        //Set agent speed to 0
        _agent.speed = 0;

        //decrease stunned time
        stunnedTime -= Time.deltaTime;
        if (stunnedTime <= 0)
        {
            //if agent is not stoped, set the agent speed to half
            _agent.speed = _agentSpeed / 2;
        }

    }
}