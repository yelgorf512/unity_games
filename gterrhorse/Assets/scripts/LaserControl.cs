using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

    public float laser_speed;
    public GameObject Explosion_fx;
    private AudioSource source;
    private AudioClip the_clip;
    private Collider the_collie;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();
        source.PlayOneShot(the_clip);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * laser_speed * Time.deltaTime);
        //this_rb = GetComponent<Rigidbody>();
        //this_rb.velocity = Vector3.forward;
    }

    public void OnCollisionEnter(Collision collision)
    {
        the_collie = collision.collider;
        Instantiate(Explosion_fx, transform.position, transform.rotation);
        Destroy(this.gameObject);
        if (the_collie.tag == "Enemy")
        {
            Destroy(the_collie.gameObject);
        }
    }

}
