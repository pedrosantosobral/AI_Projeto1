using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using TMPro;

/// <summary>
/// Class to define te ammount of agents on the simulation and update the UI dead agents counter
/// </summary>
public class AgentsAmount : MonoBehaviour
{
    /// <summary>
    /// Agent reference
    /// </summary>
    public GameObject   agentPrefab;

    /// <summary>
    /// Total amount of agents
    /// </summary>
    public int          amountOfAgents;

    /// <summary>
    /// Time between agents spawn
    /// </summary>
    public float        timeBetweenAgents;

    /// <summary>
    /// Number os agents killed
    /// </summary>
    public int          agentsKilled;

    /// <summary>
    /// Fire reference
    /// </summary>
    private GameObject fireReference;
    
    /// <summary>
    /// TMP reference
    /// </summary>
    public TextMeshProUGUI textPro;

    /// <summary>
    /// Start the coroutine
    /// </summary>
    void Start()
    {
        //Start agents spawn
        StartCoroutine(MyCounter(amountOfAgents));
    }

    /// <summary>
    /// Coroutine to spawn agents one at a time
    /// </summary>
    /// <param name="number"></param>
    /// <returns><Returns a IEnumerator</returns>
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

    /// <summary>
    ///  Gets the fire reference, ammount of killed agents and update the UI
    /// </summary>
    private void Update()
    {
        // if reference is null, get the game object
        if (fireReference == null)
        {
            //get the object from the scene
            fireReference = GameObject.Find("Fire(Clone)");
        }
        //if reference is not null
        else
        {
            //update counter
            agentsKilled = fireReference.GetComponent<KillAgentsAndCount>().agentsKilled;
            //update UI
            textPro.text = agentsKilled.ToString();
        }
    }
}
