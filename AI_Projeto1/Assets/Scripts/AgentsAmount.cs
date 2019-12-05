using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class AgentsAmount : MonoBehaviour
{
    public int _timeForSpawn;
    private int _startIterator = 1;
    private int _afterStartIterator = 1;
    public GameObject agentPrefab;
    public int amountOfAgentsAtStart;
    public int amountOfAgentsAfterStart;

    public Transform enterPosition;
    // Start is called before the first frame update


    // Update is called once per frame
    private void FixedUpdate()
    {    
        if(_startIterator < amountOfAgentsAtStart)
        {
            //Vector3 temp = new Vector3(URandom.Range(-3,3), 1, URandom.Range(-3, 3));
            //gameObject.transform.position += temp;
            Instantiate(agentPrefab, gameObject.transform);
            _startIterator = _startIterator + 1;
        }

        if (_afterStartIterator < amountOfAgentsAfterStart)
        {
            Invoke("InstantiateAgent", _timeForSpawn);
            _afterStartIterator = _afterStartIterator + 1;
        }

    }

    private void InstantiateAgent()
    {
        Instantiate(agentPrefab,enterPosition);
    }
}
