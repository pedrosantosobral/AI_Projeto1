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
    }

    private void IncreaseHealth()
    {
        _health += _healthGainSpeed * Time.fixedDeltaTime;
        if(_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
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
            IncreaseHealth,
            () => Debug.Log("Saiu"));

        State goEat = new State("Go Eat",
            ()=> Debug.Log("Entrou"),
            MoveToFood,
            null);

        State tiredState = new State("Tired",
            null,
            IncreaseStamina,
            null);

        
        goEat.AddTransition(
            new Transition(
                () => _arrived == true,
                null,
                eatingState));

        stateMachine = new StateMachine(goEat);
    }

    private void MoveToFood()
    {
        _agent.destination = _food.transform.position;
    }

    
}
