using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool     tableIsFull;
    private int     _ammountOfAgents;
    // Start is called before the first frame update
    void Start()
    {
        //start with empty table
        tableIsFull = false;
        //start with 0 agents
        _ammountOfAgents = 0; 
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add agents to the table
        _ammountOfAgents += 1;
    }
    private void OnTriggerExit(Collider other)
    {
        //Remove agents from table
        _ammountOfAgents -= 1;
    }

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
