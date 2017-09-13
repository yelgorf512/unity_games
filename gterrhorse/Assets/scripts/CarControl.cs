using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}


public class CarControl : MonoBehaviour {

    private Rigidbody rb;

    public ParticleSystem The_parts;
    private float speed;
   
    private AudioSource source;
    private AudioClip the_clip;

    private float destroy_time;

    private int stopped_count;
    private int stopped_limit;
    private float stopped_time;
    private float stopped_time_cutoff;
    private float velocity_cutoff;

    private bool correcting;
    private bool starting_up;
    private float correcting_time;

    private float flip_correction = 0f;
    
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        the_clip = GetComponent<AudioClip>();

        rb = GetComponent<Rigidbody>();

        destroy_time = 0;
        stopped_time = 0;
        stopped_time_cutoff = Time.time + 5;

        speed = 500;

        stopped_count = 0;
        stopped_limit = 5;
        velocity_cutoff = .3f;

        starting_up = true;
        correcting = false;
        correcting_time = 0;
    }
    
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
        //visualWheel.transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, 0);
    }

    public void FixedUpdate()
    {
        //float motor = maxMotorTorque;
        float motor = 0;
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //Debug.Log("correcting: " + correcting);
        //Debug.Log("starting_up: " + starting_up);
        
        //Debug.Log("stopped_count" + stopped_count);
        //Debug.Log("rb velocity z" + rb.velocity.z + " rb pt velocity z" + rb.GetPointVelocity(new Vector3(0, 0, 0)).z);
        //Debug.Log("rb pt velocity z" + rb.GetPointVelocity(new Vector3(0, 0, 0)).z);

        if (!correcting)
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }

            if (Mathf.Abs(rb.velocity.z) < velocity_cutoff && starting_up == false)
            {
                stopped_count++;

                if (stopped_count > stopped_limit)
                {
                    stopped_count = 0;

                    /*
                    if (Mathf.Abs(transform.eulerAngles.z) > 160f)
                    {
                        flip_correction = -(transform.eulerAngles.z);
                    }
                    */
                    
                    //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180.0f, transform.eulerAngles.z + flip_correction); stopped_count = 0;

                    //rb.AddForce(transform.forward * -500 * Time.deltaTime);
                    //rb.AddRelativeForce(transform.up * 50000);
                    


                    correcting_time = Time.time + 5f;
                    correcting = true;

                    //Debug.Break();

                    rb.AddRelativeForce(transform.forward * -15000, ForceMode.Impulse);
                    rb.AddForce(transform.up * 20000, ForceMode.Impulse);
                    rb.AddTorque(transform.up * 5000, ForceMode.Impulse);
                }
            }
            else
            {
                if (starting_up == true && Mathf.Abs(rb.velocity.z) > 10)
                {
                    starting_up = false;
                }
                stopped_count = 0;
                stopped_time_cutoff = Time.time + 2f;
                rb.AddForce(transform.forward * 10000);
            }

            /* BAD HACK */
            if (transform.position.y < -15)
            {
                //transform.position = new Vector3(transform.position.x, 10, transform.position.z);
                Destroy(gameObject);
            }
            //*/
        }

        else    // correcting
        {
            if (Time.time > stopped_time_cutoff)
            {
                correcting = false;
                starting_up = true;
            }
                
        //rb.AddForce(transform.forward * -1);
            //rb.AddRelativeForce(new Vector3(0, 0, -1000) * Time.deltaTime);
        }
    }

    private void Update()
    {   
        /*if (destroy_time != 0 && Time.time > destroy_time)
        {
            Destroy(gameObject);
        }*/
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "player_weapon")
        //if (collision.collider.tag == "player_weapon" || transform.position.y < -3000) ;
        {
            Instantiate(The_parts, transform.position, transform.rotation);
            source.PlayOneShot(the_clip);
            Destroy(gameObject);
        }
        //destroy_time = Time.time + .1f;
    }
}