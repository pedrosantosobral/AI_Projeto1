using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAgentsOnExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            Destroy(other.gameObject);
        }
    }

}
