using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float baseMoveSpeed = 2f;
    public float moveSpeedVariation = 1f;
    public float deadZoneY = -10f;
    public float maxSpin = 25;
    public int scoreValue = 1;
    public float coinChance = .33f;
    public GameObject coin;

    private float spinSpeed;
    private float moveSpeed;

    void Start() {
        moveSpeed = baseMoveSpeed + Random.Range(moveSpeedVariation * -1, moveSpeedVariation);
        spinSpeed = Random.Range(maxSpin * -1, maxSpin);
    }

    void Update()
    {
        transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

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
