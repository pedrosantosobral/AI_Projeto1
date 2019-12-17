using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using System;


/// <summary>
/// Class that manages all the tables for the agent, returns a free table for the agent
/// </summary>
public class TableManager : MonoBehaviour
{
    /// <summary>
    /// Table to be returned to the agent
    /// </summary>
    public GameObject        toReturn;

    /// <summary>
    /// List of tables in the simulation
    /// </summary>
    public  List<GameObject> tableList;

    /// <summary>
    /// Method that checks all the tables and return a empty table to the agent
    /// </summary>
    /// <returns></returns>
    public GameObject GiveTableToAgent()
    {
        //get a random Table
        GetNewTable();

        //If a table is full, get another table
        if(toReturn.GetComponent<Table>().tableIsFull == true)
        {
            GetNewTable();
        }

        return (toReturn);        

    }
    /// <summary>
    /// Get a randomtable from the table list
    /// </summary>
    private void GetNewTable()
    {
        toReturn = tableList[URandom.Range(0, tableList.Count)];
    }
}
