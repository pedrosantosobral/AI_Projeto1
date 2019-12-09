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
    

    private void OnTriggerExit(Collider other)
    {
        _ammountOfAgents -= 1;
    }

    private void FixedUpdate()
    {
        if(_ammountOfAgents >= 8)
        {
            tableIsFull = true;
        }
        else 
        {
            tableIsFull = false;
        }
    }

    public void IncrementTheTable()
    {
        _ammountOfAgents += 1;
    }
}
