using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float speed = 12f;
    public float delay = 1f;
    public GameObject laser;
    public bool enabled = false;
    private float timer = 0;
    private List<GameObject> lasers = new List<GameObject>();

    void Update()
    {
        if(enabled && timer >= delay) {
            Debug.Log("enemy laser");
            timer -= delay;
            GameObject newLaser = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
            newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            Destroy(newLaser, 2);
        } else {
            timer += Time.deltaTime;
        }
    }
}
