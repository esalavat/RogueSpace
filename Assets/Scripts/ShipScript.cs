using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D ship;
    public LogicManagerScript logicManagerScript;
    public bool isAlive = true;

    private Vector3 inputPosition;
    private Vector3 direction;
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;

    void Start()
    {
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        logicManagerScript = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManagerScript>();
    }

    void FixedUpdate()
    {
        
        // if(Input.touchCount > 0 && isAlive) {
        //     Touch touch = Input.GetTouch(0);
        //     inputPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //     inputPosition.z = 0;
        //     direction = (inputPosition - transform.position);
        //     ship.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
        // } else {
        //     ship.velocity = Vector2.zero;
        // }

        if(isAlive) {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inputPosition.z = 0;
            inputPosition.x = Mathf.Clamp(inputPosition.x, minScreenBounds.x, maxScreenBounds.x);
            inputPosition.y = Mathf.Clamp(inputPosition.y, minScreenBounds.y, maxScreenBounds.y);

            direction = (inputPosition - transform.position);
            ship.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
        } else {
            ship.velocity = Vector2.zero;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        logicManagerScript.gameOver();
        isAlive = false;
    }
}
