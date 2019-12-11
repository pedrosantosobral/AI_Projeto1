using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpace : MonoBehaviour
{
    public int costPerAgent;

  
    public int ammountOfAgents;

    // Start is called before the first frame update
    void Start()
    {
        //start with 0 agents
        ammountOfAgents = 0; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            ammountOfAgents += costPerAgent;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ammountOfAgents -= costPerAgent;
    }
}
