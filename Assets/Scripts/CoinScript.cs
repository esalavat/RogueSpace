using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float deadZoneY = -10f;
    public int value = 1;
    public float magnetRange = 1.5f;
    public float magnetSpeed = 5f;

    public TMP_Text coinValuePrefab;
    private GameObject ship;

    private bool suckedIn = false;
    private bool hasMagnet = false;

    void Start() {
        ship = GameObject.Find("Ship");
        hasMagnet = GameStateManager.Instance.hasUpgrade(Upgrade.Magnet1) || GameStateManager.Instance.hasUpgrade(Upgrade.Magnet2);
        if(GameStateManager.Instance.hasUpgrade(Upgrade.Magnet2)) {
            magnetRange = 2.5f;
        }
    }

    void Update()
    {
        if(!hasMagnet || !suckedIn) {
            transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
        } else {
            var direction = (ship.transform.position - transform.position);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * magnetSpeed;
        }

        checkMagnet();

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

    void checkMagnet() {
        if(hasMagnet && !suckedIn) {
            var distanceToShip = Vector3.Distance(gameObject.transform.position, ship.transform.position);
            if(distanceToShip < magnetRange) {
                suckedIn = true;
            }
        }
    }

    void spawnCoinAmount(int value) {
        if(value > 1) {
            var coinValueDisplay = Instantiate(coinValuePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            coinValueDisplay.text = value + "x";
        }
    }
}
