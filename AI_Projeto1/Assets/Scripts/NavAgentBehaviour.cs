using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;

public class NavAgentBehaviour : MonoBehaviour
{
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

    private void Start()
    {
        //generate stage preferences values
        _stage1TotalTime = Random.Range(30f, 70f);
        _stage2TotalTime = 100f - _stage1TotalTime;
        _stage1Time      = _stage1TotalTime;
        _stage2Time      = _stage2TotalTime; 

        //generate random health values
        _maxHealth          = Random.Range(80f, 100f);
        _startingHealth     = Random.Range(0, _maxHealth);
        _healthLoseSpeed    = Random.Range(0.1f, 0.5f);
        _healthGainSpeed    = Random.Range(3f, 5f);
        _health             = _startingHealth;

        //generate random stamina values
        _maxStamina         = Random.Range(70f, 100f);
        _startingStamina    = Random.Range(0, _maxStamina);
        _staminaLoseSpeed   = Random.Range(0.5f, 1f);
        _staminaGainSpeed   = Random.Range(3f, 5f);
        _stamina            = _startingStamina;

        //Get references
        _stage1      = GameObject.Find("Stage1");
        _stage2      = GameObject.Find("Stage2");
        _chillZone1  = GameObject.Find("ChillZone1");
        _chillZone2  = GameObject.Find("ChillZone2");
        _food        = GameObject.Find("Food");
    
        // Get reference to the NavMeshAgent component
        _agent = GetComponent<NavMeshAgent>();
        // Set initial agent goal
        //_agent.destination = _stage1 .transform.position;
    }

    // Method called when agent collides with something
    private void OnTriggerEnter(Collider other)
    {
        // Did agent collide with goal?
        if (other.name == "Goal")
            // If so, update destination (let goal reposition itself first)
            Invoke("UpdateDestination", 0.1f);
    }

    // Update destination
    private void UpdateDestination()
    {
        // Set destination to current goal position
        _agent.destination = _stage1.transform.position;
    }

    private void DecreaseStamina()
    {
        _stamina -= _staminaLoseSpeed * Time.fixedDeltaTime;
    }

    
    private void DecreaseHealth()
    {
        _health -= _healthLoseSpeed * Time.fixedDeltaTime;
    }

    private void IncreaseStamina()
    {
        _stamina += _staminaGainSpeed * Time.fixedDeltaTime;
    }

    private void IncreaseHealth()
    {
        _health += _healthGainSpeed * Time.fixedDeltaTime;
    }

    //test update
    private void FixedUpdate()
    {
        DecreaseStamina();
    }

}
