using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;
    private Vector3 lookat_holder;
    private Vector3 origin_position;

    private float pan_x_offset;
    private float pan_x_offset_change;
    private float pan_rate;
    private float pan_limit;

    private float cameraMoveHorizontal;
    

    private bool pan_left, pan_right, pan_up, pan_down;
    private bool correcting;

    
    // Use this for initialization
    void Start()
    {
        pan_x_offset = 0;
        pan_rate = 0.3f;
        origin_position = transform.localPosition;
        //correcting = false;
    }

    private void Update()
    {
        cameraMoveHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(cameraMoveHorizontal);

        

        /*
        if (Mathf.Abs(cameraMoveHorizontal) > .8f)
        {
            if (cameraMoveHorizontal < .8f)
            {
                pan_right = true;
                pan_left = false;
                Debug.Log("contoller pan_right");
            }
            else if (cameraMoveHorizontal > -.8f)
            {
                pan_left = true;
                pan_right = false;
                Debug.Log("controller pan_left");
            }
        }
        
        else
        {
            Debug.Log("HERE");
            if (pan_left && transform.localPosition.x < 0 && !correcting)
            {
                Debug.Log("correcting by pan right");
                pan_left = false;
                pan_right = true;
                correcting = true;
            }
            else if (pan_right && transform.localPosition.x > 0 && !correcting)
            {
                Debug.Log("correcting by pan left");
                pan_left = true;
                pan_right = false;
                correcting = true;
            }
            else if (!correcting)
            {
                pan_right = false;
                pan_left = false;
            }
            else
            {
                correcting = false;
            }
        }*/
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (Mathf.Abs(cameraMoveHorizontal) > .8f)
        {
            pan_x_offset_change = -cameraMoveHorizontal;
        }
        else
        {
            if (pan_x_offset < -0.3f)
            {
                pan_x_offset += 1;
            }
            else if (pan_x_offset > 0.3f)
            {
                pan_x_offset -= 1;
            }
            else
            {
                pan_x_offset = 0;
            }
        }

        transform.LookAt(transform);
        
        pan_x_offset += pan_x_offset_change * pan_rate;

        pan_x_offset = Mathf.Clamp(pan_x_offset, -3, 3);

        transform.localPosition = new Vector3(Mathf.Clamp(origin_position.x + pan_x_offset, -3, 3), transform.localPosition.y, transform.localPosition.z);

        Debug.Log("pan_x_offset " + pan_x_offset);
        Debug.Log("pan_x_offset_change " + pan_x_offset_change);
        Debug.Log("origin_position.x " + origin_position.x);
        Debug.Log("pan_x_offset_change " + pan_x_offset_change);
        Debug.Log("cameraMoveHorizontal" + cameraMoveHorizontal);


        /*
        if (pan_left)
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x - pan_rate, -3, 3) , transform.localPosition.y, transform.localPosition.z);
        }
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
        }*/
    }
}