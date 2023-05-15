using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAngle : MonoBehaviour
{
    public float yMoveSpeed = 3f;
    public float zagTime = 2f;
    public float deadZoneY = -10f;

    private float timer = 0;
    private int xDirection;
    

    void Start()
    {
        xDirection = transform.position.x > 0 ? -1 : 1;
    }

    void Update()
    {
        if(timer < zagTime) {
            transform.position += Vector3.down * yMoveSpeed * Time.deltaTime;
        } else {
            transform.position += Vector3.up * yMoveSpeed * Time.deltaTime;
            transform.position += Vector3.right * yMoveSpeed * xDirection * Time.deltaTime;
            if(timer > zagTime + (zagTime * .5)) {
                timer = 0;
            }
        }

        if(transform.position.y < deadZoneY) {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
    }
}
