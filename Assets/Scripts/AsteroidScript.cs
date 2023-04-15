using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float deadZoneY = -10f;
    public float minSpin = -25;
    public float maxSpin = 25;

    private float spinSpeed;

    void Start() {
        spinSpeed = Random.Range(minSpin, maxSpin);
    }

    void Update()
    {
        transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

        if(transform.position.y < deadZoneY) {
            Destroy(gameObject);
        }
    }
}
