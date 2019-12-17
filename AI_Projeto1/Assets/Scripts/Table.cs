using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the table gameobjects that define the ammount of agents per table, increment and decrement agents on the table
/// and sets if it is full or not
/// </summary>
public class Table : MonoBehaviour
{
    /// <summary>
    /// Bool to check if the table is full or not
    /// </summary>
    public bool     tableIsFull;

    /// <summary>
    /// Ammount of agents in this table
    /// </summary>
    private int     _ammountOfAgents;
    
    /// <summary>
    /// Method to start with a empty table
    /// </summary>
    void Start()
    {
        //start with empty table
        tableIsFull = false;
        //start with 0 agents
        _ammountOfAgents = 0; 
    }

    /// <summary>
    /// Method to check if a agent entered the table and increment the ammount of agents
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Add agents to the table
        _ammountOfAgents += 1;
    }
    /// <summary>
    /// Method to check if a agent exited the table and decrement the ammount of agents
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //Remove agents from table
        _ammountOfAgents -= 1;
    }

    /// <summary>
    /// Checks if the table is full or not
    /// </summary>
    private void FixedUpdate()
    {
        //If the amount of agents is bigger than 8, set the table full
        if(_ammountOfAgents >= 8)
        {
            tableIsFull = true;
        }
        else 
        {
            tableIsFull = false;
        }
    }
}
