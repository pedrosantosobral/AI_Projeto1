using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class AgentsAmount : MonoBehaviour
{
    public GameObject   agentPrefab;
    public int          amountOfAgents;
    public float        timeBetweenAgents;

    void Start()
    {
        StartCoroutine(MyCounter(amountOfAgents));
    }
    IEnumerator MyCounter(int number)
    {
        int i = 0;
        while (i < number)
        {
            Instantiate(agentPrefab, gameObject.transform);
            i++;
            yield return new WaitForSeconds(timeBetweenAgents);
        }
    }
}
