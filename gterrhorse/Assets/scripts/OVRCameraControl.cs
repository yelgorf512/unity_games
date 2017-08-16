using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRCameraControl : MonoBehaviour {

    Transform main_cam_transform;

	// Use this for initialization
	void Start () {
        main_cam_transform = GetComponentInParent<Transform>();
        transform.SetPositionAndRotation(main_cam_transform.position, main_cam_transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(main_cam_transform.position, main_cam_transform.rotation);
	}
}
