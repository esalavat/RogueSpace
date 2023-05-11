using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWaveMovement : MonoBehaviour
{
    public float yMoveSpeed = 3f;
    public float xMoveSpeed = 3f;
    public float legTime = 1f;
    public float deadZoneY = -10f;
    private float timer = 0;
    private int previousXDir = -1;
    private int xDir = 0;
    private int yDir = -1;
    
    void Start() {
        previousXDir = Random.value > .5f ? 1 : -1;
    }

    void Update()
    {

        transform.position += Vector3.up * yMoveSpeed * yDir * Time.deltaTime;
        transform.position += Vector3.right * xMoveSpeed * xDir * Time.deltaTime;
        
        timer += Time.deltaTime;

        if(transform.position.y < deadZoneY) {
            Destroy(gameObject);
        }

        if(timer >= legTime) {
            timer -= legTime;

            yDir = yDir == 0 ? -1 : 0;

            int tempXDir = xDir;
            xDir = xDir == 0 ? previousXDir * -1 : 0;
            previousXDir = tempXDir;
        }

    }
}
