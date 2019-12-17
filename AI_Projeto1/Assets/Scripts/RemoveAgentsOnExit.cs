using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to destroy agents on exit
/// </summary>
public class RemoveAgentsOnExit : MonoBehaviour
{
    /// <summary>
    /// If a agent touched the collider destroy it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            //Destroy agent
            Destroy(other.gameObject);
        }
    }

}
