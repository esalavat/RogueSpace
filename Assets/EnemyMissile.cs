using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 12f;
    public float delay = 1f;
    public float randomness = 0f;
    public GameObject missile;
    public bool weaponEnabled = true;
    private float timer = 0;
    
    void Update()
    {
        if(timer >= delay) {
            timer -= delay + Random.Range(0, randomness);
            if(weaponEnabled && transform.position.y > -4) {
                fireMissile();
            }
        } else {
            timer += Time.deltaTime;
        }
    }

    private void fireMissile() {
        float angle = Random.Range(140, 220);
        
        var newMissile = Instantiate(missile, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.Euler(0, 0, angle));
        Destroy(newMissile, 4);
    }
}
