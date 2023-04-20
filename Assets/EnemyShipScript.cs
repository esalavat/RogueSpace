using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{
    public float yMoveSpeed = 2f;
    public float xScale = 8f;
    public float xMoveSpeed = 5f;
    public int scoreValue = 1;
    public float coinChance = .5f;
    public float deadZoneY = -10f;
    public GameObject coin;
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * yMoveSpeed * Time.deltaTime;
        transform.position += Vector3.right * xScale * Time.deltaTime * Mathf.Sin(timer * xMoveSpeed);
        timer += Time.deltaTime;

        if(transform.position.y < deadZoneY) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "laser") {
            float x = transform.position.x;
            float y = transform.position.y;
            Destroy(gameObject);
            SpawnCoin(x, y);
            EventManager.EnemyDestroyed(scoreValue);
        }
    }

    void SpawnCoin(float x, float y) {
        float rand = Random.value;
        if(rand <= coinChance) {
            Instantiate(coin, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
