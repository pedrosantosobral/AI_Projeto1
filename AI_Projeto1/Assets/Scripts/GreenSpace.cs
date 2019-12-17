using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the green spaces management of ammount of agents
/// </summary>
public class GreenSpace : MonoBehaviour
{
    /// <summary>
    /// Cost per agent on this green space
    /// </summary>
    public int costPerAgent;

    /// <summary>
    /// Ammount of agents in this green space
    /// </summary>
    public int ammountOfAgents;

    /// <summary>
    /// Start with zero agents on the green space
    /// </summary>
    private void Start()
    {
        //Start with 0 agents
        ammountOfAgents = 0; 
    }
    /// <summary>
    /// Check if an agent entered the green space and increment that agent cost to the total cost
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //If the other collider is from an agent
        if (other.CompareTag("Agent"))
        {
            //Increment the cost of that agent on the green zone
            ammountOfAgents += costPerAgent;
        }
    }
    /// <summary>
    /// Checks if the agent exit the green zone and decrement the cost
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //Decrement the cost of that agent
        ammountOfAgents -= costPerAgent;
    }
}
