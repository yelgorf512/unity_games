using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;

    private Vector3 origin_position;

    private float pan_x_offset;
    private float pan_x_offset_change;
    private float pan_rate;
    private float pan_limit;

    private float cameraMoveHorizontal;
   
    void Start()
    {
        pan_x_offset = 0;
        pan_rate = 0.3f;
        origin_position = transform.localPosition;
    }

    private void Update()
    {
        cameraMoveHorizontal = Input.GetAxis("Horizontal");
    }
    
    void LateUpdate()
    {
        if (Mathf.Abs(cameraMoveHorizontal) > .6f)
        {
            pan_x_offset_change = -cameraMoveHorizontal;
        }
        else
        {
            if (pan_x_offset < -0.3f)
            {
                pan_x_offset += pan_rate;
            }
            else if (pan_x_offset > 0.3f)
            {
                pan_x_offset -= pan_rate;
            }
            else
            {
                pan_x_offset = 0;
            }
        }

        transform.LookAt(transform);
        
        pan_x_offset += pan_x_offset_change * pan_rate;

        pan_x_offset = Mathf.Clamp(pan_x_offset, -3, 3);

        transform.localPosition = new Vector3(origin_position.x + pan_x_offset, transform.localPosition.y, transform.localPosition.z);

        /*
        Debug.Log("pan_x_offset " + pan_x_offset);
        Debug.Log("pan_x_offset_change " + pan_x_offset_change);
        Debug.Log("origin_position.x " + origin_position.x);
        Debug.Log("pan_x_offset_change " + pan_x_offset_change);
        Debug.Log("cameraMoveHorizontal" + cameraMoveHorizontal);
        */
    }
}