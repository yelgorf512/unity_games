using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour {

    private ParticleSystem pb;
    private AudioSource source;
    private AudioClip the_clip;

    private float start_time;
	// Use this for initialization
	void Start () {
        
        pb = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();
        start_time = Time.time;
        source.PlayOneShot(the_clip);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //bool active_part = pb.IsAlive();

        //if (!active_part)
        if (Time.time > start_time + 1)
        {
           
            Destroy(gameObject);
        }
        
	}
}
