using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float deadZoneY = -10f;
    public int value = 1;

    public TMP_Text coinValuePrefab;

    void Update()
    {
        transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
        
        if(transform.position.y < deadZoneY) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "ship") {
            spawnCoinAmount(value);
            Destroy(gameObject);
            EventManager.CoinCollected(value);
        }
    }

    void spawnCoinAmount(int value) {
        if(value > 1) {
            var coinValueDisplay = Instantiate(coinValuePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            coinValueDisplay.text = value + "x";
        }
    }
}
