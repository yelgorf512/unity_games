using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CubemansControl : MonoBehaviour
{
    private Rigidbody rb;
    //private Rigidbody morerigid;

    private bool jump;
    private bool shotButton;
    private bool leftButton;
    private bool rightButton;

    private bool corrected;

    private bool wasd_left, wasd_right, wasd_up, wasd_down;

    private float moveHorizontal;
    private float moveVertical;
    private float rotateHorizontal;
    private float rotateVertical;

    public float speed;
    float shot_timer;

    public GameObject shot;
    public Transform the_camera;
    public Transform shotspawn;

    private Quaternion lastRotation;
    private Quaternion startRotation;
    

    private GameController gameController;

    private int bool_to_int(bool the_bool)
    {
        if (the_bool)
        {
            return 1;
        }
        return 0;
    }

    private void HorseReset()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
        rb.transform.position = new Vector3(0, 3, 0);
        rb.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Start()
    {
        corrected = false;
        rb = GetComponent<Rigidbody>();
 
        jump = false;
        shot_timer = Time.time;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        lastRotation = Quaternion.Euler(rb.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rb.transform.rotation.eulerAngles.z);
        startRotation = lastRotation;
    }

    void Update()
    {
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");            // INPUTS HERE !!!!!!!!!!         
        }

        shotButton = Input.GetButton("Fire1");
        rightButton = Input.GetButton("CustomRButton");

        

        wasd_up = Input.GetKey("w");
        wasd_down = Input.GetKey("s");
        wasd_left = Input.GetKey("a");
        wasd_right = Input.GetKey("d");

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        rotateHorizontal = Input.GetAxis("RJoystickX");
        rotateVertical = Input.GetAxis("RJoystickY");

        if (rotateHorizontal == 0)
        {
            rotateHorizontal = 2 * Input.GetAxis("Mouse X");
        }   // get mouse x

        if (rotateVertical == 0)
        {
            rotateVertical = -2 * Input.GetAxis("Mouse Y");
        }     // get mouse y

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
        if (jump)
        {
            //Debug.Log("jumpin");
            moveUp = 75.0f; // jump height
            jump = false;
        }

        if (rotateHorizontal != 0)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y + rotateHorizontal, rb.rotation.eulerAngles.z);
            //Debug.Log("HERE rotateHorizontal");
        }

        if (rotateVertical != 0)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x + rotateVertical, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z);
        }

        //Debug.Log("ROTATION X " + rb.transform.eulerAngles.x);
        if (!(rb.transform.eulerAngles.x < 80 || rb.transform.eulerAngles.x > 300))
        {
            //Debug.Log("OUT OF ROTATE BOUNDS" + rb.transform.eulerAngles.x);
            rb.rotation = lastRotation;
            corrected = true;
        }
        else
        {
            corrected = false;
        }

        if (shotButton && Time.time > shot_timer)
        {
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
            shot_timer = Time.time + 0.1f;
        }   // make laser

        if (rb.transform.position.y < -2)               // they fell off
        {
            //rb.transform.position = new Vector3(0, 0.5f, 0);
            //rb.velocity = new Vector3(0, 0, 0);
            HorseReset();
        }

        Vector3 movement = transform.forward * Time.deltaTime;
        Vector3 finalvector = movement + new Vector3(moveHorizontal, moveUp, moveVertical);

        if (!corrected)     // if we correc the rotation dont add force
        {
            rb.AddRelativeForce(finalvector * speed);
        }

        lastRotation = Quaternion.Euler(rb.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rb.transform.rotation.eulerAngles.z);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag != "platform")
        {
            HorseReset();
            //rb.transform.position = new Vector3(0, 3, 0);
            //gameController.ResetScore();
        }
    }
    
}
