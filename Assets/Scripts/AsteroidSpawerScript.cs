using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidSpawerScript : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject[] enemies;
    public float horizontalOffset = 10;
    public float spawnSpeed = 2;
    public float minSpawnSpeed = 0.5f;
    public float spawnRandomness = 1;
    private float timer = 0;
    private float nextSpawnTime;
    private float nextEnemySpawnTime;
    private float spawnSpeedAcceleration = .001f;

    void Start()
    {
        spawnAsteroid();
        nextSpawnTime = getNextAsteroidSpawnTime();
        nextEnemySpawnTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > nextSpawnTime) {
            spawnAsteroid();
            nextSpawnTime = getNextAsteroidSpawnTime();
        }

        if(timer > nextEnemySpawnTime) {
            spawnEnemy();
            nextEnemySpawnTime = getNextEnemySpawnTime();
        }
        
        timer+=Time.deltaTime;
    }

    private void spawnAsteroid() {
        int randomIndex = Random.Range(0, asteroids.Length);
        float leftestPoint = transform.position.x - horizontalOffset;
        float rightestPoint = transform.position.x + horizontalOffset;

        Instantiate(asteroids[randomIndex], new Vector3(Random.Range(leftestPoint, rightestPoint), transform.position.y, 0), transform.rotation);
    }

    private float getNextAsteroidSpawnTime() {
        spawnSpeed = System.Math.Max(spawnSpeed - spawnSpeedAcceleration, 0);
        spawnSpeedAcceleration += .002f;
        float output = timer + System.Math.Max(spawnSpeed + Random.Range(spawnRandomness*-1, spawnRandomness), minSpawnSpeed);

        // Debug.Log(
        //     "spawnSpeed: " + spawnSpeed
        //     + " spawnSpeedAcceleration: " + spawnSpeedAcceleration
        //     + " nextSpawnTime: " + output
        // );

        return output;
    }

    private void spawnEnemy() {
        int enemiesLength = enemies.Length;
        if(timer < 40) {
            enemiesLength--;
        }

        bool enemiesShoot = false;
        if(timer > 80) {
            enemiesShoot = true;
        }

        int randomIndex = Random.Range(0, enemiesLength);
        float leftestPoint = transform.position.x - horizontalOffset;
        float rightestPoint = transform.position.x + horizontalOffset;

        GameObject newEnemy = Instantiate(enemies[randomIndex], new Vector3(Random.Range(leftestPoint, rightestPoint), transform.position.y, 0), transform.rotation);
        newEnemy.GetComponent<EnemyLaser>().laserEnabled = enemiesShoot;
    }

    private float getNextEnemySpawnTime() {
        float enemySpawnSpeed = ((timer-20)*.001f * -1) + 5f;
        float output = timer + System.Math.Max(enemySpawnSpeed + Random.Range(spawnRandomness*-1, spawnRandomness), minSpawnSpeed);

        return output;
    }
}
