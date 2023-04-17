using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "asteroid") {
            Destroy(gameObject);
        }
    }
}
