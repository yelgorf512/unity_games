using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    private Quaternion last_player_rotation;
    private Vector3 lookat_holder;
    private float x_offset;
    float cameraMoveHorizontal;
    bool spec_cameraMoveHorizontal;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        x_offset = 0;
        last_player_rotation = player.transform.rotation;
    }

    private void Update()
    {
        cameraMoveHorizontal = Input.GetAxis("RJoystickX");
        spec_cameraMoveHorizontal = Input.GetButtonDown("Fire1");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Transform old_transform = transform;
        

        cameraMoveHorizontal = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(cameraMoveHorizontal) > .5f)
        {
            Debug.Log("should be moving camera" + x_offset);
            x_offset += cameraMoveHorizontal * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + x_offset, old_transform.position.y, old_transform.position.z);
        }
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
        }
        
        transform.LookAt(transform);

    }
}