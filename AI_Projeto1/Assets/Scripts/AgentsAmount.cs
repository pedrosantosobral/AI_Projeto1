using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class AgentsAmount : MonoBehaviour
{
    //Agent reference
    public GameObject   agentPrefab;
    //Total amount of agents
    public int          amountOfAgents;
    //Time between agents spawn
    public float        timeBetweenAgents;

    void Start()
    {
        //Start agents spawn
        StartCoroutine(MyCounter(amountOfAgents));
    }
    IEnumerator MyCounter(int number)
    {
        //if actual agents are less than total amount of agents, spawn more agents
        int i = 0;

        while (i < number)
        {
            //Instatiate new agent
            Instantiate(agentPrefab, gameObject.transform);
            i++;
            //Wait time between spawn to spawn another
            yield return new WaitForSeconds(timeBetweenAgents);
        }
    }
}
