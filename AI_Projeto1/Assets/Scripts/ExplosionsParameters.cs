using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls the explosions behavior
/// </summary>
public class ExplosionsParameters : MonoBehaviour
{
    /// <summary>
    /// Reference to collider volume
    /// </summary>
    private Bounds _bounds;

    /// <summary>
    /// Bool to check if a explosion have started
    /// </summary>
    private bool   _fireHaveStarted;

    /// <summary>
    /// Fire gameobject
    /// </summary>  
    public GameObject fire;

    /// <summary>
    /// Stun radius gameobject
    /// </summary>
    public GameObject stun;

    /// <summary>
    /// Panic radius gameobject
    /// </summary>
    public GameObject panic;

    /// <summary>
    /// Fire gameobject reference
    /// </summary>  
    private GameObject _fireReference;

    /// <summary>
    /// Stun radius gameobject reference
    /// </summary>
    private GameObject _stunReference;

    /// <summary>
    /// Panic radius gameobject reference
    /// </summary>
    private GameObject _panicReference;

    /// <summary>
    /// Explosion size parameter
    /// </summary>
    public float explosionSize;

    /// <summary>
    /// Explosion stun multiplier parameter
    /// </summary>
    public float stunExplosionMultilplier;

    /// <summary>
    /// Fire propagation speed parameter
    /// </summary>
    public float firePropagationSpeed;

    /// <summary>
    /// Initial explosion size vector
    /// </summary>
    private Vector3 _inicalExplosionSize;

    /// <summary>
    /// Fire propagation speed vector
    /// </summary>
    private Vector3 _fireSpeedVector;

    /// <summary>
    /// Vector for the random position of the explosion
    /// </summary>
    private Vector3 _randomPosition;

    /// <summary>
    /// Method to set the diferente parameters and get the explosion collider
    /// </summary>
    private void Start()
    {
        _fireHaveStarted = false;

        _inicalExplosionSize = new Vector3(explosionSize, explosionSize, explosionSize);
        _fireSpeedVector    = new Vector3(firePropagationSpeed, firePropagationSpeed, firePropagationSpeed);

        //Get the collider
        _bounds = gameObject.GetComponent<Collider>().bounds;
        _randomPosition = _bounds.RandomPositionInBounds(useY: false);

    }
    /// <summary>
    /// Method to check if a explosion was initiated,instatiate the diferent explosion components and increase the fire size with time
    /// </summary>
    private void Update()
    {
        //on spacebar press
        if (Input.GetKeyDown("space"))
        {
            //if there is no fires
            if(_fireHaveStarted == false)
            {
                //Instatiate an explosion on a random position of the collider
                _fireReference = Instantiate(fire, _randomPosition, Quaternion.identity);
                _stunReference = Instantiate(stun, _randomPosition, Quaternion.identity);
                _panicReference = Instantiate(panic, _randomPosition, Quaternion.identity);
            }
            
        }
        //if fire reference was found
        if (_fireReference != null)
        {
            //if there is no fire initiated
            if (_fireHaveStarted == false)
            {
                //set the explosion size
                _fireReference.transform.localScale = _inicalExplosionSize;
                _stunReference.transform.localScale = _inicalExplosionSize * stunExplosionMultilplier;
                _panicReference.transform.localScale = _inicalExplosionSize * (stunExplosionMultilplier * 2);
                //Destroy stun radius game object after 1 second of instatiation
                Invoke("DestroyStunRadius", 1f);
            }
        }
        //if explosion was initiated
        if(_fireHaveStarted == true)
        {
            //if fire is not null
            if(_fireReference != null)
            {
                //increase fire size
                _fireReference.transform.localScale += _fireSpeedVector;
            }
            
            //if panic is not null
            if(_panicReference != null)
            {
                //increase panic size
                _panicReference.transform.localScale += _fireSpeedVector;
            }
            
        }
        
        
    }
    /// <summary>
    /// Method to destroy stun radius game object after a explosion
    /// </summary>
    private void DestroyStunRadius()
    {
        //Destroy the stun gameobject reference
        Destroy(_stunReference);
        //Initiate a fire 
        _fireHaveStarted = true;
    }


}
