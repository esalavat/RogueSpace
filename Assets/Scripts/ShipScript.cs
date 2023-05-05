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
    private bool doubleLaser = false;

    void Start()
    {    
        if(GameStateManager.Instance.hasUpgrade(Upgrade.LaserSpeed1)) {
            shootSpeed /= 2;
        }
        if(GameStateManager.Instance.hasUpgrade(Upgrade.LaserSpeed2)) {
            shootSpeed /= 2;
        }
     
        if(GameStateManager.Instance.hasUpgrade(Upgrade.Shield1)) {
            life += 1;
            maxLife += 1;
        }
        if(GameStateManager.Instance.hasUpgrade(Upgrade.Shield2)) {
            life += 1;
            maxLife += 1;
        }
        EventManager.LifeUpdated(life);

        if(GameStateManager.Instance.hasUpgrade(Upgrade.DoubleLaser)) {
            doubleLaser = true;
        }

        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        logicManagerScript = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManagerScript>();
    }

    void FixedUpdate()
    {
        if(isAlive) {
            doMovement();
            doLaser();
        } else {
            ship.velocity = Vector2.zero;
        }
        
        if(GameStateManager.Instance.hasUpgrade(Upgrade.ShieldRegen) && life < maxLife && !isShieldRegenning) {
            StartCoroutine(startShieldRegen());
        }
    }

    private void doMovement() {
        inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inputPosition.z = 0;
        inputPosition.y += .4f;
        inputPosition.x = Mathf.Clamp(inputPosition.x, minScreenBounds.x, maxScreenBounds.x);
        inputPosition.y = Mathf.Clamp(inputPosition.y, minScreenBounds.y, maxScreenBounds.y);

        direction = (inputPosition - transform.position);
        ship.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
    }

    private void doLaser() {
        if(shootTimer >= shootSpeed) {
            shootLaser();
            shootTimer -= shootSpeed;
        } else {
            shootTimer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        var collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "asteroid" 
            || collisionGameObject.tag == "enemyLaser" 
            || "enemy".Equals(collisionGameObject.tag)
        ) {
            doCameraShake();

            var health = collisionGameObject.GetComponent<Health>();

            if(health != null && health.currentHp > 1) {
                health.currentHp--;
            } else {
                Destroy(collision.gameObject);
            }

            if(life > 1) {
                decrementShield();
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
        float x1Pos = transform.position.x;
        float x2Pos = transform.position.x;

        if(doubleLaser) {
            x1Pos -= .2f;
            x2Pos += .2f;
        }

        GameObject newLaser = Instantiate(laser, new Vector3(x1Pos, transform.position.y, 1), transform.rotation);
        newLaser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
        Destroy(newLaser, 2);

        if(doubleLaser) {
            GameObject newLaser2 = Instantiate(laser, new Vector3(x2Pos, transform.position.y, 1), transform.rotation);
            newLaser2.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
            Destroy(newLaser2, 2);
        }
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
        transform.position = new Vector3(0, -10, 0);
    }

    private void doCameraShake() {
        StartCoroutine(cameraShake.Shake(.2f, .10f));
    }
}
