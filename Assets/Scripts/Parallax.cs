using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed = 1;
    public float loopbackY = -8;

    void FixedUpdate()
    {
        transform.position = transform.position + Vector3.down * scrollSpeed * Time.deltaTime;
        
        if(transform.position.y < loopbackY) {
            transform.position = transform.position + Vector3.up * 18;
        }
    }
}
