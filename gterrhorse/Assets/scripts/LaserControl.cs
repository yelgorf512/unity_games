using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test
public class LaserControl : MonoBehaviour {

    public float laser_speed;
    public GameObject Explosion_fx;

    private AudioSource source;
    private AudioClip the_clip;
    private Collider the_collie;

    public int scoreValue;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();
        source.PlayOneShot(the_clip);

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
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
        
        if (the_collie.tag == "enemy")
        {
            Debug.Log("HERE IN THE COLLIE");
            gameController.AddScore(1);
            Destroy(the_collie.gameObject);
        }
        Destroy(this.gameObject);
    }

}
