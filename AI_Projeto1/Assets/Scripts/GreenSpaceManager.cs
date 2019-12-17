using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages the 3 green spaces
/// </summary>
public class GreenSpaceManager : MonoBehaviour
{
    /// <summary>
    /// Green space 1 gameobject
    /// </summary>
    public GameObject greenSpace1;

    /// <summary>
    /// Green space 2 gameobject
    /// </summary>
    public GameObject greenSpace2;

    /// <summary>
    /// Green space 3 gameobject
    /// </summary>
    public GameObject greenSpace3;

    /// <summary>
    /// Green space 1 ammount of agents reference
    /// </summary>
    private int _greenSpace1;

    /// <summary>
    /// Green space 2 ammount of agents reference
    /// </summary>
    private int _greenSpace2;

    /// <summary>
    /// Green space 3 ammount of agents reference
    /// </summary>
    private int _greenSpace3;

    /// <summary>
    /// Lowest ammount of agents in all zones reference
    /// </summary>
    private int _lowest;

    /// <summary>
    /// Array of green zones 
    /// </summary>
    private int[] objectsArray;


    /// <summary>
    /// Method to start the array and define the lowest ammount of agents at start
    /// </summary>
    public void Start()
    {
        objectsArray = new int[3];
        _greenSpace1 = greenSpace1.GetComponent<GreenSpace>().ammountOfAgents;
        _lowest = _greenSpace1;
    }


   /// <summary>
   /// Method to return one green space with the less ammount of agents to the agent 
   /// </summary>
   /// <returns></returns>
    public GameObject GiveGreenSpaceToAgent()
    {
        //Get the lowest ammount of agents
        GetAmmount();

        //If is green space 1 that have the lowest ammount of agents
        if (_lowest == _greenSpace1)
        {
            //Return green space 1
            return greenSpace1;
        }
        //If is green space 2 that have the lowest ammount of agents or 2 and 3 have the same
        else if (_lowest == _greenSpace2 || (_lowest == _greenSpace2 && _lowest == _greenSpace3))
        {
            //Return green space 2
            return greenSpace2;
        }
        //If is green space 3 that have the lowest ammount of agents
        else
        {
            //Return green space 3
            return greenSpace3;
        }

    }

    /// <summary>
    /// Update the lowest variable to check withc green space have less agents
    /// </summary>
    private void GetAmmount()
    {
        //Get the ammount of agents in every green space
        _greenSpace1 = greenSpace1.GetComponent<GreenSpace>().ammountOfAgents;
        _greenSpace2 = greenSpace2.GetComponent<GreenSpace>().ammountOfAgents;
        _greenSpace3 = greenSpace3.GetComponent<GreenSpace>().ammountOfAgents;

        //Populate the array with the ammount of agents in every green space
        objectsArray[0] = _greenSpace1;
        objectsArray[1] = _greenSpace2;
        objectsArray[2] = _greenSpace3;

        //Check what is the green space that have the lowest ammount of agents and set that ammount to the lowest variable
        foreach (int i in objectsArray)
        {
            if (i < _lowest)
            {
                _lowest = i;
            }
        }
    }
}
