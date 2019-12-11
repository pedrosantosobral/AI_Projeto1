using System;
using System.Collections;
using System.Collections.Generic;
using URandom = UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;

public class NavAgentBehaviour : MonoBehaviour
{

    private TableManager _tableManagerReference;
    private GameObject   _requestedTableReference;
    private bool         _requestedOnce;

    private float _agentSpeed = 8;
    State initialState;

    private bool _arriveFood;
    private bool _arriveChill;
    private bool _arriveStage1;
    private bool _arriveStage2;

    //stage timers variables

    private float _stage1TotalTime;
    private float _stage1LoseTime;
    private float _stage1Time;
    private float _stage2TotalTime;
    private float _stage2LoseTime;
    private float _stage2Time;

    //Health variables
    private float _health;
    private float _maxHealth;
    private float _startingHealth;
    private float _healthLoseSpeed;
    private float _healthGainSpeed;

    //Stamina variables
    private float _stamina;
    private float _maxStamina;
    private float _startingStamina;
    private float _staminaLoseSpeed;
    private float _staminaGainSpeed;

    //References to locations
    private GameObject _stage1 = null;
    private GameObject _stage2 = null;
    private GameObject _chillZone1 = null;
    private GameObject _chillZone2 = null;
    private GameObject _food = null;

    // Reference to the NavMeshAgent component
    private NavMeshAgent _agent;

    //Reference to state machine
    private StateMachine stateMachine;

