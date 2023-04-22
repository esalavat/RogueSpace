using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public int scoreValue = 1;
    public float coinChance = .33f;
    public GameObject coin;

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