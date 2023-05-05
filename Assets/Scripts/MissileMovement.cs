using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public float maxRotationSpeed = 4f;
    public float initialRotationSpeed = 0;
    public float rotationSpeedAcceleration = 2.5f;
    public float initialVelocity = 2;
    public float acceleration = 3f;
    public float maxSpeed = 20f;
    public float rotationStopTime = 2f;
    public float missileLiveTime = 4f;
    public float pauseRotationUntil = .5f;

    private GameObject player;
    private Vector2 target;
    private float velocity;
    private float timer;
    private float rotationSpeed;
    private float initialRotationAngle;

    void Start()
    {
        timer = 0;
        player = GameObject.Find("Ship");
        target = player.transform.position;

        velocity = initialVelocity;
        rotationSpeed = initialRotationSpeed;
        initialRotationAngle = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= pauseRotationUntil && timer < rotationStopTime) {
            var direction = target - (Vector2)transform.position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.back);
            var rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rotation.x = 0;
            rotation.y = 0;
            if(rotation.z > Mathf.Abs(initialRotationAngle) || rotation.z < -1 * Mathf.Abs(initialRotationAngle)) {

            }
            transform.rotation = rotation;
        }

        if(velocity < maxSpeed) {
            velocity += acceleration * Time.deltaTime;
        }
        if(timer > pauseRotationUntil && rotationSpeed < maxRotationSpeed) {
            rotationSpeed += rotationSpeedAcceleration * Time.deltaTime;
        }
        if(timer > missileLiveTime) {
            Destroy(gameObject);
        }

        transform.position = transform.position + transform.up * velocity * Time.deltaTime;
        
        timer += Time.deltaTime;
    }
}
