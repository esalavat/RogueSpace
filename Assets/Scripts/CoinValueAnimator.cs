using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinValueAnimator : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fadeOutTime = 1f;

    private float opacity = 1;
    private float timer = 0;
    void Update()
    {
        transform.position = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
        
        //fadeOut
        TMP_Text text = transform.GetComponent<TMP_Text>();
        Color newColor = text.faceColor;
        opacity = (fadeOutTime-timer)/fadeOutTime;
        newColor.a = opacity;
        text.faceColor = newColor;

        timer += Time.deltaTime;

        if(timer > fadeOutTime) {
            Destroy(gameObject);
        }
    }
}
