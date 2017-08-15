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

        pan_right = false;
        pan_left = false;

        if (cameraMoveHorizontal > .8f)
        {
            pan_right = true;
            pan_left = false;
            Debug.Log("pan_right");
        }
        else if (cameraMoveHorizontal < -.8f)
        {
            pan_left = true;
            pan_right = false;
            Debug.Log("pan_left");
        }
        else
        {
            Debug.Log("HERE");
            if (pan_left && transform.localPosition.x < 0)
            {
                pan_left = false;
                pan_right = true;
            }
            else if (pan_right && transform.localPosition.x > 0)
            {
                pan_left = true;
                pan_right = false;
            }
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
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x - pan_rate, -3, 3) , transform.localPosition.y, transform.localPosition.z);
        }
        //else if (pan_right && (transform.position.x + pan_rate < 3))
        else if (pan_right)
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + pan_rate, -3, 3), transform.localPosition.y, transform.localPosition.z);
        }
        else if (pan_up)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + pan_rate, transform.localPosition.z);
        }
        else if (pan_down)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - pan_rate, transform.localPosition.z);
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