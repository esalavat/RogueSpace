using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour, IEnemyWeapon
{
    public float speed = 12f;
    public float delay = 1f;
    public float randomness = 0f;
    public bool spreadShot = false;
    public GameObject laser;
    private float timer = 0;

    public bool WeaponEnabled { get; set; } = false;

    void Update()
    {
        if(timer >= delay) {
            timer -= delay + Random.Range(0, randomness);
            if(WeaponEnabled) {
                GameObject newLaser = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
                newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
                Destroy(newLaser, 2);
                
                if(spreadShot) {
                    GameObject newLaser2 = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation * Quaternion.Euler(0, 0, 25));
                    newLaser2.GetComponent<Rigidbody2D>().velocity = newLaser2.transform.up * -1 * speed;
                    Destroy(newLaser2, 2);
                    GameObject newLaser3 = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation * Quaternion.Euler(0, 0, -25));
                    newLaser3.GetComponent<Rigidbody2D>().velocity = newLaser3.transform.up * -1 * speed;
                    Destroy(newLaser3, 2);
                }
            }
        } else {
            timer += Time.deltaTime;
        }
    }
}
