using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour {


    private Rigidbody rb;

    public ParticleSystem The_parts;
    public float speed;

    private AudioSource source;
    private AudioClip the_clip;

    private float destroy_time;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward);
        destroy_time = 0;
       
    }

    private void Update()
    {
        //transform.position.Set(transform.position.x, .1f, transform.position.z);
        //transform.position += transform.forward;
        rb.AddForce(transform.forward * speed * Time.deltaTime);
        /*if (destroy_time != 0 && Time.time > destroy_time)
        {
            Destroy(gameObject);
        }*/
    }

    public void OnCollisionEnter(Collision collision)
    {   
        if (collision.collider.tag == "player_weapon")
        {
            Instantiate(The_parts, transform.position, transform.rotation);
            source.PlayOneShot(the_clip);
            Destroy(gameObject);
        }
        //destroy_time = Time.time + .1f;
       


    }
}
