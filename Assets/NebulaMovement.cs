using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaMovement : MonoBehaviour
{
    public float rotateSpeed = .5f;
    public float radius = 12;
    public bool clockwise = true;
    private float angle;
    private Vector2 center;
    private float alpha;
    private bool fadedIn = false;

    void Start() {
        center = transform.position;
        fadedIn = false;
        var color = transform.GetComponent<SpriteRenderer>().color;
        alpha = color.a;
        transform.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }

    void Update()
    {
        if(LogicManagerScript.nebulaOn) {
            
            if(!fadedIn) {
                var color = transform.GetComponent<SpriteRenderer>().color;
                if(color.a < alpha) {
                    transform.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, color.a + (.01f * Time.deltaTime));
                } else {
                    fadedIn = true;
                }
            }

            angle += rotateSpeed * Time.deltaTime * (clockwise ? 1 : -1);
    
            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
            transform.position = center + offset;
        }
    }
}
