using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsParameters : MonoBehaviour
{
    //Reference to collider volume
    private Bounds _bounds;
    // Explosion to instatiate
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        //Get the collider
        _bounds = gameObject.GetComponent<Collider>().bounds;
        //Instatiate an explosion on a random position of the collider
        Instantiate(explosion, _bounds.RandomPositionInBounds(useY: false), Quaternion.identity);
    }
}
