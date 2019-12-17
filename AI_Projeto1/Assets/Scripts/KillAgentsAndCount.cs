using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the fire gameobject, destroy and count the killed agents
/// </summary>
public class KillAgentsAndCount : MonoBehaviour
{
    /// <summary>
    /// total of agents killed with the fire
    /// </summary>
    public int agentsKilled;
    /// <summary>
    /// Start with 0 killed agents
    /// </summary>
    private void Start()
    {
        //start with 0 agents killed
        agentsKilled = 0;
    }

    /// <summary>
    /// Check an agent touches the collider, if yes destroy it and increment the counter
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //If other collider is a agent destroy it
        if(other.CompareTag("Agent"))
        {
            //Destroy agent
            Destroy(other.gameObject);
            //Increment the killed agents counter
            agentsKilled += 1;
        }
    }
}
