using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpaceManager : MonoBehaviour
{

    public GameObject greenSpace1;
    public GameObject greenSpace2;
    public GameObject greenSpace3;

    private int _greenSpace1;
    private int _greenSpace2;
    private int _greenSpace3;

    private int[] objectsArray;

    public void Start()
    {
        objectsArray = new int[3];
        
    }

   
    public GameObject GiveGreenSpaceToAgent()
    {
        _greenSpace1 = greenSpace1.GetComponent<GreenSpace>().ammountOfAgents;
        _greenSpace2 = greenSpace2.GetComponent<GreenSpace>().ammountOfAgents;
        _greenSpace3 = greenSpace3.GetComponent<GreenSpace>().ammountOfAgents;

        objectsArray[0] = _greenSpace1;
        objectsArray[1] = _greenSpace2;
        objectsArray[2] = _greenSpace3;

        int lowest = objectsArray[0];
        foreach (int i in objectsArray)
        {
            if (i < lowest)
            {
                lowest = i;
            }
        }

        if (lowest == _greenSpace1)
        {
            return greenSpace1;
        }
        else if (lowest == _greenSpace2)
        {
            return greenSpace2;
        }
        else
        {
            return greenSpace3;
        }
    }
}
