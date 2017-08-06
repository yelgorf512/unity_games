using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    //private Rigidbody rb;

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        //rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
