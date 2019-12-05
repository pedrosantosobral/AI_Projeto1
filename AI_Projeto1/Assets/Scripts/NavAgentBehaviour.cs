using System;
using System.Collections;
using System.Collections.Generic;
using URandom = UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;

public class NavAgentBehaviour : MonoBehaviour
{
    private bool        _arrived;

    //stage timers variables

    private int         _stage;
    private int         _startingStage;
    private float       _stage1TotalTime;
    private float       _stage1Time;
    private float       _stage2TotalTime;
    private float       _stage2Time;

    //Health variables
    private float       _health;
    private float       _maxHealth;
    private float       _startingHealth;
    private float       _healthLoseSpeed;
    private float       _healthGainSpeed;

    //Stamina variables
    private float       _stamina;
    private float       _maxStamina;
    private float       _startingStamina;
    private float       _staminaLoseSpeed;
    private float       _staminaGainSpeed;

     //References to locations
    private GameObject  _stage1             = null;
    private GameObject  _stage2             = null;
    private GameObject  _chillZone1         = null;
    private GameObject  _chillZone2         = null;
    private GameObject  _food               = null;

    // Reference to the NavMeshAgent component
    private NavMeshAgent _agent;

    //Reference to state machine
    private StateMachine stateMachine;

    private void Start()
    {
        GenerateAIAgentStats();
        CreateFSM();

        // Get reference to the NavMeshAgent component
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            _arrived = true;
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
        _arrived = false;
    }

    private void IncreaseHealth()
    {
        _health += _healthGainSpeed * Time.fixedDeltaTime;
        if(_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        _arrived = false;
    }

    private void RestingActions()
    {
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
        DecreaseHealth();
        DecreaseStamina();
        MoveToFood();

    }

    private void GoRestActions()
    {
        DecreaseHealth();
        DecreaseStamina();
        MoveToRest();

    }

    private void GoStage1Actions()
    {
        DecreaseHealth();
        DecreaseStamina();
        MoveToStage1();

    }

    private void GoStage2Actions()
    {
        DecreaseHealth();
        DecreaseStamina();
        MoveToStage2();

    }

    private void GenerateAIAgentStats()
    {
        //generate stage preferences values
        _stage1TotalTime = URandom.Range(30f, 70f);
        _stage2TotalTime = 100f - _stage1TotalTime;
        _stage1Time = _stage1TotalTime;
        _stage2Time = _stage2TotalTime;

        //generate random health values
        _maxHealth = URandom.Range(80f, 100f);
        _startingHealth = URandom.Range(0, _maxHealth);
        _healthLoseSpeed = URandom.Range(0.1f, 0.5f);
        _healthGainSpeed = URandom.Range(3f, 5f);
        _health = _startingHealth;

        //generate random stamina values
        _maxStamina = URandom.Range(70f, 100f);
        _startingStamina = URandom.Range(0, _maxStamina);
        _staminaLoseSpeed = URandom.Range(0.5f, 1f);
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
            () => Debug.Log("Entrou health++"),
            EatingActions,
            () => Debug.Log("Saiu"));

        State goEat = new State("Go Eat",
            ()=> Debug.Log("Entrou"),
            GoEatActions,
            null);

        State restingState = new State("Tired",
            null,
            RestingActions,
            null);

        State goRest = new State("Go Rest",
            () => Debug.Log("Entrou"),
            GoRestActions,
            null);

        State goStage1 = new State("Go Stage1",
            () => Debug.Log("Entrou"),
            GoStage1Actions,
            null);

        State goStage2 = new State("Go Stage2",
            () => Debug.Log("Entrou"),
            GoStage2Actions,
            null);

        State watchingStage1 = new State("Watching Stage1",
            () => Debug.Log("Entrou"),
            WatchingStage1Actions,
            null);


        goEat.AddTransition(
            new Transition(
                () => _arrived == true,
                null,
                eatingState));

        goRest.AddTransition(
           new Transition(
               () => _arrived == true,
               null,
               restingState));

        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina <= 0),
               null,
               goRest));

        restingState.AddTransition(
           new Transition(
               () => (_stamina >= _maxStamina && _health-- <= 0),
               null,
               goEat));

        eatingState.AddTransition(
           new Transition(
               () => (_health >= _maxHealth && _stamina > 0), //falta adicionar condição de escolha de palcos
               null,
               goStage1));



        stateMachine = new StateMachine(goEat);
    }

    private void MoveToFood()
    {
        _agent.destination = _food.transform.position;
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


}
