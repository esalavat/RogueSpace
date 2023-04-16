using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D ship;
    public LogicManagerScript logicManagerScript;
    public bool isAlive = true;
    public float shootSpeed = 1f;
    public GameObject laser;
    public float laserSpeed = 20f;

    private Vector3 inputPosition;
    private Vector3 direction;
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;
    private float shootTimer = 0;

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

            if(shootTimer >= shootSpeed) {
                shootLaser();
                shootTimer -= shootSpeed;
            } else {
                shootTimer += Time.deltaTime;
            }
        } else {
            ship.velocity = Vector2.zero;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("ShipScript.OnCollisionEnter");
        if(collision.gameObject.tag == "asteroid") {
            logicManagerScript.gameOver();
            isAlive = false;
        }
    }

    private void shootLaser() {
        GameObject newLaser = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
        Destroy(newLaser, 2);
    }
}
