using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float baseMoveSpeed = 2f;
    public float moveSpeedVariation = 1f;
    public float deadZoneY = -10f;
    public float maxSpin = 25;
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
}
