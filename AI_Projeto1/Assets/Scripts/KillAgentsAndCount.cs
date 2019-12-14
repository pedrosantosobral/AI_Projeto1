using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAgentsAndCount : MonoBehaviour
{
    //total ammount of agents killed
    public int agentsKilled;

    private void Start()
    {
        //start with 0 agents killed
        agentsKilled = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Agent"))
        {
            Destroy(other.gameObject);
            agentsKilled += 1;
        }
    }
}
