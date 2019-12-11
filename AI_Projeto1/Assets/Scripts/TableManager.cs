using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using System;

public class TableManager : MonoBehaviour
{

    public GameObject        waitingSpot;
    //list of all the tables
    public  List<GameObject> tableList;
    //list of avaliable tables for the players
    private List<GameObject> _availableTablesList;
    private void Start()
    {
        //start available tables for the player list
        _availableTablesList    = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        CheckAvailableTables();
    }

    //adds and removes available tables
    private void CheckAvailableTables()
    {
        //for each table in all the tables list
        foreach(GameObject i in tableList)
        {
            //check if table is not full
            if(i.GetComponent<Table>().tableIsFull == false)
            {
                //if the list dont contain this table add it
                if(!_availableTablesList.Contains(i))
                {
                    _availableTablesList.Add(i);
                }
                
            }
        }
        //TODO FIX REMOVING WHILE SEARCHING
        //checks if a table is full and removes it from the available tables list 
        foreach(GameObject i in _availableTablesList)
        {
            //if the table is full remove it from the list of available tables
            if (i.GetComponent<Table>().tableIsFull == true)
            {
                _availableTablesList.Remove(i);
            }
        }
    }

    //gives a random free table to the player and reserves a place for that agent
    public GameObject GiveTableToAgent()
    {
        GameObject toReturn;

        //if there is tables avaliable
        if(_availableTablesList.Count != 0)
        {
            //get a random table
            toReturn = _availableTablesList[URandom.Range(0, _availableTablesList.Count)];
            //reserve that table;
            toReturn.GetComponent<Table>().IncrementTheTable();
        }
        else if(_availableTablesList.Count == 0)
        {   //return a waiting spot;
            toReturn = waitingSpot;
        }

        return (toReturn);

        
        
    }
}
