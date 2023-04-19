using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float deadZoneY = -10f;
    public int value = 1;

    void Update()
    {
        transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
        
        if(transform.position.y < deadZoneY) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "ship") {
            Destroy(gameObject);
            EventManager.CoinCollected(value);
        }
    }
}
