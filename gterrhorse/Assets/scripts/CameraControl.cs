using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    
    private Vector3 lookat_holder;
    private float x_offset;
    private float pan_rate;
    private float cameraMoveHorizontal;
    private bool spec_cameraMoveHorizontal;

    private bool pan_left, pan_right, pan_up, pan_down;



    Transform old_transform;

    // Use this for initialization
    void Start()
    {
        x_offset = 0;
    }

    private void Update()
    {
        cameraMoveHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(cameraMoveHorizontal);
        if (cameraMoveHorizontal > .3f)
        {
            pan_right = true;
            pan_left = false;
            Debug.Log("pan_right");
        }
        if (cameraMoveHorizontal < - .3f)
        {
            pan_left = true;
            pan_right = false;
            Debug.Log("pan_left");
        }
    }
    

    // Update is called once per frame
    void LateUpdate()
    {

        old_transform = transform;

        //cam_position_x restricted between -6 and 6
        transform.LookAt(transform);

        pan_rate = 0.05f;

        //if (pan_left && (transform.position.x - pan_rate > -3))
        if (pan_left)
        {
            transform.position = new Vector3(transform.position.x - pan_rate , transform.position.y, transform.position.z);
        }
        //else if (pan_right && (transform.position.x + pan_rate < 3))
        else if (pan_right)
        {
            transform.position = new Vector3(transform.position.x + pan_rate, transform.position.y, transform.position.z);
        }
        else if (pan_up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + pan_rate, transform.position.z);
        }
        else if (pan_down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - pan_rate, transform.position.z);
        }
        //cameraMoveHorizontal = Input.GetAxis("Horizontal");

        /*cameraMoveHorizontal = 1;
        
        if (Mathf.Abs(cameraMoveHorizontal) < .5f)
        {
            Debug.Log("should be moving camera" + x_offset);
            x_offset += cameraMoveHorizontal;
            transform.position = new Vector3(transform.position.x + Mathf.Clamp(x_offset, -.1f, .1f), old_transform.position.y, old_transform.position.z);
        }*/
        /*
        else if (x_offset > 0)
        {
            Debug.Log("reset x over");
            transform.position = new Vector3(0, old_transform.position.y, old_transform.position.z);
            x_offset = 0;
        }
        else if (x_offset < 0)
        {
            Debug.Log("reset x under");
            transform.position = new Vector3(transform.position.x - x_offset, old_transform.position.y, old_transform.position.z);
            x_offset = 0;
        }*/



    }
}