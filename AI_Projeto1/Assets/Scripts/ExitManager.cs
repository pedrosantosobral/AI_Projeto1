using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public bool extraExit;
    public GameObject exit;
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        if (extraExit == true)
        {
            exit.SetActive(true);
            wall.SetActive(false);
        }
    }
}
