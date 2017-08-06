using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;

    private float asteroidtimer;

    void Start()
    {
        asteroidtimer = 0;
        SpawnWaves();
    }

    void SpawnWaves()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(hazard, spawnPosition, spawnRotation);
    }

    void Update()
    {
        if (Time.time > asteroidtimer)
        {
            SpawnWaves();
            asteroidtimer = Time.time + .25f;
        }
    }
}