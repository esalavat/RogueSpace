using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    public int coinValue = 10;
    public int numCoins = 20;
    public bool immune = true;

    public GameObject coin;
    public GameObject explosion;

    private Image healthBar;
    private Health health;

    void Start()
    {
        var bar = transform.Find("HealthBarCanvas/HealthBar/Bar");
        Debug.Log(bar);
        healthBar = bar.GetComponent<Image>();
        health = transform.GetComponent<Health>();
        Debug.Log("Boss1 currentHP: " + health.currentHp);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)health.currentHp / health.maxHp;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(!immune && collider.gameObject.tag == "laser") {
            Destroy(collider.gameObject);
            health.currentHp--;
            if(health.currentHp.Equals(0)) {
                float x = transform.position.x;
                float y = transform.position.y;
                Destroy(gameObject);
                SpawnCoins(x, y);
                SpawnExplosion(x, y);
                EventManager.Boss1End();
            }
        }
    }

    void SpawnCoins(float x, float y) {
        for(int i=0; i<numCoins; i++) {
            float thisY = Mathf.Clamp(y + Random.Range(-2, 2), -3, 3);
            float thisX = Mathf.Clamp(x + Random.Range(-2, 2), -3, 3);
            var newCoin = Instantiate(coin, new Vector3(thisX, thisY, 0), Quaternion.identity);
            newCoin.GetComponent<CoinScript>().value = coinValue;
        }
    }

    void SpawnExplosion(float x, float y) {
        if(explosion != null) {
            GameObject newExplosion = Instantiate(explosion, new Vector3(x, y, 0), Quaternion.identity);
            Destroy(newExplosion, 2);
        }
    }
}
