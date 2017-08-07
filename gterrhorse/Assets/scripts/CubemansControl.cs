using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CubemansControl : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody morerigid;

    private bool jump;
    private bool leftButton;
    private bool rightButton;

    public float speed;

    public GameObject shot;
    public Transform shotspawn;

    float shot_timer;

    private AudioSource source;
    public AudioClip the_clip;

    void Start()
    {
        Debug.Log("hsfffssf");
        rb = GetComponent<Rigidbody>();
        morerigid = GetComponent<Rigidbody>();
        jump = false;
        shot_timer = Time.time;
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();
        Debug.Log(the_clip);
        source.PlayOneShot(the_clip);
    }

    void Update()
    {
        if (!jump)
        {
            jump = Input.GetButtonDown("Fire1");
        }

        leftButton = Input.GetButton("CustomLButton");
        rightButton = Input.GetButton("CustomRButton");

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float rotateHorizontal = Input.GetAxis("RJoystickX");
        float rotateVertical = Input.GetAxis("RJoystickY");

        float moveUp = 0.0f;
        if (jump)
        {
            Debug.Log("jumpin");
            moveUp = 75.0f; // jump height
            jump = false;
        }

        if (rotateHorizontal != 0)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y + rotateHorizontal, rb.rotation.eulerAngles.z);
            Debug.Log("HERE rotateHorizontal");
        }

        if (rotateVertical != 0)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x + rotateVertical, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
            Debug.Log("YHERE rotateVertical");
        }

        if (leftButton)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y - 1, rb.rotation.eulerAngles.z);
            Debug.Log("leuler");
        }

        if (rightButton && Time.time > shot_timer)
        {
            //float distance = Vector3.Distance(shotspawn.position, target.position);
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
            shot_timer = Time.time + 0.1f;
            Debug.Log("shots fired");
        }

        if (rb.transform.position.y < -2)               // they fell off
        {
            rb.transform.position = new Vector3(0, 0.5f, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }

        Vector3 movement = transform.forward * Time.deltaTime;
        Vector3 finalvector = movement + new Vector3(moveHorizontal, moveUp, moveVertical);
        
        rb.AddRelativeForce(finalvector * speed);



    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag != "platform")
        {
            rb.transform.position = new Vector3(0, 3, 0);
        }
    }
    
}
