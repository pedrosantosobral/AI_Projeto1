using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TableManager : MonoBehaviour
{
    public  List<GameObject> tableList;
    private List<GameObject> _availableTablesList;

    private void Start()
    {
        _availableTablesList    = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        CheckAvailableTables();
    }

    private void CheckAvailableTables()
    {
        foreach(GameObject i in tableList)
        {
            if(i.GetComponent<Table>().tableIsFull == false)
            {
                if(!_availableTablesList.Contains(i))
                {
                    _availableTablesList.Add(i);
                }
                
            }
        }

        foreach(GameObject i in _availableTablesList)
        {
            if (i.GetComponent<Table>().tableIsFull == true)
            {
                _availableTablesList.Remove(i);
            }
        }
    }
}
