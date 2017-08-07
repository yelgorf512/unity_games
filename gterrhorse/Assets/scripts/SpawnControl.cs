using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour {

    public GameObject SpawnObject;
    public float SpawnRate;
    public int SpawnMax;
    private float next_spawn_time;
    private GameObject[] spawns;
    private Quaternion the_y_rotate;


	// Use this for initialization
	void Start ()
    {
        next_spawn_time = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        spawns = GameObject.FindGameObjectsWithTag("enemy");
        if (spawns.Length < SpawnMax && Time.time > next_spawn_time)
        {
            the_y_rotate = Random.rotation;
            the_y_rotate.x = 0;
            the_y_rotate.z = 0;
            Instantiate(SpawnObject, transform.position, the_y_rotate);
            next_spawn_time = Time.time + SpawnRate;
        }
		
	}
}
