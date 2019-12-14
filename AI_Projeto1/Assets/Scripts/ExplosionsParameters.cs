using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsParameters : MonoBehaviour
{
    //Reference to collider volume
    private Bounds _bounds;
    private bool   _fireHaveStarted;

    //Explosion objects to instatiate
    public GameObject fire;
    public GameObject stun;
    public GameObject panic;
    
    //Reference to explosion objects
    private GameObject _fireReference;
    private GameObject _stunReference;
    private GameObject _panicReference;

    //propagation speed variables
    public float explosionSize;
    public float stunExplosionMultilplier;
    public float firePropagationSpeed;


    private Vector3 _inicalExplosionSize;
    private Vector3 _fireSpeedVector;

    private Vector3 _randomPosition;

    // Start is called before the first frame update
    void Start()
    {
        _fireHaveStarted = false;

        _inicalExplosionSize = new Vector3(explosionSize, explosionSize, explosionSize);
        _fireSpeedVector    = new Vector3(firePropagationSpeed, firePropagationSpeed, firePropagationSpeed);

        //Get the collider
        _bounds = gameObject.GetComponent<Collider>().bounds;
        _randomPosition = _bounds.RandomPositionInBounds(useY: false);

    }

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

        if (_fireReference != null)
        {
            if (_fireHaveStarted == false)
            {
                _fireReference.transform.localScale = _inicalExplosionSize;
                _stunReference.transform.localScale = _inicalExplosionSize * stunExplosionMultilplier;
                _panicReference.transform.localScale = _inicalExplosionSize * (stunExplosionMultilplier * 2);
                Invoke("DestroyStunRadius", 1f);
            }
        }

        if(_fireHaveStarted == true)
        {
            if(_fireReference != null)
            {
                _fireReference.transform.localScale += _fireSpeedVector;
            }
            
            if(_panicReference != null)
            {
                _panicReference.transform.localScale += _fireSpeedVector * 4f;
            }
            
        }
        
        
    }

    private void DestroyStunRadius()
    {
        Destroy(_stunReference);
        _fireHaveStarted = true;
    }


}
