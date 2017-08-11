using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CubemansControl : MonoBehaviour
{
    private Rigidbody rb;
    //private Rigidbody morerigid;

    private bool jump;
    private bool alt_jump;
    private bool leftButton;
    private bool rightButton;
    private bool wasd_left, wasd_right, wasd_up, wasd_down;

    private float moveHorizontal;
    private float moveVertical;
    private float rotateHorizontal;
    private float rotateVertical;

    public float speed;

    public GameObject shot;
    public Transform the_camera;
    public Transform shotspawn;

    float shot_timer;

    private AudioSource source;
    public AudioClip the_clip;

    private GameController gameController;

    private int bool_to_int(bool the_bool)
    {
        if (the_bool)
        {
            return 1;
        }
        return 0;
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        //morerigid = GetComponent<Rigidbody>();
        jump = false;
        alt_jump = false;
        shot_timer = Time.time;
        //source = GetComponent<AudioSource>();
        //the_clip = GetComponent<AudioClip>();
        
        //source.PlayOneShot(the_clip);

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

    void Update()
    {
        if (!jump && !alt_jump)
        {
            jump = Input.GetButtonDown("Fire1");
            alt_jump = Input.GetButtonDown("Fire2");
        }

        leftButton = Input.GetButton("CustomLButton");
        rightButton = Input.GetButton("CustomRButton");

        wasd_up = Input.GetKey("w");
        wasd_down = Input.GetKey("s");
        wasd_left = Input.GetKey("a");
        wasd_right = Input.GetKey("d");

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        rotateHorizontal = Input.GetAxis("RJoystickX");
        rotateVertical = Input.GetAxis("RJoystickY");


        if (wasd_down)
        {
            moveVertical = -2;
        }
        else if (wasd_up)
        {
            moveVertical = 2;
        }
        

        if (wasd_left)
        {
            moveHorizontal = -2; 
        }
        else if (wasd_right)
        {
            moveHorizontal = 2;
        }

        
        
        float moveUp = 0.0f;
        if (jump || alt_jump)
        {
            //Debug.Log("jumpin");
            moveUp = 75.0f; // jump height
            jump = false;
            alt_jump = false;
        }



        if (rotateHorizontal != 0)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y + rotateHorizontal, rb.rotation.eulerAngles.z);
            //Debug.Log("HERE rotateHorizontal");
        }

        if (rotateVertical != 0)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x + rotateVertical, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
            //Debug.Log("YHERE rotateVertical");
        }

        if (leftButton)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y - 1, rb.rotation.eulerAngles.z);
            //Debug.Log("leuler");
        }

        if (rightButton && Time.time > shot_timer)
        {
            //float distance = Vector3.Distance(shotspawn.position, target.position);
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
            shot_timer = Time.time + 0.1f;
            //Debug.Log("shots fired");
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
            gameController.ResetScore();
        }
    }
    
}
