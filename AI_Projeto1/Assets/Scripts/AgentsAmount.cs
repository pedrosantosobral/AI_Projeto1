using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using TMPro;

public class AgentsAmount : MonoBehaviour
{
    //Agent reference
    public GameObject   agentPrefab;
    //Total amount of agents
    public int          amountOfAgents;
    //Time between agents spawn
    public float        timeBetweenAgents;
    // Number os agents killed
    public int          agentsKilled;
    //Fire reference
    private GameObject fireReference;

    public TextMeshProUGUI textPro;


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

    private void Update()
    {
        if (fireReference == null)
        {
            fireReference = GameObject.Find("Fire(Clone)");
        }
        else
        {
            agentsKilled = fireReference.GetComponent<KillAgentsAndCount>().agentsKilled;
            textPro.text = agentsKilled.ToString();
        }
    }
}
