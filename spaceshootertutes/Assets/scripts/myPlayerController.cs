using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class myPlayerController : MonoBehaviour {

    private Rigidbody rb;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotspawn;

    public float tilt;
    public float speed;
    public float firerate;
    private float nextfire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextfire = 0;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.velocity = movement * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);

    }
}
