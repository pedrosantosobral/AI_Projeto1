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
        tableIsFull = false;
        _ammountOfAgents = 0; 
    }

    private void OnTriggerEnter(Collider other)
    {
        _ammountOfAgents += 1;
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

}
