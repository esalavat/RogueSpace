using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class LogicManagerScript : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public int scoreIncrement = 1;
    public int scoreDelay =  3;
    public float sunMaxIntensity = 3f;

    public GameObject gameOverScreen;
    public GameObject sun;
    public List<GameObject> lightRays;

    private float sunIntensity = 0;
    private float timer = 0;

    void OnEnable() {
        EventManager.OnEnemyDestroyed += addScore;
    }

    void FixedUpdate() {
        timer += Time.deltaTime;
        updateSunIntensity();
    }

    private void updateSunIntensity() {
        sunIntensity = Math.Min(sunMaxIntensity, timer / 100f);
        sun.GetComponent<Light2D>().intensity = sunIntensity;
        foreach(var light in lightRays) {
            light.GetComponent<Light2D>().intensity = sunIntensity/15;
        }
    }

    [ContextMenu("addScore")]
    public void addScore() {
        addScore(scoreIncrement);
    }

    public void addScore(int amount) {
        score += amount;
        EventManager.ScoreUpdated(score);
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver() {
        if(!gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(true);
        }
    }
}
