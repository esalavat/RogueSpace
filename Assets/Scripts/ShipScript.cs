using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public bool isAlive = true;
    public float shootSpeed = 1f;
    public float laserSpeed = 20f;
    public float shieldRegenTime = 20f;

    public Rigidbody2D ship;
    public LogicManagerScript logicManagerScript;
    public GameObject laser;
    public GameObject loseShieldParticle;
    public CameraShake cameraShake;
    public GameObject explosionPrefab;

    private int life = 1;
    private int maxLife = 1;

    private Vector3 inputPosition;
    private Vector3 direction;
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;
    private float shootTimer = 0;
    private bool isShieldRegenning = false;

    void Start()
    {    
        if(GameStateManager.Instance.hasUpgrade(Upgrade.LaserSpeed1)) {
            shootSpeed /= 3;
        }
     
        if(GameStateManager.Instance.hasUpgrade(Upgrade.Shield1)) {
            life += 1;
            maxLife += 1;
        }
        EventManager.LifeUpdated(life);

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
        
        if(GameStateManager.Instance.hasUpgrade(Upgrade.ShieldRegen) && life < maxLife && !isShieldRegenning) {
            StartCoroutine(startShieldRegen());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "enemyLaser") {
            doCameraShake();
            if(life > 1) {
                decrementShield();
                Destroy(collision.gameObject);
            } else {
                Invoke("triggerGameOver", 1f);
                destroyShip();
                isAlive = false;
            }
        }
    }

    private void triggerGameOver() {
        logicManagerScript.gameOver();
    }

    private void shootLaser() {
        GameObject newLaser = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
        Destroy(newLaser, 2);
    }

    private void decrementShield() {
        life -= 1;
        EventManager.LifeUpdated(life);
        GameObject loseShield = Instantiate(loseShieldParticle, transform);
        Destroy(loseShield, 1);
    }

    private IEnumerator startShieldRegen() {
        isShieldRegenning = true;

        float regenTimer = 0;

        while(regenTimer < shieldRegenTime) {
            regenTimer += Time.deltaTime;
            EventManager.ShieldRegen(regenTimer/shieldRegenTime);
            yield return new WaitForFixedUpdate();
        }

        life++;
        EventManager.LifeUpdated(life);
        isShieldRegenning = false;
    }

    private void destroyShip() {
        if(explosionPrefab != null) {
            GameObject newExplosion = Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(newExplosion, 2);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = new Vector3(100, 100, 0);
    }

    private void doCameraShake() {
        StartCoroutine(cameraShake.Shake(.2f, .10f));
    }
}
