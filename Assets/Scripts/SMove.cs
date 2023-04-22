using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMove : MonoBehaviour
{
    public float yMoveSpeed = 2f;
    public float xScale = 8f;
    public float xMoveSpeed = 5f;
    public float deadZoneY = -10f;
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
}
