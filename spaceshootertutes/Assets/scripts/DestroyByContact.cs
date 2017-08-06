using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerexplosion;
    public GameObject respawnplayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boundary")
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            Instantiate(playerexplosion, other.transform.position, other.transform.rotation);
            Instantiate(respawnplayer, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(other.gameObject);
        }

        //other.transform.position = new Vector3(0, 0, 0);
        Destroy(gameObject);
    }
}
