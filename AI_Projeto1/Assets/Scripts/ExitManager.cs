using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the extra exit
/// </summary>
public class ExitManager : MonoBehaviour
{
    /// <summary>
    /// bool too check if there is an extra exit at the start of the simulation
    /// </summary>
    public bool extraExit;

    /// <summary>
    /// exit gameobject reference
    /// </summary>
    public GameObject exit;

    /// <summary>
    /// wall gameobject reference
    /// </summary>
    public GameObject wall;

   /// <summary>
   /// Activate or desactivate the extra exit at the start.
   /// </summary>
    void Start()
    {
        //if extraExit is true
        if (extraExit == true)
        {
            //activate the extra exit
            exit.SetActive(true);
            //remove the wall
            wall.SetActive(false);
        }
    }
}
