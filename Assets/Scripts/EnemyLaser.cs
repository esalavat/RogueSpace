using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float speed = 12f;
    public float delay = 1f;
    public float randomness = 0f;
    public GameObject laser;
    public bool laserEnabled = false;
    private float timer = 0;
    
    void Update()
    {
        if(timer >= delay) {
            timer -= delay + Random.Range(0, randomness);
            if(laserEnabled) {
                GameObject newLaser = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
                newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
                Destroy(newLaser, 2);
            }
        } else {
            timer += Time.deltaTime;
        }
    }
}