    private void Start()
    {
        //start with no requested table
        _requestedOnce = false;
        //get reference of table manager
        _tableManagerReference = GameObject.Find("TableManager").GetComponent<TableManager>();

        GenerateAIAgentStats();
        CreateFSM();

        // Get reference to the NavMeshAgent component
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Action actions = stateMachine.Update();
        actions?.Invoke();
        RecalculatePath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            _arriveFood = true;
        }
        if (other.CompareTag("Rest"))
        {
            _arriveChill = true;
        }
        if (other.CompareTag("Stage1"))
        {
            _arriveStage1 = true;
        }
        if (other.CompareTag("Stage2"))
        {
            _arriveStage2 = true;
        }


    }

    private void OnTriggerStay(Collider other)
    {
        //while is on requesting spot allow table requests
        if (other.CompareTag("WaitingSpot"))
        {
            _requestedOnce = false;
        }
    }



    private void DecreaseStamina()
    {
        _stamina -= _staminaLoseSpeed * Time.fixedDeltaTime;
        if (_stamina <= 0)
        {
            _stamina = 0;
        }
    }


    private void DecreaseHealth()
    {
        _health -= _healthLoseSpeed * Time.fixedDeltaTime;
        if (_health <= 0)
        {
            _health = 0;
        }
    }

    private void IncreaseStamina()
    {
        _stamina += _staminaGainSpeed * Time.fixedDeltaTime;
        if (_stamina >= _maxStamina)
        {
            _stamina = _maxStamina;
        }
        _arriveChill = false;
    }

    private void IncreaseHealth()
    {
        _health += _healthGainSpeed * Time.fixedDeltaTime;
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        _arriveFood = false;
    }

    private void RestingActions()
    {
        _agent.speed -= Time.deltaTime * 3;
        IncreaseStamina();
        DecreaseHealth();
    }

    private void EatingActions()
    {
        IncreaseHealth();
        DecreaseStamina();
    }

    private void GoEatActions()
    {
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        if(_requestedOnce == false)
        {
            RequestTable();
            _requestedOnce = true;
        }
        MoveToFood();

    }

    private void GoRestActions()
    {
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        MoveToRest();

    }

    private void GoStage1Actions()
    {
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        MoveToStage1();

    }

    private void GoStage2Actions()
    {
        _agent.speed = _agentSpeed;
        DecreaseHealth();
        DecreaseStamina();
        MoveToStage2();

    }

    private void WatchingStage1Actions()
    {
        _agent.speed -= Time.deltaTime;
        _arriveStage1 = false;
        DecreaseHealth();
        DecreaseStamina();
        //Reset Stage 2 Watching time
        _stage2Time = _stage2TotalTime;
        _stage1Time -= _stage1LoseTime * Time.deltaTime;
        if (_stage1Time <= 0)
        {
            _stage1Time = 0;
        }
    }

    private void WatchingStage2Actions()
    {
        _agent.speed -= Time.deltaTime; ;
        _arriveStage2 = false;
        DecreaseHealth();
        DecreaseStamina();
        //Reset Stage 2 Watching time
        _stage1Time = _stage1TotalTime;
        _stage2Time -= _stage1LoseTime * Time.deltaTime;
        if (_stage2Time <= 0)
        {
            _stage2Time = 0;
        }
    }

    private void GenerateAIAgentStats()
    {
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

        //Get references
        _stage1 = GameObject.Find("Stage1");
        _stage2 = GameObject.Find("Stage2");
        _chillZone1 = GameObject.Find("ChillZone1");
        _chillZone2 = GameObject.Find("ChillZone2");
        _food = GameObject.Find("Food");
    }

    private void CreateFSM()
    {
        State eatingState = new State("Hungry",
            BlockAgent,
            EatingActions,
            UnblockAgent);

        State goEat = new State("Go Eat",
            () => Debug.Log("vai comer"),
            GoEatActions,
            () => Debug.Log("chegou a comida"));

        State restingState = new State("Tired",
            () => Debug.Log("está a descansar"),
            RestingActions,
            () => Debug.Log("parar de descansar"));

        State goRest = new State("Go Rest",
            () => Debug.Log("vai descansar"),
            GoRestActions,
            () => Debug.Log("chegou ao descanso"));

        State goStage1 = new State("Go Stage1",
            () => Debug.Log("vai para o palco 1"),
            GoStage1Actions,
            () => Debug.Log("chegou ao palco 1"));

        State goStage2 = new State("Go Stage2",
            () => Debug.Log("vai para o palco 2"),
            GoStage2Actions,
            () => Debug.Log("chegou ao palco 2"));

        State watchingStage1 = new State("Watching Stage1",
            () => Debug.Log("Esta a ver o palco 1"),
            WatchingStage1Actions,
            () => Debug.Log("acabou de ver o palco 1"));

        State watchingStage2 = new State("Watching Stage2",
            () => Debug.Log("Esta a ver o palco 2"),
            WatchingStage2Actions,
            () => Debug.Log("acabou de ver o palco 2"));


        goEat.AddTransition(
            new Transition(
                () => _arriveFood == true,
                null,
                eatingState));

        goRest.AddTransition(
           new Transition(
               () => _arriveChill == true,
               null,
               restingState));

        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina <= 0),
               null,
               goRest));

        restingState.AddTransition(
           new Transition(
               () => (_stamina >= _maxStamina && _health <= 0),
               null,
               goEat));

        //Go to stages transitions
        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina > 0 && _stage1Time > _stage2Time),
               null,
               goStage1));

        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina > 0 && _stage2Time > _stage1Time),
               null,
               goStage2));

        restingState.AddTransition(
          new Transition(
              () => (_stamina >= _maxStamina && _health > 0 && _stage1Time > _stage2Time),
              null,
              goStage1));

        restingState.AddTransition(
           new Transition(
               () => (_stamina >= _maxStamina && _health > 0 && _stage2Time > _stage1Time),
               null,
               goStage2));

        goStage1.AddTransition(
           new Transition(
               () => _arriveStage1 == true,
               null,
               watchingStage1));

        goStage1.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        goStage1.AddTransition(
           new Transition(
               () => _stamina <= 0,
               null,
               goRest));

        goStage2.AddTransition(
           new Transition(
               () => _arriveStage2 == true,
               null,
               watchingStage2));

        goStage2.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        goStage2.AddTransition(
           new Transition(
               () => _stamina <= 0,
               null,
               goRest));

        watchingStage1.AddTransition(
           new Transition(
               () => _stage1Time <= 0,
               null,
               goStage2));

        watchingStage1.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        watchingStage1.AddTransition(
           new Transition(
               () => _stamina <= 0,
               null,
               goRest));

        watchingStage2.AddTransition(
          new Transition(
              () => _stage2Time <= 0,
              null,
              goStage1));

        watchingStage2.AddTransition(
           new Transition(
               () => _health <= 0,
               null,
               goEat));

        watchingStage2.AddTransition(
           new Transition(
               () => _stamina <= 0 && _health >= 0,
               null,
               goRest));


        //get initial state
        if (_stage1Time > _stage2Time)
        {
            initialState = goStage1;
        }
        else
        {
            initialState = goStage2;
        }

        stateMachine = new StateMachine(initialState);
    }

    private void MoveToFood()
    {
         _agent.destination = _requestedTableReference.transform.position;
     
    }
    
    private void MoveToRest()
    {
        _agent.destination = _chillZone1.transform.position;
    }

    private void MoveToStage1()
    {
        _agent.destination = _stage1.transform.position;
    }

    private void MoveToStage2()
    {
        _agent.destination = _stage2.transform.position;

    }

    private void BlockAgent()
    {
        //disbale player movement
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<NavMeshObstacle>().enabled = true;
    }

    private void UnblockAgent()
    {
        //restet request table permition
        _requestedOnce = false;
        //enable player movement
        gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    private void RequestTable()
    {
        _requestedTableReference = _tableManagerReference.GiveTableToAgent();
    }

    private void RecalculatePath()
    {
       //gameObject.position
    }
}