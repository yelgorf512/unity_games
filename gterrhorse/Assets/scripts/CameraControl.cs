using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    private Quaternion last_player_rotation;
    private Vector3 lookat_holder;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        last_player_rotation = player.transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform);
    }
}