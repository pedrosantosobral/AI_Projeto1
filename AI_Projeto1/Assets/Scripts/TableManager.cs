using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using System;

public class TableManager : MonoBehaviour
{
    public GameObject        toReturn;
    public GameObject        waitingSpot;
    //list of all the tables
    public  List<GameObject> tableList;

    public GameObject GiveTableToAgent()
    {
        //get a random Table
        GetNewTable();

        if(toReturn.GetComponent<Table>().tableIsFull == true)
        {
            GetNewTable();
        }

        return (toReturn);        

    }
    private void GetNewTable()
    {
        toReturn = tableList[URandom.Range(0, tableList.Count)];
    }
}
