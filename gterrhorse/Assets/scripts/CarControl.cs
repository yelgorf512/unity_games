using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour {


    private Rigidbody rb;

    public ParticleSystem The_parts;
    public float speed;
    public int stop_limit;

    private AudioSource source;
    private AudioClip the_clip;

    private float destroy_time;
    private int stopped_count;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward);
        destroy_time = 0;
        stopped_count = 0;
    }

    private void Update()
    {
        rb.AddForce(transform.forward * speed * Time.deltaTime);
        if (rb.velocity.x < .1f && rb.velocity.z < .1f)
        {
            stopped_count++;
            if (stopped_count > stop_limit)
            {
                stopped_count = 0;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180.0f, transform.eulerAngles.z); stopped_count = 0;
                rb.AddForce(transform.forward * speed * Time.deltaTime);
            }
        }
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
