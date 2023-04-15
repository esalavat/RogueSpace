using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidSpawerScript : MonoBehaviour
{
    public GameObject[] asteroids;
    public float horizontalOffset = 10;
    public float spawnSpeed = 2;
    public float minSpawnSpeed = 0.5f;
    public float spawnRandomness = 1;
    private float timer = 0;
    private float nextSpawnTime;
    private float spawnSpeedAcceleration = .001f;

    void Start()
    {
        spawnAsteroid();
        nextSpawnTime = getNextSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > nextSpawnTime) {
            spawnAsteroid();
            timer = 0;
            nextSpawnTime = getNextSpawnTime();
        } else {
            timer+=Time.deltaTime;
        }
    }

    private void spawnAsteroid() {
        int randomIndex = Random.Range(0, asteroids.Length);
        float leftestPoint = transform.position.x - horizontalOffset;
        float rightestPoint = transform.position.x + horizontalOffset;

        Instantiate(asteroids[randomIndex], new Vector3(Random.Range(leftestPoint, rightestPoint), transform.position.y, 0), transform.rotation);
    }

    private float getNextSpawnTime() {
        spawnSpeed = System.Math.Max(spawnSpeed - spawnSpeedAcceleration, 0);
        spawnSpeedAcceleration += .002f;
        float output = System.Math.Max(spawnSpeed + Random.Range(spawnRandomness*-1, spawnRandomness), minSpawnSpeed);

        Debug.Log(
            "spawnSpeed: " + spawnSpeed
            + " spawnSpeedAcceleration: " + spawnSpeedAcceleration
            + " nextSpawnTime: " + output
        );

        return output;
    }
}
