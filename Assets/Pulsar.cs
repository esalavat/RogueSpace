using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pulsar : MonoBehaviour
{
    public float variation = 1.5f;
    public float moveSpeed = .5f;
    public float flickerSpeed = .3f;
    public List<float[]> colors = new List<float[]> {
        new float[] {.84f, .015f, 1f, .35f, .55f},
        new float[] {.74f, .39f, .32f, .35f, .55f},
        new float[] {.27f, .39f, .32f, .35f, .55f},
        new float[] {1f, 1f, 1f, .35f, .45f}
    };

    private float flickerTimer;
    private bool dim = true;
    private float[] color;

    private Light2D pulsarLight;

    void Start() {
        pulsarLight = transform.GetChild(0).GetComponent<Light2D>();

        flickerTimer = flickerSpeed;
        dim = true;
        
        color = colors[Random.Range(0, colors.Count)];
        Color newColor = new Color(color[0], color[1], color[2], Random.Range(color[3], color[4]));
        transform.GetComponent<SpriteRenderer>().color = newColor;
        pulsarLight.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(flickerTimer<=0) {
            if(dim) {
                pulsarLight.intensity += variation;
            } else {
                pulsarLight.intensity -= variation;            
            }
            flickerTimer += Random.Range(.1f, flickerSpeed);
            dim = !dim;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);

        flickerTimer -= Time.deltaTime;
    }
}
