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
    public int coinMaxValue = 50;
    public int enemyHPMax = 10;
    public bool spawnAsteroids = true;
    public bool spawnEnemies = true;

    private float timer = 0;
    private float nextSpawnTime;
    private float nextEnemySpawnTime;
    private float spawnSpeedAcceleration = .001f;
    private int coinValue = 1;
    private int enemyHP = 1;

    void Start()
    {
        spawnAsteroid();
        nextSpawnTime = getNextAsteroidSpawnTime();
        nextEnemySpawnTime = 20;
    }

    void FixedUpdate()
    {
        if(timer > nextSpawnTime) {
            spawnAsteroid();
            nextSpawnTime = getNextAsteroidSpawnTime();
        }

        if(timer > nextEnemySpawnTime) {
            spawnEnemy();
            nextEnemySpawnTime = getNextEnemySpawnTime();
        }
        
        if(!LogicManagerScript.bossFight) {
            updateCoinValue();
            updateEnemyHP();
            timer+=Time.deltaTime;
        }
    }

    private void updateCoinValue() {
        coinValue = Mathf.Min(coinMaxValue, (int)(timer / 40) + 1);
    }

    private void updateEnemyHP() {
         enemyHP = Mathf.Min(enemyHPMax, (int)(timer / 60) + 1);
    }

    private void spawnAsteroid() {
        if(spawnAsteroids) {
            int randomIndex = Random.Range(0, asteroids.Length);
            float leftestPoint = transform.position.x - horizontalOffset;
            float rightestPoint = transform.position.x + horizontalOffset;

            var asteroid = Instantiate(asteroids[randomIndex], new Vector3(Random.Range(leftestPoint, rightestPoint), transform.position.y, 0), transform.rotation);
            asteroid.GetComponent<EnemyScript>().coinValue = coinValue;
        }
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
        if(spawnEnemies) {
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
            newEnemy.GetComponent<EnemyScript>().coinValue = coinValue;
        }
    }

    private float getNextEnemySpawnTime() {
        float enemySpawnSpeed = ((timer-20)*.001f * -1) + 5f;
        float output = timer + System.Math.Max(enemySpawnSpeed + Random.Range(spawnRandomness*-1, spawnRandomness), minSpawnSpeed);

        return output;
    }
}
