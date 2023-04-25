using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude) {
        
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration) {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y +y, originalPosition.z);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
